using Unity.VisualScripting;
using UnityEngine;

public class NpcToPlayer : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    public bool isHolding;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 distance = (transform.position - player.transform.position);
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
