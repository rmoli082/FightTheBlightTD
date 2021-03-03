using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeMixer : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider fxSlider;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Music Volume", 0.75f);
        fxSlider.value = PlayerPrefs.GetFloat("FX Volume", 0.5f);
    }

    public void SetMusicVol(float sliderVal)
    {
        mixer.SetFloat("musicVol", Mathf.Log10(sliderVal) * 20);
        PlayerPrefs.SetFloat("Music Volume", sliderVal);
    }

    public void SetFxVol(float sliderVal)
    {
        mixer.SetFloat("fxVol", Mathf.Log10(sliderVal) * 20);
        PlayerPrefs.SetFloat("FX Volume", sliderVal);
    }
}
