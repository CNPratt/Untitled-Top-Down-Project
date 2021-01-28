using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class dashEffect : MonoBehaviour
{
//    public Animator anim;
    public bool fadeOn;
    public Color color;
    public SpriteRenderer rend;
    public SpriteRenderer prend;
//    public int dashFXstate;

    // Start is called before the first frame update
    void Start()
    {
        prend = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        rend = GetComponent<SpriteRenderer>();
        color = rend.color;
        color.a = 1f;

        rend.sprite = prend.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerAnimScript.currentState == 1 || PlayerAnimScript.currentState == 5 || PlayerAnimScript.currentState == 6 || PlayerAnimScript.currentState == 9 || PlayerAnimScript.currentState == 13 || PlayerAnimScript.currentState == 14)
        {
            rend.sortingOrder = 1;
        }
        else
        {
            rend.sortingOrder = -1;
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
