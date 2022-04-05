using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Slider volumeSlider;
    Resolution[] resolutions;

    // Start is called before the first frame update
    async void Start()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        LoadSettings(currentResolutionIndex);
    }

    public void SetVolume()
    {
        audioMixer.SetFloat("Volume", volumeSlider.value);
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public int GetResolution() {
        int currentResolutionIndex = 0;
        resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
                currentResolutionIndex = i;
                Debug.Log("GetResolution() " + currentResolutionIndex);
                break;
            }
        }

        return currentResolutionIndex;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        SetResolution(resolutionDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", Convert.ToInt32(fullscreenToggle.isOn));
        PlayerPrefs.SetFloat("VolumePreference", volumeSlider.value);
        PlayerPrefs.Save();
    }

    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("ResolutionPreference")) {
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
            Debug.Log("resolutionDropdown: " + PlayerPrefs.GetInt("ResolutionPreference"));
        } else {
            resolutionDropdown.value = currentResolutionIndex;
        }

        if (PlayerPrefs.HasKey("FullscreenPreference")) {
            Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
            Debug.Log("fullScreen: " + PlayerPrefs.GetInt("FullscreenPreference"));
        } else {
            Screen.fullScreen = true;
        }

        if (PlayerPrefs.HasKey("VolumePreference")) {
            volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
            Debug.Log("volumeSlider: " + PlayerPrefs.GetFloat("VolumePreference"));
        } else {
            volumeSlider.value = 0.5f;
        }
    }
}
