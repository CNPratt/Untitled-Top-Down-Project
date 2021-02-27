using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorbase : MonoBehaviour
{
    public bool enterSwitch;
    public bool isRoomClear;

    public Animator anim;
    public Collider2D doorCol;
    public int doorState;

    // Start is called before the first frame update
   virtual protected void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        if (gameObject.GetComponent<Collider2D>() != null)
        {
            doorCol = gameObject.GetComponent<Collider2D>();
        }
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if(doorState == 0)
        {
            doorCol.enabled = false;
        }
        else
        {
            doorCol.enabled = true;
        }
    }
}
