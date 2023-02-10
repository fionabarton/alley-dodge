using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Acts as a menu to access ancillary features such as audio settings, instructions, controls, etc.
public class MoreMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Button       audioButton;
    public Button       programmerButton;
    public Button       instructionsButton;
    public Button       controlsButton;
    public Button       tutorialButton;
    public Button       helpButton;
    public Button       mainMenuButton;

    // Audio, Programmer, Instructions, Controls, Tutorial, Help
    public List<GameObject> menuGOs;

    //public List<GameObject> cursorGO;

    void Start() {
        //
        //

        // Add listeners to buttons
        audioButton.onClick.AddListener(delegate { ActivateMenuGO(0); });
        programmerButton.onClick.AddListener(delegate { ActivateMenuGO(1); });
        instructionsButton.onClick.AddListener(delegate { ActivateMenuGO(2); });
        controlsButton.onClick.AddListener(delegate { ActivateMenuGO(3); });
        tutorialButton.onClick.AddListener(delegate { ActivateMenuGO(4); });
        helpButton.onClick.AddListener(delegate { ActivateMenuGO(5); });
        mainMenuButton.onClick.AddListener(delegate { BackToMainMenuButton(); });

        Invoke("ActivateAudioMenu", 0.1f);

        gameObject.SetActive(false);
    }

    void ActivateAudioMenu() {
        ActivateMenuGO(0);
    }

    //
    void ActivateMenuGO(int ndx) {
        // Deactivate all menus
        GameManager.utilities.SetActiveList(menuGOs, false);

        // Activate selected menu
        menuGOs[ndx].SetActive(true);
    }

    //
    public void BackToMainMenuButton() {
        // Deactivate this menu
        gameObject.SetActive(false);

        // Activate Main Menu
        GameManager.S.mainMenuGO.SetActive(true);

        //
        GameManager.S.previouslyHighlightedGO = null;
    }
}