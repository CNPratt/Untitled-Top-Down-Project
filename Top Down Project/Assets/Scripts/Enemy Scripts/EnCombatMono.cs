using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnCombatMono : MonoBehaviour
{
    public bool beenDefeated;

    public int enHealthMax;
    public int enHealthCurrent;

    public bool gotHit;
    public bool gotHitSwitch;

    public int bodyDamage;
    public int skillDamage1;

    public void DropLoot(GameObject loot)
    {
        Instantiate(loot, transform.position, Quaternion.Euler(Vector3.zero));
    }

    public void CallGotHit(int damage, float kbPower, float invTime)
    {
        StartCoroutine(GotHit(damage, kbPower, invTime));
    }

    protected virtual IEnumerator GotHit(int damage, float kbPower, float invTime)
    {
        yield return null;
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
