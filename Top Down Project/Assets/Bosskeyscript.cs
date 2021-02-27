using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosskeyscript : BaseCollectableScript
{

    public override void Collected()
    {
        base.Collected();

        PlayerInventoryScript.bossKeys += 1;
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
