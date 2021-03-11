using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stepswitchscript : Switchbase
{
    public Collider2D col;

    public SpriteRenderer rend;

    public Sprite offsprite;
    public Sprite onsprite;

    public bool isOn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            isOn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            isOn = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rend.sprite = offsprite;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn)
        {
            rend.sprite = onsprite;
        }

        else if(!isOn)
        {
            rend.sprite = offsprite;
        }
    }
}
