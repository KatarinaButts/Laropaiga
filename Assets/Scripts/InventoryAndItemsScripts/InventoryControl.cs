using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Todo: add the same recycling stuff that was done in the scroll list tutorial
public class InventoryControl : MonoBehaviour
{
    public Transform contentPanel;

    Inventory inventory;
    private List<GameObject> buttons = new List<GameObject>();

    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private GridLayoutGroup gridGroup;
    //[SerializeField]
    //private Sprite[] iconSprites;
    [SerializeField]
    Image itemSprite;

    public Text wordJapaneseText;
    public Text descriptionText;
    private string itemType;

    /*
    public class PlayerItem
    {
        public Sprite iconSprite;
    }
    */

    void Start()
    {
        inventory = Inventory.instance;
        GenInventoryButtons();

        //itemSlots = contentPanel.GetComponentsInChildren<InventoryButton>();
        inventory.onItemChangedCallback += UpdateUI;

        /*
        //temp
        for (int i = 0; i <= 99; i ++)
        {
            Item newItem = new Item();
            newItem.sprite = iconSprites[Random.Range(0, iconSprites.Length)];

            playerInventory.Add(newItem);
        }
        */
    }
    void Update()
    {

    }
    /*
    public void addItem(Item item)
    {
        if(!item.isDefaultItem)
        {
            playerInventory.Add(item);
        }
    }

    public void removeItem(Item item)
    {
        playerInventory.Remove(item);
    }
    */

    void UpdateUI()
    {
        Debug.Log("UPDATING UI");
        GenInventoryButtons();
    }
    void GenInventoryButtons()
    {
        if (buttons != null && buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }

        if (inventory.items.Count < 6)
        {
            gridGroup.constraintCount = inventory.items.Count;
        }
        else
        {
            gridGroup.constraintCount = 5;
        }

        foreach (Item item in inventory.items)
        {
            GameObject newButton = Instantiate(buttonTemplate) as GameObject;
            newButton.SetActive(true);
            //newButton.GetComponent<InventoryButton>().AddItem(item);
            newButton.GetComponent<InventoryButton>().SetIcon(item.sprite);
            newButton.GetComponent<InventoryButton>().SetData(item);
            newButton.transform.SetParent(buttonTemplate.transform.parent, false);

            buttons.Add(newButton);
        }


    }

    public void ItemButtonClicked(string wordJapanese, string type, Image myIcon, string itemDescription)
    {
        Debug.Log("Clicked Item Button: " + wordJapanese);
        itemType = type;

        wordJapaneseText.text = wordJapanese;
        itemSprite.sprite = myIcon.sprite;
        //ToDo: change this to if(type == ...) {display either definition panel or stats panel}
        descriptionText.text = "Description: " + itemDescription;
    }
}