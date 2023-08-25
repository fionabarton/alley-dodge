using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Button objectSpeedButton;
    public Button amountToIncreaseButton;
    public Button spawnSpeedButton;
    public Button amountToDecreaseButton;

    public Button defaultSettingsButton;

    private void OnEnable() {
        // Display text
        if (Time.time > 0.01f) {
            SetGeneralDisplayTextMessage();  
        }
    }

    void Start() {
        // Get speedDropdowns PlayerPrefs
        if (PlayerPrefs.HasKey("Speed Dropdown 0")) {
            SetStartingObjectSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 0"));
        } else {
            SetStartingObjectSpeedDropdownValue(9); // 10
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 1")) {
            SetAmountToIncreaseObjectSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 1"));
        } else {
            SetAmountToIncreaseObjectSpeedDropdownValue(2); // 0.2f
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 2")) {
            SetStartingSpawnSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 2"));
        } else {
            SetStartingSpawnSpeedDropdownValue(19); // 2.0f
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 3")) {
            SetAmountToDecreaseSpawnSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 3"));
        } else {
            SetAmountToDecreaseSpawnSpeedDropdownValue(1); // 0.1f
        }

        // Add listeners to button
        defaultSettingsButton.onClick.AddListener(delegate { AddDefaultSettingsConfirmationListeners(); });
    }

    //
    public void SetStartingObjectSpeedDropdownValue(int value) {
        GameManager.S.spawner.startingObjectSpeed = value + 1;

        PlayerPrefs.SetInt("Speed Dropdown 0", value);
    }

    public void SetAmountToIncreaseObjectSpeedDropdownValue(int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.amountToIncreaseObjectSpeed = (valueAsFloat / 10);

        PlayerPrefs.SetInt("Speed Dropdown 1", value);
    }

    public void SetStartingSpawnSpeedDropdownValue(int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.startingSpawnSpeed = (valueAsFloat / 10) + 0.1f;

        PlayerPrefs.SetInt("Speed Dropdown 2", value);
    }

    public void SetAmountToDecreaseSpawnSpeedDropdownValue(int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.amountToDecreaseSpawnSpeed = (valueAsFloat / 10);

        PlayerPrefs.SetInt("Speed Dropdown 3", value);
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
            // Set to default values
            SetStartingObjectSpeedDropdownValue(9); // 10
            SetAmountToIncreaseObjectSpeedDropdownValue(2); // 0.2f
            SetStartingSpawnSpeedDropdownValue(19); // 2.0f
            SetAmountToDecreaseSpawnSpeedDropdownValue(1); // 0.1f

            // Set button text
            objectSpeedButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "10";
            amountToIncreaseButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "0.2";
            spawnSpeedButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "2.0";
            amountToDecreaseButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "0.1";

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Options set to their default values!");
        } else {
            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the speed menu!");
        }
    }

    // Sets the DisplayText's message depending on which page of the menu is visible
    void SetGeneralDisplayTextMessage() {
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the speed menu:\nAdjust gameplay settings such object speed\nand how often objects are spawned.");
    }
}