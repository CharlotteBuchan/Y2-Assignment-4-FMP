using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HatchBaby : MonoBehaviour
{
    [Header("Hatch")]

    [Header("Find")]
    [SerializeField] string targetTag;
    private float maxDistanceToAdult = 50;
    private GameObject aboveOB;
    private NpcToPlayer npcScript;
    private FollowStop followStop;
    private GameObject nearestAdult;

    [Header("Grow")]
    [SerializeField] private GameObject adultPrefab;
    [SerializeField] private RuntimeAnimatorController babyAnimator;
    [SerializeField] private RuntimeAnimatorController adultAnimator;
    private bool bigEnough;
    private bool isAdult;
    private Animator animator;
    private float updateSize = 1.15f;
    private float time;


    private void Start()
    {
        npcScript = GetComponent<NpcToPlayer>();
        followStop = GetComponent<FollowStop>();
        animator = GetComponent<Animator>();
        FindClosestAdult();
        FindAboveGO();
    }

    private void Update()
    {
        if (nearestAdult == null)
        {
            npcScript.enabled = false;
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

            this.npcScript.enabled = true;
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
        if ((this.transform.localScale.x < 1.5f) && (animator.runtimeAnimatorController == babyAnimator))
        {
            Vector3 newSize = this.transform.localScale * updateSize;
            StartCoroutine(ScaleUp(5.0f, newSize));
        }

        else if ((this.transform.localScale.x > 1.5f) && (animator.runtimeAnimatorController == babyAnimator))
        {
            Vector3 newSize = new Vector3(0.75f, 0.75f, 1);
            StartCoroutine(ScaleUp(5.0f, newSize));

            animator.runtimeAnimatorController = adultAnimator;
        }

        else if (animator.runtimeAnimatorController == adultAnimator)
        {
            Vector3 newSize = new Vector3(1, 1, 1);

            bigEnough = true;

            StartCoroutine(ScaleUp(5.0f, newSize));
        }

        else
        {
            Debug.Log("Nothing");
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

            Transform animalSet = transform.parent;

            newAdult.transform.parent = animalSet.parent;

            Destroy(this.gameObject);
        }
    }
}
