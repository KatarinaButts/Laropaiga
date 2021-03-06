using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level = 1;
    public int health = 0;
    public int maxHealth = 0;
    public int magic = 0;
    public string enemyName = "";
    public Item[] possibleDrops;

    void Start()
    {
        possibleDrops = new Item[0];
    }

    public Item[] GetPossibleDrops()
    {
        return possibleDrops;
    }

    public int GetHealth()
    {
        return health;
    }


    public void ChangeHealth(int damage)
    {
        if (damage < 0)
        {
            //heal
        }
        else
        {
            health = Mathf.Clamp(health - damage, 0, maxHealth);
        }

        Debug.Log(health + "/" + maxHealth);
    }


}
