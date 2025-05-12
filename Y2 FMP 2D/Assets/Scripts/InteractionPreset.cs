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

    public InteractType interactType;
    private NpcToPlayer npcToPlayer;
    [HideInInspector] public string outputText;

    private void Start()
    {
        npcToPlayer = this.GetComponent<NpcToPlayer>();
    }

    public enum InteractType
    {
        feed,
        chop,
        harvest,
        mine,
        collect,
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
            outputText = "[E] - Chop Tree";
            return outputText;
        }

        else if (interactType == InteractType.harvest)
        {
            outputText = "[E] - Harvest";
            return outputText;
        }

        else if (interactType == InteractType.collect)
        {
            outputText = "[E] - Collect";
            return outputText;
        }

        else if (interactType == InteractType.mine)
        {
            outputText = "[E] - Mine";
            return outputText;
        }

        else
        {
            outputText = " ";
            return outputText;
        }
    }
}
