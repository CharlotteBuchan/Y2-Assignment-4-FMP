using UnityEngine;

public class StartDefaults : MonoBehaviour
{
    public Item[] startTools;
    public Item startSeeds;
    private int seedAmount = 5;
    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>();
        
        for (int i = 0; i < startTools.Length; i++)
        {
            startTools[i].usesLeft = 25;
            inventoryManager.AddItem(startTools[i]);
        }


        if (startSeeds != null)
        {
            for (int i = 0; i < seedAmount; i++)
            {
                inventoryManager.AddItem(startSeeds);
            }
        }
    }
}
