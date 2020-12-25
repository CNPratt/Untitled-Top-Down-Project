using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float slashStutter;

    public float xVel2;
    public float yVel2;
    public float velInt2;
    public float dashFXThreshold;

    public float dashSpeed;
    public float dashEqualizer;
    private float xVel;
    private float yVel;
    public float velInt;

    public bool backpedal;
    public GameObject dashFX;
    public bool dashEffectOn;
    public bool isDashing;
    public Rigidbody2D rb;
    public int moveSpeed;
    public Animator anim;
    public static int idleState;
    public static int currentState;
    public static int runState;
    public static int bpState;
    public bool isFlipped;

    // Start is called before the first frame update
    void Start()
    {
        dashFXThreshold = 3.5f;
        dashSpeed = 15f;
        dashEffectOn = false;
        isDashing = false;
        moveSpeed = 1;

        slashStutter = 25f;

    }

    private void Update()
    {
        anim.SetInteger("animState", currentState);

        xVel2 = Mathf.Abs(rb.velocity.x);
        yVel2 = Mathf.Abs(rb.velocity.y);

        IEnumerator DashFX()
        {
//            Debug.Log("dashFX called");
            dashEffectOn = true;

            while (xVel2 >= dashFXThreshold || yVel2 >= dashFXThreshold)
            {
                //  dashEqualizer = .001f;

                //             dashEffectOn = true;
                
                xVel2 = Mathf.Abs(rb.velocity.x);
                yVel2 = Mathf.Abs(rb.velocity.y);

                velInt2 = xVel2 + yVel2;

                if (velInt2 > 0)
                {
                    dashEqualizer = .01f / velInt2 / 2;
                }
                Instantiate(dashFX, transform.position, transform.rotation);
                yield return new WaitForSeconds(dashEqualizer);

            }
            dashEffectOn = false;

        }


 //       if (rb.velocity.x >= dashFXThreshold || rb.velocity.y >= dashFXThreshold)
   //     {
            if (dashEffectOn == false)
            {
                StartCoroutine(DashFX());
            }
   //     }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
   //     anim.SetInteger("animState", currentState);

        if (WeaponController2.stopSwitch == true)
        {
            slashStutter = 0f;
        }
        else
        {
            slashStutter = 25f;
            //           anim.SetInteger("animState", currentState);
        }

//        if (WeaponController2.slashCDOn)
//        {
//            anim.SetInteger("animState", WeaponController2.animState);
//        }
 //       else
 //       {
 //           anim.SetInteger("animState", currentState);
 //       }

        



        //        if(dashEqualizer >= 3f)
        //        {
        //            dashEqualizer = .1f;
        //        }
        //
        //        if (dashEqualizer <= 1)
        //        {
        //            dashEqualizer = .001f;
        //        }


        //        Debug.Log(currentState);


            if (!Input.GetKey(KeyCode.LeftShift))
            {
                backpedal = false;

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


                if (Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("a"))
                {
                    if (currentState != 1)
                    {
                    runState = 1;
                    currentState = runState;
                    //            anim.SetInteger("animState", 1);
                    idleState = 9;
                    }
                }

                else if (Input.GetKey("a") && !Input.GetKey("w") && !Input.GetKey("s"))
                {
                    if (currentState != 2)
                    {
                    runState = 2;
                    currentState = runState;
                    //            anim.SetInteger("animState", 2);
                    idleState = 10;
                    }
                }

                else if (Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d"))
                {
                    if (currentState != 3)
                    {
                    runState = 3;
                    currentState = runState;
                    //            anim.SetInteger("animState", 3);
                    idleState = 11;
                    }
                }

                else if (Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("s"))
                {
                    if (currentState != 4)
                    {
                    runState = 4;
                    currentState = runState;
                    //            anim.SetInteger("animState", 4);
                    idleState = 12;
                    }
                }

                else if (Input.GetKey("w") && Input.GetKey("a"))
                {
                    if (currentState != 5)
                    {
                    runState = 5;
                    currentState = runState;
                    //            anim.SetInteger("animState", 5);
                    idleState = 13;
                    }
                }

                else if (Input.GetKey("w") && Input.GetKey("d"))
                {
                    if (currentState != 6)
                    {
                    runState = 6;
                    currentState = runState;
                    //            anim.SetInteger("animState", 6);
                    idleState = 14;
                    }
                }

                else if (Input.GetKey("s") && Input.GetKey("a"))
                {
                    if (currentState != 7)
                    {
                    runState = 7;
                    currentState = runState;
                    //             anim.SetInteger("animState", 7);
                    idleState = 15;
                    }
                }

                else if (Input.GetKey("s") && Input.GetKey("d"))
                {
                    if (currentState != 8)
                    {
                        runState = 8;
                    currentState = runState;
                    //              anim.SetInteger("animState", 8);
                    idleState = 16;
                    }
                }

                else if (!Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("s") && !Input.GetKey("d"))
                {
                    if (rb.velocity.x <= .01f || rb.velocity.y <= .01f)
                    {
                        currentState = idleState;
           //             anim.SetInteger("animState", idleState);
                    }
                }
            }

            else if (Input.GetKey(KeyCode.LeftShift))
            {
                backpedal = true;

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


                if (Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("a"))
                {
                    if (currentState != 3)
                    {
                        bpState = 3;
                    currentState = bpState;
                    //               anim.SetInteger("animState", 3);
                    idleState = 11;
                    }
                }

                else if (Input.GetKey("a") && !Input.GetKey("w") && !Input.GetKey("s"))
                {
                    if (currentState != 4)
                    {
                        currentState = 4;
                    currentState = bpState;
                    //               anim.SetInteger("animState", 4);
                    idleState = 12;
                    }
                }

                else if (Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d"))
                {
                    if (currentState != 1)
                    {
                    bpState = 1;
                    currentState = bpState;
                    //               anim.SetInteger("animState", 1);
                    idleState = 9;
                    }


                }

                else if (Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("s"))
                {
                    if (currentState != 2)
                    {
                    bpState = 2;
                    currentState = bpState;
                    //            anim.SetInteger("animState", 2);
                    idleState = 10;
                    }

                }

                else if (Input.GetKey("w") && Input.GetKey("a"))
                {
                    if (currentState != 8)
                    {
                    bpState = 8;
                    currentState = bpState;
                    //            anim.SetInteger("animState", 8);
                    idleState = 16;
                    }
                }

                else if (Input.GetKey("w") && Input.GetKey("d"))
                {
                    if (currentState != 7)
                    {
                    bpState = 7;
                    currentState = bpState;
                    //          anim.SetInteger("animState", 7);
                    idleState = 15;
                    }
                }

                else if (Input.GetKey("s") && Input.GetKey("a"))
                {
                    if (currentState != 6)
                    {
                    bpState = 6;
                    currentState = bpState;
                    //        anim.SetInteger("animState", 6);
                    idleState = 14;
                    }

                }

                else if (Input.GetKey("s") && Input.GetKey("d"))
                {
                    if (currentState != 5)
                    {
                    bpState = 5;
                    currentState = bpState;
                    //       anim.SetInteger("animState", 5);
                    idleState = 13;
                    }

                }

                else if (!Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("s") && !Input.GetKey("d"))
                {
                    if (rb.velocity.x <= .01f || rb.velocity.y <= .01f)
                    {
                        currentState = idleState;
   //                     anim.SetInteger("animState", idleState);
                    }
                }
            

        }

        IEnumerator DashMethod()
        {
            Debug.Log("Dashmethod called");

            if (backpedal == false && !isDashing)
            {
                isDashing = true;

                if (currentState == 3 || currentState == 11 || currentState == 0)
                {
            //        StartCoroutine(DashFX());
                    rb.AddForce(Vector2.down * moveSpeed * dashSpeed, ForceMode2D.Impulse);
           //         StartCoroutine(DashFX());

                }

                else if (currentState == 1 || currentState == 9)
                {
            //        StartCoroutine(DashFX());
                    rb.AddForce(Vector2.up * moveSpeed * dashSpeed, ForceMode2D.Impulse);
           //         StartCoroutine(DashFX());
                }

                else if (currentState == 2 || currentState == 10)
                {
            //        StartCoroutine(DashFX());
                    rb.AddForce(Vector2.left * moveSpeed * dashSpeed, ForceMode2D.Impulse);
           //         StartCoroutine(DashFX());
                }

                else if (currentState == 4 || currentState == 12)
                {
        //            StartCoroutine(DashFX());
                    rb.AddForce(Vector2.right * moveSpeed * dashSpeed, ForceMode2D.Impulse);
       //             StartCoroutine(DashFX());
                }

                else if (currentState == 8 || currentState == 16)
                {
          //          StartCoroutine(DashFX());
                    rb.AddForce((Vector2.down + Vector2.right) * moveSpeed * dashSpeed, ForceMode2D.Impulse);
          //          StartCoroutine(DashFX());
                }

                else if (currentState == 7 || currentState == 15)
                {
         //           StartCoroutine(DashFX());
                    rb.AddForce((Vector2.down + Vector2.left) * moveSpeed * dashSpeed, ForceMode2D.Impulse);
        //            StartCoroutine(DashFX());
                }

                else if (currentState == 6 || currentState == 14)
                {
        //            StartCoroutine(DashFX());
                    rb.AddForce((Vector2.up + Vector2.right) * moveSpeed * dashSpeed, ForceMode2D.Impulse);
       //             StartCoroutine(DashFX());
                }

                else if (currentState == 5 || currentState == 13)
                {
        //            StartCoroutine(DashFX());
                    rb.AddForce((Vector2.up + Vector2.left) * moveSpeed * dashSpeed, ForceMode2D.Impulse);
       //             StartCoroutine(DashFX());
                }

                yield return new WaitForSeconds(1f);
                isDashing = false;
            }

            yield break;
        }

        if (Input.GetKeyDown(KeyCode.P) && backpedal == false  && !isDashing)
        {
     //       Debug.Log("Dash pressed");

            StartCoroutine(DashMethod());
        }

        

 //       if(isDashing == true && dashEffectOn == false)
 //       {
 //           StartCoroutine(DashFX());
 //       }
    }
}
