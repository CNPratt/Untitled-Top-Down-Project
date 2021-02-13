using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Pathfinding;
using UnityEngine.Tilemaps;

public class ScarabPath : MonoBehaviour
{
    public List<Vector3> randomPointOnARandomNode;
    public GraphNode node;

    public Vector2 startPos;

    public List<GameObject> Enemies;
    public ScarabCombat thiskCom;
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

        Enemies = new List<GameObject>();
        Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy Handler"));
        Enemies.Remove(gameObject);

        thiskCom = gameObject.GetComponent<ScarabCombat>();

        handlerCol = gameObject.GetComponent<CapsuleCollider2D>();

        target = GameObject.Find("Player").transform;

        seeker = GetComponent<Seeker>();
        koboldRB = GetComponent<Rigidbody2D>();


        InvokeRepeating("UpdatePath", 0f, Random.Range(1f, 2f));
    }


    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if (Vector2.Distance(koboldRB.position, target.position) < 5f && !thiskCom.isAttacking)
            {
                node = AstarPath.active.GetNearest(transform.position, NNConstraint.Default).node;

                if (node != null)

                {

                    var reachable = PathUtilities.GetReachableNodes(node);

                    randomPointOnARandomNode = PathUtilities.GetPointsOnNodes(reachable, 1);

                }

                //            pathRandomizer = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                //             seeker.StartPath(koboldRB.position, target.position - pathRandomizer, OnPathComplete);

 //               seeker.StartPath(koboldRB.position, koboldRB.transform.position - pathRandomizer * 5, OnPathComplete);

                seeker.StartPath(koboldRB.position, randomPointOnARandomNode[Random.Range(0, randomPointOnARandomNode.Count - 1)], OnPathComplete);

                    CancelInvoke();
                    InvokeRepeating("UpdatePath", 0, Random.Range(3f, 5f));
            }
            else if (Vector2.Distance(koboldRB.position, target.position) > 5f && !thiskCom.isAttacking)
            {
                node = AstarPath.active.GetNearest(transform.position, NNConstraint.Default).node;

                if (node != null)

                {

                    var reachable = PathUtilities.GetReachableNodes(node);

                    randomPointOnARandomNode = PathUtilities.GetPointsOnNodes(reachable, 1);

                }

                if (reachedEndofPath)
                {
                    seeker.StartPath(koboldRB.position, randomPointOnARandomNode[Random.Range(0, randomPointOnARandomNode.Count - 1)], OnPathComplete);
                }

                //                pathRandomizer = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                //                seeker.StartPath(koboldRB.position, (Vector3)AstarPath.active.GetNearest(koboldRB.position + ((Vector2)pathRandomizer * 4)), OnPathComplete);


                                CancelInvoke();
                                InvokeRepeating("UpdatePath", 0, Random.Range(3f, 5f));
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

        if (thiskCom.inRange != updateSwitch)
        {
            updateSwitch = thiskCom.inRange;

            if(thiskCom.inRange)
            {
                Invoke("UpdatePath", 0);
            }
        }

        foreach (GameObject enemy in Enemies)
        {
            //           Debug.Log(Mathf.Abs(transform.InverseTransformPoint(enemy.transform.position).x));
            //           Debug.Log(target.transform.position - enemy.transform.position);
            //           Debug.Log(enemy.transform.InverseTransformPoint(transform.position).normalized);

            if (enemy != null)
            {
                if (Mathf.Abs(transform.InverseTransformPoint(enemy.transform.position).x) < .5f || Mathf.Abs(transform.InverseTransformPoint(enemy.transform.position).y) < .5f)
                {
                    //           Debug.Log("spreader activated on " + gameObject);

                    koboldRB.AddForce(enemy.transform.InverseTransformPoint(transform.position).normalized, ForceMode2D.Force);
                }
            }
        }


        AstarPath.active.UpdateGraphs(handlerCol.bounds);

        thisEnemy.transform.position = transform.position;

        if(path == null)
        {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
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
        float awareDistance = 5f;

        if (targDist < awareDistance && !thiskCom.isAttacking && !thiskCom.gotHit)
        {
   //         koboldRB.velocity = new Vector2(0, 0);
//            koboldRB.AddForce(force/10, ForceMode2D.Force);
            koboldRB.AddForce(force/20, ForceMode2D.Force);

        }
        else if (targDist > awareDistance && !thiskCom.isAttacking && !thiskCom.gotHit)
        {
            koboldRB.AddForce(force/20, ForceMode2D.Force);
        }

            float distance = Vector2.Distance(koboldRB.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
