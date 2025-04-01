using UnityEngine;
using UnityEngine.InputSystem;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private options Option;
    private Animator animator;
    [SerializeField] private GameObject player;
    private PlayerInput playerInput;
    private bool touching;

    private enum options
    {
        touch,
        chop,
        dig,
        hoe,
        water,
    }

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
                switch (Option)
                {
                    case options.touch:
                        Touch();
                        break;

                    case options.chop:
                        Debug.Log("Option Chop");
                        Chop();
                        break;

                    case options.dig:
                        Dig();
                        break;

                    case options.hoe:
                        Hoe();
                        break;

                    case options.water:
                        Water();
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

    private void Water()
    {

    }
    private void Hoe()
    {

    }
    private void Dig()
    {

    }
    private void Touch()
    {

    }

}
