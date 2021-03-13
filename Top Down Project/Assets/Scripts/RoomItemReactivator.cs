using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomItemReactivator : MonoBehaviour
{
    public Transform[] children;

    private void OnEnable()
    {
        for (int i = 0; i < children.Length - 1; i++)
        {
            transform.GetChild(i).transform.gameObject.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        children = gameObject.GetComponentsInChildren<Transform>(); ;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
