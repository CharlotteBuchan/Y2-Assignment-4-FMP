using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InteractionPlayer : MonoBehaviour
{
    private bool isNear;
    private InventoryManager inventoryManager;
    private TextMeshProUGUI interactText;
    private GameObject textGO;
    private bool isTrigger;
    private InteractionPreset interactionPreset;
    private string outputText;
    private List<InteractionPreset> nearbyInteractables = new List<InteractionPreset>();


    void Start()
    {
        isTrigger = false;
        textGO = GameObject.FindWithTag("InteractTag");
        interactText = textGO.GetComponent<TextMeshProUGUI>();
        inventoryManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>();
        interactText.text = " ";
    }

    void Update()
    {
        UpdateClosestAnimal();

        if (isNear == true)
        {
            interactText.text = interactionPreset.VariantCheck();

            if (interactText.text != " ")
            {
                isTrigger = true;
            }

            else
            {
                isTrigger = false;
            }

            onClick();
        }

        else if (isNear == false)
        {
            isTrigger = false;
            interactText.text = " ";
        }
    }

    private void onClick()
    {
        if ((Input.GetKeyDown(KeyCode.E)) && (isTrigger == true))
        {
            if (interactionPreset.useItem == true)
            {
                inventoryManager.GetSelectedItem(true);
            }

            if (interactText.text != "Not Enough Water")
            {
                interactionPreset.OnEvent?.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<InteractionPreset>(out var foundPreset))
        {
            nearbyInteractables.Add(foundPreset);
            UpdateClosestAnimal();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<InteractionPreset>(out var foundPreset))
        {
            nearbyInteractables.Remove(foundPreset);
            UpdateClosestAnimal();
        }
    }

    private void UpdateClosestAnimal()
    {
        if (nearbyInteractables.Count == 0)
        {
            isNear = false;
            interactionPreset = null;
            return;
        }

        interactionPreset = nearbyInteractables
            .OrderBy(p => Vector2.Distance(transform.position, p.transform.position))
            .FirstOrDefault();

        isNear = true;
    }
}
