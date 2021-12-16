using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text health;
    public Text magic;
    public Slider healthSlider;

    public void SetHUDInfo (BattleEntity entity)
    {
        nameText.text = entity.entityName;
        health.text = entity.health.ToString();
        magic.text = entity.magic.ToString();

        healthSlider.maxValue = entity.maxHealth;
        healthSlider.value = entity.health;


    }

    public void SetHealth(int newHealth)
    {
        health.text = newHealth.ToString();
        healthSlider.value = newHealth;
    }
}
