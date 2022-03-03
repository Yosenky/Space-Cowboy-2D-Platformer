using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    //Menu Controller variable
    public static MenuControl instance;

    //Main menu buttons
    public Button startButton;
    public Button optionsButton;
    public Button exitButton;
    public Button optionsBackButton;
    //The options menu panel as a GameObject
    public GameObject optionsPanel;

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
    }


    //Starts up the game's main scene
    void startGame() => SceneManager.LoadScene(mainScene);

    //Toggles the options menu on and off (triggered by back and options)
    void toggleOptions() {
        //If the panel is active, disable it
        if(optionsPanel.activeSelf) {
            optionsPanel.SetActive(false);
        }
        //If the panel is inactive, enable it
        else {
            optionsPanel.SetActive(true);
        }
    }

    //Exits the game
    void exitGame() => Application.Quit();
}
