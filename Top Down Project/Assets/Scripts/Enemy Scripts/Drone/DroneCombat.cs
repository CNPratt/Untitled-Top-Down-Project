using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DroneCombat : MonoBehaviour
{
    public Animator anim;
    public static int idleState;
    public static int currentState;
    public static int runState;

    public SpriteRenderer rend;
    public GameObject player;
    public bool gotHit;
    public bool gotHitSwitch;
    public Rigidbody2D droneRB;

    IEnumerator GotHit()
    {
        //        Debug.Log("Gothit engaged");

        rend.color = Color.white;
        yield return new WaitForSeconds(.1f);
        rend.color = Color.clear;
        yield return new WaitForSeconds(.1f);
        rend.color = Color.white;
        yield return new WaitForSeconds(.1f);
        rend.color = Color.clear;
        yield return new WaitForSeconds(.1f);
        rend.color = Color.white;
        yield return new WaitForSeconds(.1f);

        gotHit = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        droneRB = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
 //       Physics2D.IgnoreCollision(player.GetComponent<CapsuleCollider2D>(), gameObject.GetComponent<CapsuleCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.enCollisionIgnore == false)
        {
            Physics2D.IgnoreCollision(player.GetComponent<CapsuleCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), false);
        }

        else
        {
            Physics2D.IgnoreCollision(player.GetComponent<CapsuleCollider2D>(), gameObject.GetComponent<BoxCollider2D>());
        }

        if (gotHit == true && gotHitSwitch == true)
        {
            StartCoroutine(GotHit());
            gotHitSwitch = false;
        }

        if (gotHit == true)
        {
            droneRB.velocity = new Vector2(0, 0);
            droneRB.AddForce((transform.position - player.transform.position).normalized * 50f, ForceMode2D.Force);
        }

        //animation handling

        Debug.Log(droneRB.velocity);

        anim.SetInteger("animState", currentState);

        if (droneRB.velocity.y > 0 && Mathf.Abs(droneRB.velocity.x) <= Mathf.Abs(droneRB.velocity.y)/2)
        {
                runState = 1;
                currentState = runState;
                idleState = 9;
            
        }

        else if (droneRB.velocity.x < 0 && Mathf.Abs(droneRB.velocity.y) <= Mathf.Abs(droneRB.velocity.x)/2)
        {
                runState = 7;
                currentState = runState;
                idleState = 15;
        }

        else if (droneRB.velocity.y < 0 && Mathf.Abs(droneRB.velocity.x) <= Mathf.Abs(droneRB.velocity.y)/2)
        {
                runState = 5;
                currentState = runState;
                idleState = 13;

        }

        else if (droneRB.velocity.x > 0 && Mathf.Abs(droneRB.velocity.y) <= Mathf.Abs(droneRB.velocity.x)/2)
        {
                runState = 3;
                currentState = runState;
                idleState = 11;
        }

        else if (droneRB.velocity.x < 0 && droneRB.velocity.y > 0)
        {
                runState = 8;
                currentState = runState;
                idleState = 16;
            
        }

        else if (droneRB.velocity.x > 0 && droneRB.velocity.y > 0)
        {
                runState = 2;
                currentState = runState;
                idleState = 10;
        }

        else if (droneRB.velocity.x < 0 && droneRB.velocity.y < 0)
        {
                runState = 6;
                currentState = runState;
                idleState = 16;
        }

        else if (droneRB.velocity.x > 0 && droneRB.velocity.y < 0)
        {
                runState = 4;
                currentState = runState;
                idleState = 12;
        }
    }
}
