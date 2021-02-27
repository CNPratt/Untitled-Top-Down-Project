using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomclearDoor : Doorbase
{
    public bool isOpening;

    public bool isOpen;
    public bool isClosed;

    // Start is called before the first frame update
    protected override void Start()
    {
        isOpen = true;

        base.Start();

        doorState = 0;

        anim.SetInteger("Door State", doorState);

 //       gameObject.SetActive(false);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        anim.SetInteger("Door State", doorState);

        if (enterSwitch == true)
        {
            if (!isRoomClear)
            {
                doorState = 2;
                anim.SetInteger("Door State", doorState);
            }

            else if (isRoomClear)
            {
                doorState = 0;
                anim.SetInteger("Door State", doorState);
            }

            enterSwitch = false;
        }

        else if (isRoomClear && !enterSwitch && isClosed)
        {
            doorState = 1;
        }
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