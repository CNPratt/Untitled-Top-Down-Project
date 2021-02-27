using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Doorbase
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        doorState = 0;

        anim.SetInteger("Door State", doorState);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
