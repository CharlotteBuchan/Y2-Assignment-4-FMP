using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FontDilate : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        this.tmp.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, -1f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float textDilation = Mathf.Lerp(-1f, 0.1f, time / 2);
        this.tmp.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, textDilation);
    }
}