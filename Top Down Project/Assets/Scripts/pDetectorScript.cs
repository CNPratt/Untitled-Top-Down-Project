using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pDetectorScript : MonoBehaviour
{
    public KoboldCombat kCom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && !kCom.isAttacking)
        {
//            Debug.Log("Kobold has detected the player");
            kCom.pDetected = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        kCom = gameObject.GetComponentInParent<KoboldCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
