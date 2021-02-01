using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KGunnerHD : MonoBehaviour
{
    public KGCombat handlerAI;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Weapons")
        {
            if (handlerAI.gotHit == false)
            {
                handlerAI.gotHit = true;
                handlerAI.gotHitSwitch = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        handlerAI = gameObject.GetComponentInParent<KGCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
