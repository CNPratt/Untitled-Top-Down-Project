using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerInventoryScript : MonoBehaviour
{
    
    public static int redKeys;
    public static int blueKeys;
    public static int greenKeys;
    public static int bossKeys;

    public static int chipCount;
    public static int bombCount;

    public static int maxHealth;
    public static int currentHealth;

    public static int maxEnergy;
    public static int currentEnergy;

    public static void TakeDamage(int damage)
    {
//        Debug.Log("takedmg called: " + damage);

        currentHealth = currentHealth - damage;
    }

    public static void Heal(int amount)
    {
 //       Debug.Log("heal " + amount);

        currentHealth = currentHealth + amount;
    }

    public static void Recharge(int amount)
    {
//        Debug.Log("recharge " + amount);

        currentEnergy = currentEnergy + amount;
    }

    public static void GetChips(int amount)
    {
        chipCount = chipCount + amount;
    }

    // Start is called before the first frame update
    void Start()
    {
        chipCount = 0;
        bombCount = 0;

        maxHealth = 12;
        currentHealth = maxHealth;

        maxEnergy = 25;
        currentEnergy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
//        Debug.Log(redKeys);

        if(currentHealth <= 0)
        {
            currentHealth = 0;
        }

        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
        }
    }
}
