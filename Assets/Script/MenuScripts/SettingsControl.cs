using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsControl : MonoBehaviour
{
    //Audio mixer to change volume of
    public AudioMixer audioMixer;
    //Dropdown of resolution values
    public Dropdown resolutionDropdown;
    //Toggle button for enabling/disabling fullscreen
    public Toggle fullscreenToggle;
    //Volume slider to control audio mixer volume
    public Slider volumeSlider;
    //Resolution array to hold all resolution options for game
    Resolution[] resolutions;

    // Start is called before the first frame update
    async void Start()
    {
        //Clear any previous options from the resolution dropdown
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        //Set resolutions equal to the resolutions options from the Screen class
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        //Iterate through all resolutions and add them to the options list
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            //Set the resolution index to the one the screen is currently set to
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        //Add the options to the dropdown and refresh the shown options
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();

        //Load the settings from PlayerPrefs
        LoadSettings(currentResolutionIndex);
    }

    //Set the volume of the audio mixer to the slider's value (0-1)
    public void SetVolume()
    {
        audioMixer.SetFloat("Volume", volumeSlider.value);
    }

    //Set the fullscreen option to whatever the value of the toggle button is
    public void SetFullscreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    //Get the resolution option from the index value currently selected, set screen resolution to it
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //Get the current screen resolution (unused as of now)
    public int GetResolution() {
        //Initialize current resolution index to 0 and resolutions to Screen resolutions options
        int currentResolutionIndex = 0;
        resolutions = Screen.resolutions;

        //Loop through all the resolutions options and return if our screen matches the width and height
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
                Debug.Log("GetResolution() " + currentResolutionIndex);
                return i;
            }
        }

        //Couldn't find resolution so return 1 (first option in array)
        return 1;
    }

    //Save the player settings to PlayerPrefs and set the resolution after saving
    //(Doesn't sound logical to set resolution here but it occurs after save button is pressed)
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        SetResolution(resolutionDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", Convert.ToInt32(fullscreenToggle.isOn));
        PlayerPrefs.SetFloat("VolumePreference", volumeSlider.value);
        PlayerPrefs.Save();
    }

    //Load the settings in from PlayerPrefs or set them to default options after loading settings menu
    public void LoadSettings(int currentResolutionIndex)
    {
        //If PlayerPrefs has resolution then set the dropdown value to it, if not set it to the index held
        if (PlayerPrefs.HasKey("ResolutionPreference")) {
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
            Debug.Log("resolutionDropdown: " + PlayerPrefs.GetInt("ResolutionPreference"));
        } else {
            resolutionDropdown.value = currentResolutionIndex;
        }

        //If PlayerPrefs has fullscreen option set then load it, else set it to true
        if (PlayerPrefs.HasKey("FullscreenPreference")) {
            Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
            Debug.Log("fullScreen: " + PlayerPrefs.GetInt("FullscreenPreference"));
        } else {
            Screen.fullScreen = true;
        }

        //If PlayerPrefs has volume option set then load it into the slider, else set it to 50%
        if (PlayerPrefs.HasKey("VolumePreference")) {
            volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
            Debug.Log("volumeSlider: " + PlayerPrefs.GetFloat("VolumePreference"));
        } else {
            volumeSlider.value = 0.5f;
        }
    }
}
