using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class NightCycle : MonoBehaviour
{

    [Header("Misc")]

    [SerializeField] private TextMeshProUGUI daysCount;

    public bool isNight = false;
    private bool isRunning = false;
    public bool isSleeping = false;

    private int daysNum;

    [Header("Global")]

    [SerializeField] private Light2D lightCol;

    private Color currentCol;
    private Color dayColour = new Color(0.7783019f, 0.8361362f,1f,1f);
    private Color nightColour = new Color(0f, 0.03227407f, 0.1226415f, 1f);

    [Header("Windows")]

    public Light2D[] windows;

    private Color currentWinCol;
    private Color dayWindow = new Color(0.8196079f, 0.6680537f, 0.0509804f, 1f);
    private Color nightWindow = new Color(0.05032932f, 0.4211291f, 0.8207547f, 1f);

    [Header("Room Global")]

    [SerializeField] private Light2D roomCol;

    private float currentRoomCol;
    private float dayRoomCol = 0.25f;
    private float nightRoomCol = 0.05f;

    [Header("Beams")]

    [SerializeField] private GameObject dayLightBeam;
    [SerializeField] private GameObject nightLightBeam;
    [SerializeField] private GameObject leftBeam;
    [SerializeField] private GameObject rightBeam;

    private Light2D dayLightBeamCol;
    private Light2D nightLightBeamCol;

    private Color dayCurrentBeamCol;
    private Color nightCurrentBeamCol;
    private Color newBeamCol;
    private Color dayBeamCol = new Color(1f, 0.5f, 0f, 1f);
    private Color nightBeamCol = new Color(0f, 0.5f, 1f, 1f);

    private Quaternion currentBeamPos;
    private Quaternion newBeamPos;
    private GameObject currentBeam;
    private GameObject oppositeBeam;
    private Quaternion oppBeamPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dayLightBeamCol = dayLightBeam.GetComponent<Light2D>();
        nightLightBeamCol = nightLightBeam.GetComponent<Light2D>();
        daysNum = 1;
        StartCoroutine(CycleLength(10f));
    }

    // Update is called once per frame
    void Update()
    {
        if (isNight == true && isRunning == false)
        {
            daysNum++;
            daysCount.text = ("Day " + daysNum);

            StartCoroutine(ToDay(3f));
        }

        else if (isNight == false && isRunning == false)
        {
            StartCoroutine(ToNight(3f));
        }

        else if (isSleeping == true && isNight == true)
        {
            StopAllCoroutines();

            daysNum++;
            daysCount.text = ("Day " + daysNum);

            StartCoroutine(ToDay(1f));
        }

    }

    private IEnumerator ToNight(float time)
    {
        isNight = true;
        isRunning = true;

        currentCol = lightCol.color;
        currentRoomCol = roomCol.intensity;
        dayCurrentBeamCol = dayLightBeamCol.color;
        nightCurrentBeamCol = nightLightBeamCol.color;

        newBeamCol = new Color(dayCurrentBeamCol.r, dayCurrentBeamCol.g, dayCurrentBeamCol.b, 0f);

        foreach (Light2D window in windows)
        {
            currentWinCol = window.color;
        }

        float i = 0;
        float rate = 1 / time;

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            lightCol.color = Color.Lerp(currentCol, nightColour, i);
            roomCol.intensity = Mathf.Lerp(currentRoomCol, nightRoomCol, i);
            dayLightBeamCol.color = Color.Lerp(dayCurrentBeamCol, newBeamCol, i);
            nightLightBeamCol.color = Color.Lerp(nightCurrentBeamCol, nightBeamCol, i);

            foreach (Light2D window in windows)
            {
                window.color = Color.Lerp(currentWinCol, nightWindow, i);
            }

            yield return 0;
        }

        StartCoroutine(CycleLength(5f));
    }

    private IEnumerator ToDay(float time)
    {
        isNight = false;
        isSleeping = false;
        isRunning = true;

        currentCol = lightCol.color;
        currentRoomCol = roomCol.intensity;
        nightCurrentBeamCol = nightLightBeamCol.color;
        dayCurrentBeamCol = dayLightBeamCol.color;

        newBeamCol = new Color(nightCurrentBeamCol.r, nightCurrentBeamCol.g, nightCurrentBeamCol.b, 0f);

        foreach (Light2D window in windows)
        {
            currentWinCol = window.color;
        }

        float i = 0;
        float rate = 1 / time;

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            lightCol.color = Color.Lerp(currentCol, dayColour, i);
            roomCol.intensity = Mathf.Lerp(currentRoomCol, dayRoomCol, i);
            nightLightBeamCol.color = Color.Lerp(nightCurrentBeamCol, newBeamCol, i);
            dayLightBeamCol.color = Color.Lerp(dayCurrentBeamCol, dayBeamCol, i);

            foreach (Light2D window in windows)
            {
                window.color = Color.Lerp(currentWinCol, dayWindow, i);
            }

            yield return 0;
        }

        StartCoroutine(CycleLength(5f));
    }

    private IEnumerator CycleLength(float time)
    {
        isRunning = true;

        float i = 0;
        float rate = 1 / time;

        newBeamPos = rightBeam.transform.rotation;

        if (isNight == false) // if day
        {
            currentBeam = dayLightBeam;
            oppositeBeam = nightLightBeam;
            oppBeamPos = oppositeBeam.transform.rotation;
            currentBeamPos = leftBeam.transform.rotation;
        }

        else if (isNight == true) // if night
        {
            currentBeam = nightLightBeam;
            oppositeBeam = dayLightBeam;
            oppBeamPos = oppositeBeam.transform.rotation;
            currentBeamPos = leftBeam.transform.rotation;
        }

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            currentBeam.transform.rotation = Quaternion.Lerp(currentBeamPos, newBeamPos, i);
            oppositeBeam.transform.rotation = Quaternion.Lerp(oppBeamPos, leftBeam.transform.localRotation, i);
            yield return 0;
        }

        isRunning = false;
    }
}
