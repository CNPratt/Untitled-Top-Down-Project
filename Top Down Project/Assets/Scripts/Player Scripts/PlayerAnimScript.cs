using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimScript : MonoBehaviour
{
        public Animator anim;
        public static Vector3 faceRoto;
        public static Vector3 faceDirection;
        public static int idleState;
        public static int currentState;
        public static int runState;
        public static int bpState;

    void FixedUpdate()
    {
        anim.SetInteger("animState", currentState);

        //      anim state setter

        if (!PlayerController.beenHit)
        {
            if (Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("a"))
            {
                if (PlayerController.backpedal == false)
                {
                    runState = 1;
                    currentState = runState;
                    idleState = 9;
                }

                else if (PlayerController.backpedal == true)
                {
                    bpState = 5;
                    currentState = bpState;
                    idleState = 13;
                }
            }

            else if (Input.GetKey("a") && !Input.GetKey("w") && !Input.GetKey("s"))
            {
                if (PlayerController.backpedal == false)
                {
                    runState = 7;
                    currentState = runState;
                    idleState = 15;
                }


                else if (PlayerController.backpedal == true)
                {
                    bpState = 3;
                    currentState = bpState;
                    idleState = 11;
                }
            }

            else if (Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d"))
            {
                if (PlayerController.backpedal == false)
                {
                    runState = 5;
                    currentState = runState;
                    idleState = 13;
                }

                else if (PlayerController.backpedal == true)
                {
                    bpState = 1;
                    currentState = bpState;
                    idleState = 9;
                }
            }

            else if (Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("s"))
            {
                if (PlayerController.backpedal == false)
                {
                    runState = 3;
                    currentState = runState;
                    idleState = 11;
                }

                else if (PlayerController.backpedal == true)
                {
                    bpState = 7;
                    currentState = bpState;
                    idleState = 15;
                }
            }

            else if (Input.GetKey("w") && Input.GetKey("a"))
            {
                if (PlayerController.backpedal == false)
                {
                    runState = 8;
                    currentState = runState;
                    idleState = 16;
                }

                else if (PlayerController.backpedal == true)
                {
                    bpState = 4;
                    currentState = bpState;
                    idleState = 12;
                }
            }

            else if (Input.GetKey("w") && Input.GetKey("d"))
            {
                if (PlayerController.backpedal == false)
                {
                    runState = 2;
                    currentState = runState;
                    idleState = 10;
                }

                else if (PlayerController.backpedal == true)
                {
                    bpState = 6;
                    currentState = bpState;
                    idleState = 14;
                }
            }

            else if (Input.GetKey("s") && Input.GetKey("a"))
            {
                if (PlayerController.backpedal == false)
                {
                    runState = 6;
                    currentState = runState;
                    idleState = 14;
                }

                else if (PlayerController.backpedal == true)
                {
                    bpState = 2;
                    currentState = bpState;
                    idleState = 10;
                }
            }

            else if (Input.GetKey("s") && Input.GetKey("d"))
            {
                if (PlayerController.backpedal == false)
                {
                    runState = 4;
                    currentState = runState;
                    idleState = 12;
                }

                else if (PlayerController.backpedal == true)
                {
                    bpState = 8;
                    currentState = bpState;
                    idleState = 16;
                }
            }

            else if (!Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("s") && !Input.GetKey("d"))
            {
                currentState = idleState;
            }
        }

//         set facedirection and roto

        if (currentState == 5 || currentState == 13 || currentState == 0)
        {
            faceDirection = Vector2.down;
            faceRoto = new Vector3(0, 0, 0);
        }

        else if (currentState == 1 || currentState == 9)
        {
            faceDirection = Vector2.up;
            faceRoto = new Vector3(0, 0, 180);
        }

        else if (currentState == 7 || currentState == 15)
        {
            faceDirection = Vector2.left;
            faceRoto = new Vector3(0, 0, -90);
        }

        else if (currentState == 3 || currentState == 11)
        {
            faceDirection = Vector2.right;
            faceRoto = new Vector3(0, 0, 90);
        }

        else if (currentState == 4 || currentState == 12)
        {
            faceDirection = (Vector2.down + Vector2.right);
            faceRoto = new Vector3(0, 0, 45);
        }

        else if (currentState == 6 || currentState == 14)
        {
            faceDirection = (Vector2.down + Vector2.left);
            faceRoto = new Vector3(0, 0, -45);
        }

        else if (currentState == 2 || currentState == 10)
        {
            faceDirection = (Vector2.up + Vector2.right);
            faceRoto = new Vector3(0, 0, 135);
        }

        else if (currentState == 8 || currentState == 16)
        {
            faceDirection = (Vector2.up + Vector2.left);
            faceRoto = new Vector3(0, 0, -135);
        }
    }
}
