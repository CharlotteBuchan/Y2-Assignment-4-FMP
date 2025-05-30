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
    [SerializeField] GameObject animalSP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

    public void PurchaseItem(Item item, int uses)
    {
        purchasePrice = item.purchasePrice;
        if (money >= purchasePrice)
        {
            money -= purchasePrice;
            if (item.useUp == false)
            {
                item.usesLeft = uses;
            }
            inventoryManager.AddItem(item);
        }

        else if (money < purchasePrice)
        {
            Debug.Log("NOT ENOUGH GRAH");
        }
    }

    public void PurchaseAnimal(Animal animal)
    {
        purchasePrice = animal.purchasePrice;
        if (money >= purchasePrice)
        {
            money -= purchasePrice;

            GameObject newAnimal = Instantiate(animal.prefab, animalSP.transform.position, Quaternion.identity);
            newAnimal.transform.parent = animalSP.transform;
        }
    }


    public void SellItem(Item item)
    {
        sellPrice = item.sellPrice;

        money += sellPrice;
    }
}
