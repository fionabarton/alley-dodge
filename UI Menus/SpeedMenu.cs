using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    // Dropdowns & buttons
    public List<TMPro.TMP_Dropdown> speedDropdowns;

    public Button defaultSettingsButton;

    private void OnEnable() {
        // Display text
        if (Time.time > 0.01f) {
            SetGeneralDisplayTextMessage();  
        }
    }

    private void OnDisable() {
        SavePlayerPrefs();
    }

    void Start() {
        // Get speedDropdowns PlayerPrefs
        if (PlayerPrefs.HasKey("Speed Dropdown 0")) {
            SetStartingObjectSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 0"));
        } else {
            speedDropdowns[0].value = 4; // 5
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 1")) {
            SetAmountToIncreaseObjectSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 1"));
        } else {
            speedDropdowns[1].value = 1; // 0.1f
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 2")) {
            SetStartingSpawnSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 2"));
        } else {
            speedDropdowns[2].value = 19; // 2.0f
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 3")) {
            SetAmountToDecreaseSpawnSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 3"));
        } else {
            speedDropdowns[3].value = 1; // 0.1f
        }

        // Add listener to dropdowns
        speedDropdowns[0].onValueChanged.AddListener(delegate { SetStartingObjectSpeedDropdownValue(speedDropdowns[0].value); });
        speedDropdowns[1].onValueChanged.AddListener(delegate { SetAmountToIncreaseObjectSpeedDropdownValue(speedDropdowns[1].value); });
        speedDropdowns[2].onValueChanged.AddListener(delegate { SetStartingSpawnSpeedDropdownValue(speedDropdowns[2].value); });
        speedDropdowns[3].onValueChanged.AddListener(delegate { SetAmountToDecreaseSpawnSpeedDropdownValue(speedDropdowns[3].value); });

        // Add listeners to button
        defaultSettingsButton.onClick.AddListener(delegate { AddDefaultSettingsConfirmationListeners(); });
    }

    //
    void SetStartingObjectSpeedDropdownValue(int value) {
        GameManager.S.spawner.startingObjectSpeed = value + 1;

        speedDropdowns[0].value = value;
    }

    void SetAmountToIncreaseObjectSpeedDropdownValue(int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.amountToIncreaseObjectSpeed = (valueAsFloat / 10);

        speedDropdowns[1].value = value;
    }

    void SetStartingSpawnSpeedDropdownValue(int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.startingSpawnSpeed = (valueAsFloat / 10) + 0.1f;

        speedDropdowns[2].value = value;
    }

    void SetAmountToDecreaseSpawnSpeedDropdownValue(int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.amountToDecreaseSpawnSpeed = (valueAsFloat / 10);

        speedDropdowns[3].value = value;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Adds functions to the sub menu's yes/no buttons
    void AddDefaultSettingsConfirmationListeners() {
        GameManager.S.subMenuCS.AddListeners(DefaultSettings, "Are you sure that you would like to\nreset this menu's options to their default values?");
    }
    // On 'Yes' button click, returns all menu settings to their default value
    public void DefaultSettings(int yesOrNo = -1) {
        // Deactivate sub menu
        GameManager.S.subMenuGO.SetActive(false);

        // 
        if (yesOrNo == 0) {
            // Set dropdowns to default values
            SetStartingObjectSpeedDropdownValue(4); // 5
            SetAmountToIncreaseObjectSpeedDropdownValue(1); // 0.1f
            SetStartingSpawnSpeedDropdownValue(19); // 2.0f
            SetAmountToDecreaseSpawnSpeedDropdownValue(1); // 0.1f

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Options set to their default values!");
        } else {
            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the speed menu!");
        }
    }

    void SavePlayerPrefs() {
        // Speed dropdowns
        for (int i = 0; i < speedDropdowns.Count; i++) {
            string tString = "Speed Dropdown " + i.ToString();
            PlayerPrefs.SetInt(tString, speedDropdowns[i].value);
        }
    }

    // Sets the DisplayText's message depending on which page of the menu is visible
    void SetGeneralDisplayTextMessage() {
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the speed menu:\nAdjust gameplay settings such object speed\nand how often objects are spawned.");
    }
}