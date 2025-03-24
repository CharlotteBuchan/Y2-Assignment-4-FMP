using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] GameObject transitionUI;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("YAY");
            transitionUI.SetActive(true);
            StartCoroutine(ScaleDownAnimation(2.0f));
        }
    }

    private IEnumerator ScaleDownAnimation(float time)
    {
        float i = 0;
        float rate = 1 / time;
        Vector3 size = new Vector3(0.1f, 0.1f, 0.1f);

        Vector3 fromScale = transitionUI.transform.localScale;
        Vector3 toScale = Vector3.Scale(size, size);

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            transitionUI.transform.localScale = Vector3.Lerp(fromScale, toScale, i);
            yield return 0;
        }
    }
}
