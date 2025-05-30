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
    [SerializeField] private NightCycle nightCycle;
    private InputControl playerInput;
    private PlaceCrop cropScript;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cropScript = player.GetComponent<PlaceCrop>();
        playerInput = player.GetComponent<InputControl>();
        playerAnimator = player.GetComponent<Animator>();
        inventoryManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>();
        nightCycle = GameObject.FindGameObjectWithTag("EdittyYay").GetComponent<NightCycle>();
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
            //playerInput.enabled = false;
            StartCoroutine(treeFall(5f));
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
            this.transform.rotation = Quaternion.Lerp(oldRotation.localRotation, newRotation.localRotation, i);
            this.transform.position = Vector2.Lerp(oldRotation.position, newRotation.position, i);
            yield return 0;
        }

        time = 2f;

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

        //playerInput.enabled = true;

        this.gameObject.SetActive(false);
        
    }

    public void Mine()
    {

    }

    public void Harvest()
    {
        playerAnimator.SetBool("IsHoeing", true);

        StartCoroutine(Timer(2f));

        playerAnimator.SetBool("IsHoeing", false);

        cropScript.HarvestCrop();

        AddItems();
    }

    public void Refill()
    {
        playerAnimator.SetBool("IsWatering", true);

        StartCoroutine(Timer(2f));

        playerAnimator.SetBool("IsWatering", false);

        inventoryManager.GetSelectedItem(false).usesLeft = 25;
    }

    public void Sleep()
    {
        playerInput.overide = true;
        nightCycle.isSleeping = true;
    }

    private IEnumerator Timer(float time)
    {
        float i = 0;
        float rate = 1 / time;

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            yield return 0;
        }
    }
}
