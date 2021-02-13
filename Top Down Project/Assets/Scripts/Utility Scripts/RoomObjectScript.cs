using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObjectScript : MonoBehaviour
{

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject enemy6;

    private void OnEnable()
    {
        enemy1.SetActive(true);
        enemy2.SetActive(true);
        enemy3.SetActive(true);
        enemy4.SetActive(true);
        enemy5.SetActive(true);
        enemy6.SetActive(true);
    }

    private void OnDisable()
    {
        if (enemy1 != null)
        {
            enemy1.SetActive(false);
        }

        if (enemy2 != null)
        {
            enemy2.SetActive(false);
        }

        if (enemy3 != null)
        {
            enemy3.SetActive(false);
        }

        if (enemy4 != null)
        {
            enemy4.SetActive(false);
        }

        if (enemy5 != null)
        {
            enemy5.SetActive(false);
        }

        if (enemy6 != null)
        {
            enemy6.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (enemy1 != null)
        {
            enemy1.SetActive(false);
        }

        if (enemy2 != null)
        {
            enemy2.SetActive(false);
        }

        if (enemy3 != null)
        {
            enemy3.SetActive(false);
        }

        if (enemy4 != null)
        {
            enemy4.SetActive(false);
        }

        if (enemy5 != null)
        {
            enemy5.SetActive(false);
        }

        if (enemy6 != null)
        {
            enemy6.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
