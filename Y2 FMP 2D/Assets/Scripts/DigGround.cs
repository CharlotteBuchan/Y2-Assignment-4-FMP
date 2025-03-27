using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class DigGround : MonoBehaviour
{

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
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3Int tilePos = highlightMap.WorldToCell(worldPos);

        //Vector3Int currentCell = highlightMap.WorldToCell(tilePos);

        Vector3Int currentCell = highlightMap.WorldToCell(transform.position);

        if (Input.GetKeyDown(KeyCode.Q))
        {

            speedScript.speed = 0;

            animator.SetBool("IsHoeing", true);

            animator.Play("Hoeing Blend Tree");

            Debug.Log(speedScript.speed);

            highlightMap.SetTile(currentCell, null);

            animator.SetBool("IsHoeing", false);
        }

        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Hoeing Blend Tree")) == false)
        {
            speedScript.speed = 20;
        }
    }
}
