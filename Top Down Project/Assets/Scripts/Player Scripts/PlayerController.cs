using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public int lineMask;

    public static Coroutine thisOKEY;
    public static bool okeyUp;

    public Color solidColor;
    public static Color rendColor;

    public static Vector3 desiredDir;
    public GameObject nanoTarget;

    public Rigidbody2D rb;
    public PlayerAnimScript animScript;
    public EnCombatMono lastCombatAI;
    public SpriteRenderer rend;
    public Collider2D lastEnemyCollider;

    public static bool handsFree;
    public static bool beenHit;
    public static bool canAttack;
    public static bool isHolding;
    public static bool backpedal;
    public static bool enCollisionIgnore;

    public static float moveSpeed;
    public float slashStutter;

    public Vector3 lastColPos;

    public static IEnumerator OKeyUp()
    {
//        Debug.Log("called");

        if (thisOKEY == null)
        {
//            Debug.Log("inloop");

            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.O));
            okeyUp = true;

            thisOKEY = null;
        }
    }

    IEnumerator Recoil()
    {
        //        Debug.Log("Recoil called");
            beenHit = true;
            rend.color = Color.white;
            yield return new WaitForSeconds(.1f);
            rend.color = Color.clear;
            yield return new WaitForSeconds(.1f);
            rend.color = Color.white;
            yield return new WaitForSeconds(.1f);
            rend.color = Color.clear;
            yield return new WaitForSeconds(.1f);
            rend.color = Color.white;
            yield return new WaitForSeconds(.1f);

            beenHit = false;

            canAttack = true;
            DashScript.canDash = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy" && beenHit == false && !collider.GetComponentInParent<EnCombatMono>().gotHit && !DodgeScript.isDodging)
        {
      //      Debug.Log("Initial enemy hit.");

            animScript.currentState = PlayerAnimScript.idleState;

            lastEnemyCollider = collider.GetComponentInParent<Collider2D>();
            lastColPos = lastEnemyCollider.transform.position;
            lastCombatAI = lastEnemyCollider.GetComponentInParent<EnCombatMono>();

            rb.velocity = new Vector2(0, 0);

            PlayerInventoryScript.TakeDamage(lastCombatAI.bodyDamage);
            StartCoroutine(Recoil());
        }

        if (collider.tag == "Enemy" && beenHit == true)
        {
            rb.AddForce((transform.position - lastColPos).normalized * 5f, ForceMode2D.Impulse);
    //        Debug.Log("Hit while beenHit = true" + (transform.position - lastEnemyCollider.transform.position));
        }

//          for enemy weapons

        if (collider.tag == "Enemy Weapons" && beenHit == false && !DodgeScript.isDodging)
        {

            animScript.currentState = PlayerAnimScript.idleState;

            lastEnemyCollider = collider.GetComponentInParent<Collider2D>();
            lastColPos = lastEnemyCollider.transform.position;
            lastCombatAI = lastEnemyCollider.GetComponentInParent<EnCombatMono>();

            rb.velocity = new Vector2(0, 0);

            PlayerInventoryScript.TakeDamage(lastCombatAI.skillDamage1);
            StartCoroutine(Recoil());
        }

        if (collider.tag == "Enemy Weapons" && beenHit == true)
        {
            rb.AddForce((transform.position - lastColPos).normalized * 5f, ForceMode2D.Impulse);

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && beenHit == false && !collision.GetComponentInParent<EnCombatMono>().gotHit && !DodgeScript.isDodging)
        {
            //      Debug.Log("Initial enemy hit.");

            animScript.currentState = PlayerAnimScript.idleState;

            lastEnemyCollider = collision.GetComponentInParent<Collider2D>();
            lastColPos = lastEnemyCollider.transform.position;
            lastCombatAI = lastEnemyCollider.GetComponentInParent<EnCombatMono>();

            PlayerInventoryScript.TakeDamage(lastCombatAI.bodyDamage);
            StartCoroutine(Recoil());
        }

        if (collision.tag == "Enemy" && beenHit == true)
        {
            rb.AddForce((transform.position - lastColPos).normalized * 2f, ForceMode2D.Force);
            //        Debug.Log("Hit while beenHit = true" + (transform.position - lastEnemyCollider.transform.position));
        }
    }

