using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public Transform target;
    public float smoother;
    public float cameraZ;

    // Start is called before the first frame update
    void Start()
    {
        maxBounds = new Vector2(-3.75f, 7f);
        minBounds = new Vector2(-6f, 3f);
        cameraZ = -10f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, smoother);

    }
}
