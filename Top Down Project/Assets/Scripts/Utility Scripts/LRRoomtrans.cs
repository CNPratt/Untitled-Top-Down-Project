using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRRoomtrans : MonoBehaviour
{
    public GameObject roomObjLeft;
    public GameObject roomObjRight;

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

            if (playerTrans.position.x > transform.position.x)
            {
                roomObjRight.SetActive(true);
                roomObjLeft.SetActive(false);

//                camscript.maxBounds = rightmaxBounds;
//                camscript.minBounds = rightminBounds;

                while(playerTrans.position.x > transform.position.x)
                {
//                    camscript.smoother = 1;
                    return;
                }
            }

            if (playerTrans.position.x < transform.position.x)
            {
                roomObjLeft.SetActive(true);
                roomObjRight.SetActive(false);

//                camscript.maxBounds = leftmaxBounds;
//                camscript.minBounds = leftminBounds;

                while (playerTrans.position.x < transform.position.x)
                {
//                    camscript.smoother = 1;
                    return;
                }
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
