using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InteractionPlayer : MonoBehaviour
{
    private bool isNear;
    private TextMeshProUGUI interactText;
    private GameObject textGO;
    private bool isTrigger;
    private InteractionPreset interactionPreset;
    private string outputText;
    private List<InteractionPreset> nearbyAnimals = new List<InteractionPreset>();


    void Start()
    {
        isTrigger = false;
        textGO = GameObject.FindWithTag("InteractTag");
        interactText = textGO.GetComponent<TextMeshProUGUI>();
        interactText.text = " ";
    }

    void Update()
    {
        UpdateClosestAnimal();
        Debug.Log(nearbyAnimals.Count);

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
        if ((Input.GetKey(KeyCode.E)) && (isTrigger == true))
        {
            interactionPreset.OnEvent?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.gameObject.TryGetComponent<InteractionPreset>(out var foundPreset))
        //{
        //    interactionPreset = foundPreset;
        //    isNear = true;
        //}
        //
        //else
        //{
        //    isNear = false;
        //    return;
        //}

        if (col.gameObject.TryGetComponent<InteractionPreset>(out var foundPreset))
        {
            nearbyAnimals.Add(foundPreset);
            UpdateClosestAnimal();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<InteractionPreset>(out var foundPreset))
        {
            nearbyAnimals.Remove(foundPreset);
            UpdateClosestAnimal();
        }
    }

    private void UpdateClosestAnimal()
    {
        if (nearbyAnimals.Count == 0)
        {
            isNear = false;
            interactionPreset = null;
            return;
        }

        interactionPreset = nearbyAnimals
            .OrderBy(p => Vector2.Distance(transform.position, p.transform.position))
            .FirstOrDefault();

        isNear = true;
    }
}
