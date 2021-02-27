using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : EnCombatMono
{
    public float offset;
    public BoxCollider2D col;
    public bool offSetter;
    public bool returned;
    public bool apex;
    public Animator anim;

    IEnumerator SpikeMethod()
    {
//        anim.SetInteger("SpikeStage", 0);

//        if(offSetter)
//        {
//            offSetter = false;
//
//            yield return new WaitForSeconds(offset);
//        }

        yield return new WaitForSeconds(2f);

        anim.SetInteger("SpikeStage", 1);

        yield return new WaitForSeconds(1f);

        anim.SetInteger("SpikeStage", 2);

        col.enabled = true;

        yield return new WaitUntil(() => apex);

        apex = false;

        anim.SetInteger("SpikeStage", 3);

        yield return new WaitForSeconds(1f);

        anim.SetInteger("SpikeStage", 4);

        yield return new WaitUntil(() => returned);

        returned = false;

        col.enabled = false;

        anim.SetInteger("SpikeStage", 5);

        StartCoroutine(SpikeMethod());
    }

    public void ThrustPeak()
    {
        apex = true;
    }

    public void SpikeReturn()
    {
        returned = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpikeMethod());

        offSetter = true;
        returned = false;
        apex = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
