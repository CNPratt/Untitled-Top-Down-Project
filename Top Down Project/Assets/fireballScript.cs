using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballScript : MonoBehaviour
{
    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
//        rb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.transform.position;

        IEnumerator fireballDestroy()
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }

        StartCoroutine(fireballDestroy());
    }
}
