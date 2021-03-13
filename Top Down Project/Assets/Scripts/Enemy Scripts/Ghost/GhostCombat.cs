using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GhostCombat : EnCombatMono
{
    public enHitdetector detector;
    public Slotscript thisSlot;

    public bool hasReturned;
    public Vector2 seekstartPos;

    public bool isExecuting;
    public bool cooldown;

    public NanofloatScript fscript;
    public bool isReturning;
    public bool isSeeking;
    public GameObject vortex;
    public GameObject colCenter;
    public GameObject dropLoot;

    //    public int enHealthMax;
    //    public int enHealthCurrent;
    public GameObject deathFX;
    public GhostAnimScript kAnimScript;
    public GhostPath gpath;

    public Coroutine kslashRoutine;
    public int currAttDir;
    public bool spriteSwitch;
    public bool isAttacking;
    public bool inRange;
    public bool pDetected;

    public GameObject pdetector;
    public GameObject kslashl;
    public GameObject kslashr;

    public GhostscytheScript kslashlScript;
    public GhostscytheScript kslashrScript;

    public SpriteRenderer rend;
    public GameObject player;
    public CapsuleCollider2D pCol;
    public CapsuleCollider2D enCol;
    //    public bool gotHit;
    //    public bool gotHitSwitch;
    public Rigidbody2D koboldRB;

    IEnumerator EnDeath()
    {
        DropLoot(dropLoot);
        Instantiate(deathFX, transform.position, transform.rotation);
        //       Destroy(gameObject);
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

            StopCoroutine(kslashlScript.thisSlash);
            StopCoroutine(kslashrScript.thisSlash);
            kslashlScript.isSlashing = false;
            kslashrScript.isSlashing = false;

            kslashlScript.midReached = false;
            kslashlScript.hasReachedEnd = false;
            kslashlScript.thisSlash = null;

            kslashrScript.midReached = false;
            kslashrScript.hasReachedEnd = false;
            kslashrScript.thisSlash = null;

            gpath.gseekEndReached = false;

            thisSlot.isOccupied = false;
            isExecuting = false;

            gpath.target = gpath.player;
            enCol.enabled = true;
            gpath.triggerCollider.enabled = false;
            fscript.enabled = true;

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

    private void OnDisable()
    {
        inRange = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        detector = gameObject.GetComponentInChildren<enHitdetector>();

        kslashrScript = kslashr.GetComponent<GhostscytheScript>();
        kslashlScript = kslashl.GetComponent<GhostscytheScript>();


        fscript = gameObject.GetComponent<NanofloatScript>();

        enHealthMax = 3;
        enHealthCurrent = enHealthMax;

        gpath = gameObject.GetComponent<GhostPath>();
        kAnimScript = gameObject.GetComponent<GhostAnimScript>();
        koboldRB = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        pCol = player.GetComponent<CapsuleCollider2D>();
        enCol = gameObject.GetComponent<CapsuleCollider2D>();

    }

    IEnumerator GhostSeek()
    {
//        Debug.Log("seek called");

        thisSlot = SlotManager.OpenSlots[gpath.randomOpenSpot];

        seekstartPos = transform.position;

        fscript.enabled = false;

        isSeeking = true;

        gpath.target = thisSlot.transform;

        thisSlot.isOccupied = true;

        enCol.enabled = false;
        gpath.triggerCollider.enabled = false;

        yield return new WaitUntil(() => gpath.gseekEndReached);

        kslashRoutine =  StartCoroutine("KSlash");
        isSeeking = false;

    }

    IEnumerator KSlash()
    {
//        Debug.Log("slash called");

        isExecuting = true;
        cooldown = true;

//        gpath.randomOpenSpot = Random.Range(0, SlotManager.OpenSlots.Count);

        vortex.SetActive(true);

        koboldRB.velocity = Vector3.zero;

        yield return new WaitForSeconds(.1f);

        currAttDir = kAnimScript.currentState;
        spriteSwitch = true;

        kslashl.SetActive(true);
        kslashr.SetActive(true);

        yield return new WaitUntil(() => kslashlScript.isSlashing);

        isAttacking = true;

//          to track player movement before attacking, much more difficult:
//             currAttDir = kAnimScript.currentState;
//             spriteSwitch = true;

        yield return new WaitUntil(() => kslashlScript.hasReachedEnd);

        yield return new WaitForSeconds(.5f);

        vortex.SetActive(false);

        gpath.target = gpath.player;

//        enCol.enabled = true;

        spriteSwitch = false;
        isAttacking = false;

        thisSlot.isOccupied = false;
//        isExecuting = false;

        gpath.triggerCollider.enabled = false;
        fscript.enabled = true;

        gpath.gseekEndReached = false;

        isReturning = true;

        yield return new WaitUntil(() => hasReturned);

        isReturning = false;

        isExecuting = false;
        hasReturned = false;

        StartCoroutine("GhostCD");
    }

    IEnumerator GhostCD()
    {
        yield return new WaitForSeconds(5);

        cooldown = false;
    }


    // Update is called once per frame
    void Update()
    {
        gpath.randomOpenSpot = Random.Range(0, SlotManager.OpenSlots.Count);

        if (isExecuting && Vector2.Distance(transform.position, seekstartPos) < .5f)
        {
            hasReturned = true;
        }

        else
        {
            hasReturned = false;
        }

        if(inRange && !isAttacking && !isSeeking && !cooldown)
        {
            StartCoroutine("GhostSeek");
        }

        if (gotHit)
        {
            pdetector.SetActive(false);
        }
        else
        {
            pdetector.SetActive(true);
        }

        //        Debug.Log(enHealthCurrent + "/" + enHealthMax);

        if (!inRange && kslashRoutine != null)
        {
            StopCoroutine(kslashRoutine);
            StopCoroutine(kslashlScript.thisSlash);
            StopCoroutine(kslashrScript.thisSlash);

            kslashlScript.isSlashing = false;
            kslashrScript.isSlashing = false;

            kslashlScript.midReached = false;
            kslashlScript.hasReachedEnd = false;
            kslashlScript.thisSlash = null;

            kslashrScript.midReached = false;
            kslashrScript.hasReachedEnd = false;
            kslashrScript.thisSlash = null;

            SlotManager.OpenSlots[gpath.randomOpenSpot].isOccupied = false;
            isExecuting = false;

            gpath.target = gpath.player;
            enCol.enabled = true;
            gpath.triggerCollider.enabled = false;
            fscript.enabled = true;

            gpath.gseekEndReached = false;

            isAttacking = false;
            spriteSwitch = false;
        }

        //        Debug.Log(transform.InverseTransformPoint(player.transform.position).normalized);

        if (Vector2.Distance(koboldRB.position, player.transform.position) < 5)
        {
            inRange = true;
        }

//        if (pDetected == true && !isAttacking)
//        {
//            kslashRoutine = StartCoroutine(KSlash());
//        }

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

        if (gotHit == true)
        {
            //            koboldRB.velocity = new Vector2(0, 0);
            //            koboldRB.AddForce((transform.position - player.transform.position).normalized * 50f, ForceMode2D.Force);
        }

        //      klash location updater
        //      used to be double the distance form kobold

        colCenter.transform.localEulerAngles = kAnimScript.faceRoto;
    }
}
