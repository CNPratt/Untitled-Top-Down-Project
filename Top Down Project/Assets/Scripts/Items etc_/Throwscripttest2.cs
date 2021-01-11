﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Throwscripttest2 : MonoBehaviour
{
    // MODULER MASS BUT LINEAR DRAG = 2

    public float objMass;
    public float objDrag;

    public float speedBefore;
    public GameObject player;
    public CapsuleCollider2D pCol;
    public Rigidbody2D bombRB;
    public CircleCollider2D bombCol;
    public CompositeCollider2D tCol; 
    public Vector3 throwPoint;
    public bool isHeld;
    public bool hasbeenSwitched;
    public bool isIgnoringPlayer;
    public bool canpickUp;

    private void OnCollisionStay2D(Collision2D collision)
    {
//        Debug.Log("collision detected");

        if(canpickUp== true && (collision.collider.name == "Player" && Input.GetKey(KeyCode.O) && PlayerController.isHolding == false))
        {
            PlayerController.isHolding = true;

            canpickUp = false;
            isHeld = true;
            gameObject.transform.SetParent(player.transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        tCol = GameObject.Find("Tilemap").GetComponent<CompositeCollider2D>();

        canpickUp = true;
        player = GameObject.Find("Player");
        pCol = GameObject.Find("Player").GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        objMass = bombRB.mass;
        objDrag = bombRB.drag;

        if (isIgnoringPlayer == true)
        {
            Physics2D.IgnoreCollision(bombCol, pCol);
        }

        else if (isIgnoringPlayer == false)
        {
            Physics2D.IgnoreCollision(bombCol, pCol, false);
        }

        if (isHeld == true && hasbeenSwitched == false)
        {
            PlayerController.moveSpeed = PlayerController.moveSpeed / 3;
            hasbeenSwitched = true;
        }

        if (isHeld == true)
        {
            Physics2D.IgnoreCollision(bombCol, pCol);

            bombRB.velocity = new Vector3(0, 0, 0);

            isIgnoringPlayer = true;

            bombCol.enabled = false;
            bombRB.isKinematic = true;
            gameObject.transform.localPosition = new Vector2(0, .3f);
        }

        if (isHeld == true && Input.GetKeyDown(KeyCode.I))
        {
            PlayerController.isHolding = false;

            isHeld = false;

//            throwPoint = gameObject.transform.TransformPoint(gameObject.transform.localPosition);
            
            gameObject.transform.parent = null;

            bombRB.isKinematic = false;

            bombRB.AddForce(PlayerAnimScript.faceDirection * 10, ForceMode2D.Impulse);

            bombRB.gravityScale = .3f;

            if (hasbeenSwitched == true)
            {
                hasbeenSwitched = false;
                PlayerController.moveSpeed = PlayerController.moveSpeed * 3;
            }

            //            bombCol.enabled = true;

            StartCoroutine(skipMethod());


        }

        IEnumerator skipMethod()
        {

            bombCol.enabled = true;

            speedBefore = PlayerController.moveSpeed;

            PlayerController.moveSpeed = 0;

            yield return new WaitForSeconds(.2f);

            PlayerController.moveSpeed = speedBefore;

            yield return new WaitForSeconds(.2f);

            isIgnoringPlayer = false;

            yield return new WaitForSeconds(.1f);

            bombRB.gravityScale = 0;
            bombRB.AddForce(Vector3.up * (1.5f * objMass), ForceMode2D.Impulse);

            yield return new WaitForSeconds(.2f);

            bombRB.gravityScale = .3f;

            yield return new WaitForSeconds(.5f);

            bombRB.gravityScale = 0;
            bombRB.AddForce(Vector3.up * (1 * objMass), ForceMode2D.Impulse);

            yield return new WaitForSeconds(.2f);

            bombRB.gravityScale = .3f;

            yield return new WaitForSeconds(.2f);

            bombRB.gravityScale = 0;
            bombRB.AddForce(Vector3.up * (.6667f * objMass), ForceMode2D.Impulse);

            yield return new WaitForSeconds(.2f);

            bombRB.gravityScale = .3f;

            yield return new WaitForSeconds(.2f);

            bombRB.gravityScale = 0;
            bombRB.velocity = new Vector2(0, 0);
            canpickUp = true;

            yield return new WaitForSeconds(.2f);
        }


    }
}