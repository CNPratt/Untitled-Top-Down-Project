using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class dashEffect : MonoBehaviour
{
    public PlayerAnimScript animScript;

//    public Animator anim;
    public bool fadeOn;
    public Color color;
    public SpriteRenderer rend;
    public SpriteRenderer prend;
//    public int dashFXstate;

    // Start is called before the first frame update
    void Start()
    {
        animScript = GameObject.Find("Player").GetComponent<PlayerAnimScript>();
        prend = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        rend = GetComponent<SpriteRenderer>();
        color = rend.color;
        color.a = prend.color.a;

        rend.sprite = prend.sprite;
    }

    // Update is called once per frame
    void Update()
    {
//        if (animScript.currentState == 4 || animScript.currentState == 5 || animScript.currentState == 6 || animScript.currentState == 12 || animScript.currentState == 13 || animScript.currentState == 14)
//        {
//            rend.sortingOrder = -1;
//        }

        if(PlayerAnimScript.faceDirection == Vector3.down || PlayerAnimScript.faceDirection == Vector3.down + Vector3.right || PlayerAnimScript.faceDirection == Vector3.down + Vector3.left)
        {
            rend.sortingOrder = -1;
        }
        else
        {
            rend.sortingOrder = 1;
        }


        //        dashFXstate = PlayerAnimScript.currentState;
        //        anim.SetInteger("animState", dashFXstate);

        rend.color = color;

        IEnumerator DashFade()
        {
            color.a = color.a - .05f;
            yield return new WaitForSeconds(.25f);
            fadeOn = false;
        }

        if (fadeOn == false)
        {
//            Debug.Log("Dashfade called");

            StartCoroutine(DashFade());
        }

        if(color.a <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}
