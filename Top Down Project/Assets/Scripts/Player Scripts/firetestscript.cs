using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firetestscript : MonoBehaviour
{
    public GameObject fire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("l"))
        {
            Debug.Log("firing");
            GameObject thisFire = Instantiate(fire, transform.position, transform.rotation);
            thisFire.transform.parent = gameObject.transform;
        }
    }
}
