using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionPreset : MonoBehaviour
{
    public InteractType interactType;
    private TextMeshProUGUI interactText;
    private NpcToPlayer npcToPlayer;
    private GameObject textGO;
    private bool isNear;

    public enum InteractType
    {
        feed,
        chop,
        harvest,
    }

    private void Start()
    {
        npcToPlayer = GetComponent<NpcToPlayer>();
        textGO = GameObject.FindWithTag("InteractTag");
        interactText = textGO.GetComponent<TextMeshProUGUI>();
        textGO.SetActive(false);
    }

    private void Update()
    {
        if (isNear == true)
        {
            textGO.SetActive(true);
            VariantCheck();
        }

        else if (isNear == false)
        {
            textGO.SetActive(false);
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
                interactText.text = "Feed animal";
            }
        }

        else if (interactType == InteractType.chop)
        {

        }

        else if (interactType == InteractType.harvest)
        {

        }
    }
}
