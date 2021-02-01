using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
 
    public GameObject nanoTarget;

    public Rigidbody2D rb;
    public EnCombatMono lastCombatAI;
    public SpriteRenderer rend;
    public Collider2D lastEnemyCollider;

    public static bool beenHit;
    public static bool canAttack;

    public static bool isHolding;

    public static bool backpedal;
    public static bool enCollisionIgnore;

    public static float moveSpeed;
    public float slashStutter;

    public Vector3 lastColPos;


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

            PlayerAnimScript.currentState = PlayerAnimScript.idleState;

            lastEnemyCollider = collider.GetComponentInParent<Collider2D>();
            lastColPos = lastEnemyCollider.transform.position;

            rb.velocity = new Vector2(0, 0);

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

            PlayerAnimScript.currentState = PlayerAnimScript.idleState;

            lastEnemyCollider = collider.GetComponentInParent<Collider2D>();
            lastColPos = lastEnemyCollider.transform.position;

            rb.velocity = new Vector2(0, 0);

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

            PlayerAnimScript.currentState = PlayerAnimScript.idleState;

            lastEnemyCollider = collision.GetComponentInParent<Collider2D>();
            lastColPos = lastEnemyCollider.transform.position;

            StartCoroutine(Recoil());
        }

        if (collision.tag == "Enemy" && beenHit == true)
        {
            rb.AddForce((transform.position - lastColPos).normalized * 2f, ForceMode2D.Force);
            //        Debug.Log("Hit while beenHit = true" + (transform.position - lastEnemyCollider.transform.position));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && beenHit == false && !collision.GetComponentInParent<EnCombatMono>().gotHit && !DodgeScript.isDodging)
        {
            //      Debug.Log("Initial enemy hit.");

            PlayerAnimScript.currentState = PlayerAnimScript.idleState;

            lastEnemyCollider = collision.GetComponentInParent<Collider2D>();
            lastColPos = lastEnemyCollider.transform.position;

            StartCoroutine(Recoil());
        }

        if (collision.tag == "Enemy" && beenHit == true)
        {
            rb.AddForce((transform.position - lastColPos).normalized * 2f, ForceMode2D.Impulse);
            //        Debug.Log("Hit while beenHit = true" + (transform.position - lastEnemyCollider.transform.position));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enCollisionIgnore = true;

        canAttack = true;

        moveSpeed = 1f;

        slashStutter = 25f;

    }

    private void Update()
    {

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
    }
}
