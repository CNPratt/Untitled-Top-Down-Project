﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ScarabCombat : EnCombatMono
{
    public enHitdetector detector;

    public GameObject dropLoot;

//    public int enHealthMax;
//    public int enHealthCurrent;
    public GameObject deathFX;

    public ScarabAnimScript kAnimScript;

    public Coroutine kslashRoutine;
    public int currAttDir;
    public bool spriteSwitch;
    public bool isAttacking;
    public bool inRange;
    public bool pDetected;

    public GameObject pdetector;
    public GameObject kslashl;
    public GameObject kslashr;

    public SpriteRenderer rend;
    public GameObject player;
    public CapsuleCollider2D pCol;
    public CapsuleCollider2D enCol;
    public Rigidbody2D koboldRB;

    IEnumerator EnDeath()
    {
        DropLoot(dropLoot);
        Instantiate(deathFX, transform.position, transform.rotation);
        gameObject.SetActive(false);
        yield break;
    }

    protected override IEnumerator GotHit(int damage, float kbPower, float invTime)
    {
        //               Debug.Log("Gothit engaged");

        koboldRB.velocity = Vector3.zero;

        enHealthCurrent = enHealthCurrent - (1 * damage);

        if (isAttacking)
        {
            StopCoroutine(kslashRoutine);
            isAttacking = false;
            spriteSwitch = false;
        }

        koboldRB.AddForce((transform.position - player.transform.position).normalized * 1.5f * kbPower, ForceMode2D.Impulse);

        //      used to be double the time below - 5sec instead of 2.5s

        rend.color = Color.black;
        //        rend.color = Color.white;
        yield return new WaitForSeconds(.05f * invTime);
        //        rend.color = Color.clear;
        yield return new WaitForSeconds(.05f * invTime);
        //        rend.color = Color.white;
        yield return new WaitForSeconds(.05f * invTime);
        //        rend.color = Color.clear;
        yield return new WaitForSeconds(.05f * invTime);
        rend.color = Color.white;
        yield return new WaitForSeconds(.05f * invTime);

        if (enHealthCurrent <= 0)
        {
            StartCoroutine(EnDeath());
        }

        koboldRB.velocity = Vector3.zero;

        gotHit = false;
    }

    private void OnEnable()
    {
        enHealthCurrent = enHealthMax;
    }

    // Start is called before the first frame update
    void Start()
    {
        detector = gameObject.GetComponentInChildren<enHitdetector>();
        enHealthMax = 3;
        enHealthCurrent = enHealthMax;

        kAnimScript = gameObject.GetComponent<ScarabAnimScript>();
        koboldRB = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        pCol = player.GetComponent<CapsuleCollider2D>();
        enCol = gameObject.GetComponent<CapsuleCollider2D>();

    }

    IEnumerator KSlash()
    {
        pDetected = false;
        isAttacking = true;

        koboldRB.AddForce(kAnimScript.backDirection, ForceMode2D.Impulse);

        yield return new WaitForSeconds(.5f);

        currAttDir = kAnimScript.currentState;
        spriteSwitch = true;

        koboldRB.AddForce(kAnimScript.faceDirection * 4, ForceMode2D.Impulse);

        kslashl.SetActive(true);
        kslashr.SetActive(true);

        yield return new WaitForSeconds(.2f);

        koboldRB.velocity = new Vector2(0, 0);

        kslashl.SetActive(false);
        kslashr.SetActive(false);

        yield return new WaitForSeconds(.5f);

        spriteSwitch = false;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!inRange && kslashRoutine != null)
        {
            StopCoroutine(kslashRoutine);
            isAttacking = false;
            spriteSwitch = false;
        }

        //        Debug.Log(transform.InverseTransformPoint(player.transform.position).normalized);

        if (Vector2.Distance(koboldRB.position, player.transform.position) < 5)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        if (pDetected == true && !isAttacking)
        {
            kslashRoutine = StartCoroutine(KSlash());
        }

        if (PlayerController.enCollisionIgnore == false)
        {

            Physics2D.IgnoreLayerCollision(11, 10, false);
        }

        if (PlayerController.enCollisionIgnore == true)
        {

            Physics2D.IgnoreLayerCollision(11, 10, true);
        }

        if (gotHit == true && gotHitSwitch == true)
        {
            StartCoroutine(GotHit(detector.damage, detector.kbPower, detector.invTime));
            gotHitSwitch = false;
        }

        //      klash location updater

        if ( kAnimScript.faceDirection == Vector3.down ||  kAnimScript.faceDirection == Vector3.up ||  kAnimScript.faceDirection == Vector3.right ||  kAnimScript.faceDirection == Vector3.left)
        {
            kslashl.transform.localPosition =  kAnimScript.faceDirection / 4;
            kslashl.transform.localEulerAngles = kAnimScript.faceRoto;
            kslashr.transform.localPosition =  kAnimScript.faceDirection / 4;
            kslashr.transform.localEulerAngles = kAnimScript.faceRoto;
            pdetector.transform.localPosition =  kAnimScript.faceDirection / 4;
            pdetector.transform.localEulerAngles = kAnimScript.faceRoto;
        }

        else if ( kAnimScript.faceDirection == (Vector3.down + Vector3.right) ||  kAnimScript.faceDirection == (Vector3.down + Vector3.left) ||  kAnimScript.faceDirection == (Vector3.up + Vector3.right) ||  kAnimScript.faceDirection == (Vector3.up + Vector3.left))
        {
            kslashl.transform.localPosition =  kAnimScript.faceDirection * .3334f / 2;
            kslashl.transform.localEulerAngles = kAnimScript.faceRoto;
            kslashr.transform.localPosition =  kAnimScript.faceDirection * .3334f / 2;
            kslashr.transform.localEulerAngles = kAnimScript.faceRoto;
            pdetector.transform.localPosition =  kAnimScript.faceDirection * .3334f / 2;
            pdetector.transform.localEulerAngles = kAnimScript.faceRoto;
        }
    }
}
