using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Button                   defaultSettingsButton;

    // Delayed text display
    public DelayedTextDisplay       delayedTextDisplay;

    private void OnEnable() {
        // Display text
        delayedTextDisplay.DisplayText("Welcome to the options menu!");

        // Set selected game object to null
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Start() {
        gameObject.SetActive(false);
    }

    // On click of defaultSettingsButton, returns all menu settings to their default value
    public void DefaultSettings() {
        // Reset slider and dropdown values
        GameManager.S.mainMenuCS.playerHeightSlider.value = 168;
        GameManager.S.mainMenuCS.levelSelectDropdown.value = 0;
        GameManager.S.mainMenuCS.alleyAmountDropdown.value = 0;

        // Reset alley amount
        GameManager.S.mainMenuCS.SetAlleyAmount(3);

        // Reset spawn speed
        GameManager.S.spawner.timeDuration = 2.0f;

        // Reset scene colors
        GameManager.color.ResetPalette();

        // Set display text colors
        GameManager.color.SetDisplayTextPalette();

        // Delayed text display
        delayedTextDisplay.DisplayText("Options set to their default values!");
    }

    //
    public void GoToMainMenuButton() {
        // Deactivate this menu
        gameObject.SetActive(false);

        // Activate keyboard input menu
        GameManager.S.mainMenuGO.SetActive(true);
    }
}