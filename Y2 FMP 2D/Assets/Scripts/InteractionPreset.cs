using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionPreset : MonoBehaviour
{
    public InteractType interactType;
    private TextMeshProUGUI interactText;
    private NpcToPlayer npcToPlayer;
    private GameObject textGO;
    [HideInInspector] public bool isNear;

    public enum InteractType
    {
        feed,
        chop,
        harvest,
        collect,
    }

    private void Start()
    {
        npcToPlayer = GetComponent<NpcToPlayer>();
        textGO = GameObject.FindWithTag("InteractTag");
        interactText = textGO.GetComponent<TextMeshProUGUI>();
        interactText.text = " ";
    }

    private void Update()
    {
        if (isNear == true)
        {
            VariantCheck();
        }

        else if (isNear == false)
        {
            interactText.text = " ";
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isNear = false;
        }
    }

    private void VariantCheck()
    {
        if (interactType == InteractType.feed)
        {
            if (npcToPlayer.isHolding == true)
            {
                interactText.text = "[E] - Feed Animal";
            }

            //else if ()

            else
            {
                interactText.text = " ";
            }
        }

        else if (interactType == InteractType.chop)
        {
            interactText.text = "[E] - Chop Wood";
        }

        else if (interactType == InteractType.harvest)
        {
            interactText.text = "[E] - Harvest";
        }

        else if (interactType == InteractType.collect)
        {
            interactText.text = "[E] - Collect";
        }
    }
}
