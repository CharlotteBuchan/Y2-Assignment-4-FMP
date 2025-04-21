using UnityEngine;

public class Adult : MonoBehaviour
{

    NpcToPlayer npcToPlayer;
    FollowStop followStop;
    [SerializeField] private string targetTag;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npcToPlayer = this.GetComponent<NpcToPlayer>();

        npcToPlayer.target = GameObject.FindGameObjectWithTag(targetTag);

        followStop = this.GetComponent<FollowStop>();

        followStop.target = GameObject.FindGameObjectWithTag(targetTag);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
