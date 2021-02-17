using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTileCorrector : MonoBehaviour
{
    public Rigidbody2D rb;

    private void OnTriggerStay2D(Collider2D collider)
    {
//        Debug.Log("staying");

        if(collider.name == "Main Tilemap" || collider.tag == "Environment")

        {
//            rb.AddForce(Vector2.down);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
