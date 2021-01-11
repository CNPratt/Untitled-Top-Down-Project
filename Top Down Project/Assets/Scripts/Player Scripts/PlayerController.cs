using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer rend;
    public Collider2D lastEnemyCollider;
    public static bool beenHit;
    public static bool canAttack;
    public static bool canDash;
    public static bool isHolding;

    public static bool enCollisionIgnore;

    private float xVel;
    private float yVel;
    public float velInt;

    public Rigidbody2D rb;
    public GameObject dashFX;
    public static float moveSpeed;
    public float slashStutter;

    public float dashSpeed;
    public float dashEqualizer;
    public float dashFXThreshold;
    public bool dashEffectOn;
    public bool isDashing;
    public static bool backpedal;

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
        canDash = true;
    }

    void DashFX()
    {
        if (xVel > dashFXThreshold || yVel > dashFXThreshold)
        {
            Instantiate(dashFX, transform.position, transform.rotation);
        }
        return;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy" && beenHit == false)
        {
      //      Debug.Log("Initial enemy hit.");

            PlayerAnimScript.currentState = PlayerAnimScript.idleState;

            lastEnemyCollider = collider.GetComponentInParent<Collider2D>();

            rb.velocity = new Vector2(0, 0);

            StartCoroutine(Recoil());
        }

        if (collider.tag == "Enemy" && beenHit == true)
        {
            rb.AddForce((transform.position - lastEnemyCollider.transform.position).normalized * 5f, ForceMode2D.Impulse);
    //        Debug.Log("Hit while beenHit = true" + (transform.position - lastEnemyCollider.transform.position));
        }

//          for enemy weapons

        if (collider.tag == "Enemy Weapons" && beenHit == false)
        {

            PlayerAnimScript.currentState = PlayerAnimScript.idleState;

            lastEnemyCollider = collider.GetComponentInParent<Collider2D>();

            rb.velocity = new Vector2(0, 0);

            StartCoroutine(Recoil());
        }

        if (collider.tag == "Enemy Weapons" && beenHit == true)
        {
            rb.AddForce((transform.position - lastEnemyCollider.transform.position).normalized * 5f, ForceMode2D.Impulse);

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && beenHit == false)
        {
            //      Debug.Log("Initial enemy hit.");

            PlayerAnimScript.currentState = PlayerAnimScript.idleState;

            lastEnemyCollider = collision.GetComponentInParent<Collider2D>();

            StartCoroutine(Recoil());
        }

        if (collision.tag == "Enemy" && beenHit == true)
        {
            rb.AddForce((transform.position - lastEnemyCollider.transform.position).normalized * 2f, ForceMode2D.Force);
            //        Debug.Log("Hit while beenHit = true" + (transform.position - lastEnemyCollider.transform.position));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && beenHit == false)
        {
            //      Debug.Log("Initial enemy hit.");

            PlayerAnimScript.currentState = PlayerAnimScript.idleState;

            lastEnemyCollider = collision.GetComponentInParent<Collider2D>();

            StartCoroutine(Recoil());
        }

        if (collision.tag == "Enemy" && beenHit == true)
        {
            rb.AddForce((transform.position - lastEnemyCollider.transform.position).normalized * 2f, ForceMode2D.Impulse);
            //        Debug.Log("Hit while beenHit = true" + (transform.position - lastEnemyCollider.transform.position));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        enCollisionIgnore = true;

        canAttack = true;
        canDash = true;
        dashEffectOn = false;
        isDashing = false;

        dashFXThreshold = 3.5f;
        dashSpeed = 15f;

        moveSpeed = 1f;

        slashStutter = 25f;

    }

    private void Update()
    {
        if(beenHit == true)
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
            canDash = false;
    //        Debug.Log("holding");
        }
        else
        {
            canAttack = true;
            canDash = true;
        }

//      calculate equalizer for  dashfx instantiation

        xVel = Mathf.Abs(rb.velocity.x);
        yVel = Mathf.Abs(rb.velocity.y);
        velInt = xVel + yVel;

        if(xVel > yVel)
        {
            dashEqualizer = .1f / xVel;
        }
        if (yVel > xVel)
        {
            dashEqualizer = .1f / yVel;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (beenHit == true)
        {
            //    Debug.Log("beenhit if loop triggered");
       //     rb.velocity = new Vector2(0, 0);
            rb.AddForce((transform.position - lastEnemyCollider.transform.position).normalized * 5f, ForceMode2D.Force);
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

            IEnumerator DashMethod()
            {
                //            Debug.Log("Dashmethod called");

                if (backpedal == false && !isDashing)
                {
                    dashEffectOn = true;
                    isDashing = true;

                    rb.AddForce(PlayerAnimScript.faceDirection * moveSpeed * dashSpeed, ForceMode2D.Impulse);
                    InvokeRepeating("DashFX", 0, dashEqualizer);

                    yield return new WaitForSeconds(.5f);
                    CancelInvoke("DashFX");
                    yield return new WaitForSeconds(.5f);

                    CancelInvoke("DashFX");
                    isDashing = false;
                    dashEffectOn = false;
                }

                yield break;
            }

            if (Input.GetKeyDown(KeyCode.P) && backpedal == false && !isDashing & canDash == true)
            {
                //       Debug.Log("Dash pressed");

                StartCoroutine(DashMethod());
            }
        }
    }
}
