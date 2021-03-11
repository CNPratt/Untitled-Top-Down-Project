using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortexscript : MonoBehaviour
{
    public bool dashEntered;

    public Rigidbody2D pRB;
    public GameObject player;
    public GhostPath gPath;

    public float beforespeed;
    public bool isColliding;

    public void OnDisable()
    {
        PlayerController.vortexEffector = 1;
        isColliding = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
//            Debug.Log("enter");

            isColliding = true;
            PlayerController.vortexEffector = .2f;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (DashScript.isDashing && other.name == "Player")
        {
            gPath.gseekEndReached = false;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
//        Debug.Log("exit");

        //        PlayerController.moveSpeed = beforespeed;

//        if (gPath.gseekEndReached)
//        {
//           PlayerController.vortexEffector = 1;
//            isColliding = false;
//        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        pRB = player.GetComponent<Rigidbody2D>();
        gPath = gameObject.GetComponentInParent<GhostPath>();
    }

    // Update is called once per frame
    void Update()
    {
        if(DashScript.canDash)
        {
//            Debug.Log("canDash returned");

//            dashEntered = false;
        }

        if (!gPath.gseekEndReached)
        {
            PlayerController.vortexEffector = 1;
            isColliding = false;
        }
    }
}
