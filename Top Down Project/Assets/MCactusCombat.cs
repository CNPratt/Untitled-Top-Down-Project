using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MCactusCombat : EnCombatMono
{
    public enHitdetector detector;

    public GameObject dropLoot;

    public GameObject deathFX;
    public MCactusAnimScript kAnimScript;

    public int currAttDir;
    public bool spriteSwitch;
    public bool inRange;

    public SpriteRenderer rend;
    public GameObject player;
    public CapsuleCollider2D pCol;
    public CapsuleCollider2D enCol;

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
        enHealthMax = 1;
        enHealthCurrent = enHealthMax;

        kAnimScript = gameObject.GetComponent<MCactusAnimScript>();
        koboldRB = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        pCol = player.GetComponent<CapsuleCollider2D>();
        enCol = gameObject.GetComponent<CapsuleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {

        //        Debug.Log(transform.InverseTransformPoint(player.transform.position).normalized);

        if (Vector2.Distance(koboldRB.position, player.transform.position) < 3)
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
            StartCoroutine(GotHit(detector.damage, detector.kbPower, detector.invTime));
            gotHitSwitch = false;
        }

        if (gotHit == true)
        {
            //            koboldRB.velocity = new Vector2(0, 0);
            //            koboldRB.AddForce((transform.position - player.transform.position).normalized * 50f, ForceMode2D.Force);
        }
    }
}
