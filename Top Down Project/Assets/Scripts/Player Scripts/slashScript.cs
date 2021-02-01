using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashScript : MonoBehaviour
{
    public SpriteRenderer rend;
    public float knockBack;
    public Vector3 enPos;
    public Transform playerTrans;

    //disabler from animevent on slashanim

    public void SlashDisable(int i)
    {
        if (i == 1)
        {
//            Debug.Log("slashdis called");

            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        knockBack = 1f;  
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerAnimScript.currentState == 4 || PlayerAnimScript.currentState == 5 || PlayerAnimScript.currentState == 6 || PlayerAnimScript.currentState == 12 || PlayerAnimScript.currentState == 13 || PlayerAnimScript.currentState == 14)
        {
            rend.sortingOrder = 1;
        }
        else
        {
            rend.sortingOrder = -1;
        }
    }
}
