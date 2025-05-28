using TMPro;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    [SerializeField] private int money;
    [SerializeField] private TextMeshProUGUI textUI;
    private InventoryManager inventoryManager;
    private int purchasePrice;
    private int sellPrice;
    private string plural = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        money = 25;
        inventoryManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((money > 1) || (money < 1))
        {
            plural = "'s";
        }
        else
        {
            plural = "";
        }

        textUI.text = (money + " Loti" + plural);
    }

    public void PurchaseItem(Item item)
    {
        purchasePrice = item.purchasePrice;
        if (money >= purchasePrice)
        {
            money -= purchasePrice;
            inventoryManager.AddItem(item);
        }

        else if (money < purchasePrice)
        {
            Debug.Log("NOT ENOUGH GRAH");
        }
    }

    public void SellItem(Item item)
    {
        sellPrice = item.sellPrice;

        money += sellPrice;
    }
}
