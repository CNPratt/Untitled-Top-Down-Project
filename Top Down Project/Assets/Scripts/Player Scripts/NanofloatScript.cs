using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanofloatScript : MonoBehaviour
{
    float originalY;
    float floatSpeed;

    public float floatStrength = .0025f; // You can change this in the Unity Editor to 
                                    // change the range of y positions that are possible.

    void Start()
    {
        floatSpeed = 3;
        this.originalY = this.transform.position.y;
    }

    void Update()
    {

        if(transform.position.y != originalY + ((float)Mathf.Sin(Time.time * 5) * floatStrength))
        {
            this.originalY = this.transform.position.y;
        }

        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Mathf.Sin(Time.time * floatSpeed) * floatStrength),
            transform.position.z);
    }
}
