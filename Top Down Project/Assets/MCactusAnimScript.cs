using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCactusAnimScript : MonoBehaviour
{
    public GameObject player;
    public MCactusCombat thiskCom;

    public Animator anim;
    public int currentState;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        thiskCom = gameObject.GetComponent<MCactusCombat>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pDistancenorm = transform.InverseTransformPoint(player.transform.position).normalized;

        anim.SetInteger("animState", currentState);

        if(thiskCom.inRange)
        {
            currentState = 5;
        }
        else
        {
            currentState = 13;
        }
    }
}
