using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        inventory = Inventory.instance;
        GenInventoryButtons();

        inventory.onItemChangedCallback += UpdateUI;
    }

    void UpdateUI()
    {
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
            newButton.GetComponent<InventoryButton>().SetIcon(item.sprite);
            newButton.GetComponent<InventoryButton>().SetData(item);
            newButton.transform.SetParent(buttonTemplate.transform.parent, false);

            buttons.Add(newButton);
        }
    }

    public void ItemButtonClicked(string wordJapanese, string type, Image myIcon, string itemDescription)
    {
        itemType = type;

        wordJapaneseText.text = wordJapanese;
        itemSprite.sprite = myIcon.sprite;
        descriptionText.text = "Description: " + itemDescription;
    }
}