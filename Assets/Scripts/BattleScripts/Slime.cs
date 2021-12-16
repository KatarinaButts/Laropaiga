using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    void Awake()
    {
        maxHealth = 15;
        health = maxHealth;
        magic = 0;
        enemyName = "Slime";
    }

}
