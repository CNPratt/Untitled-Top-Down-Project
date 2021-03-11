using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRBoundschangeScript : MonoBehaviour
{
    public Transform playerTrans;
    public Camera cam;
    public CameraScript camscript;
    public Vector3 leftminBounds;
    public Vector3 leftmaxBounds;
    public Vector3 rightminBounds;
    public Vector3 rightmaxBounds;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            //            Debug.Log("trig");

            if (playerTrans.position.y > transform.position.y)
            {
                camscript.maxBounds = leftmaxBounds;
                camscript.minBounds = leftminBounds;
            }

            if (playerTrans.position.y < transform.position.y)
            {
                camscript.maxBounds = rightmaxBounds;
                camscript.minBounds = rightminBounds;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        camscript = cam.GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
