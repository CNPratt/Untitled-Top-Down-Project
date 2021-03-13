using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Pathfinding;
using UnityEngine.Tilemaps;

public class MCactusPath : MonoBehaviour
{
    public Bounds penaltyBounds;
    public GraphUpdateObject graphBoundsUpdate;
    public GraphUpdateObject penaltyUpdateBounds;
    public bool canRoam;

    public bool pursuitMode;

    public List<Vector3> randomPointOnARandomNode;
    public GraphNode node;

    public Vector2 startPos;

    public MCactusCombat thiskCom;
    public bool updateSwitch;
    public static bool animswitch;

    public Vector3 thisNode;
    public Vector3 pathRandomizer;
    public CapsuleCollider2D handlerCol;

    public SpriteRenderer rend;
    public bool gotHitSwitch;
    public bool gotHit;
    public GameObject thisEnemy;
    public Transform target;
    public float speed = 2f;
    public float nextWaypointDistance = 1;

    Path path;
    int currentWaypoint = 0;
    public bool reachedEndofPath = false;

    Seeker seeker;
    Rigidbody2D koboldRB;

    private void OnEnable()
    {
        transform.position = startPos;
    }

    private void Awake()
    {
        startPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {

        //        Enemies = new List<GameObject>();
        //        Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy Handler"));
        //        Enemies.Remove(gameObject);

        thiskCom = gameObject.GetComponent<MCactusCombat>();

        handlerCol = gameObject.GetComponent<CapsuleCollider2D>();

        target = GameObject.Find("Player").transform;

        seeker = GetComponent<Seeker>();
        koboldRB = GetComponent<Rigidbody2D>();

        graphBoundsUpdate = new GraphUpdateObject(handlerCol.bounds);

        InvokeRepeating("UpdatePath", 0f, Random.Range(2f, 4f));

//        Invoke("UpdatePath", 0);

    }


    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if (thiskCom.inRange)
            {
                pursuitMode = true;

                pathRandomizer = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                seeker.StartPath(koboldRB.position, target.position, OnPathComplete);
                //                CancelInvoke();
                //                InvokeRepeating("UpdatePath", 0, Random.Range(1f, 2f));
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path != null)
        {
            //            Debug.Log(Vector2.Distance((Vector2)path.vectorPath[path.vectorPath.Count - 1], koboldRB.position));
        }

        if (path != null)
        {
            if (thiskCom.inRange && Vector2.Distance((Vector2)path.vectorPath[path.vectorPath.Count - 1], target.position) > 3)
            {
                Invoke("UpdatePath", 0);
            }
        }

        if (reachedEndofPath)
        {
            Invoke("UpdatePath", 0);
        }

        penaltyBounds = new Bounds(handlerCol.bounds.center, handlerCol.bounds.size * new Vector2(2, 2));

        penaltyUpdateBounds = new GraphUpdateObject(penaltyBounds);

        penaltyUpdateBounds.addPenalty = 15000;

//        AstarPath.active.UpdateGraphs(graphBoundsUpdate);
        AstarPath.active.UpdateGraphs(handlerCol.bounds);

        thisEnemy.transform.position = transform.position;

        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
            return;
        }
        else
        {
            reachedEndofPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - koboldRB.position).normalized;
        Vector2 force = direction * speed;
        //
        float targDist = Vector2.Distance(koboldRB.position, target.position);

        //        float awareDistance = 5f;

        if (thiskCom.inRange && !thiskCom.gotHit)
        {
            koboldRB.AddForce(force / 8, ForceMode2D.Force);
        }

        float distance = Vector2.Distance(koboldRB.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
