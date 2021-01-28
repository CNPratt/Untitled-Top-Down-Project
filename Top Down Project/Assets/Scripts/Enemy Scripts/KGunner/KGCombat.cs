using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class KGCombat : EnCombatMono
{
    public Coroutine kshotRoutine;
    public bool shotSwitch;
    public GameObject photonPrefab;
    public KGAnimScript kAnimScript;

    public Coroutine kslashRoutine;
    public int currAttDir;
    public bool spriteSwitch;
    public bool isAttacking;
    public bool inRange;
    public bool toShoot;

    public GameObject pdetector;
    public GameObject kslashl;
    public GameObject kslashr;

    public SpriteRenderer rend;
    public GameObject player;
    public CapsuleCollider2D pCol;
    public CapsuleCollider2D enCol;
//    public bool gotHit;
    public bool gotHitSwitch;
    public Rigidbody2D koboldRB;

    

    IEnumerator GotHit()
    {
        //        Debug.Log("Gothit engaged");

        if (isAttacking)
        {
            StopCoroutine(kshotRoutine);
            isAttacking = false;
            spriteSwitch = false;
        }

        koboldRB.velocity = Vector3.zero;

        koboldRB.AddForce((transform.position - player.transform.position).normalized * 5f, ForceMode2D.Impulse);

        rend.color = Color.white;
        yield return new WaitForSeconds(.1f);
        rend.color = Color.clear;
        yield return new WaitForSeconds(.1f);
        rend.color = Color.white;
        yield return new WaitForSeconds(.1f);
        rend.color = Color.clear;
        yield return new WaitForSeconds(.1f);
        rend.color = Color.white;
        yield return new WaitForSeconds(.1f);

        koboldRB.velocity = Vector3.zero;

        gotHit = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        shotSwitch = true;
        kAnimScript = gameObject.GetComponent<KGAnimScript>();
        koboldRB = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        pCol = player.GetComponent<CapsuleCollider2D>();
        enCol = gameObject.GetComponent<CapsuleCollider2D>();

    }

    IEnumerator KShotCD()
    {
 //       Debug.Log("called");

        yield return new WaitForSeconds(Random.Range(4f, 5f));

        if (!gotHit && inRange)
        {
            kshotRoutine = StartCoroutine("KShot", 0);
        }

        StartCoroutine("KShotCD", 0);
    }

    IEnumerator KShot()
    {
        toShoot = false;
        isAttacking = true;

        koboldRB.velocity = new Vector2(0, 0);

//        koboldRB.AddForce(kAnimScript.backDirection, ForceMode2D.Impulse);

        yield return new WaitForSeconds(.5f);

//       spriteSwitch = true;

//       Instantiate(photonPrefab, kslashl.transform.position, kslashl.transform.rotation);

//        koboldRB.AddForce(kAnimScript.faceDirection * 4, ForceMode2D.Impulse);

//        kslashl.SetActive(true);
//        kslashr.SetActive(true);

        yield return new WaitForSeconds(.2f);

//        koboldRB.velocity = new Vector2(0, 0);

//        kslashl.SetActive(false);
//        kslashr.SetActive(false);

        yield return new WaitForSeconds(.5f);

        currAttDir = kAnimScript.currentState;
        spriteSwitch = true;
        Instantiate(photonPrefab, kslashl.transform.position, kslashl.transform.rotation);

        koboldRB.AddForce(kAnimScript.backDirection, ForceMode2D.Impulse);

        yield return new WaitForSeconds(.1f);

        koboldRB.AddForce(kAnimScript.faceDirection, ForceMode2D.Impulse);

        yield return new WaitForSeconds(.4f);

        spriteSwitch = false;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(shotSwitch)
        {
 //           Debug.Log("called");
            shotSwitch = false;
            StartCoroutine("KShotCD", 0);
        }

//       if(!inRange && kslashRoutine != null)
//       {
//            StopCoroutine(kslashRoutine);
//            isAttacking = false;
//            spriteSwitch = false;
//        }

        //        Debug.Log(transform.InverseTransformPoint(player.transform.position).normalized);

        if (Vector2.Distance(koboldRB.position, player.transform.position) < 5)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

 //       if (toShoot == true && !isAttacking)
 //       {
 //           kslashRoutine = StartCoroutine(KShot());
 //       }

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
            StartCoroutine(GotHit());
            gotHitSwitch = false;
        }

        if (gotHit == true)
        {
            koboldRB.velocity = new Vector2(0, 0);
            koboldRB.AddForce((transform.position - player.transform.position).normalized * 50f, ForceMode2D.Force);
        }

        //      klash location updater

        if ( kAnimScript.faceDirection == Vector3.down ||  kAnimScript.faceDirection == Vector3.up ||  kAnimScript.faceDirection == Vector3.right ||  kAnimScript.faceDirection == Vector3.left)
        {
 //           Debug.Log(kslashl.transform.localPosition);

            kslashl.transform.localPosition =  kAnimScript.faceDirection * 4;
            kslashl.transform.localEulerAngles = kAnimScript.faceRoto;
            kslashr.transform.localPosition =  kAnimScript.faceDirection * 4;
            kslashr.transform.localEulerAngles = kAnimScript.faceRoto;
            pdetector.transform.localPosition =  kAnimScript.faceDirection * 4;
            pdetector.transform.localEulerAngles = kAnimScript.faceRoto;
        }

        else if ( kAnimScript.faceDirection == (Vector3.down + Vector3.right) ||  kAnimScript.faceDirection == (Vector3.down + Vector3.left) ||  kAnimScript.faceDirection == (Vector3.up + Vector3.right) ||  kAnimScript.faceDirection == (Vector3.up + Vector3.left))
        {
//            Debug.Log(kslashl.transform.localPosition);

            kslashl.transform.localPosition =  kAnimScript.faceDirection * 2;
            kslashl.transform.localEulerAngles = kAnimScript.faceRoto;
            kslashr.transform.localPosition =  kAnimScript.faceDirection * 2;
            kslashr.transform.localEulerAngles = kAnimScript.faceRoto;
            pdetector.transform.localPosition =  kAnimScript.faceDirection * 2;
            pdetector.transform.localEulerAngles = kAnimScript.faceRoto;
        }
    }
}
