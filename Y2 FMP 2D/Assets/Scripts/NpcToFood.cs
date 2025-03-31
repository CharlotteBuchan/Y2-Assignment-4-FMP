using UnityEngine;

public class NPCtoFood : MonoBehaviour
{

    [SerializeField] private GameObject foodTrough;
    [SerializeField] private float speed;

    private float distance;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        distance = Vector2.Distance(transform.position, foodTrough.transform.position);
        Vector2 direction = foodTrough.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, foodTrough.transform.position, speed * Time.deltaTime);
    }
}
