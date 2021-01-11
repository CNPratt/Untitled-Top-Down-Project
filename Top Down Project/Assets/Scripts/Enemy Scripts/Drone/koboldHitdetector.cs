using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koboldHitdetector : MonoBehaviour
{
    public KoboldCombat handlerAI;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Weapons")
        {
            handlerAI.gotHit = true;
            handlerAI.gotHitSwitch = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        handlerAI = gameObject.GetComponentInParent<KoboldCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
