using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            //Debug.LogWarning("More than one instance of Inventory found!");
            Destroy(gameObject);

        }
        else
        {
            instance = this;
        }
    }
    #endregion

    int space = 20;

    public List<Item> items = new List<Item>();


    public bool addItem(Item item)
    {
        if (!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                return false;
            }
            items.Add(item);

            if(onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }
        return true;
    }

    public void removeItem(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    public Item findItem(string name)
    {
        return items.Find(x => x.nameEnglish == name);
    }
}
