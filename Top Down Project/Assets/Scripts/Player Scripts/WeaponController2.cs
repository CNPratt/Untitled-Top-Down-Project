using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController2 : MonoBehaviour
{
    public bool isCharging;
    public float slashCharger;
    public int slashintCounter;
    public static bool stopSwitch;
    public static bool slashCDOn;

    public float slashInterval;
    public float stopInterval;

    public static int thisIdle;
    public GameObject slash;
    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        slashInterval = .25f;
        stopInterval = .3f;
    }

    // Update is called once per frame
    void Update()
    {
        IEnumerator slashCharge()
        {
            slashCharger = slashCharger + 1;
            yield return new WaitForSeconds(.2f);
            isCharging = false;

        }

        if (Input.GetKey(KeyCode.Space) && isCharging == false)
        {
            isCharging = true;
            StartCoroutine(slashCharge());
        }

        if(Input.GetKeyUp(KeyCode.Space) && slashCharger >= 3)
        {
            slashCharger = 0;
            slash.transform.localScale = new Vector3(2, 2, 1);
            if (PlayerController.faceDirection == Vector3.down || PlayerController.faceDirection == Vector3.up || PlayerController.faceDirection == Vector3.right || PlayerController.faceDirection == Vector3.left)
            {
                rb.AddForce(PlayerController.faceDirection * 100);
                slash.transform.localPosition = PlayerController.faceDirection / 2;
                slash.transform.localEulerAngles = PlayerController.faceRoto;
            }

            else if (PlayerController.faceDirection == (Vector3.down + Vector3.right) || PlayerController.faceDirection == (Vector3.down + Vector3.left) || PlayerController.faceDirection == (Vector3.up + Vector3.right) || PlayerController.faceDirection == (Vector3.up + Vector3.left))
            {
                rb.AddForce(PlayerController.faceDirection * 100);
                slash.transform.localPosition = PlayerController.faceDirection * .3334f;
                slash.transform.localEulerAngles = PlayerController.faceRoto;
            }

            StartCoroutine(Slash2());
        }

        else if (Input.GetKeyUp(KeyCode.Space) && slashCharger < 3)
        {
            slashCharger = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && slashCDOn == false)
        {
            slash.transform.localScale = new Vector3(1, 1, 1);

            // slash location updater

            if (PlayerController.faceDirection == Vector3.down || PlayerController.faceDirection == Vector3.up || PlayerController.faceDirection == Vector3.right || PlayerController.faceDirection == Vector3.left)
            {
                rb.AddForce(PlayerController.faceDirection * 100);
                slash.transform.localPosition = PlayerController.faceDirection / 2;
                slash.transform.localEulerAngles = PlayerController.faceRoto;
            }

            else if (PlayerController.faceDirection == (Vector3.down + Vector3.right) || PlayerController.faceDirection == (Vector3.down + Vector3.left)  || PlayerController.faceDirection == (Vector3.up + Vector3.right) || PlayerController.faceDirection == (Vector3.up + Vector3.left))
            {
                rb.AddForce(PlayerController.faceDirection * 100);
                slash.transform.localPosition = PlayerController.faceDirection * .3334f;
                slash.transform.localEulerAngles = PlayerController.faceRoto;
            }

            StartCoroutine(Slash2());
        }

        IEnumerator Slash2()
        {

            slash.SetActive(true);

            thisIdle = PlayerController.idleState;

            slashintCounter = slashintCounter + 1;

            slashCDOn = true;
            stopSwitch = true;

            yield return new WaitForSeconds(slashInterval);
            slashCDOn = false;

            yield return new WaitForSeconds(stopInterval - slashInterval);

            slashintCounter = slashintCounter - 1;
            if (slashintCounter <= 0)
            {
                stopSwitch = false;
            }

        }
    }
}
