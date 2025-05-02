using UnityEngine;

public class DemoPickupScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;
    public GameObject prefabChick;
    public GameObject spawnTarget;
    public GameObject prefabEgg;

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
            
        }
        else
        {
            Debug.Log("No item used!");
        }
    }

    public void SPAWNCHICKEN()
    {
        Instantiate(prefabChick, spawnTarget.transform.position, Quaternion.identity);
    }

    public void SPAWNEGG()
    {
        Instantiate(prefabEgg, spawnTarget.transform.position, Quaternion.identity);
    }
}
