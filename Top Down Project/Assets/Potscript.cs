using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potscript : ThrowableMaster
{
    public GameObject loot;

    public Transform ogParent;

    public Sprite ogSprite;
    public Vector2 startPos;

    public bool breakonHit;

    public Color color;
    public SpriteRenderer rend;

    public RecoilConveyorScript recoilStats;

    public enHitdetector enDetector;
    public EnCombatMono hitAI;

    public bool canDamage;

    public Animator anim;

    public float objMass;
    public float objDrag;

    public float speedBefore;
    public GameObject player;
    public CapsuleCollider2D pCol;
    public Rigidbody2D bombRB;
    public CircleCollider2D bombCol;
    public CompositeCollider2D tCol;
    public Vector3 throwPoint;
//    public bool isHeld;
    public bool hasbeenSwitched;
    public bool isIgnoringPlayer;
    //    public bool canpickUp;

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
        canpickUp = true;
        isIgnoringPlayer = false;
        bombCol.enabled = true;
        rend.sprite = ogSprite;
        transform.position = startPos;
        color.a = 1;
        anim.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "Main Tilemap" || collision.collider.name == "Overlay Tilemap" || collision.collider.name == "Room Separator TM")
        {
            Break();
        }

        if(collision.collider.tag == "Enemy Handler" && canDamage)
        {
            Debug.Log("called");

            hitAI = collision.collider.GetComponent<EnCombatMono>();

            enDetector = collision.collider.GetComponentInChildren<enHitdetector>();

            enDetector.damage = recoilStats.damage;
            enDetector.kbPower = recoilStats.kbPower;
            enDetector.invTime = recoilStats.invTime;

            hitAI.gotHit = true;
            hitAI.gotHitSwitch = true;

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
        canDamage = false;

        bombCol.enabled = false;

        DropLoot(loot);

        bombRB.velocity = Vector2.zero;

        bombRB.isKinematic = true;

        anim.enabled = true;
    }

    public IEnumerator ThrowBreak()
    {
        yield return new WaitForSeconds(.5f);

        Invoke("Break", 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(13, 16);

        rend = GetComponent<SpriteRenderer>();
        color = rend.color;

        bombRB.isKinematic = true;

        tCol = GameObject.Find("Main Tilemap").GetComponent<CompositeCollider2D>();

        canpickUp = true;
        player = GameObject.Find("Player");
        pCol = GameObject.Find("Player").GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.enabled == true)
        {
            color.a = color.a - .05f;
        }

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
            PlayerController.moveSpeed = .33f;
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

        if (isHeld == true && Input.GetKeyDown(KeyCode.O) && PlayerController.okeyUp)
        {
            Physics2D.IgnoreLayerCollision(13, 13);

            canDamage = true;

            PlayerController.okeyUp = false;
            PlayerController.thisOKEY = StartCoroutine(PlayerController.OKeyUp());

            PlayerController.isHolding = false;

            isHeld = false;

            if (transform.parent.gameObject != null)
            {
                gameObject.transform.parent = ogParent;
            }
            else
            {
                gameObject.transform.parent = null;
            }

            bombRB.isKinematic = false;

            bombRB.AddForce(PlayerAnimScript.faceDirection * 10, ForceMode2D.Impulse);

            bombRB.gravityScale = .3f;

            if (hasbeenSwitched == true)
            {
                hasbeenSwitched = false;
                PlayerController.moveSpeed = 1;
            }

            bombCol.enabled = true;

            //           canDamage = false;

            StartCoroutine("ThrowBreak");
        }
    }
}