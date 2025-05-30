using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static InteractionPlayer;

public class InteractionPreset : MonoBehaviour
{
    [Serializable] public class EventType : UnityEvent { }

    [SerializeField] public EventType OnEvent;

    [SerializeField] public Item itemNeeded;

    public bool useItem = true;

    private NightCycle nightCycle;
    private InventoryManager inventoryManager;
    public InteractType interactType;
    private NpcToPlayer npcToPlayer;
    private GameObject player;
    public PlaceCrop cropScript;
    public CropGrow growScript;
    [HideInInspector] public string outputText;
    private int index = 100;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        growScript = this.GetComponent<CropGrow>();
        npcToPlayer = this.GetComponent<NpcToPlayer>();
        inventoryManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>();
        nightCycle = GameObject.FindGameObjectWithTag("EdittyYay").GetComponent<NightCycle>();
    }

    public enum InteractType
    {
        feed,
        chop,
        harvest,
        water,
        mine,
        collect,
        sleep,
        open,
        plant,
        refill,
    }

    public string VariantCheck()
    {
        if (interactType == InteractType.feed)
        {
            if ((npcToPlayer.needsFood == true) && (npcToPlayer.isHolding == true))
            {
                outputText = "[E] - Feed Animal";
                return outputText;
            }

            else if ((npcToPlayer.needsFood == false) && (npcToPlayer.isHolding == true))
            {
                outputText = "[E] - Feed Baby Animal";
                return outputText;
            }

            else
            {
                outputText = " ";
                return outputText;
            }
        }

        else if (interactType == InteractType.chop)
        {
            if (inventoryManager.GetSelectedItem(false) == itemNeeded)
            {
                outputText = "[E] - Chop Tree";
                return outputText;
            }

            else
            {
                outputText = " ";
                return outputText;
            }
        }

        else if (interactType == InteractType.harvest)
        {
            if (inventoryManager.GetSelectedItem(false) == itemNeeded)
            {
                outputText = "[E] - Harvest";
                return outputText;
            }

            else
            {
                outputText = " ";
                return outputText;
            }
            
        }

        else if (interactType == InteractType.water)
        {
            if (inventoryManager.GetSelectedItem(false) == itemNeeded && growScript.watered == false && inventoryManager.GetSelectedItem(false).usesLeft > 0)
            {
                outputText = "E - Water Crop";
                return outputText;
            }

            else if (inventoryManager.GetSelectedItem(false) == itemNeeded && growScript.watered == false && inventoryManager.GetSelectedItem(false).usesLeft <= 0)
            {
                outputText = "Not Enough Water";
                return outputText;
            }

            else
            {
                outputText = " ";
                return outputText;
            }
        }

        else if (interactType == InteractType.collect)
        {
            useItem = false;
            outputText = "[E] - Pick Up";
            return outputText;
        }

        else if (interactType == InteractType.mine)
        {
            outputText = "[E] - Mine";
            return outputText;
        }

        else if (interactType == InteractType.sleep)
        {
            useItem = false;

            if (nightCycle.isNight == true)
            {
                outputText = "E - Sleep";
                return outputText;
            }
            else
            {
                outputText = " ";
                return outputText;
            }
            
        }

        else if (interactType == InteractType.open)
        {
            useItem = false;
            outputText = "E - Open Shop";
            return outputText;
        }

        else if (interactType == InteractType.plant)
        {
            for (int i = 0; i < cropScript.seedPouch.Length; i++)
            {
                if (cropScript.seedPouch[i] == inventoryManager.GetSelectedItem(false))
                {
                    index = i;

                    break;
                }

                else
                {
                    index = 100;
                }
            }

            if (index != 100)
            {
                outputText = "E - Plant Crop";
                return outputText;
            }

            else
            {
                outputText = " ";
                return outputText;
            }
        }

        else if (interactType == InteractType.refill)
        {
            if (inventoryManager.GetSelectedItem(false) == itemNeeded)
            {
                outputText = "E - Refill";
                return outputText;
            }

            else
            {
                outputText = " ";
                return outputText;
            }
        }

        else
        {
            outputText = " ";
            return outputText;
        }
    }
}
