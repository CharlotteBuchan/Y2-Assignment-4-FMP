using Unity.VisualScripting;
using UnityEngine;

public class NpcToPlayer : MonoBehaviour
{

    [SerializeField] private GameObject target;
    [SerializeField] private float speed;
    [SerializeField] private GameObject babyFollow;
    public Vector2 space;
    public float range;
    public bool isHolding;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector3 lastPos;
    private Vector2 playerWithSpace;
    private float x;
    private float y;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance <= range)
        {
            Vector2 playerLocation = target.transform.position;
            playerWithSpace = playerLocation - space;

            rb.position = Vector2.MoveTowards(this.transform.position, playerWithSpace, speed * Time.deltaTime);

            x = (target.transform.position.x - transform.position.x);
            y = (target.transform.position.y - transform.position.y);
        }

        if ((this.transform.position.Equals(lastPos)) == false)
        {
            if (x != playerWithSpace.x || y != playerWithSpace.y)
            {
                animator.SetFloat("X", x);
                animator.SetBool("IsWalking", true);
            }

            if (babyFollow != null)
            {
                if (x > 5)
                {
                    babyFollow.transform.localPosition = new Vector2(-2.5f, -1);
                }

                else if (x < 5)
                {
                    babyFollow.transform.localPosition = new Vector2(2.5f, -1);
                }
            }
        }

        else if ((this.transform.position.Equals(lastPos)) == true)
        {
            animator.SetBool("IsWalking", false);
        }

        lastPos = this.transform.position;
    }
}
