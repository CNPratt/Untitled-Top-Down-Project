using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Pathfinding;

public class DronePath : MonoBehaviour
{
    public static bool animswitch;

    public BoxCollider2D handlerCol;

    //public CapsuleCollider2D handlerCol;
    public SpriteRenderer rend;
    public bool ignorePlayer;
    public bool gotHitSwitch;
    public bool gotHit;
    public GameObject thisEnemy;
    public Transform target;
    public float speed = 50f;
    public float nextWaypointDistance = 3;

    Path path;
    int currentWaypoint = 0;
    public bool reachedEndofPath = false;

    Seeker seeker;
    Rigidbody2D droneRB;

//    IEnumerator GotHit()
 //   {
//        Debug.Log("Gothit engaged");

//        rend.color = Color.white;
//        yield return new WaitForSeconds(.1f);
//        rend.color = Color.clear;
//        yield return new WaitForSeconds(.1f);
//        rend.color = Color.white;
//        yield return new WaitForSeconds(.1f);
//        rend.color = Color.clear;
//        yield return new WaitForSeconds(.1f);
//        rend.color = Color.white;
 //       yield return new WaitForSeconds(.1f);

 //       gotHit = false;
//    }

    // Start is called before the first frame update
    void Start()
    {

        handlerCol = gameObject.GetComponent<BoxCollider2D>();

        target = GameObject.Find("Player").transform;

  //      Physics2D.IgnoreCollision(target.GetComponent<CapsuleCollider2D>(), gameObject.GetComponent<CapsuleCollider2D>());

        seeker = GetComponent<Seeker>();
        droneRB = GetComponent<Rigidbody2D>();


        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(droneRB.position, target.position, OnPathComplete);
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

 //       if (gotHit == true && gotHitSwitch == true)
 //       {
 //           StartCoroutine(GotHit());
            gotHitSwitch = false;
  //      }

  //      if (gotHit == true)
  //      {
  //          droneRB.velocity = new Vector2(0, 0);
  //          droneRB.AddForce((transform.position - target.transform.position).normalized * 50f, ForceMode2D.Force);
  //      }

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

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - droneRB.position).normalized;
        Vector2 force = direction * speed;
        //
        float targDist = Vector2.Distance(droneRB.position, target.position);
        float awareDistance = 4f;
 //       Debug.Log(targDist);
        //
        if (Vector2.Distance(droneRB.position, target.position) < awareDistance)
        {
            droneRB.AddForce(force * 15f, ForceMode2D.Force);
        }
        //

        float distance = Vector2.Distance(droneRB.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
