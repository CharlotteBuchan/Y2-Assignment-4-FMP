using System.Collections;
using UnityEngine;

public class ObjectAction : MonoBehaviour
{
    private GameObject player;
    private Animator playerAnimator;
    private bool isDoing = false;
    [SerializeField] GameObject fallenPreset;
    private SpriteRenderer spriteRenderer;
    private Color oldCol;
    private Color newCol;
    public Item droppedItem;
    private InventoryManager inventoryManager;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
        inventoryManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>();
    }

    public void Chop()
    {
        inventoryManager.AddItem(droppedItem);
        oldCol = new Color(1, 1, 1, 1);
        newCol = new Color(1, 1, 1, 0);
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        if (isDoing == false)
        {
            isDoing = true;
            
            StartCoroutine(rotateTree(5.0f));

            isDoing = false;

            //int droppedAmount = Random.Range(3, 6);
            //
            //for (int a = 0; a <= droppedAmount; a++)
            //{
            //    inventoryManager.AddItem(droppedItem);
            //}
            //inventoryManager.AddItem(droppedItem);
        }
    }

    private IEnumerator rotateTree(float time)
    {
        playerAnimator.SetBool("IsChopping", true);
        float i = 0;
        float rate = 1 / time;

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            yield return 0;
        }
        
        playerAnimator.SetBool("IsChopping", false);

        i = 0;
        rate = 1 / time;

        Transform oldRotation = this.transform;
        Transform newRotation = fallenPreset.transform;


        while (i < 1)
        {
            i += Time.deltaTime * rate;
            this.transform.rotation = Quaternion.Lerp(oldRotation.localRotation, newRotation.localRotation, Time.deltaTime * rate);
            this.transform.position = Vector2.Lerp(oldRotation.position, newRotation.position, i);
            yield return 0;
        }

        time = 3f;

        i = 0;
        rate = 1 / time;

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            spriteRenderer.color = Color.Lerp(oldCol, newCol, i);
            yield return 0;
        }

        
    }

    public void Mine()
    {

    }

    public void Harvest()
    {

    }
}
