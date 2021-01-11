using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Pathfinding;

public class KoboldPath : MonoBehaviour
{
    public static bool animswitch;

    public BoxCollider2D handlerCol;

    public SpriteRenderer rend;
    public bool ignorePlayer;
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

    // Start is called before the first frame update
    void Start()
    {

        handlerCol = gameObject.GetComponent<BoxCollider2D>();

        target = GameObject.Find("Player").transform;

        seeker = GetComponent<Seeker>();
        koboldRB = GetComponent<Rigidbody2D>();


        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(koboldRB.position, target.position, OnPathComplete);
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
    void Update()
    {
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

    //    Vector2 directionX = new Vector2(path.vectorPath[currentWaypoint].x - koboldRB.position.x, 0f).normalized;
    //    Vector2 directionY = new Vector2(0f, path.vectorPath[currentWaypoint].y - koboldRB.position.y).normalized;
   //     Vector2 normDir = new Vector2(directionX.x, directionY.y);

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - koboldRB.position).normalized;
        Vector2 force = direction * speed;
        //
        float targDist = Vector2.Distance(koboldRB.position, target.position);
        float awareDistance = 5f;

        if (targDist < awareDistance)
        {
            //            Debug.Log("Direction: " + direction);
            //            Debug.Log("Force: " + force);
//                        Debug.Log("Distance to Player: " + Vector2.Distance(koboldRB.position, target.position));

            koboldRB.AddForce(force, ForceMode2D.Force);
        }
        //

        float distance = Vector2.Distance(koboldRB.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
