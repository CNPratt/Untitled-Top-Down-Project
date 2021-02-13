using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarabAnimScript : MonoBehaviour
{
    public GameObject player;
    public ScarabCombat thiskCom;
    public Rigidbody2D koboldRB;

    public Vector3 faceDirection;
    public Vector3 faceRoto;
    public Vector3 backDirection;

    public Animator anim;
    public int idleState;
    public int currentState;
    public int runState;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        koboldRB = gameObject.GetComponent<Rigidbody2D>();
        thiskCom = gameObject.GetComponent<ScarabCombat>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 pDistancenorm = transform.InverseTransformPoint(player.transform.position).normalized;

        anim.SetInteger("animState", currentState);
        //

        if (thiskCom.spriteSwitch)
        {
//            Debug.Log("switch on");

            currentState = thiskCom.currAttDir;
        }

 //       else if (!thiskCom.inRange && thiskCom.spriteSwitch == false)
          else
        {
            if (koboldRB.velocity.y > .2 && Mathf.Abs(koboldRB.velocity.x) <= Mathf.Abs(koboldRB.velocity.y) / 2)
            {
                runState = 1;
                currentState = runState;
                idleState = 9;

            }

            else if (koboldRB.velocity.x < .2 && Mathf.Abs(koboldRB.velocity.y) <= Mathf.Abs(koboldRB.velocity.x) / 2)
            {
                runState = 7;
                currentState = runState;
                idleState = 15;
            }

            else if (koboldRB.velocity.y < .2 && Mathf.Abs(koboldRB.velocity.x) <= Mathf.Abs(koboldRB.velocity.y) / 2)
            {
                runState = 5;
                currentState = runState;
                idleState = 13;

            }

            else if (koboldRB.velocity.x > .2 && Mathf.Abs(koboldRB.velocity.y) <= Mathf.Abs(koboldRB.velocity.x) / 2)
            {
                runState = 3;
                currentState = runState;
                idleState = 11;
            }

            else if (koboldRB.velocity.x < .2 && koboldRB.velocity.y > .1)
            {
                runState = 8;
                currentState = runState;
                idleState = 16;

            }

            else if (koboldRB.velocity.x > 0.2 && koboldRB.velocity.y > 0.2)
            {
                runState = 2;
                currentState = runState;
                idleState = 10;
            }

            else if (koboldRB.velocity.x < 0.2 && koboldRB.velocity.y < 0.2)
            {
                runState = 6;
                currentState = runState;
                idleState = 16;
            }

            else if (koboldRB.velocity.x > 0.2 && koboldRB.velocity.y < 0.2)
            {
                runState = 4;
                currentState = runState;
                idleState = 12;
            }
            else
            {
                currentState = idleState;
            }
        }

//        else if (thiskCom.inRange && thiskCom.spriteSwitch == false)
//        {
//            if (pDistancenorm.y > 0 && Mathf.Abs(pDistancenorm.x) < .25f)
//            {
//       //         Debug.Log("inRange look up" + pDistancenorm);
//
//                runState = 1;
//                currentState = runState;
//                idleState = 9;
//
//            }
//
//            else if (pDistancenorm.x < 0 && Mathf.Abs(pDistancenorm.y) < .25f)
//            {
//      //          Debug.Log("inRange look left" + pDistancenorm);
//
//                runState = 7;
//                currentState = runState;
//                idleState = 15;
//            }
//
//            else if (pDistancenorm.y < 0 && Mathf.Abs(pDistancenorm.x) < .25f)
//            {
//     //           Debug.Log("inRange look down" + pDistancenorm);
//
//                runState = 5;
//                currentState = runState;
//                idleState = 13;
//
//            }
//
//            else if (pDistancenorm.x > 0 && Mathf.Abs(pDistancenorm.y) < .25f)
//            {
//    //            Debug.Log("inRange look right" + pDistancenorm);
//
//                runState = 3;
//                currentState = runState;
//                idleState = 11;
//            }
//
//            else if (pDistancenorm.y > 0 && pDistancenorm.x < -.25f)
//            {
////                Debug.Log("inRange look up left" + pDistancenorm);
//            
//                runState = 8;
//                currentState = runState;
//                idleState = 16;
//            
//            }
//            
//            else if (pDistancenorm.y > 0 && pDistancenorm.x > .25f)
//            {
////                Debug.Log("inRange look up right" + pDistancenorm);
//            
//                runState = 2;
//                currentState = runState;
//                idleState = 10;
//            }
//            
//            else if (pDistancenorm.y < 0 && pDistancenorm.x < -.25f)
//            {
////                Debug.Log("inRange look down left" + pDistancenorm);
//            
//                runState = 6;
//                currentState = runState;
//                idleState = 16;
//            }
//            
//            else if (pDistancenorm.y < 0 && pDistancenorm.x > .25f)
//            {
// //               Debug.Log("inRange look down right" + pDistancenorm);
//            
//                runState = 4;
//                currentState = runState;
//                idleState = 12;
//            }
//            else
//            {
//                currentState = idleState;
//            }
//        }


        //          facedirection setting

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
