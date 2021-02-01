using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeScript : MonoBehaviour
{
    public static bool isDodging;
    public SpriteRenderer rend;
    public Rigidbody2D rb;
    public float dodgeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        dodgeSpeed = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && PlayerController.backpedal == false && !isDodging && !WeaponController.slashCDOn && WeaponController.stopSwitch)
        {
            Debug.Log("Dodge pressed");

            StartCoroutine(DodgeMethod());
        }

        //          alternate dodge move method

        //       if (isDodging)
        //       {
        //          Debug.Log("called");

        //           if (Input.GetKey("w"))
        //           {
        //               rb.AddForce(Vector2.up * Time.deltaTime * 30, ForceMode2D.Impulse);
        //           }
        //           if (Input.GetKey("a"))
        //           {
        //               rb.AddForce(Vector2.left * Time.deltaTime * 30, ForceMode2D.Impulse);
        //           }
        //           if (Input.GetKey("s"))
        //           {
        //               rb.AddForce(Vector2.down * Time.deltaTime * 30, ForceMode2D.Impulse);
        //           }
        //           if (Input.GetKey("d"))
        //           {
        //               rb.AddForce(Vector2.right * Time.deltaTime * 30, ForceMode2D.Impulse);
        //           }
        //       }

        IEnumerator DodgeMethod()
        {
            //            Debug.Log("Dashmethod called");

            if (PlayerController.backpedal == false && !isDodging)
            {
    //            rend.color = Color.white;
    //            rend.color = Color.grey;

                //                dashEffectOn = true;
                isDodging = true;

                rb.AddForce(PlayerAnimScript.faceDirection * PlayerController.moveSpeed * dodgeSpeed, ForceMode2D.Impulse);
                //               InvokeRepeating("DashFX", 0, dashEqualizer);

                //                InvokeRepeating("DashFX", 0, .05f);

                yield return new WaitForSeconds(.25f);
                //                CancelInvoke("DashFX");
                //                yield return new WaitForSeconds(.75f);

                //                CancelInvoke("DashFX");
                isDodging = false;
                //                dashEffectOn = false;

                WeaponController.stopSwitch = true;

      //          rend.color = Color.white;
      //          rend.color = Color.blue;
                WeaponController.isComWindow = true;

                yield return new WaitForSeconds(.25f);

                WeaponController.isComWindow = false;
      //          rend.color = Color.white;

                if (!WeaponController.slashCDOn)
                {
                    WeaponController.stopSwitch = false;
                }
            }

            yield break;
        }
    }
}
