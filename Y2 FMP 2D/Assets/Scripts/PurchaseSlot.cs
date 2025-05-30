using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseSlot : MonoBehaviour
{
    public Item item;
    public Animal animal;

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDesc;
    [SerializeField] private TextMeshProUGUI itemPriceTxt;
    [SerializeField] private Image itemIcon;
    [SerializeField] public int uses;

    private MoneySystem moneySystem;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moneySystem = GameObject.FindGameObjectWithTag("EdittyYay").GetComponent<MoneySystem>();

        if (item != null)
        {
            itemName.text = item.name;
            itemDesc.text = item.description;
            itemPriceTxt.text = ("$" + item.purchasePrice);
            itemIcon.sprite = item.image;
        }
        else if (item == null)
        {
            itemName.text = animal.name;
            itemDesc.text = animal.description;
            itemPriceTxt.text = ("$" + animal.purchasePrice);
            itemIcon.sprite = animal.icon;
        }
    }

    public void PurchaseItem()
    {
        moneySystem.PurchaseItem(item, uses);
    }

    public void PurchaseAnimal()
    {
        moneySystem.PurchaseAnimal(animal);
    }
}
