using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObjectScript : MonoBehaviour
{
    public GameObject enemy1;

    private void OnEnable()
    {
        enemy1.SetActive(true);
    }

    private void OnDisable()
    {
        if (enemy1 != null)
        {
            enemy1.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
