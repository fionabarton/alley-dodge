using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Acts as a menu to access ancillary features such as audio settings, resetting save data, altering the game algorithm etc.
public class MoreMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Button           audioButton;
    public Button           algorithmButton;
    public Button           instructionsButton;
    public Button           savedDataButton;
    public Button           creditsButton;
    public Button           mainMenuButton;

    // Audio, Programmer, Instructions, Saved Data, Credits
    public List<GameObject> menuGOs;
    public List<Button>     menuButtons;

    //
    public List<GameObject> cursorGO;

    // Delayed text display
    public DelayedTextDisplay delayedTextDisplay;

    void Start() {
        // Add listeners to buttons
        audioButton.onClick.AddListener(delegate { ActivateMenuGO(0); });
        algorithmButton.onClick.AddListener(delegate { ActivateMenuGO(1); });
        instructionsButton.onClick.AddListener(delegate { ActivateMenuGO(2); });
        savedDataButton.onClick.AddListener(delegate { ActivateMenuGO(3); });
        creditsButton.onClick.AddListener(delegate { ActivateMenuGO(4); });
        mainMenuButton.onClick.AddListener(delegate { BackToMainMenuButton(); });

        Invoke("ActivateAlgorithmMenu", 0.1f);

        gameObject.SetActive(false);
    }

    void ActivateAlgorithmMenu() {
        ActivateMenuGO(1);
    }

    //
    void ActivateMenuGO(int ndx) {
        // Ensure gameplay algorithm is set to valid values
        if (GameManager.S.algorithmMenuCS.CheckAllButtonsForValidValues()) {
            // Deactivate all menus
            GameManager.utilities.SetActiveList(menuGOs, false);

            // Activate selected menu
            menuGOs[ndx].SetActive(true);

            // Reset all menu buttons' colorss
            for (int i = 0; i < menuButtons.Count; i++) {
                ColorBlock colors = menuButtons[i].colors;
                colors.normalColor = Color.white;
                colors.highlightedColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 1);
                menuButtons[i].colors = colors;
            }

            // Set selected menu button's colors
            ColorBlock c = menuButtons[ndx].colors;
            c.normalColor = new Color(1, 0.7843137f, 0, 1);
            c.highlightedColor = new Color(0.7264151f, 0.5697374f, 0, 1);
            menuButtons[ndx].colors = c;

            // Set cursor positions
            GameManager.utilities.PositionCursor(cursorGO[0], menuButtons[ndx].gameObject, -125f, 0, 0);
            GameManager.utilities.PositionCursor(cursorGO[1], menuButtons[ndx].gameObject, 125f, 0, 2);
        } else {
            // Audio: Damage
            GameManager.audioMan.PlayRandomDamageSFX();
        }
    }

    //
    public void BackToMainMenuButton() {
        //
        if (GameManager.S.algorithmMenuCS.CheckAllButtonsForValidValues()) {
            // Deactivate this menu
            gameObject.SetActive(false);

            // Activate Main Menu
            GameManager.S.mainMenuGO.SetActive(true);

            //
            GameManager.S.previouslyHighlightedGO = null;
        } else {
            // Audio: Damage
            GameManager.audioMan.PlayRandomDamageSFX();
        }
    }
}