using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseSlot : MonoBehaviour
{
    public Item item;

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDesc;
    [SerializeField] private TextMeshProUGUI itemPriceTxt;
    [SerializeField] private Image itemIcon;

    private MoneySystem moneySystem;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moneySystem = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<MoneySystem>();

        itemName.text = item.name;
        itemDesc.text = item.description;
        itemPriceTxt.text = ("$" + item.purchasePrice);
        itemIcon.sprite = item.image;
    }

    public void Purchase()
    {
        moneySystem.PurchaseItem(item);
    }
}
