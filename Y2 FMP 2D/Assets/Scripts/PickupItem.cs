using UnityEngine;
using UnityEngine.InputSystem;
using static Item;

public class PickupItem : MonoBehaviour
{
    private ActionType actionType;
    public ItemType itemType;
    private Animator animator;
    [SerializeField] private GameObject player;
    private PlayerInput playerInput;
    private bool touching;
    public Item item;

    private void Awake()
    {
        animator = player.GetComponent<Animator>();
        playerInput = player.GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        touching = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        touching = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (touching == true)
            {
                Debug.Log("Input");
                switch (actionType)
                {
                    case Item.ActionType.chop:
                        Debug.Log("Option Chop");
                        Chop();
                        break;

                    default:
                        break;
                }
            }
        }

        if (((animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Blend Tree")) == true) || ((animator.GetCurrentAnimatorStateInfo(0).IsName("Walking Blend Tree")) == true))
        {
            playerInput.enabled = true;
        }
    }

    private void Chop()
    {
        playerInput.enabled = false;

        animator.SetBool("IsChopping", true);

        animator.Play("Chopping Blend Tree");

        animator.SetBool("IsChopping", false);
    }

    private void RefreshInput()
    {
        if (((animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Blend Tree")) == true) || ((animator.GetCurrentAnimatorStateInfo(0).IsName("Walking Blend Tree")) == true))
        {
            playerInput.enabled = true;
        }
    }
}
