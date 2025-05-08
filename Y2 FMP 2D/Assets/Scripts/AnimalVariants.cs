using UnityEngine;

public class AnimalVariants : MonoBehaviour
{
    public RuntimeAnimatorController[] variantType;
    public bool random;
    private Animator animator;
    [HideInInspector] public int ranVariantNum;
    [HideInInspector] public int variantNum = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (random == true)
        {
            animator = GetComponentInParent<Animator>();
            ranVariantNum = Random.Range(0, variantType.Length);
            animator.runtimeAnimatorController = variantType[ranVariantNum];
        }

        else if (random == false)
        {
            animator = GetComponentInParent<Animator>();
            animator.runtimeAnimatorController = variantType[variantNum];
        }

        else
        {
            Debug.Log("nuhuh");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
