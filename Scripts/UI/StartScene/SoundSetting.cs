using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private GameObject SettingsUI;
    private const string Master = "Master";
    private const string BGM = "BGM";
    private const string SFX = "SFX";
    private void Start()
    {
        float loadedMasterVolume = PlayerPrefs.GetFloat(Master, 0f);
        float loadedBGMVolume = PlayerPrefs.GetFloat(BGM, 0f);
        float loadedSFXVolume = PlayerPrefs.GetFloat(SFX, 0f);
        audioMixer.SetFloat(Master, loadedMasterVolume);
        audioMixer.SetFloat(BGM, loadedBGMVolume);
        audioMixer.SetFloat(SFX, loadedSFXVolume);
        MasterSlider.value= loadedMasterVolume;
        BGMSlider.value= loadedBGMVolume;
        SFXSlider.value= loadedSFXVolume;
        SettingsUI.SetActive(false);
    }
    public void SetMasterVolume()
    {
        float volume = MasterSlider.value;
        audioMixer.SetFloat(Master, volume);
        PlayerPrefs.SetFloat(Master, volume);
    }
    public void SetBGMVolume()
    {
        float volume = BGMSlider.value;
        audioMixer.SetFloat(BGM, volume);
        PlayerPrefs.SetFloat(BGM, volume);
    }
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        audioMixer.SetFloat(SFX, volume);
        PlayerPrefs.SetFloat(SFX, volume);
    }
}
