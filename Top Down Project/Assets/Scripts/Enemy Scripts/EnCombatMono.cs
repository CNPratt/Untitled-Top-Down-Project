using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnCombatMono : MonoBehaviour
{
    public bool gotHit;
    public bool gotHitSwitch;

    public int bodyDamage;
    public int skillDamage1;

    public void DropLoot(GameObject loot)
    {
        Instantiate(loot, transform.position, Quaternion.Euler(Vector3.zero));
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
