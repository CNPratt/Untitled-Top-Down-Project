using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBExplScript : MonoBehaviour
{
    public Vector2 pos;
    public Throwscripttest2 throwScript;
    public GameObject thisBomb;

    public void BombDestroy()
    {
        Destroy(thisBomb);
//        thisBomb.SetActive(false);
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
