using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDetect : MonoBehaviour
{
    public GhostCombat kCom;

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
        kCom = gameObject.GetComponentInParent<GhostCombat>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
