using UnityEngine;

public class ActivateGO : MonoBehaviour
{

    [SerializeField] private GameObject activatedGO;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if(activatedGO.activeSelf == false)
            {
                activatedGO.SetActive(true);
            }

            else
            {
                activatedGO.SetActive(false);
            }

        }
    }


}
