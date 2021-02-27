using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollectableScript : MonoBehaviour
{
    public virtual void Collected()
    {
//        Debug.Log("called");

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
//            Debug.Log("called");

            Collected();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
