using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer doorOpen;
    [SerializeField] private SpriteRenderer doorClosed;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            doorOpen.enabled = true;
            doorClosed.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            doorOpen.enabled = false;
            doorClosed.enabled = true;
        }
    }
}
