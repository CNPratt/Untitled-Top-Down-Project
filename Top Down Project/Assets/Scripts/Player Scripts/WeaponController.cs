using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject colCenter;
    public GameObject slash;
    public GameObject medslash;
    public GameObject bigslash;

    public Rigidbody2D rb;
    public SpriteRenderer sprite;

    public int checker;
    public int stopintCounter;
    public int comboCounter;
    public static int thisIdle;

    public bool isSlashing;
    public bool isCharging;
    public static bool stopSwitch;
    public static bool slashCDOn;
    public static bool isComWindow;

    public float slashCharger;
    public float slashInterval;
    public float stopInterval;


    // Start is called before the first frame update
    void Start()
    {
        isComWindow = false;
        slashInterval = .25f;
        stopInterval = .5f;
    }

    // Update is called once per frame
    void Update()
    {
        colCenter.transform.localEulerAngles = PlayerAnimScript.faceRoto;

        //       Debug.Log(stopintCounter);

        if (checker != comboCounter)
        {
//            Debug.Log(comboCounter);
        }

        checker = comboCounter;

        if (Input.GetKey(KeyCode.Space) && isCharging == false && PlayerController.canAttack)
        {
            isCharging = true;
            StartCoroutine(slashCharge());
        }

        //        if(Input.GetKeyUp(KeyCode.Space) && slashCharger >= 3)
        //        {
        //          THIS IS FOR THE CHARGE FUNCTION

        //            slashCharger = 0;

        //            if (PlayerAnimScript.faceDirection == Vector3.down || PlayerAnimScript.faceDirection == Vector3.up || PlayerAnimScript.faceDirection == Vector3.right || PlayerAnimScript.faceDirection == Vector3.left)
        //            {
        //                rb.AddForce(PlayerAnimScript.faceDirection * 100);
        //                colCenter.transform.localEulerAngles = PlayerAnimScript.faceRoto;
        //            }
        //
        //            else if (PlayerAnimScript.faceDirection == (Vector3.down + Vector3.right) || PlayerAnimScript.faceDirection == (Vector3.down + Vector3.left) || PlayerAnimScript.faceDirection == (Vector3.up + Vector3.right) || PlayerAnimScript.faceDirection == (Vector3.up + Vector3.left))
        //            {
        //                rb.AddForce(PlayerAnimScript.faceDirection * 100);
        //                colCenter.transform.localEulerAngles = PlayerAnimScript.faceRoto;
        //            }
        //
        //            StartCoroutine(Slash());
        //        }

        else if (Input.GetKeyUp(KeyCode.Space) && slashCharger < 3)
        {
            slashCharger = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && slashCDOn == false && PlayerController.canAttack && !DodgeScript.isDodging)
        {
//            slash.transform.localScale = new Vector3(1, 1, 1);

            // slash location updater

            if (PlayerAnimScript.faceDirection == Vector3.down || PlayerAnimScript.faceDirection == Vector3.up || PlayerAnimScript.faceDirection == Vector3.right || PlayerAnimScript.faceDirection == Vector3.left)
            {
                if (comboCounter != 0)
                {
                    rb.AddForce(PlayerAnimScript.faceDirection * 100);
                }

                colCenter.transform.localEulerAngles = PlayerAnimScript.faceRoto;
            }

            else if (PlayerAnimScript.faceDirection == (Vector3.down + Vector3.right) || PlayerAnimScript.faceDirection == (Vector3.down + Vector3.left)  || PlayerAnimScript.faceDirection == (Vector3.up + Vector3.right) || PlayerAnimScript.faceDirection == (Vector3.up + Vector3.left))
            {
                if (comboCounter != 0)
                {
                    rb.AddForce(PlayerAnimScript.faceDirection * 100);
                }

                colCenter.transform.localEulerAngles = PlayerAnimScript.faceRoto;
            }

            StartCoroutine(Slash());
        }

        IEnumerator slashCharge()
        {
            slashCharger = slashCharger + 1;
            yield return new WaitForSeconds(.2f);
            isCharging = false;

        }

        IEnumerator Slash()
        {
//            sprite.color = Color.white;

            //         Debug.Log("called");
            if (comboCounter >= 2)
            {
                comboCounter = 0;
                bigslash.SetActive(true);
            }
            else if (comboCounter == 1)
            {
                medslash.SetActive(true);
            }
            else if (comboCounter == 0)
            {
                slash.SetActive(true);
            }

            thisIdle = PlayerAnimScript.idleState;

            stopintCounter = stopintCounter + 1;
            comboCounter = comboCounter + 1;

            slashCDOn = true;
            stopSwitch = true;

            yield return new WaitForSeconds(slashInterval);

            int countCheck = comboCounter;
            slashCDOn = false;

//            sprite.color = Color.blue;
            isComWindow = true;

            yield return new WaitForSeconds(stopInterval - slashInterval);

            isComWindow = false;
//            sprite.color = Color.white;
//
//            slashCDOn = false;
//
            if(countCheck >= comboCounter)
            {
                comboCounter = 0;
            }

            stopintCounter = stopintCounter - 1;

            if (stopintCounter <= 0)
            {
                stopSwitch = false;
            }

        }
    }
}
