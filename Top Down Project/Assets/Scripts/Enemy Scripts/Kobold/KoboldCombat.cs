using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class KoboldCombat : MonoBehaviour
{
    public KoboldAnimScript kAnimScript;

    public bool inRange;
    public bool pDetected;

    public GameObject pdetector;
    public GameObject kslashl;
    public GameObject kslashr;
//    public Vector3  kAnimScript.faceDirection;
//    public Vector3 faceRoto;

//    public Animator anim;
//    public int idleState;
//    public int currentState;
//    public int runState;

    public SpriteRenderer rend;
    public GameObject player;
    public CapsuleCollider2D pCol;
    public BoxCollider2D enCol;
    public bool gotHit;
    public bool gotHitSwitch;
    public Rigidbody2D koboldRB;

    IEnumerator GotHit()
    {
        //        Debug.Log("Gothit engaged");

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

        gotHit = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        kAnimScript = gameObject.GetComponent<KoboldAnimScript>();
        koboldRB = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        pCol = player.GetComponent<CapsuleCollider2D>();
        enCol = gameObject.GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
//        Debug.Log(transform.InverseTransformPoint(player.transform.position).normalized);

//        Vector3 pDistancenorm = transform.InverseTransformPoint(player.transform.position).normalized;

        if (Vector2.Distance(koboldRB.position, player.transform.position) < 1)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        if (pDetected == true)
        {
            kslashl.SetActive(true);
            kslashr.SetActive(true);
            pDetected = false;
        }

        if (PlayerController.enCollisionIgnore == false)
        {
//            Physics2D.IgnoreCollision(pCol, enCol, false);

            Physics2D.IgnoreLayerCollision(11, 10, false);
        }

        if (PlayerController.enCollisionIgnore == true)
        {
  //          Physics2D.IgnoreCollision(pCol, enCol);

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
            kslashl.transform.localPosition =  kAnimScript.faceDirection / 2 / 2;
            kslashl.transform.localEulerAngles = kAnimScript.faceRoto;
            kslashr.transform.localPosition =  kAnimScript.faceDirection / 2 / 2;
            kslashr.transform.localEulerAngles = kAnimScript.faceRoto;
            pdetector.transform.localPosition =  kAnimScript.faceDirection / 2 / 2;
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
