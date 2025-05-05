using Unity.VisualScripting;
using UnityEngine;

public class NpcToPlayer : MonoBehaviour
{

    [SerializeField] private float speed;
    private InventoryManager inventoryManager;
    public GameObject target;
    public bool isActive;
    public bool needsFood;
    public string foodReq;
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
        isActive = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inventoryManager = GameObject.FindWithTag("GameController").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActive == true)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);

            if (distance <= range && isHolding == true)
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
            }

            else if ((this.transform.position.Equals(lastPos)) == true)
            {
                animator.SetBool("IsWalking", false);
            }

            lastPos = this.transform.position;
        }
    }

    private void Update()
    {
        if (needsFood == true)
        {
            Item foodItem = inventoryManager.GetSelectedItem(false);

            if ((foodItem != null) && (foodItem.name == foodReq))
            {
                isHolding = true;
            }

            else if ((foodItem == null) || (foodItem.name != foodReq))
            {
                isHolding = false;
            }
        }

        else
        {
            isHolding = true;
        }
    }
}
