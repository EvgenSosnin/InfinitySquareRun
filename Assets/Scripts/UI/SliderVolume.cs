using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    public AudioSource audioSource;

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        OnValueChanged();
    }

    public void OnValueChanged()
    {
        audioSource.volume = slider.value;
    }

    void OnDestroy()
    {
        PlayerPrefs.SetFloat("MusicVolume", slider.value);
    }
}
