using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwscripttest : MonoBehaviour
{
    public float speedBefore;
    public GameObject player;
    public CapsuleCollider2D pCol;
    public Rigidbody2D bombRB;
    public CircleCollider2D bombCol;
    public Vector3 throwPoint;
    public bool isHeld;
    public bool hasbeenSwitched;
    public bool isIgnoringPlayer;
    public bool canpickUp;

    private void OnCollisionStay2D(Collision2D collision)
    {
//        Debug.Log("collision detected");

        if(canpickUp== true && (collision.collider.name == "Player" && Input.GetKey(KeyCode.O)))
        {
            canpickUp = false;
            isHeld = true;
            gameObject.transform.SetParent(player.transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        canpickUp = true;
        player = GameObject.Find("Player");
        pCol = GameObject.Find("Player").GetComponent<CapsuleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
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
            isIgnoringPlayer = true;

            bombCol.enabled = false;
            bombRB.isKinematic = true;
            gameObject.transform.localPosition = new Vector2(0, .3f);
        }

        if (isHeld == true && Input.GetKeyDown(KeyCode.I))
        {
            isHeld = false;

//            throwPoint = gameObject.transform.TransformPoint(gameObject.transform.localPosition);

            gameObject.transform.parent = null;

            bombRB.isKinematic = false;

            if (PlayerController.faceDirection == Vector3.down || PlayerController.faceDirection == Vector3.up || PlayerController.faceDirection == Vector3.right || PlayerController.faceDirection == Vector3.left)
            {
                bombRB.AddForce(PlayerController.faceDirection * 200, ForceMode2D.Impulse);
            }

            else if (PlayerController.faceDirection == (Vector3.down + Vector3.right) / 2 || PlayerController.faceDirection == (Vector3.down + Vector3.left) / 2 || PlayerController.faceDirection == (Vector3.up + Vector3.right) / 2 || PlayerController.faceDirection == (Vector3.up + Vector3.left) / 2)
            {
                bombRB.AddForce(PlayerController.faceDirection * 400, ForceMode2D.Impulse);
            }

       //     bombRB.AddForce(PlayerController.faceDirection * 200, ForceMode2D.Impulse);

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
            bombRB.AddForce(Vector3.up * 45, ForceMode2D.Impulse);

            yield return new WaitForSeconds(.2f);

            bombRB.gravityScale = .3f;

            yield return new WaitForSeconds(.5f);

            bombRB.gravityScale = 0;
            bombRB.AddForce(Vector3.up * 30, ForceMode2D.Impulse);

            yield return new WaitForSeconds(.2f);

            bombRB.gravityScale = .3f;

            yield return new WaitForSeconds(.3f);

            bombRB.gravityScale = 0;
            bombRB.AddForce(Vector3.up * 20, ForceMode2D.Impulse);

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
