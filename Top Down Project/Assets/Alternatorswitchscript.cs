using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alternatorswitchscript : Switchbase
{
    public GameObject player;
    public Rigidbody2D pRB;

    public Collider2D col;

    public SpriteRenderer rend;

    public Sprite offsprite;
    public Sprite onsprite;

    public bool isOn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapons")
        {
            isOn = !isOn;
            pRB.AddForce(transform.InverseTransformPoint(player.transform.position) * 5, ForceMode2D.Impulse);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        pRB = player.GetComponent<Rigidbody2D>();

        rend.sprite = offsprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            rend.sprite = onsprite;
        }

        else if (!isOn)
        {
            rend.sprite = offsprite;
        }
    }
}