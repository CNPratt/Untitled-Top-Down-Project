using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KslashScript : MonoBehaviour
{
    public float knockBack;
    public Vector3 enPos;
    public Transform koboldTrans;

    //disabler from animevent on slashanim

    public void KSlashDisable(int i)
    {
        koboldTrans = transform;

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
