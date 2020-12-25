using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneScript : MonoBehaviour
{
    public bool isDragging;
    public Transform pTrans;
    public GameObject player;
    public int dragCounter = 1;
    public Rigidbody2D rb;

   // IEnumerator hitDrag()
   // {
      
   // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Slash(Clone)")
        {
          
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