//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        if (collision.tag != null)
//        {
//            if (collision.tag == "Enemy" && beenHit == false && !collision.GetComponentInParent<EnCombatMono>().gotHit && !DodgeScript.isDodging)
//            {
//                //      Debug.Log("Initial enemy hit.");
//
//                animScript.currentState = PlayerAnimScript.idleState;
//
//                lastEnemyCollider = collision.GetComponentInParent<Collider2D>();
//                lastColPos = lastEnemyCollider.transform.position;
//
//                StartCoroutine(Recoil());
//            }
//        }
//
//        if (collision.tag == "Enemy" && beenHit == true)
//        {
//            rb.AddForce((transform.position - lastColPos).normalized * 2f, ForceMode2D.Impulse);
//            //        Debug.Log("Hit while beenHit = true" + (transform.position - lastEnemyCollider.transform.position));
//        }
//    }

    // Start is called before the first frame update
    void Start()
    {
        lineMask = (1 << 10) | (1 << 12) | (1 << 14);

        lineMask = ~lineMask;

        okeyUp = true;

        Physics2D.IgnoreLayerCollision(10, 12, true);

        animScript = gameObject.GetComponent<PlayerAnimScript>();

        solidColor = rend.color;

        solidColor.a = 1f;

        rendColor = rend.color;

        enCollisionIgnore = true;

        canAttack = true;

        moveSpeed = 1f;

        slashStutter = 25f;

    }

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position + PlayerAnimScript.faceDirection/2);
        var hit =  Physics2D.Linecast(transform.position, transform.position + PlayerAnimScript.faceDirection/2, lineMask);

        if (hit.transform != null)
        {
            Debug.Log(hit.transform.name);

            if (hit.transform.tag == "Keycard Door" && Input.GetKeyDown(KeyCode.O) && handsFree && PlayerInventoryScript.redKeys > 0 && okeyUp)
            {
                var door = hit.transform.GetComponent<Doorbase>();

                Debug.Log("inloop");

                PlayerInventoryScript.redKeys = PlayerInventoryScript.redKeys - 1;
                door.doorState = 1;

                thisOKEY = StartCoroutine(OKeyUp());
            }
        }

        rendColor.a = Random.Range(.1f, .8f);

        if(!beenHit && !isHolding && !DashScript.isDashing && !WeaponController.stopSwitch && !WeaponController.isCharging)
        {
            handsFree = true;
        }

        else
        {
            handsFree = false;
        }

        if(DashScript.dashEffectOn)
        {
            rend.color = rendColor;
            Physics2D.IgnoreLayerCollision(10, 11, true);
            Physics2D.IgnoreLayerCollision(10, 8, true);

        }
        else
        {
            rend.color = solidColor;
            Physics2D.IgnoreLayerCollision(10, 11, false);
            Physics2D.IgnoreLayerCollision(10, 8, false);
        }

        if (PlayerAnimScript.faceDirection == Vector3.down || PlayerAnimScript.faceDirection == Vector3.up || PlayerAnimScript.faceDirection == Vector3.right || PlayerAnimScript.faceDirection == Vector3.left)
        {
            nanoTarget.transform.localPosition = PlayerAnimScript.faceDirection / 2;
            nanoTarget.transform.localEulerAngles = PlayerAnimScript.faceRoto;
        }

        else if (PlayerAnimScript.faceDirection == (Vector3.down + Vector3.right) || PlayerAnimScript.faceDirection == (Vector3.down + Vector3.left) || PlayerAnimScript.faceDirection == (Vector3.up + Vector3.right) || PlayerAnimScript.faceDirection == (Vector3.up + Vector3.left))
        {
            nanoTarget.transform.localPosition = PlayerAnimScript.faceDirection * .3334f;
            nanoTarget.transform.localEulerAngles = PlayerAnimScript.faceRoto;
        }
//
        if (beenHit == true)
        {
            enCollisionIgnore = false;
        }
        else
        {
            enCollisionIgnore = true;
        }

        if (isHolding == true || beenHit == true)
        {
            canAttack = false;
            DashScript.canDash = false;
    //        Debug.Log("holding");
        }
        else
        {
            canAttack = true;
            DashScript.canDash = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (beenHit == true)
        {
            //    Debug.Log("beenhit if loop triggered");
       //     rb.velocity = new Vector2(0, 0);
            rb.AddForce((transform.position - lastColPos).normalized * 5f, ForceMode2D.Force);
        }

        //          recently couched input and anim switching inside this bool, eventually to be switched with a more general canInput bool

        if (!beenHit)
        {
            if (WeaponController.stopSwitch == true)
            {
                slashStutter = 0f;
            }
            else
            {
                slashStutter = 25f;
            }

            if (!Input.GetKey(KeyCode.LeftShift))
            {
                backpedal = false;
            }


            if (Input.GetKey("w"))
            {
                rb.AddForce(Vector2.up * moveSpeed * Time.deltaTime * slashStutter, ForceMode2D.Impulse);
            }
            if (Input.GetKey("a"))
            {
                rb.AddForce(Vector2.left * moveSpeed * Time.deltaTime * slashStutter, ForceMode2D.Impulse);
            }
            if (Input.GetKey("s"))
            {
                rb.AddForce(Vector2.down * moveSpeed * Time.deltaTime * slashStutter, ForceMode2D.Impulse);
            }
            if (Input.GetKey("d"))
            {
                rb.AddForce(Vector2.right * moveSpeed * Time.deltaTime * slashStutter, ForceMode2D.Impulse);
            }


            if (Input.GetKey(KeyCode.LeftShift))
            {
                backpedal = true;
            }
        }

        if (Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("a"))
        {
            moveSpeed = 1f;

            desiredDir = Vector3.up;
        }

        else if (Input.GetKey("a") && !Input.GetKey("w") && !Input.GetKey("s"))
        {
            moveSpeed = 1f;

            desiredDir = Vector3.left;
        }

        else if (Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d"))
        {
            moveSpeed = 1f;
            desiredDir = Vector3.down;
        }

        else if (Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("s"))
        {
            moveSpeed = 1f;
            desiredDir = Vector3.right;
        }

        else if (Input.GetKey("w") && Input.GetKey("a"))
        {
            moveSpeed = .7f;

            desiredDir = Vector3.up + Vector3.left;
        }

        else if (Input.GetKey("w") && Input.GetKey("d"))
        {
            moveSpeed = .7f;

            desiredDir = Vector3.up + Vector3.right;
        }

        else if (Input.GetKey("s") && Input.GetKey("a"))
        {
            moveSpeed = .7f;

            desiredDir = Vector3.down + Vector3.left;
        }

        else if (Input.GetKey("s") && Input.GetKey("d"))
        {
            moveSpeed = .7f;

            desiredDir = Vector3.down + Vector3.right;
        }
    }
}

