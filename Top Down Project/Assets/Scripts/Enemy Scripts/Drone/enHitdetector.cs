using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enHitdetector : MonoBehaviour
{
    public DroneCombat handlerAI;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Weapons")
        {
            handlerAI.gotHit = true;
            handlerAI.gotHitSwitch = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Weapons" && !handlerAI.gotHit)
        {
            handlerAI.gotHit = true;
            handlerAI.gotHitSwitch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Weapons" && !handlerAI.gotHit)
        {
            handlerAI.gotHit = true;
            handlerAI.gotHitSwitch = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        handlerAI = gameObject.GetComponentInParent<DroneCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
