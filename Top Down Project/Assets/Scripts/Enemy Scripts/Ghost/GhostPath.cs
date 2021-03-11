using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Pathfinding;
using UnityEngine.Tilemaps;

public class GhostPath : MonoBehaviour
{
    public Vector2 detachedTarget;

    public bool gseekEndReached;

    public int randomOpenSpot;
    public bool pursuitMode;

    public List<Vector3> randomPointOnARandomNode;
    public GraphNode node;

    public Vector2 startPos;

    public List<GameObject> Enemies;
    public GhostCombat thiskCom;
    public bool updateSwitch;
    public static bool animswitch;

    public Vector3 thisNode;
    public Vector3 pathRandomizer;
    public CapsuleCollider2D handlerCol;

    public CapsuleCollider2D triggerCollider;

    public SpriteRenderer rend;
    public bool gotHitSwitch;
    public bool gotHit;
    public GameObject thisEnemy;
    public Transform player;
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

        Enemies = new List<GameObject>();
        Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy Handler"));
        Enemies.Remove(gameObject);

        thiskCom = gameObject.GetComponent<GhostCombat>();

        handlerCol = gameObject.GetComponent<CapsuleCollider2D>();

        player = GameObject.Find("Player").transform;
        target = player;

        seeker = GetComponent<Seeker>();
        koboldRB = GetComponent<Rigidbody2D>();


        //        InvokeRepeating("UpdatePath", 0f, Random.Range(1f, 2f));

        Invoke("UpdatePath", 0);

    }

//          HAD PROBLEMS WITH GETREACHABLENODES CAUSING MASSIVE CPU SPIKES = THIS SOLUTION MAY CAUSE GHOST TO SELECT A NODEPATH TO A POINT OFFSCREEN OR OUT OF CURRENT ROOM BUT IT IS UNTESTED

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if (thiskCom.inRange && !thiskCom.isAttacking)
            {
                pursuitMode = true;

                pathRandomizer = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

                if (thiskCom.isSeeking)
                {
                    seeker.StartPath(koboldRB.position, target.position, OnPathComplete);
                }
                else if (!thiskCom.isSeeking)
                {
                    node = AstarPath.active.GetNearest(transform.position, NNConstraint.Default).node;

                    if (node != null)

                    {

//                        var reachable = PathUtilities.GetReachableNodes(node);

//                        randomPointOnARandomNode = PathUtilities.GetPointsOnNodes(reachable, 1);

                    }

 //                   seeker.StartPath(koboldRB.position, randomPointOnARandomNode[Random.Range(0, randomPointOnARandomNode.Count - 1)], OnPathComplete);

                    seeker.StartPath(koboldRB.position, AstarPath.active.GetNearest((Vector2)transform.position + Random.insideUnitCircle * 10).position, OnPathComplete);
                }

                //                CancelInvoke();
                //                InvokeRepeating("UpdatePath", 0, Random.Range(1f, 2f));
            }
            else if (!thiskCom.inRange && !thiskCom.isAttacking)
            {
                pursuitMode = false;

                node = AstarPath.active.GetNearest(transform.position, NNConstraint.Default).node;

                if (node != null)

                {

 //                   var reachable = PathUtilities.GetReachableNodes(node);

//                    randomPointOnARandomNode = PathUtilities.GetPointsOnNodes(reachable, 1);

                }

//                seeker.StartPath(koboldRB.position, randomPointOnARandomNode[Random.Range(0, randomPointOnARandomNode.Count - 1)], OnPathComplete);

                seeker.StartPath(koboldRB.position, AstarPath.active.GetNearest((Vector2)transform.position + Random.insideUnitCircle * 10).position, OnPathComplete);
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
        if(thiskCom.isSeeking && Vector2.Distance(transform.position, target.position) < .5f)
        {
            gseekEndReached = true;
        }
//        else
//        {
//            gseekEndReached = false;
//        }

        if (path != null)
        {
            //            Debug.Log(Vector2.Distance((Vector2)path.vectorPath[path.vectorPath.Count - 1], koboldRB.position));
        }

 //       if (path != null)
 //       {
 //           if (thiskCom.inRange && !thiskCom.isAttacking && Vector2.Distance((Vector2)path.vectorPath[path.vectorPath.Count - 1], target.position) > 3)
 //           {
 //               Invoke("UpdatePath", 0);
 //           }
 //       }

        if (reachedEndofPath)
        {
            Invoke("UpdatePath", 0);
        }


 //       foreach (GameObject enemy in Enemies)
 //       {
 //           //           Debug.Log(Mathf.Abs(transform.InverseTransformPoint(enemy.transform.position).x));
 //           //           Debug.Log(target.transform.position - enemy.transform.position);
 //           //           Debug.Log(enemy.transform.InverseTransformPoint(transform.position).normalized);
 //
 //           if (enemy != null)
 //           {
 //               if (Mathf.Abs(transform.InverseTransformPoint(enemy.transform.position).x) < .5f || Mathf.Abs(transform.InverseTransformPoint(enemy.transform.position).y) < .5f)
 //               {
 //                   //           Debug.Log("spreader activated on " + gameObject);
 //
 //                   koboldRB.AddForce(enemy.transform.InverseTransformPoint(transform.position).normalized, ForceMode2D.Force);
 //               }
 //           }
 //       }


//        AstarPath.active.UpdateGraphs(handlerCol.bounds);

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

        if (thiskCom.inRange && !thiskCom.isAttacking && !thiskCom.gotHit && !gseekEndReached)
        {
            if (!thiskCom.isSeeking)
            {
                koboldRB.AddForce(force / 8, ForceMode2D.Force);
            }

            else if (thiskCom.isSeeking)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, 0.25f);

  //              koboldRB.AddForce(direction * 20, ForceMode2D.Force);
            }
        }
        else if (!thiskCom.inRange && !thiskCom.isAttacking && !thiskCom.gotHit && !gseekEndReached)
        {
           koboldRB.AddForce(direction, ForceMode2D.Force);

        }

        else if(gseekEndReached)
        {
//            koboldRB.velocity = Vector2.zero;
//            transform.position  = SlotManager.Slots[randomOpenSpot].transform.position;

            transform.position = Vector3.MoveTowards(transform.position, target.position, 1f * Time.deltaTime * 50);

            detachedTarget = target.position;

//            transform.position = target.position;
        }

        if (thiskCom.isExecuting && !gseekEndReached && !thiskCom.isReturning)
        {
//            Debug.Log("detached");

            koboldRB.velocity = Vector2.zero;
            transform.position = Vector3.MoveTowards(transform.position, detachedTarget, 1f * Time.deltaTime * 50);
        }

        if (thiskCom.isExecuting && !gseekEndReached && thiskCom.isReturning)
        {
//                        Debug.Log("returning");

            koboldRB.velocity = Vector2.zero;
            transform.position = Vector3.MoveTowards(transform.position, thiskCom.seekstartPos, 1f * Time.deltaTime * 50);
        }

        float distance = Vector2.Distance(koboldRB.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
