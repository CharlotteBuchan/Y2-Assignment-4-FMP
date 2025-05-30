using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CropGrow : MonoBehaviour
{
    [Header("Visual")]

    public Sprite[] phase;
    [SerializeField] private SpriteRenderer waterDrop;
    [SerializeField] private SpriteRenderer shadow;
    [SerializeField] private SpriteRenderer fullIcon;
    public SpriteRenderer plantSprite;

    private Color onCol = new Color(1,1,1,1);
    private Color offCol = new Color(1, 1, 1, 0);
    private Color grownCol = new Color(1, 0.7767295f, 0.8064458f, 1);
    private Color wateredGround = new Color(0.3019608f, 0.2078431f, 0.145098f, 0.3490196f);
    private Color unwateredGround = new Color(0.3019608f, 0.2078431f, 0.145098f, 0f);
    private Color currentCol;
    private Color currentSCol;

    [Header("Scripting")]
    [SerializeField] private float speed;
    [SerializeField] public bool watered;
    private bool fullyGrown;
    private NightCycle dayOrNight;
    private InteractionPreset interactionPreset;
    private ObjectAction objectAction;
    private int p;
    [SerializeField] private Item hoe;

    [Header("Both")]
    private Animator animator;
    private PlayerInput characterController;

    private void Start()
    {
        dayOrNight = GameObject.FindGameObjectWithTag("EdittyYay").GetComponent<NightCycle>();
        plantSprite = this.gameObject.GetComponent<SpriteRenderer>();
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        watered = false;
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        interactionPreset = this.gameObject.GetComponent<InteractionPreset>();
        objectAction = this.gameObject.GetComponent<ObjectAction>();

        p = 0;
        plantSprite.sprite = phase[p];
    }

    private void Update()
    {
        //if (dayOrNight.isNight == true && fullyGrown == false)
        //{
        //    StartCoroutine(UnWatered(2.0f));
        //}

        if (plantSprite.sprite == phase[4])
        {
            fullyGrown = true;
            interactionPreset.itemNeeded = hoe;
            interactionPreset.interactType = InteractionPreset.InteractType.harvest;
        }
    }

    public void Water()
    {
        if (watered == false && fullyGrown == false)
        {
            animator.SetBool("IsWatering", true);
            watered = true;
            StartCoroutine(Watered(2f));
            //characterController.enabled = false;
        }

        else if (fullyGrown == true)
        {
            objectAction.Harvest();
        }
    }

    public IEnumerator GrowTimer(float speed)
    {
        float i = 0;
        float rate = 1 / speed;

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            yield return 0;
        }

        p++;
        plantSprite.sprite = phase[p];

        if (plantSprite.sprite != phase[4])
        {
            StartCoroutine(UnWatered(2f));
        }

        else
        {
            StartCoroutine(FullGrown(2f));
        }

    }

    public IEnumerator Watered(float speed)
    {
        watered = true;
        float i = 0;
        float rate = 1 / speed;

        currentCol = waterDrop.color;
        currentSCol = shadow.color;

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            waterDrop.color = Color.Lerp(currentCol, offCol, i);
            shadow.color = Color.Lerp(currentSCol, wateredGround, i);
            yield return 0;
        }

        StartCoroutine(GrowTimer(45f));

        animator.SetBool("IsWatering", false);
        //characterController.enabled = true;
    }

    private IEnumerator UnWatered(float speed)
    {
        if (fullyGrown == false)
        {
            watered = false;
            float i = 0;
            float rate = 1 / speed;

            currentCol = waterDrop.color;
            currentSCol = shadow.color;

            while (i < 1)
            {
                i += Time.deltaTime * rate;
                waterDrop.color = Color.Lerp(currentCol, onCol, i);
                shadow.color = Color.Lerp(currentSCol, unwateredGround, i);
                yield return 0;
            }
        }
        
    }

    private IEnumerator FullGrown(float speed)
    {
        float i = 0;
        float rate = 1 / speed;

        currentCol = fullIcon.color;

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            fullIcon.color = Color.Lerp(currentCol, grownCol, i);
            yield return 0;
        }
    }
}
