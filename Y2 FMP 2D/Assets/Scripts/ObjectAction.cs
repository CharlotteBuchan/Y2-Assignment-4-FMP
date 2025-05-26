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
    [SerializeField] private int minDrops;
    [SerializeField] private int maxDrops;
    private int droppedAmount;
    public Item droppedItem;
    private InventoryManager inventoryManager;


    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
        inventoryManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>();
    }

    public void AddItems()
    {
        Debug.Log("dROPPING?");
        droppedAmount = Random.Range(minDrops, maxDrops);
        int i = 0;
        while (i < droppedAmount)
        {
            inventoryManager.AddItem(droppedItem);
            i++;
        }
    }

    public void Chop()
    {
        oldCol = new Color(1, 1, 1, 1);
        newCol = new Color(1, 1, 1, 0);
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        if (isDoing == false)
        {
            isDoing = true;
            
            StartCoroutine(treeFall(5.0f));

            //int droppedAmount = Random.Range(3, 6);
            //
            //for (int a = 0; a <= droppedAmount; a++)
            //{
            //    inventoryManager.AddItem(droppedItem);
            //}
            //inventoryManager.AddItem(droppedItem);
        }
    }

    private IEnumerator treeFall(float time)
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

        isDoing = false;

        AddItems();

        Destroy(this); 
        
    }

    public void Mine()
    {

    }

    public void Harvest()
    {

    }
}
