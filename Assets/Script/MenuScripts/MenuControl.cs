using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuControl : MonoBehaviour
{
    //Menu Controller variable
    public static MenuControl instance;

    //Main menu buttons
    public Button startButton;
    public Button optionsButton;
    public Button exitButton;
    public Button optionsBackButton;
    public Button SaveSettingsButton;
    //The options menu panel as a GameObject
    public GameObject optionsPanel;

    //String name of the main scene to be loaded from the start button
    public string mainScene = "SampleScene";
    private bool optionsMenuActive;


    // Awake is called before Start
    void Awake() {
        //If we don't have an instance of the GameController, create one and assign it to THIS Controller
        if(instance == null) {
            instance = this;
        }
        //If we have another GameController active, delete it and replace it with this one
        //(Enforces singleton pattern)
        else if(instance != this) {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start() {
        //Initialize the onClick methods of the buttons
        startButton.onClick.AddListener(() => startGame());
        optionsButton.onClick.AddListener(() => toggleOptions());
        optionsBackButton.onClick.AddListener(() => toggleOptions());
        exitButton.onClick.AddListener(() => exitGame());
        SaveSettingsButton.onClick.AddListener(() => saveSettings());
    }


    //Starts up the game's main scene and unload the main menu
    void startGame() {
        SceneManager.LoadScene(mainScene);
        SceneManager.UnloadSceneAsync("MainMenu");
    }

    //Toggles the options menu on and off (triggered by back and options)
    public void toggleOptions() {
        //If the panel is active, disable it
        if(optionsPanel.activeSelf) {
            optionsPanel.SetActive(false);
            optionsPanel.GetComponent<SettingsControl>().LoadSettings(optionsPanel.GetComponent<SettingsControl>().GetResolution());
        }
        //If the panel is inactive, enable it
        else {
            optionsPanel.SetActive(true);
            optionsPanel.GetComponent<SettingsControl>().LoadSettings(optionsPanel.GetComponent<SettingsControl>().GetResolution());
        }
    }

    //Saves the settings and goes back to the main menu
    void saveSettings() {
        optionsPanel.GetComponent<SettingsControl>().SaveSettings();
        toggleOptions();
    }

    //Exits the game
    void exitGame() => Application.Quit();
}
