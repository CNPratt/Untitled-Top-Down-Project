using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLootScript : MonoBehaviour
{
    public GameObject hcrystal;
    public GameObject battery;
    public GameObject singlechip;

    public int roller;

    // Start is called before the first frame update
    void Start()
    {
        roller = Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if(roller == 0)
        {
            Instantiate(hcrystal, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (roller == 1)
        {
            Instantiate(battery, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (roller == 2)
        {
            Instantiate(singlechip, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (roller == 3)
        {
            Destroy(gameObject);
        }
    }
}
