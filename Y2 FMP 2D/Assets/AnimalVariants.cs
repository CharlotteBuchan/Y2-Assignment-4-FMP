using UnityEngine;

public class AnimalVariants : MonoBehaviour
{
    public RuntimeAnimatorController[] variantType;
    private Animator animator;
    private int variantNum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        variantNum = Random.Range(0, variantType.Length);
        animator.runtimeAnimatorController = variantType[variantNum];
        Debug.Log(variantNum);
        Debug.Log(animator.runtimeAnimatorController);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
