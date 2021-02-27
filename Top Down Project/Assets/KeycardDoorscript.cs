using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardDoorscript : Doorbase
{
    public Transform playert;
    public bool isOpening;

    public bool isOpen;
    public bool isClosed;

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
////        Debug.Log("collision.collider.name");
//
//        if(collision.name == "Interacter Field" && Input.GetKeyDown(KeyCode.O) && PlayerController.handsFree && PlayerInventoryScript.redKeys > 0)
//        {
////            Debug.Log("enter inloop");
//
//            PlayerInventoryScript.redKeys = PlayerInventoryScript.redKeys - 1;
//            doorState = 1;
//            anim.SetInteger("Door State", doorState);
//        }
//    }
//
//    private void OnTriggerStay2D(Collider2D collision)
//    {
////        Debug.Log("collision.collider.name");
//
//        if (collision.name == "Interacter Field" && Input.GetKeyDown(KeyCode.O) && PlayerController.handsFree && PlayerInventoryScript.redKeys > 0)
//        {
////            Debug.Log("stay inloop");
//
//            PlayerInventoryScript.redKeys = PlayerInventoryScript.redKeys - 1;
//            doorState = 1;
//            anim.SetInteger("Door State", doorState);
//        }
//    }

    // Start is called before the first frame update
    protected override void Start()
    {
        playert = GameObject.Find("Player").transform;

        isOpen = true;

        base.Start();

        doorState = 3;

        anim.SetInteger("Door State", doorState);
    }

    // Update is called once per frame
    protected override void Update()
    {
//        Debug.Log(Vector2.Distance(playert.position, transform.position));

        base.Update();

        anim.SetInteger("Door State", doorState);

 //      if (Vector2.Distance(playert.position, transform.position) < 1 && Input.GetKeyDown(KeyCode.O) && PlayerController.handsFree && PlayerInventoryScript.redKeys > 0)
 //      {
 //          Debug.Log("enter inloop");
 //
 //          PlayerInventoryScript.redKeys = PlayerInventoryScript.redKeys - 1;
 //          doorState = 1;
 //          anim.SetInteger("Door State", doorState);
 //      }
    }

    public void hasClosed()
    {
        isOpen = false;
        isClosed = true;
        doorState = 3;
    }

    public void hasOpened()
    {
        isClosed = false;
        isOpen = true;
        doorState = 0;
    }
}

