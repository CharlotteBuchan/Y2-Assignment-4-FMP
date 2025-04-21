using UnityEngine;

public class FollowStop : MonoBehaviour
{
    public GameObject target;
    private NpcToPlayer NpcScript;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == target)
        {
            NpcScript = this.gameObject.GetComponent<NpcToPlayer>();
            NpcScript.enabled = false;

            animator.SetBool("IsWalking", false);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == target)
        {
            NpcScript = this.gameObject.GetComponent<NpcToPlayer>();
            NpcScript.enabled = true;

            animator.SetBool("IsWalking", true);
        }
    }
}
