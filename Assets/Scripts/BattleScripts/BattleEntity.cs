using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntity : MonoBehaviour
{
    public int level = 1;
    public int health = 0;
    public int maxHealth = 0;
    public int damage = 0;
    public int magic = 0;
    public string entityName = "";
    public Item[] possibleDrops;

    void Start()
    {
        possibleDrops = new Item[0];
    }
    public Item[] getPossibleDrops()
    {
        return possibleDrops;
    }

    public int getHealth()
    {
        return health;
    }

    public void setPlayerStats(int playerLevel, int playerHealth, int playerMaxHealth, int playerDamage, int playerMagic, string playerName)
    {
        level = playerLevel;
        health = playerHealth;
        maxHealth = playerMaxHealth;
        damage = playerDamage;
        magic = playerMagic;
        entityName = playerName;
    }
   
    public bool TakeDamage(int damageReceived)
    {
        health -= damageReceived;

        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;

        }
    }
}
