using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    void Start()
    {
        maxHealth = 15;
        magic = 0;
        enemyName = "Slime";
    }

}
