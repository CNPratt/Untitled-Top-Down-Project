using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDRoomtrans : MonoBehaviour
{
    public GameObject roomObjUp;
    public GameObject roomObjDown;

    public Transform playerTrans;
    public Camera cam;
    public CameraScript camscript;
    public Vector3 upminBounds;
    public Vector3 upmaxBounds;
    public Vector3 downminBounds;
    public Vector3 downmaxBounds;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
//            Debug.Log("trig");

            if (playerTrans.position.y > transform.position.y)
            {
                roomObjUp.SetActive(true);
                roomObjDown.SetActive(false);

                camscript.maxBounds = upmaxBounds;
                camscript.minBounds = upminBounds;

                while(playerTrans.position.y > transform.position.y)
                {
                    camscript.smoother = 1;
                    return;
                }
            }

            if (playerTrans.position.y < transform.position.y)
            {
                roomObjDown.SetActive(true);
                roomObjUp.SetActive(false);

                camscript.maxBounds = downmaxBounds;
                camscript.minBounds = downminBounds;

                while (playerTrans.position.y < transform.position.y)
                {
                    camscript.smoother = 1;
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
