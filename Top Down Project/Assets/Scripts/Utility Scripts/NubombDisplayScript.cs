using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NubombDisplayScript : MonoBehaviour
{
    public Throwscripttest2 throwScript;

    public int tickTimer;

    public bool alphaSwitch;
//    public int roller;
    public SpriteRenderer rend;
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        tickTimer = 0;

        rend = gameObject.GetComponent<SpriteRenderer>();

        color = rend.color;
    }

    // Update is called once per frame
    void Update()
    {
        color = rend.color;

        //        color.a = Mathf.Sin(Random.Range(.3f, .8f));
//        Debug.Log(color.a);

        if(color.a == 1)
        {
            alphaSwitch = true;
        }

        if (color.a <= .5f)
        {
            alphaSwitch = false;
        }

            if (alphaSwitch)
            {
                color.a = color.a - (.01f);
            }
            else if (!alphaSwitch)
            {
                color.a = color.a + (.01f);
            }

        if (tickTimer == 1)
        {
            tickTimer = 0;
            color = Color.yellow;
        }

        if (tickTimer == 2)
        {
            tickTimer = 0;
            color = Color.red;
        }

        rend.color = color;


//        roller = Random.Range(0, 2);

//        if(roller == 0)
//        {
//            rend.color = color;
//        }

    }
}
