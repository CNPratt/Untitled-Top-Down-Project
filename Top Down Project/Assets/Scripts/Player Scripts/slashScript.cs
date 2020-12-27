using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashScript : MonoBehaviour
{
    public int animState;
    public float knockBack;
    public Vector3 enPos;
    public Transform playerTrans;

    //disabler from animevent on slashanim

    public void SlashDisable(int i)
    {
        if (i == 1)
        {
//            Debug.Log("slashdis called");

            gameObject.SetActive(false);
        }
    }

    //hit effect

    private void OnTriggerEnter2D(Collider2D collider)
    {
        knockBack = 25f;

        if (collider.tag == "Enemy")
        {
            playerTrans = GameObject.Find("Player").GetComponent<Transform>();
            enPos = playerTrans.InverseTransformPoint(collider.transform.position);


            IEnumerator hitMethod()
            {
                Rigidbody2D thisenRB = collider.GetComponent<Rigidbody2D>();

    //            Debug.Log("hit called, force: " + enPos * knockBack + knockBack);

                playerTrans.InverseTransformPoint(collider.transform.position);
                thisenRB.AddForce(enPos * knockBack, ForceMode2D.Impulse);


                yield return null;
            }

            StartCoroutine(hitMethod());
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
