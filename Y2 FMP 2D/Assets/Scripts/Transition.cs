using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Transition : MonoBehaviour
{
    [SerializeField] GameObject transitionUI;
    [SerializeField] GameObject teleportTo;
    [SerializeField] GameObject player;
    [SerializeField] GameObject vignetteImage;
    [SerializeField] bool activeInactive;
    private PlayerInput playerInput;

    Vector3 smallSize = new Vector3(0.1f, 0.1f, 0.1f);
    Vector3 bigSize = new Vector3(3.5f, 3.5f, 3.5f);

    private void Start()
    {
        playerInput = player.gameObject.GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("YAY");
            Debug.Log(playerInput);
            transitionUI.SetActive(true);
            playerInput.enabled = false;
            StartCoroutine(ScaleDownAnimation(2.0f));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(ScaleUpAnimation(2.0f));
    }

    private IEnumerator ScaleDownAnimation(float time)
    {
        float i = 0;
        float rate = 1 / time;

        Vector3 fromScale = Vector3.Scale(bigSize, bigSize);
        Vector3 toScale = Vector3.Scale(smallSize, smallSize);

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            transitionUI.transform.localScale = Vector3.Lerp(fromScale, toScale, i);
            yield return 0;
        }

        TeleportTo();
    }

    private IEnumerator ScaleUpAnimation(float time)
    {
        if (vignetteImage != null)
        {
            vignetteImage.SetActive(activeInactive);
        }
        float i = 0;
        float rate = 1 / time;

        Vector3 fromScale = Vector3.Scale(smallSize, smallSize);
        Vector3 toScale = Vector3.Scale(bigSize, bigSize);

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            transitionUI.transform.localScale = Vector3.Lerp(fromScale, toScale, i);
            yield return 0;
        }

        transitionUI.SetActive(false);

        playerInput.enabled = true;
    }

    private void TeleportTo()
    {
        if (teleportTo != null)
        {
            player.transform.localPosition = teleportTo.transform.position;
        }
        else
        {
            return;
        }
    }
}
