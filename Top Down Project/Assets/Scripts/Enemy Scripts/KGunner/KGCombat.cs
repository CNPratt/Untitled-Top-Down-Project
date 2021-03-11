using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class KGCombat : EnCombatMono
{
    public GameObject dropLoot;

    public KGPath thisPath;
    public bool canShoot;

 //   public int enHealthMax;
 //   public int enHealthCurrent;
    public GameObject deathFX;

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

    IEnumerator GotHit()
    {
//                Debug.Log("Gothit engaged");

        koboldRB.velocity = Vector3.zero;

        enHealthCurrent = enHealthCurrent - 1;

        if (isAttacking)
        {
            if (kshotRoutine != null)
            {
                StopCoroutine(kshotRoutine);
            }

            StartCoroutine(KShotCD());
            isAttacking = false;
            spriteSwitch = false;
        }

        koboldRB.AddForce((transform.position - player.transform.position).normalized * 1.5f, ForceMode2D.Impulse);

        //      used to be double the time below - .05sec instead of .025s

        rend.color = Color.black;
//        rend.color = Color.white;
        yield return new WaitForSeconds(.05f);
//        rend.color = Color.clear;
        yield return new WaitForSeconds(.05f);
//        rend.color = Color.white;
        yield return new WaitForSeconds(.05f);
//        rend.color = Color.clear;
        yield return new WaitForSeconds(.05f);
        rend.color = Color.white;
        yield return new WaitForSeconds(.05f);

        if (enHealthCurrent <= 0)
        {
            StartCoroutine(EnDeath());
        }

        koboldRB.velocity = Vector3.zero;

        gotHit = false;
    }

    private void OnEnable()
    {
        canShoot = true;
        enHealthCurrent = enHealthMax;
    }

    private void OnDisable()
    {
        inRange = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;

        enHealthMax = 3;
        enHealthCurrent = enHealthMax;

        thisPath = gameObject.GetComponent<KGPath>();
        shotSwitch = true;
        kAnimScript = gameObject.GetComponent<KGAnimScript>();
        koboldRB = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        pCol = player.GetComponent<CapsuleCollider2D>();
        enCol = gameObject.GetComponent<CapsuleCollider2D>();

    }

    IEnumerator KShotCD()
    {
//        Debug.Log("called");

        yield return new WaitForSeconds(Random.Range(3f, 4f));

        canShoot = true;
    }

    IEnumerator KShot()
    {
        canShoot = false;

        toShoot = false;
        isAttacking = true;

        koboldRB.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(.1f);

        yield return new WaitForSeconds(.2f);

        yield return new WaitForSeconds(.1f);

        currAttDir = kAnimScript.currentState;
        spriteSwitch = true;
        Instantiate(photonPrefab, kslashl.transform.position, kslashl.transform.rotation);

        koboldRB.AddForce(kAnimScript.backDirection, ForceMode2D.Impulse);

        yield return new WaitForSeconds(.1f);

        koboldRB.AddForce(kAnimScript.faceDirection, ForceMode2D.Impulse);

        yield return new WaitForSeconds(.1f);

        spriteSwitch = false;
        isAttacking = false;
        StartCoroutine("KShotCD", 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (canShoot && thisPath.losHit.collider == null && inRange)
        {
//            Debug.Log("called");
//            shotSwitch = false;
            kshotRoutine = StartCoroutine("KShot", 0);
        }

//        Debug.Log(transform.InverseTransformPoint(player.transform.position).normalized);

        if (Vector2.Distance(koboldRB.position, player.transform.position) < 5)
        {
            inRange = true;
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
            StartCoroutine(GotHit());
            gotHitSwitch = false;
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
