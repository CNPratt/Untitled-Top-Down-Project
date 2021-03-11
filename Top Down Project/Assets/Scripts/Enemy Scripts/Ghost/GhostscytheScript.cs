using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostscytheScript : MonoBehaviour
{

    public GhostPath gPath;

    public Coroutine thisSlash;
    public float zRoto1;
    public float zRoto2;

    public Quaternion startRoto;
    public Quaternion endRoto;

    public bool isSlashing;
    public bool midReached;
    public bool hasReachedEnd;

    public GameObject scytheEnd;
    public GameObject scytheMid;
    public GameObject scytheStart;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            gPath.gseekEndReached = false;
        }
    }

    IEnumerator ScytheSlash()
    {
//        Debug.Log("called");

        transform.position = scytheStart.transform.position;

        yield return new WaitForSeconds(1f);

        isSlashing = true;

//        transform.rotation = scytheMid.transform.rotation;

        yield return new WaitUntil(() => midReached);

        transform.rotation = scytheMid.transform.rotation;

//        transform.rotation = scytheEnd.transform.rotation;

        yield return new WaitUntil(() => hasReachedEnd);

        transform.rotation = scytheEnd.transform.rotation;

        yield return new WaitForSeconds(.5f);

        isSlashing = false;

        transform.rotation = scytheStart.transform.rotation;

        transform.position = scytheStart.transform.position;

        midReached = false;
        hasReachedEnd = false;

        thisSlash = null;

        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        thisSlash = StartCoroutine("ScytheSlash");
    }

    // Start is called before the first frame update
    void Start()
    {
        gPath = gameObject.GetComponentInParent<GhostPath>();

        startRoto = transform.rotation;
//        endRoto = new Quaternion(transform.rotation.x, transform.rotation.y, zRoto, transform.rotation.w);
    }

    // Update is called once per frame
    void Update()
    {
        if(isSlashing && !midReached)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, scytheMid.transform.localPosition, .025f * Time.deltaTime * 100);

//            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, zRoto, transform.rotation.w);
        }

        if(midReached)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, scytheEnd.transform.localPosition, .05f * Time.deltaTime * 100);
        }

        if(transform.position == scytheEnd.transform.position)
        {
            hasReachedEnd = true;
        }

        if(transform.position == scytheMid.transform.position)
        {
            midReached = true;
        }
    }
}
