using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HatchBaby : MonoBehaviour
{
    [Header("Hatch")]

    [Header("Find")]
    [SerializeField] string targetTag;
    [SerializeField] private GameObject player;
    private float maxDistanceToAdult = 50;
    private GameObject aboveOB;
    private NpcToPlayer npcScript;
    private FollowStop followStop;
    private GameObject nearestAdult;

    [Header("Grow")]
    [SerializeField] private GameObject adultPrefab;
    [SerializeField] public bool isAdult;
    private bool bigEnough;
    private Animator animator;
    public Vector3 updateSize = new Vector3(0.125f, 0.125f, 0.125f);
    private float time;


    private void Start()
    {
        npcScript = GetComponent<NpcToPlayer>();
        followStop = GetComponent<FollowStop>();
        animator = GetComponent<Animator>();
        if (isAdult == false)
        {
            FindClosestAdult();
            FindAboveGO();
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAdult == false)
        {
            if (nearestAdult == null)
            {
                npcScript.isActive = false;
                followStop.enabled = false;

                FindClosestAdult();
            }

            if ((npcScript.target == null) || (followStop.target == null))
            {
                aboveOB = FindAboveGO();

                if (aboveOB != null)
                {
                    npcScript.target = aboveOB;
                    followStop.target = aboveOB;
                }

                else
                {
                    npcScript.target = nearestAdult;
                    followStop.target = nearestAdult;
                }
            }
        }
    }

    void FindClosestAdult()
    {
        GameObject[] adults = GameObject.FindGameObjectsWithTag(targetTag);
        nearestAdult = null;

        foreach (GameObject adult in adults)
        {
            float distance = Vector2.Distance(transform.position, adult.transform.position);

            if (distance < maxDistanceToAdult)
            {
                maxDistanceToAdult = distance;
                nearestAdult = adult;
            }
        }

        if (nearestAdult != null)
        {

            this.npcScript.isActive = true;
            this.followStop.enabled = true;

            this.transform.SetParent(nearestAdult.transform);

            aboveOB = FindAboveGO();

            if (aboveOB != null)
            {
                npcScript.target = aboveOB;
                followStop.target = aboveOB;
            }

            else
            {
                npcScript.target = nearestAdult;
                followStop.target = nearestAdult;
            }
        }
    }

    GameObject FindAboveGO()
    {
        Transform parentTransform = transform.parent;

        if (parentTransform == null)
        {
            return null;
        }

        for (int i = 1; i < parentTransform.childCount; i++)
        {
            Transform sibling = parentTransform.GetChild(i);

            if (sibling == transform)
            {
                return parentTransform.GetChild(i - 1).gameObject;
            }
        }

        return null;
    }

    public void AgeUp()
    {
        if ((this.transform.localScale.x < 1f) && (isAdult == false))
        {
            Vector3 newSize = this.transform.localScale + updateSize;

            StartCoroutine(ScaleUp(5.0f, newSize));
        }

        else if ((this.transform.localScale.x >= 1f) && (isAdult == false))
        {
            Vector3 newSize = new Vector3(1f, 1f, 1);

            bigEnough = true;

            StartCoroutine(ScaleUp(1.0f, newSize));
        }

        else if ((this.transform.localScale.x < 1) && (isAdult == true))
        {
            Vector3 newSize = new Vector3(1, 1, 1);

            bigEnough = false;

            StartCoroutine(ScaleUp(5.0f, newSize));
        }

        else
        {
            return;
        }
    }

    private IEnumerator ScaleUp(float time, Vector3 sizeTo)
    {
        float i = 0;
        time = 2f;
        float rate = 1 / time;

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, sizeTo, i);
            yield return 0;
        }

        if (bigEnough == true)
        {
            GameObject newAdult = Instantiate(adultPrefab, this.transform.position, Quaternion.identity);

            newAdult.transform.localScale = new Vector3(0.75f, 0.75f, 1);

            AnimalVariants currentVariant = this.GetComponentInChildren<AnimalVariants>();
            AnimalVariants adultVariant = newAdult.GetComponentInChildren<AnimalVariants>();

            newAdult.GetComponent<HatchBaby>().isAdult = true;

            adultVariant.random = false;

            adultVariant.variantNum = currentVariant.ranVariantNum;

            GameObject animalSet = GameObject.Find("Animals");

            newAdult.transform.parent = animalSet.transform;

            Destroy(this.gameObject);
        }
    }
}
