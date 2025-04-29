using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class EggProcess : MonoBehaviour
{
    private float timer;
    private bool isDone;
    [SerializeField] SpriteRenderer pulseOverlay;
    private Color oldCol;
    private Color newCol;
    void Start()
    {
        timer = 5f;
        isDone = false;

        oldCol = new Color(1, 1, 1, 0);
        newCol = new Color(1, 1, 1, 0.5f);
    }
    void Update()
    {
        if (isDone == false)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                Debug.Log("Donesies..");
                isDone = true;
                StartCoroutine(Pulsate(1f));
            }
        }
    }

    private IEnumerator Pulsate(float time)
    {
        float i = 0;
        float rate = 1 / time;
        float t = 0;

        while (t < 5)
        {
            i = 0;
            rate = 1 / time;

            while (i < 1)
            {
                Debug.Log("Start While");
                i += Time.deltaTime * rate;
                pulseOverlay.color = Color.Lerp(oldCol, newCol, i);
                Debug.Log("End While");
                yield return 0;
            }

            i = 0;
            rate = 1 / time;

            while (i < 1)
            {
                Debug.Log("Start While");
                i += Time.deltaTime * rate;
                pulseOverlay.color = Color.Lerp(newCol, oldCol, i);
                Debug.Log("End While");
                yield return 0;
            }

            t++;
        }

        Debug.Log("Done");
    }
    private IEnumerator PulsateDown(float time)
    {
        float i = 0;
        float rate = 1 / time;

        while (i < 1)
        {
            Debug.Log("Start While");
            i += Time.deltaTime * rate;
            pulseOverlay.color = Color.Lerp(newCol, oldCol, i);
            Debug.Log("End While");
            yield return 0;
        }
        Debug.Log("Done");

    }
}
