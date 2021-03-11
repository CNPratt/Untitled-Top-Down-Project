using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeRoomObjScript : MonoBehaviour
{
    public bool doorSwitch;
    public int enDefeated;
    public bool enemiesCleared;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject enemy6;

    public GameObject enemy7;
    public GameObject enemy8;
    public GameObject enemy9;
    public GameObject enemy10;
    public GameObject enemy11;
    public GameObject enemy12;

    public Doorbase door1;
    public Doorbase door2;
    public Doorbase door3;
    public Doorbase door4;

    public EnCombatMono ai1;
    public EnCombatMono ai2;
    public EnCombatMono ai3;
    public EnCombatMono ai4;
    public EnCombatMono ai5;
    public EnCombatMono ai6;

    public EnCombatMono ai7;
    public EnCombatMono ai8;
    public EnCombatMono ai9;
    public EnCombatMono ai10;
    public EnCombatMono ai11;
    public EnCombatMono ai12;

    public List<Doorbase> Doors;
    public List<EnCombatMono> ComAI;
    public List<GameObject> RoomEnemies;

    private void OnEnable()
    {
        if (enDefeated < ComAI.Count)
        {
            enDefeated = 0;
        }

        foreach (GameObject enemy in RoomEnemies)
        {
            //           Debug.Log("enable loop" + enemy);

            if (enemy != null)
            {
                enemy.SetActive(true);
            }
        }

        foreach (EnCombatMono enemy in ComAI)
        {
            //           Debug.Log("enable loop" + enemy);

            if (enemy != null && !enemiesCleared)
            {
                enemy.beenDefeated = false;
            }
        }

        foreach (Doorbase door in Doors)
        {
            if (door != null)
            {
                door.enterSwitch = true;
            }
        }
    }

    private void OnDisable()
    {
        foreach (GameObject enemy in RoomEnemies)
        {
            //            Debug.Log("disable loop " + enemy);

            if (enemy != null)
            {
                enemy.SetActive(false);
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 6; i++)
        {
            GameObject thisenemy;

            if ((GameObject)this.GetType().GetField("enemy" + i).GetValue(this) != null)
            {
                thisenemy = (GameObject)this.GetType().GetField("enemy" + i).GetValue(this);

                //                Debug.Log(thisenemy.name);
            }
        }

        if (door1 != null)
        {
            Doors.Add(door1);
        }

        if (door2 != null)
        {
            Doors.Add(door2);
        }

        if (door3 != null)
        {
            Doors.Add(door3);
        }

        if (door4 != null)
        {
            Doors.Add(door4);
        }

        if (enemy1 != null)
        {
            RoomEnemies.Add(enemy1);

            enemy1.SetActive(false);

            ai1 = enemy1.GetComponent<EnCombatMono>();

            ComAI.Add(ai1);
        }

        if (enemy2 != null)
        {
            RoomEnemies.Add(enemy2);

            enemy2.SetActive(false);

            ai2 = enemy2.GetComponent<EnCombatMono>();

            ComAI.Add(ai2);
        }

        if (enemy3 != null)
        {
            RoomEnemies.Add(enemy3);

            enemy3.SetActive(false);

            ai3 = enemy3.GetComponent<EnCombatMono>();

            ComAI.Add(ai3);
        }

        if (enemy4 != null)
        {
            RoomEnemies.Add(enemy4);

            enemy4.SetActive(false);

            ai4 = enemy4.GetComponent<EnCombatMono>();

            ComAI.Add(ai4);
        }

        if (enemy5 != null)
        {
            RoomEnemies.Add(enemy5);

            enemy5.SetActive(false);

            ai5 = enemy5.GetComponent<EnCombatMono>();

            ComAI.Add(ai5);
        }

        if (enemy6 != null)
        {
            RoomEnemies.Add(enemy6);

            enemy6.SetActive(false);

            ai6 = enemy6.GetComponent<EnCombatMono>();

            ComAI.Add(ai6);
        }

        if (enemy7 != null)
        {
            RoomEnemies.Add(enemy7);

            enemy7.SetActive(false);

            ai7 = enemy7.GetComponent<EnCombatMono>();

            ComAI.Add(ai7);
        }

        if (enemy8 != null)
        {
            RoomEnemies.Add(enemy8);

            enemy8.SetActive(false);

            ai8 = enemy8.GetComponent<EnCombatMono>();

            ComAI.Add(ai8);
        }

        if (enemy9 != null)
        {
            RoomEnemies.Add(enemy9);

            enemy9.SetActive(false);

            ai9 = enemy9.GetComponent<EnCombatMono>();

            ComAI.Add(ai9);
        }

        if (enemy10 != null)
        {
            RoomEnemies.Add(enemy10);

            enemy10.SetActive(false);

            ai10 = enemy10.GetComponent<EnCombatMono>();

            ComAI.Add(ai10);
        }

        if (enemy11 != null)
        {
            RoomEnemies.Add(enemy11);

            enemy11.SetActive(false);

            ai11 = enemy11.GetComponent<EnCombatMono>();

            ComAI.Add(ai11);
        }

        if (enemy12 != null)
        {
            RoomEnemies.Add(enemy12);

            enemy12.SetActive(false);

            ai12 = enemy12.GetComponent<EnCombatMono>();

            ComAI.Add(ai12);
        }

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (EnCombatMono enemy in ComAI)
        {
            if (enemy.enHealthCurrent == 0 && !enemy.beenDefeated)
            {
                enDefeated = enDefeated + 1;

                enemy.beenDefeated = true;
            }
        }

        if (enDefeated == ComAI.Count)
        {
            enemiesCleared = true;
        }

        if (enemiesCleared == true && doorSwitch == false)
        {
            doorSwitch = true;

            foreach (Doorbase door in Doors)
            {
                door.isRoomClear = true;
            }
        }
    }
}