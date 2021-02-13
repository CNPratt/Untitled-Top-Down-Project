using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonshotScript : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D thisRB;
    public Vector3 dest;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" || collision.name == "Main Tilemap")
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        dest = player.transform.position - transform.position;
        thisRB = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        thisRB.velocity = dest.normalized * 5;

//        transform.Rotate(0, 0, 20);

    }
}
