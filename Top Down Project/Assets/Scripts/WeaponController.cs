﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject slash;
    public GameObject thisSlash;
    public int animState;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animState = PlayerController.currentState;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Slash());
        }

        IEnumerator Slash()
        {
            if (animState == 3 || animState == 11 || animState == 0)
            {
                thisSlash = Instantiate(slash, transform.position + Vector3.down / 2, transform.rotation);
                thisSlash.transform.parent = gameObject.transform;
                thisSlash.transform.Rotate(0, 0, 0, 0);
            }

            else if (animState == 1 || animState == 9)
            {
                thisSlash = Instantiate(slash, transform.position + Vector3.up / 2, transform.rotation);
                thisSlash.transform.parent = gameObject.transform;
                thisSlash.transform.Rotate(0, 0, 180, 0);
            }

            else if (animState == 2 || animState == 10)
            {
                thisSlash = Instantiate(slash, transform.position + Vector3.left / 2, transform.rotation);
                thisSlash.transform.parent = gameObject.transform;
                thisSlash.transform.Rotate(0, 0, -90, 0);
            }

            else if (animState == 4 || animState == 12)
            {
                thisSlash = Instantiate(slash, transform.position + Vector3.right / 2, transform.rotation);
                thisSlash.transform.parent = gameObject.transform;
                thisSlash.transform.Rotate(0, 0, 90, 0);
            }

            else if (animState == 8 || animState == 16)
            {
                thisSlash = Instantiate(slash, transform.position + (Vector3.down + Vector3.right) / 3, transform.rotation);
                thisSlash.transform.parent = gameObject.transform;
                thisSlash.transform.Rotate(0, 0, 45, 0);
            }

            else if (animState == 7 || animState == 15)
            {
                thisSlash = Instantiate(slash, transform.position + (Vector3.down + Vector3.left) / 3, transform.rotation);
                thisSlash.transform.parent = gameObject.transform;
                thisSlash.transform.Rotate(0, 0, -45, 0);
            }

            else if (animState == 6 || animState == 14)
            {
                thisSlash = Instantiate(slash, transform.position + (Vector3.up + Vector3.right) / 3, transform.rotation);
                thisSlash.transform.parent = gameObject.transform;
                thisSlash.transform.Rotate(0, 0, 135, 0);
            }

            else if (animState == 5 || animState == 13)
            {
                thisSlash = Instantiate(slash, transform.position + (Vector3.up + Vector3.left) / 3, transform.rotation);
                thisSlash.transform.parent = gameObject.transform;
                thisSlash.transform.Rotate(0, 0, -135, 0);
            }
            yield break;
        }
    }
}
