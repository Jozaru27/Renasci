using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider musicSlider;


    void Start()
    {
        // Default Values
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);

        // Apply Default Values
        SetMasterVolume(masterSlider.value);
        SetSFXVolume(sfxSlider.value);
        SetMusicVolume(musicSlider.value);

        // Assign Events
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    public void SetMasterVolume(float volume)
    {
        if (volume <= -75)
            audioMixer.SetFloat("MasterVolume", -80);
        else
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        if (volume <= -75)
            audioMixer.SetFloat("SFXVolume", -80);
        else
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        if (volume <= -75)
            audioMixer.SetFloat("MusicVolume", -80);
        else
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}
