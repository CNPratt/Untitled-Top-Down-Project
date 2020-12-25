using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float xVel;
    private float yVel;
    public float velInt;

    public Rigidbody2D rb;
    public GameObject dashFX;
    public int moveSpeed;
    public float slashStutter;

    public float dashSpeed;
    public float dashEqualizer;
    public float dashFXThreshold;
    public bool dashEffectOn;
    public bool isDashing;
    public bool backpedal;

    public Animator anim;
    public static Vector3 faceRoto;
    public static Vector3 faceDirection;
    public static int idleState;
    public static int currentState;
    public static int runState;
    public static int bpState;

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
 //      set parameter animstate in animator

        anim.SetInteger("animState", currentState);

//      calculatw equalizer for  dashfx instantiation

        xVel = Mathf.Abs(rb.velocity.x);
        yVel = Mathf.Abs(rb.velocity.y);
        velInt = xVel + yVel;

        if (velInt > 0)
        {
            dashEqualizer = .01f / velInt / 2;
        }

        IEnumerator DashFX()
        {
//            Debug.Log("dashFX called");

            dashEffectOn = true;

            Instantiate(dashFX, transform.position, transform.rotation);

            while (xVel >= dashFXThreshold || yVel >= dashFXThreshold)
            {
                yield return new WaitForSeconds(dashEqualizer);
                Instantiate(dashFX, transform.position, transform.rotation);
            }

            dashEffectOn = false;

        }


 //       if (rb.velocity.x >= dashFXThreshold || rb.velocity.y >= dashFXThreshold)
   //     {
            if (dashEffectOn == false && (xVel >= dashFXThreshold || yVel >= dashFXThreshold))
            {
                StartCoroutine(DashFX());
            }
        //     }


//      calculates which direction player is facing based on current animation state

        if (currentState == 3 || currentState == 11 || currentState == 0)
        {
            faceDirection = Vector2.down;
            faceRoto = new Vector3(0, 0, 0);
        }

        else if (currentState == 1 || currentState == 9)
        {
            faceDirection = Vector2.up;
            faceRoto = new Vector3(0, 0, 180);
        }

        else if (currentState == 2 || currentState == 10)
        {
            faceDirection = Vector2.left;
            faceRoto = new Vector3(0, 0, -90);
        }

        else if (currentState == 4 || currentState == 12)
        {
            faceDirection = Vector2.right;
            faceRoto = new Vector3(0, 0, 90);
        }

        else if (currentState == 8 || currentState == 16)
        {
            faceDirection = (Vector2.down + Vector2.right)/2;
            faceRoto = new Vector3(0, 0, 45);
        }

        else if (currentState == 7 || currentState == 15)
        {
            faceDirection = (Vector2.down + Vector2.left)/2;
            faceRoto = new Vector3(0, 0, -45);
        }

        else if (currentState == 6 || currentState == 14)
        {
            faceDirection = (Vector2.up + Vector2.right)/2;
            faceRoto = new Vector3(0, 0, 135);
        }

        else if (currentState == 5 || currentState == 13)
        {
            faceDirection = (Vector2.up + Vector2.left)/2;
            faceRoto = new Vector3(0, 0, -135);
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //    moved to update

        //     anim.SetInteger("animState", currentState);

        //        if (WeaponController2.slashCDOn)
        //        {
        //            currentState = WeaponController2.animState);
        //        }
        //       else
        //       {
        //           anim.SetInteger("animState", currentState);
        //       }

               if (WeaponController2.stopSwitch == true)
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


                if (Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("a"))
                {
                    if (!Input.GetKeyDown(KeyCode.LeftShift))
                    {
                    runState = 1;
                    currentState = runState;
                    idleState = 9;
                    }

                else if (backpedal == true)
                    {
                        bpState = 3;
                     currentState = bpState;
                      idleState = 11;
                    }
                 }

                else if (Input.GetKey("a") && !Input.GetKey("w") && !Input.GetKey("s"))
                {
                    if (!Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        runState = 2;
                        currentState = runState;
                        idleState = 10;
                    }


                    else if (backpedal == true)
                    {
                     bpState = 4;
                     currentState = bpState;
                      idleState = 12;
                    }
        }

                else if (Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d"))
                {
                    if (backpedal == false)
                    {
                     runState = 3;
                     currentState = runState;
                      idleState = 11;
                 }

                else if (backpedal == true)
                {
                    bpState = 1;
                    currentState = bpState;
                    idleState = 9;
                }
            }

                else if (Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("s"))
                {
                  if (backpedal == false)
                   {
                       runState = 4;
                        currentState = runState;
                        idleState = 12;
                   }

                else if (backpedal == true)
                {
                    bpState = 2;
                    currentState = bpState;
                    idleState = 10;
                }
            }

                else if (Input.GetKey("w") && Input.GetKey("a"))
                {
                 if (backpedal == false)
                 {
                      runState = 5;
                       currentState = runState;
                       idleState = 13;
                 }

                else if (backpedal == true)
                {
                    bpState = 8;
                    currentState = bpState;
                    idleState = 16;
                }
            }

                else if (Input.GetKey("w") && Input.GetKey("d"))
                {
                  if (backpedal == false)
                   {
                       runState = 6;
                       currentState = runState;
                       idleState = 14;
                   }

                else if (backpedal == true)
                {
                    bpState = 7;
                    currentState = bpState;
                    idleState = 15;
                }
            }

                else if (Input.GetKey("s") && Input.GetKey("a"))
                {
                   if (backpedal == false)
                    {
                      runState = 7;
                      currentState = runState;
                      idleState = 15;
                    }

                else if (backpedal == true)
                {
                    bpState = 6;
                    currentState = bpState;
                    idleState = 14;
                }
            }

                else if (Input.GetKey("s") && Input.GetKey("d"))
                {
                   if (backpedal == false)
                   {
                      runState = 8;
                      currentState = runState;
                      idleState = 16;
                   }

                else if (backpedal == true)
                {
                    bpState = 5;
                    currentState = bpState;
                    idleState = 13;
                }
            }

                else if (!Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("s") && !Input.GetKey("d"))
                {
                    if (rb.velocity.x <= .01f || rb.velocity.y <= .01f)
                    {
                        currentState = idleState;
                    }
                }


            if (Input.GetKey(KeyCode.LeftShift))
            {
                backpedal = true;
            }

        IEnumerator DashMethod()
        {
            Debug.Log("Dashmethod called");

            if (backpedal == false && !isDashing)
            {
                isDashing = true;
                rb.AddForce(faceDirection * moveSpeed * dashSpeed, ForceMode2D.Impulse);
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
    }
}
