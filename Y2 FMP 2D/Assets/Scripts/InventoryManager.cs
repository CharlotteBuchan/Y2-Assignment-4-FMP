using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 64;
    public InventorySlot[] inventorySlots;
    public InventorySlot[] sellSlots;
    public GameObject inventoryItemPrefab;
    private InventorySlot inventorySlot;
    private MoneySystem moneySystem;

    int selectedSlot = -1;

    private void Start()
    {
        moneySystem = GameObject.FindGameObjectWithTag("EdittyYay").GetComponent<MoneySystem>();
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 10)
            {
                ChangeSelectedSlot(number - 1);
            }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            int newValue = selectedSlot + (int)(scroll / Mathf.Abs(scroll));
            if (newValue < 0)
            {
                newValue = inventorySlots.Length - 1;
            }
            else if (newValue >= inventorySlots.Length)
            {
                newValue = 0;
            }
            ChangeSelectedSlot(newValue % 9);
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (inventorySlot.isOnSlot == true)
        //    {
        //        Debug.Log("Is On");
        //    }
        //
        //    else
        //    {
        //        Debug.Log("Nuhuh");
        //    }
        //}
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item)
    {
        // Check if any slot has the same item and is stackable
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if ((itemInSlot != null) && 
                (itemInSlot.item == item) && 
                (itemInSlot.count < maxStackedItems) && 
                (itemInSlot.item.stackable == true))
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        //Find empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == true && item.useUp == true)
            {
                itemInSlot.count--;

                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }

            else if (use == true && item.useUp == false)
            {
                item.usesLeft--;

                if (item.usesLeft <= 0 && item.refillable == false)
                {
                    Destroy(itemInSlot.gameObject);
                }
            }

                return itemInSlot.item;
        }

        return null;
    }

    public void SellItems(bool use)
    {
        foreach (InventorySlot sellSlot in sellSlots)
        {
            InventoryItem itemInSlot = sellSlot.GetComponentInChildren<InventoryItem>();

            if (use == true)
            {
                if (itemInSlot != null && (itemInSlot.item.sellPrice > 0))
                {
                    while (itemInSlot.count > 0)
                    {
                        moneySystem.SellItem(itemInSlot.item);
                        itemInSlot.count--;
                    }

                    if (itemInSlot.count <= 0)
                    {
                        Destroy(itemInSlot.gameObject);
                    }
                }
            }
        }
    }
}
