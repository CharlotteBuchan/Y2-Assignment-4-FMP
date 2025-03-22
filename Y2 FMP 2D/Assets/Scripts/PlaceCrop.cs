using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class PlaceCrop : MonoBehaviour
{
    [SerializeField] private RuleTile highlightTile;
    [SerializeField] private Tilemap highlightMap;
    [SerializeField] private Tilemap cropMap;
    [SerializeField] private RuleTile cropTile;

    private Animator animator;
    public CharacterMovement speedScript;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        speedScript = GetComponent<CharacterMovement>();
    }


    private void LateUpdate()
    {
        Vector3Int currentCell = highlightMap.WorldToCell(transform.position);
        
        var currentTile = highlightMap.GetTile(currentCell);

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (currentTile == highlightTile)
            {
                speedScript.speed = 0;

                animator.SetBool("IsHoeing", true);

                animator.Play("Hoeing Blend Tree");

                Debug.Log("CROP?");

                cropMap.SetTile(currentCell, cropTile);

                animator.SetBool("IsHoeing", false);
            }
        }
        
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Hoeing Blend Tree")) == false)
        {
            speedScript.speed = 20;
        }
    }
}
