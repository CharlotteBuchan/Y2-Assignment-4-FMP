using UnityEngine;
using UnityEngine.InputSystem;

public class InputControl : MonoBehaviour
{
    private GameObject player;
    private PlayerInput playerInput;
    private Animator animator;
    public bool overide;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInput = player.GetComponent<PlayerInput>();
        animator = player.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Walking Blend Tree") == false) && (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Blend Tree") == false))
        {
            playerInput.enabled = false;
        }

        else if (overide == true)
        {
            playerInput.enabled = false;
        }

        else
        {
            playerInput.enabled = true;
        }
    }
}
