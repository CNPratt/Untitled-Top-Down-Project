using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NanobotAnimScript : MonoBehaviour
{
    public GameObject targPos;
    public Animator anim;
    public AIPath ai;
    public Rigidbody2D koboldRB;
    public int runState;
    public int currentState;
    public int idleState;

    public Vector3 faceDirection;
    public Vector3 faceRoto;
    public Vector3 backDirection;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<AIPath>(); // For example

        anim = GetComponent<Animator>();
        koboldRB = gameObject.GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
//              Debug.Log(ai.velocity);

        anim.SetInteger("animState", currentState);

        //      anim state setter

        if(Vector2.Distance(transform.position, targPos.transform.position) < 1f)
        {
            runState = PlayerAnimScript.runState;
            currentState = runState;
            idleState = PlayerAnimScript.idleState;
        }

        else if (ai.velocity.y > 0 && Mathf.Abs(ai.velocity.x) <= Mathf.Abs(ai.velocity.y) / 2)
        {
            runState = 1;
            currentState = runState;
            idleState = 9;

        }

        else if (ai.velocity.x < 0 && Mathf.Abs(ai.velocity.y) <= Mathf.Abs(ai.velocity.x) / 2)
        {
            runState = 7;
            currentState = runState;
            idleState = 15;
        }

        else if (ai.velocity.y < 0 && Mathf.Abs(ai.velocity.x) <= Mathf.Abs(ai.velocity.y) / 2)
        {
            runState = 5;
            currentState = runState;
            idleState = 13;

        }

        else if (ai.velocity.x > 0 && Mathf.Abs(ai.velocity.y) <= Mathf.Abs(ai.velocity.x) / 2)
        {
            runState = 3;
            currentState = runState;
            idleState = 11;
        }

        else if (ai.velocity.x < 0 && ai.velocity.y > 0)
        {
            runState = 8;
            currentState = runState;
            idleState = 16;

        }

        else if (ai.velocity.x > 0 && ai.velocity.y > 0)
        {
            runState = 2;
            currentState = runState;
            idleState = 10;
        }

        else if (ai.velocity.x < 0 && ai.velocity.y < 0)
        {
            runState = 6;
            currentState = runState;
            idleState = 16;
        }

        else if (ai.velocity.x > 0 && ai.velocity.y < 0)
        {
            runState = 4;
            currentState = runState;
            idleState = 12;
        }
        else
        {
            currentState = idleState;
        }

//      faceDirection && roto here

        if (currentState == 5 || currentState == 13 || currentState == 0)
        {
            faceDirection = Vector2.down;
            faceRoto = new Vector3(0, 0, 0);
            backDirection = Vector2.up;

        }

        else if (currentState == 1 || currentState == 9)
        {
            faceDirection = Vector2.up;
            faceRoto = new Vector3(0, 0, 180);
            backDirection = Vector2.down;
        }

        else if (currentState == 7 || currentState == 15)
        {
            faceDirection = Vector2.left;
            faceRoto = new Vector3(0, 0, -90);
            backDirection = Vector2.right;
        }

        else if (currentState == 3 || currentState == 11)
        {
            faceDirection = Vector2.right;
            faceRoto = new Vector3(0, 0, 90);
            backDirection = Vector2.left;
        }

        else if (currentState == 4 || currentState == 12)
        {
            faceDirection = (Vector2.down + Vector2.right);
            faceRoto = new Vector3(0, 0, 45);
            backDirection = Vector2.up + Vector2.left;
        }

        else if (currentState == 6 || currentState == 14)
        {
            faceDirection = (Vector2.down + Vector2.left);
            faceRoto = new Vector3(0, 0, -45);
            backDirection = Vector2.up + Vector2.right;
        }

        else if (currentState == 2 || currentState == 10)
        {
            faceDirection = (Vector2.up + Vector2.right);
            faceRoto = new Vector3(0, 0, 135);
            backDirection = Vector2.down + Vector2.left;
        }

        else if (currentState == 8 || currentState == 16)
        {
            faceDirection = (Vector2.up + Vector2.left);
            faceRoto = new Vector3(0, 0, -135);
            backDirection = Vector2.down + Vector2.right;
        }
    }
}
