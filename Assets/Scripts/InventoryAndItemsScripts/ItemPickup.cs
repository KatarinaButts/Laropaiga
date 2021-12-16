using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact(Transform PlayerTransform)
    {
        base.Interact(PlayerTransform);

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.nameJapanese + ".");
        //Add to inventory
        bool wasPickedUp = Inventory.instance.addItem(item);
        
        if(wasPickedUp)
        {
            //Destroy the item
            Destroy(gameObject);
        }
       
    }

}
