using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : BaseCollectableScript
{
    public override void Collected()
    {
        Debug.Log("override");

        base.Collected();
        PlayerInventoryScript.Recharge(5);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
