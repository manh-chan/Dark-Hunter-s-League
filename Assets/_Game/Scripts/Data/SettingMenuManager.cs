using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenuManager : MonoBehaviour
{
    public TMP_Dropdown graphicsDrops;
    public Slider masterVolume;
    public Slider musicVolume;
    public Slider sFXVolume;
    private void Start()
    {
        graphicsDrops.value = (int)PlayerPrefs.GetFloat("graphicsQuality", 1f);
        masterVolume.value = PlayerPrefs.GetFloat("VolumeMaster", 1f);
        musicVolume.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sFXVolume.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }
    public void ChangeGraphicsQuality() {
        QualitySettings.SetQualityLevel(graphicsDrops.value);
        PlayerPrefs.SetFloat("graphicsQuality", graphicsDrops.value);
        PlayerPrefs.Save();
    }
    public void SetMasterVolume()
    {
        AudioManager.Instance.SetMasterVolume(masterVolume.value);
    }

    public void SetMusicVolume()
    {
        AudioManager.Instance.SetMusicVolume(musicVolume.value);
    }

    public void SetSFXVolume()
    {
        AudioManager.Instance.SetSFXVolume(sFXVolume.value);
    }
}
