using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController2 : MonoBehaviour
{
    public int slashintCounter;
    public static bool stopSwitch;
    public static bool slashCDOn;

    public float slashInterval;
    public float stopInterval;

    public GameObject slash;
    public static int animState;
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

        if (Input.GetKeyDown(KeyCode.Space) && slashCDOn == false)
        {
            // slash location updater

//            Debug.Log(animState);

            animState = PlayerController.currentState;

            if (PlayerController.faceDirection == Vector3.down || PlayerController.faceDirection == Vector3.up || PlayerController.faceDirection == Vector3.right || PlayerController.faceDirection == Vector3.left)
            {
                rb.AddForce(PlayerController.faceDirection * 100);
                slash.transform.localPosition = PlayerController.faceDirection / 2;
                slash.transform.localEulerAngles = PlayerController.faceRoto;
            }

            else if (PlayerController.faceDirection == (Vector3.down + Vector3.right) /2 || PlayerController.faceDirection == (Vector3.down + Vector3.left) / 2 || PlayerController.faceDirection == (Vector3.up + Vector3.right) / 2 || PlayerController.faceDirection == (Vector3.up + Vector3.left) / 2)
            {
                rb.AddForce(PlayerController.faceDirection * 200);
                slash.transform.localPosition = PlayerController.faceDirection * .6667f;
                slash.transform.localEulerAngles = PlayerController.faceRoto;
            }

            StartCoroutine(Slash2());
        }

        IEnumerator Slash2()
        {


            slash.SetActive(true);

            //            rb.AddForce(rb.velocity * 2, ForceMode2D.Impulse);

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
