using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandoSpawnscript : MonoBehaviour
{
    public GameObject thisEnemy;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    public void RandoSpawn()
    {
        int rando = Random.Range(1, 4);

        if (rando == 1)
        {
            thisEnemy = Instantiate(enemy1, transform.position, transform.rotation);
        }

        if (rando == 2)
        {
            thisEnemy = Instantiate(enemy2, transform.position, transform.rotation);
        }

        if (rando == 3)
        {
            thisEnemy = Instantiate(enemy3, transform.position, transform.rotation);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (thisEnemy == null || !thisEnemy.activeSelf)
        {
            Invoke("RandoSpawn", 0);
        }
    }
}
