using UnityEngine;

public class NPCtoPlayer : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private float speed;

    private float distance;

    void Start()
    {

    }


    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
