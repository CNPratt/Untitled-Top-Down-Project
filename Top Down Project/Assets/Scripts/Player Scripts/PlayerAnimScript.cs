using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimScript : MonoBehaviour
{
    public SpriteRenderer rend;

    public Animator anim;
    public static Vector3 faceRoto;
    public static Vector3 faceDirection;

    public static int phaseState;
    public static int attackState;
    public static int idleState;
    public int currentState;
    public static int runState;
    public static int bpState;


    void Update()
    {
        anim.SetInteger("animState", currentState);

        //      anim state setter

        if (!PlayerController.beenHit && !WeaponController.slashCDOn)
        {

            if (Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("a"))
            {
                if (PlayerController.backpedal == false)
                {
                    runState = 1;
                    phaseState = 25;
                    idleState = 9;
                }

                else if (PlayerController.backpedal == true)
                {
                    bpState = 5;
                    idleState = 13;
                }
            }

            else if (Input.GetKey("a") && !Input.GetKey("w") && !Input.GetKey("s"))
            {
                if (PlayerController.backpedal == false)
                {
                    phaseState = 31;
                    runState = 7;
                    idleState = 15;
                }


                else if (PlayerController.backpedal == true)
                {
                    bpState = 3;
                    idleState = 11;
                }
            }

            else if (Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d"))
            {
                if (PlayerController.backpedal == false)
                {
                    phaseState = 29;
                    runState = 5;
                    idleState = 13;
                }

                else if (PlayerController.backpedal == true)
                {
                    bpState = 1;
                    idleState = 9;
                }
            }

            else if (Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("s"))
            {
                if (PlayerController.backpedal == false)
                {
                    phaseState = 27;
                    runState = 3;
                    idleState = 11;
                }

                else if (PlayerController.backpedal == true)
                {
                    bpState = 7;
                    idleState = 15;
                }
            }

            else if (Input.GetKey("w") && Input.GetKey("a"))
            {
                if (PlayerController.backpedal == false)
                {
                    phaseState = 32;
                    runState = 8;
                    idleState = 16;
                }

                else if (PlayerController.backpedal == true)
                {
                    bpState = 4;
                    idleState = 12;
                }
            }

            else if (Input.GetKey("w") && Input.GetKey("d"))
            {
                if (PlayerController.backpedal == false)
                {
                    phaseState = 26;
                    runState = 2;
                    idleState = 10;
                }

                else if (PlayerController.backpedal == true)
                {
                    bpState = 6;
                    idleState = 14;
                }
            }

            else if (Input.GetKey("s") && Input.GetKey("a"))
            {
                if (PlayerController.backpedal == false)
                {
                    phaseState = 30;
                    runState = 6;
                    idleState = 14;
                }

                else if (PlayerController.backpedal == true)
                {
                    bpState = 2;
                    idleState = 10;
                }
            }

            else if (Input.GetKey("s") && Input.GetKey("d"))
            {
                if (PlayerController.backpedal == false)
                {
                    phaseState = 28;
                    runState = 4;
                    idleState = 12;
                }

                else if (PlayerController.backpedal == true)
                {
                    bpState = 8;
                    idleState = 16;
                }
            }
        }

//      currentState conditions

        if (!WeaponController.slashCDOn && !PlayerController.beenHit && !DashScript.dashEffectOn && !PlayerController.backpedal && (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")))
        {
            currentState = runState;
        }

        else if (DashScript.dashEffectOn && !PlayerController.beenHit)
        {
            currentState = phaseState;
        }

        else if (WeaponController.slashCDOn && !DashScript.dashEffectOn && !PlayerController.beenHit)
        {
            currentState = attackState;
        }

        else if (!Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("s") && !Input.GetKey("d"))
        {
            currentState = idleState;
        }

        else if(PlayerController.backpedal)
        {
            currentState = bpState;
        }


        //         set facedirection and roto

        if (currentState == 5 || currentState == 13 || currentState == 0)
        {
            attackState = 21;
            faceDirection = Vector2.down;
            faceRoto = new Vector3(0, 0, 0);
        }

        else if (currentState == 1 || currentState == 9)
        {
            attackState = 17;
            faceDirection = Vector2.up;
            faceRoto = new Vector3(0, 0, 180);
        }

        else if (currentState == 7 || currentState == 15)
        {
            attackState = 23;
            faceDirection = Vector2.left;
            faceRoto = new Vector3(0, 0, -90);
        }

        else if (currentState == 3 || currentState == 11)
        {
            attackState = 19;
            faceDirection = Vector2.right;
            faceRoto = new Vector3(0, 0, 90);
        }

        else if (currentState == 4 || currentState == 12)
        {
            attackState = 20;
            faceDirection = (Vector2.down + Vector2.right);
            faceRoto = new Vector3(0, 0, 45);
        }

        else if (currentState == 6 || currentState == 14)
        {
            attackState = 22;
            faceDirection = (Vector2.down + Vector2.left);
            faceRoto = new Vector3(0, 0, -45);
        }

        else if (currentState == 2 || currentState == 10)
        {
            attackState = 18;
            faceDirection = (Vector2.up + Vector2.right);
            faceRoto = new Vector3(0, 0, 135);
        }

        else if (currentState == 8 || currentState == 16)
        {
            attackState = 24;
            faceDirection = (Vector2.up + Vector2.left);
            faceRoto = new Vector3(0, 0, -135);
        }
    }
}
