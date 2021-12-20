using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string nameJapanese = "New Japanese Item Name";
    public string nameEnglish = "New English Item Name";
    public string type = "Item Type";
    public string description = "insertDescriptionHere";
    public int buyPrice = -1;
    public int sellPrice = -1;
    public Sprite sprite = null;
    public bool isDefaultItem = false;

    void Start()
    {
        //Empty
    }

    void Update()
    {
        //Empty
    }
}
