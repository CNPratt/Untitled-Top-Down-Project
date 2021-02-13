using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseColliderScript : MonoBehaviour
{
    private void OnEnable()
    {
        Physics2D.IgnoreLayerCollision(11, 10);
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
