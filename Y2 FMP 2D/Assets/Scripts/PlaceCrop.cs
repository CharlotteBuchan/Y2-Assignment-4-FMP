using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class PlaceCrop : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private RuleTile highlightTile;
    [SerializeField] private Tilemap highlightMap;
    [SerializeField] private Texture2D cropSprite;
    private Sprite mySprite;
    private SpriteRenderer sr;
    GameObject cropTile;

    private Animator animator;
    public CharacterMovement speedScript;


    private void Awake()
    {
        animator = player.GetComponent<Animator>();
        speedScript = player.GetComponent<CharacterMovement>();
    }


    private void LateUpdate()
    {
        Vector3Int currentCell = highlightMap.WorldToCell(player.transform.position);

        Vector3 currentCellV3 = highlightMap.CellToWorld(currentCell);

        currentCellV3.x = -currentCellV3.x;
        
        var currentTile = highlightMap.GetTile(currentCell);

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (currentTile == highlightTile)
            {
                cropTile = new GameObject("crop Tile");

                sr = cropTile.AddComponent<SpriteRenderer>() as SpriteRenderer;
                sr.color = new Color(1, 1, 1, 1);

                speedScript.speed = 0;

                animator.SetBool("IsHoeing", true);

                animator.Play("Hoeing Blend Tree");

                Debug.Log(speedScript.speed);

                sr.sprite = Sprite.Create(cropSprite, new Rect(currentCellV3.x, currentCellV3.y, cropSprite.width, cropSprite.height), currentCellV3, 16f);

                animator.SetBool("IsHoeing", false);
            }
        }
        
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Hoeing Blend Tree")) == false)
        {
            speedScript.speed = 20;
        }
    }
}
