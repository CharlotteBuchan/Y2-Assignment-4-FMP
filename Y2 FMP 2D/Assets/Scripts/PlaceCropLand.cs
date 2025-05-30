using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class PlaceCropLand : MonoBehaviour
{

    [SerializeField] private RuleTile highlightTile;
    [SerializeField] private Tilemap highlightMap;

    private Animator animator;
    public PlayerInput speedScript;

    private InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>();
        animator = GetComponent<Animator>();
        speedScript = GetComponent<PlayerInput>();
    }


    private void LateUpdate()
    {
        Vector3Int currentCell = highlightMap.WorldToCell(transform.position);

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (inventoryManager.GetSelectedItem(false).name == "Hoe")
            {
                //speedScript.enabled = false;

                animator.SetBool("IsHoeing", true);

                animator.Play("Hoeing Blend Tree");

                highlightMap.SetTile(currentCell, highlightTile);

                animator.SetBool("IsHoeing", false);

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hoeing Blend Tree") == false)
                {
                    //speedScript.enabled = true;
                }
            }
        }
    }
}
