using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NubombScript : MonoBehaviour
{
    public Vector2 pos;

    public bool startSwitch;

    public GameObject explosion;
    public GameObject display;

    public SpriteRenderer sprite;
    public Animator anim;
    public Throwscripttest2 throwScript;
    public NubombDisplayScript displayScript;

    IEnumerator BombCountdown()
    {
//        sprite.color = Color.green;

        yield return new WaitForSeconds(2f);

        displayScript.tickTimer = 1;

//        sprite.color = Color.yellow;

        yield return new WaitForSeconds(2f);

        displayScript.tickTimer = 2;

//        sprite.color = Color.red;

        yield return new WaitForSeconds(2f);

        displayScript.tickTimer = 3;

        anim.SetInteger("bombpara", 1);

 //       transform.parent = null;

        explosion.SetActive(true);
        display.SetActive(false);
    }

    public void Popped()
    {
        sprite.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        throwScript = gameObject.GetComponent<Throwscripttest2>();
    }

    // Update is called once per frame
    void Update()
    {
        if(throwScript.isHeld)
        {
            startSwitch = true;
        }

        if(startSwitch == true)
        {
            startSwitch = false;

            StartCoroutine(BombCountdown());
        }

        if(explosion.activeSelf && transform.parent != null)
        {
//            pos = transform.TransformPoint(gameObject.transform.localPosition);

            PlayerController.isHolding = false;

            throwScript.isHeld = false;

            throwScript.enabled = false;

            gameObject.transform.SetParent(null, true);

//            Debug.Log(pos);
        }
    }
}
