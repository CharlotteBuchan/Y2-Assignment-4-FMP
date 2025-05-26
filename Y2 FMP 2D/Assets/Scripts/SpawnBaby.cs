using UnityEngine;

public class SpawnBaby : MonoBehaviour
{
    [SerializeField] private GameObject babyPrefab;
    private HatchBaby adultCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        adultCheck = this.GetComponent<HatchBaby>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        if (adultCheck.isAdult == true && adultCheck.bigEnough == true)
        {
            GameObject newBaby = Instantiate(babyPrefab, this.transform.position, Quaternion.identity);
        }
    }
}
