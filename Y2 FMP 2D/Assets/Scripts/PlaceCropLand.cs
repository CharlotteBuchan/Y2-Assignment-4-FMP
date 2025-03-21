using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class PlaceCropLand : MonoBehaviour
{

    [SerializeField] private RuleTile highlightTile;
    [SerializeField] private Tilemap highlightMap;

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

        if (Input.GetKeyDown(KeyCode.R))
        {

            speedScript.speed = 0;

            animator.SetBool("IsHoeing", true);

            animator.Play("Hoeing Blend Tree");

            Debug.Log(speedScript.speed);

            highlightMap.SetTile(currentCell, highlightTile);

            animator.SetBool("IsHoeing", false);
        }

        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Hoeing Blend Tree")) == false)
        {
            speedScript.speed = 20;
        }
    }
}
