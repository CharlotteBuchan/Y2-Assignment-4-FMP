using UnityEngine;

public class EggProcess : MonoBehaviour
{
    private float timer;
    void Start()
    {
        timer = 60f;
    }
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Debug.Log("done timer");
        }
    }
}
