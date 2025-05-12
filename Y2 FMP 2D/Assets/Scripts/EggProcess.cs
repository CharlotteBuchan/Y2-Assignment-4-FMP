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
    [SerializeField] private GameObject chickPrefab;
    [SerializeField] private AudioSource eggPop;

    void Start()
    {
        eggPop.Play();

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
                i += Time.deltaTime * rate;
                pulseOverlay.color = Color.Lerp(oldCol, newCol, i);
                yield return 0;
            }

            i = 0;
            rate = 1 / time;

            while (i < 1)
            {
                i += Time.deltaTime * rate;
                pulseOverlay.color = Color.Lerp(newCol, oldCol, i);
                yield return 0;
            }

            t++;
        }

        StartCoroutine(Hatch(1f));
    }

    private IEnumerator Hatch(float time)
    {
        float i = 0;
        float rate = 1 / time;

        i = 0;
        rate = 1 / time;

        Vector3 oldSize = this.transform.localScale;
        Vector3 newSize = new Vector3(3, 3, 3);

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            this.transform.localScale = Vector3.Lerp(oldSize, newSize, i);
            pulseOverlay.color = Color.Lerp(oldCol, newCol, i);
            yield return 0;
        }

        eggPop.Play();

        GameObject newChick = Instantiate(chickPrefab, this.transform.position, Quaternion.identity);

        while (eggPop.isPlaying == true)
        {
            yield return 0;
        }

        Destroy(this.gameObject);
    }
}
