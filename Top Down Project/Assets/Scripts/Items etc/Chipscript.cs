using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chipscript : BaseCollectableScript
{
    public override void Collected()
    {
//        Debug.Log("override");

        base.Collected();
        PlayerInventoryScript.GetChips(1);
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
