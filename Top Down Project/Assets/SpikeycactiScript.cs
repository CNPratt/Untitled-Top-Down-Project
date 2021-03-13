using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeycactiScript : ThrowableMaster
{
    public GameObject loot;

    public Transform ogParent;

    public Sprite ogSprite;
    public Vector2 startPos;

    public bool breakonHit;

    public Color color;
    public SpriteRenderer rend;

    public bool canDamage;

    public Animator anim;

    public Rigidbody2D bombRB;
    public CircleCollider2D bombCol;
    public CompositeCollider2D tCol;

    public void DropLoot(GameObject loot)
    {
        Instantiate(loot, transform.position, Quaternion.Euler(Vector3.zero));
    }

    private void Awake()
    {
        if (transform.parent != null)
        {
            ogParent = transform.parent;
        }

        ogSprite = rend.sprite;
        startPos = transform.position;
    }

    private void OnEnable()
    {
        bombCol.enabled = true;
        rend.sprite = ogSprite;
        transform.position = startPos;
        color.a = 1;
        anim.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Main Tilemap" || collision.collider.name == "Overlay Tilemap" || collision.collider.name == "Room Separator TM")
        {
            Break();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapons" && breakonHit == true)
        {

            Break();
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Break()
    {
        bombCol.enabled = false;

        DropLoot(loot);

        anim.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(13, 16);

        rend = GetComponent<SpriteRenderer>();
        color = rend.color;

        bombRB.isKinematic = true;
        tCol = GameObject.Find("Main Tilemap").GetComponent<CompositeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.enabled == true)
        {
            color.a = color.a - .05f;
        }
    }
}