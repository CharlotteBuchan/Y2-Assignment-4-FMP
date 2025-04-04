using UnityEngine;

public class DemoPickupScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result == true)
        {
            Debug.Log("ItemAdded");
        }
        else
        {
            Debug.Log("ItemNotAdded");
        }
    }

    public void GetSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log("Received item" + receivedItem);
        }
        else
        {
            Debug.Log("Not received item.");
        }
    }

    public void UseSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log("Used item" + receivedItem);
        }
        else
        {
            Debug.Log("No item used!");
        }
    }
}
