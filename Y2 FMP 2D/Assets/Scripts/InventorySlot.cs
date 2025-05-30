using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour , IDropHandler , IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public Sprite selectedSprite, notSelectedSprite;
    public bool isOnSlot;

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.sprite = selectedSprite;
    }

    public void Deselect()
    {
        image.sprite = notSelectedSprite;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            InventoryItem inventoryItem = dropped.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
        else
        {
            GameObject dropped = eventData.pointerDrag;
            InventoryItem inventoryItem = dropped.GetComponent<InventoryItem>();

            GameObject current = transform.GetChild(0).gameObject;
            InventoryItem currentDraggable = current.GetComponent<InventoryItem>();

            currentDraggable.transform.SetParent(inventoryItem.parentAfterDrag);
            inventoryItem.parentAfterDrag = transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOnSlot = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOnSlot = false;
    }
}