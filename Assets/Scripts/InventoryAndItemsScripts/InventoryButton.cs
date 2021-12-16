using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField]
    private Image myIcon;
    [SerializeField]
    private InventoryControl inventoryControl;

    private string wordJapanese;
    private string wordEnglish;
    private string itemType;
    private string itemDescription;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetIcon(Sprite mySprite)
    {
        myIcon.sprite = mySprite;
    }

    public void SetData(Item item)
    {
        wordJapanese = item.nameJapanese;
        wordEnglish = item.nameEnglish;
        itemType = item.type;
        itemDescription = item.description;
    }
    public void OnClick()
    {
        inventoryControl.ItemButtonClicked(wordJapanese, itemType, myIcon, itemDescription);
    }
}

