using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashScript : MonoBehaviour
{
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
        knockBack = 1f;  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
