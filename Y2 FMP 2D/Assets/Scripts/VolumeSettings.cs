using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;
    private string volumeParameterName;

    private void Start()
    {

    }

    private void VolumeUpdate(float sliderValue, string volumeParameterName)
    {
        if ( sliderValue < 1)
        {
            sliderValue = 0.001f;
        }

        //float dbValue = Mathf.Log10(sliderValue/100) * 20;

        RefreshSlider(sliderValue);

        audioMixer.SetFloat(volumeParameterName, Mathf.Log10(sliderValue / 100) * 20);
    }

    private void RefreshSlider(float sliderValue)
    {
        slider.value = sliderValue;
    }

    public void SetVolume(string volumeParameterName)
    {
        VolumeUpdate(slider.value, volumeParameterName);
    }
}