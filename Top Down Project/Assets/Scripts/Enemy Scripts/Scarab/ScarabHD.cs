using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarabHD : MonoBehaviour
{
    public ScarabCombat handlerAI;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Weapons")
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
        handlerAI = gameObject.GetComponentInParent<ScarabCombat>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
