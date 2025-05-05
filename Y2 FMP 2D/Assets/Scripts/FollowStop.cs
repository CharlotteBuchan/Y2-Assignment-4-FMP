using UnityEngine;

public class FollowStop : MonoBehaviour
{
    public GameObject target;
    private NpcToPlayer NpcScript;
    private InventoryManager inventoryManager;
    private Animator animator;

    private void Start()
    {
        NpcScript = this.gameObject.GetComponent<NpcToPlayer>();
        inventoryManager = GameObject.FindWithTag("GameController").GetComponent<InventoryManager>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == target)
        {
            NpcScript.isActive = false;

            animator.SetBool("IsWalking", false);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == target)
        {
            NpcScript.isActive = true;

            animator.SetBool("IsWalking", true);
        }
    }
    private void Update()
    {
        if (NpcScript.isActive == false)
        {
            if (NpcScript.needsFood == true)
            {
                Item foodItem = inventoryManager.GetSelectedItem(false);

                if ((foodItem != null) && (foodItem.name == NpcScript.foodReq))
                {
                    NpcScript.isHolding = true;
                }

                else if ((foodItem == null) || (foodItem.name != NpcScript.foodReq))
                {
                    NpcScript.isHolding = false;
                }
            }

            else
            {
                return ;
            }
        }
    }
}
