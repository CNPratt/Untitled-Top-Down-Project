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

            Debug.Log(animState);

            animState = PlayerController.currentState;


            if (animState == 3 || animState == 11 || animState == 0)
            {
                rb.AddForce(Vector3.down * 100);
                slash.transform.localPosition = Vector3.down / 2;
                slash.transform.localEulerAngles = new Vector3(0, 0, 0);
            }

            else if (animState == 1 || animState == 9)
            {
                rb.AddForce(Vector3.up * 100);
                slash.transform.localPosition = Vector3.up / 2;
                slash.transform.localEulerAngles = new Vector3(0, 0, 180);
            }

            else if (animState == 2 || animState == 10)
            {
                rb.AddForce(Vector3.left * 100);
                slash.transform.localPosition = Vector3.left / 2;
                slash.transform.localEulerAngles = new Vector3(0, 0, -90);
            }

            else if (animState == 4 || animState == 12)
            {
                rb.AddForce(Vector3.right * 100);
                slash.transform.localPosition = Vector3.right / 2;
                slash.transform.localEulerAngles = new Vector3(0, 0, 90);
            }

            else if (animState == 8 || animState == 16)
            {
                rb.AddForce((Vector3.down + Vector3.right) * 100);
                slash.transform.localPosition = (Vector3.down + Vector3.right) / 3;
                slash.transform.localEulerAngles = new Vector3(0, 0, 45);
            }

            else if (animState == 7 || animState == 15)
            {
                rb.AddForce((Vector3.down + Vector3.left) * 100);
                slash.transform.localPosition = (Vector3.down + Vector3.left) / 3;
                slash.transform.localEulerAngles = new Vector3(0, 0, -45);
            }

            else if (animState == 6 || animState == 14)
            {
                rb.AddForce((Vector3.up + Vector3.right) * 100);
                slash.transform.localPosition = (Vector3.up + Vector3.right) / 3;
                slash.transform.localEulerAngles = new Vector3(0, 0, 135);
            }

            else if (animState == 5 || animState == 13)
            {
                rb.AddForce((Vector3.up + Vector3.left) * 100);
                slash.transform.localPosition = (Vector3.up + Vector3.left) / 3;
                slash.transform.localEulerAngles = new Vector3(0, 0, -135);
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
