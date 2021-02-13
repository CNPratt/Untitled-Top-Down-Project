using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{
    public Animator anim;

    public Rigidbody2D rb;

    public GameObject dashFX;
    public static bool canDash;
    public static bool dashEffectOn;
    public static bool isDashing;
    public float dashSpeed;
    public float dashEqualizer;
    public float dashFXThreshold;

    private float xVel;
    private float yVel;
    public float velInt;

    void DashFX()
    {
        //        if (xVel > dashFXThreshold || yVel > dashFXThreshold)
        //        {
        Instantiate(dashFX, transform.position, transform.rotation);
//        }
        return;
    }


    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        rb = gameObject.GetComponent<Rigidbody2D>();

        canDash = true;
        dashEffectOn = false;
        isDashing = false;

        dashFXThreshold = 3.5f;
        dashSpeed = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && PlayerController.backpedal == false && !isDashing & canDash == true)
        {
            //       Debug.Log("Dash pressed");

            StartCoroutine(DashMethod());
        }

        //      calculate equalizer for  dashfx instantiation

        xVel = Mathf.Abs(rb.velocity.x);
        yVel = Mathf.Abs(rb.velocity.y);
        velInt = xVel + yVel;

        dashEqualizer = .2f / velInt;

        IEnumerator DashMethod()
        {
            //            Debug.Log("Dashmethod called");

            if (PlayerController.backpedal == false && !isDashing)
            {
                dashEffectOn = true;
                isDashing = true;

  //              rb.AddForce(PlayerAnimScript.faceDirection * PlayerController.moveSpeed * dashSpeed, ForceMode2D.Impulse);

                rb.AddForce(PlayerController.desiredDir * PlayerController.moveSpeed * dashSpeed, ForceMode2D.Impulse);

                //               InvokeRepeating("DashFX", 0, dashEqualizer);


                InvokeRepeating("DashFX", .03f, .03f);

                yield return new WaitForSeconds(.25f);

                dashEffectOn = false;
                CancelInvoke("DashFX");

                yield return new WaitForSeconds(.75f);

                CancelInvoke("DashFX");
                isDashing = false;
                
            }

            yield break;
        }

    }
}
