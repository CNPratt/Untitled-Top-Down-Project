using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SmallChestScript : MonoBehaviour
{
    public Sprite openSprite;
    public SpriteRenderer rend;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.name == "Player" && Input.GetKeyDown(KeyCode.O) && PlayerController.handsFree)
        {
            rend.sprite = openSprite;
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
