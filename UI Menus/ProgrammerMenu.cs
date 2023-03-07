using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Allows users to adjust the random object instantiation algorithm
public class ProgrammerMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public List<TMPro.TMP_Dropdown> chanceDropdowns;
    public List<TMPro.TMP_Dropdown> spawnDropdowns;

    public List<TMPro.TMP_Dropdown> speedDropdowns;
    public Button                   defaultSettingsButton;

    void Start() {
        // Get top level chanceDropdowns PlayerPrefs
        if (PlayerPrefs.HasKey("Chance Dropdown 0")) {
            chanceDropdowns[0].value = PlayerPrefs.GetInt("Chance Dropdown 0");
        } else {
            chanceDropdowns[0].value = 6; // 30%
        }
        SetChanceDropdownValue(0, chanceDropdowns[0].value); 
        if (PlayerPrefs.HasKey("Chance Dropdown 1")) {
            chanceDropdowns[1].value = PlayerPrefs.GetInt("Chance Dropdown 1");
        } else {
            chanceDropdowns[1].value = 7; // 35%
        }
        SetChanceDropdownValue(1, chanceDropdowns[1].value);
        if (PlayerPrefs.HasKey("Chance Dropdown 4")) {
            chanceDropdowns[4].value = PlayerPrefs.GetInt("Chance Dropdown 4");
        } else {
            chanceDropdowns[4].value = 7; // 35%
        }
        SetChanceDropdownValue(4, chanceDropdowns[4].value);

        // Get mid level chanceDropdowns PlayerPrefs
        if (PlayerPrefs.HasKey("Chance Dropdown 2")) {
            chanceDropdowns[2].value = PlayerPrefs.GetInt("Chance Dropdown 2");
        } else {
            chanceDropdowns[2].value = 10; // 50%
        }
        SetChanceDropdownValue(2, chanceDropdowns[2].value);
        if (PlayerPrefs.HasKey("Chance Dropdown 3")) {
            chanceDropdowns[3].value = PlayerPrefs.GetInt("Chance Dropdown 3");
        } else {
            chanceDropdowns[3].value = 10; // 50%
        }
        SetChanceDropdownValue(3, chanceDropdowns[3].value);
        if (PlayerPrefs.HasKey("Chance Dropdown 5")) {
            chanceDropdowns[5].value = PlayerPrefs.GetInt("Chance Dropdown 5");
        } else {
            chanceDropdowns[5].value = 15; // 75%
        }
        SetChanceDropdownValue(5, chanceDropdowns[5].value);
        if (PlayerPrefs.HasKey("Chance Dropdown 6")) {
            chanceDropdowns[6].value = PlayerPrefs.GetInt("Chance Dropdown 6");
        } else {
            chanceDropdowns[6].value = 5; // 25%
        }
        SetChanceDropdownValue(6, chanceDropdowns[6].value);

        // Get spawnDropdowns PlayerPrefs
        if (PlayerPrefs.HasKey("Spawn Dropdown 0")) {
            spawnDropdowns[0].value = PlayerPrefs.GetInt("Spawn Dropdown 0");
        } else {
            spawnDropdowns[0].value = 0; // Horizontal block
        }
        SetSpawnDropdownValue(0, spawnDropdowns[0].value);
        if (PlayerPrefs.HasKey("Spawn Dropdown 1")) {
            spawnDropdowns[1].value = PlayerPrefs.GetInt("Spawn Dropdown 1");
        } else {
            spawnDropdowns[1].value = 1; // Vertical low block
        }
        SetSpawnDropdownValue(1, spawnDropdowns[1].value);
        if (PlayerPrefs.HasKey("Spawn Dropdown 2")) {
            spawnDropdowns[2].value = PlayerPrefs.GetInt("Spawn Dropdown 2");
        } else {
            spawnDropdowns[2].value = 2; // Vertical high block
        }
        SetSpawnDropdownValue(2, spawnDropdowns[2].value);
        if (PlayerPrefs.HasKey("Spawn Dropdown 3")) {
            spawnDropdowns[3].value = PlayerPrefs.GetInt("Spawn Dropdown 3");
        } else {
            spawnDropdowns[3].value = 3; // Quid pickup
        }
        SetSpawnDropdownValue(3, spawnDropdowns[3].value);
        if (PlayerPrefs.HasKey("Spawn Dropdown 4")) {
            spawnDropdowns[4].value = PlayerPrefs.GetInt("Spawn Dropdown 4");
        } else {
            spawnDropdowns[4].value = 4; // Shield pickup
        }
        SetSpawnDropdownValue(4, spawnDropdowns[4].value);

        // Get speedDropdowns PlayerPrefs
        if (PlayerPrefs.HasKey("Speed Dropdown 0")) {
            speedDropdowns[0].value = PlayerPrefs.GetInt("Speed Dropdown 0");
        } else {
            speedDropdowns[0].value = 4; // 5
        }
        SetStartingObjectSpeedDropdownValue(speedDropdowns[0].value); 
        if (PlayerPrefs.HasKey("Speed Dropdown 1")) {
            speedDropdowns[1].value = PlayerPrefs.GetInt("Speed Dropdown 1");
        } else {
            speedDropdowns[1].value = 0; // 0
        }
        SetAmountToIncreaseObjectSpeedDropdownValue(speedDropdowns[1].value);
        if (PlayerPrefs.HasKey("Speed Dropdown 2")) {
            speedDropdowns[2].value = PlayerPrefs.GetInt("Speed Dropdown 2");
        } else {
            speedDropdowns[2].value = 19; // 2.0f
        }
        SetStartingSpawnSpeedDropdownValue(speedDropdowns[2].value);
        if (PlayerPrefs.HasKey("Speed Dropdown 3")) {
            speedDropdowns[3].value = PlayerPrefs.GetInt("Speed Dropdown 3");
        } else {
            speedDropdowns[3].value = 1; // 0.1f
        }
        SetAmountToDecreaseSpawnSpeedDropdownValue(speedDropdowns[3].value);

        // Add listener to dropdowns
        spawnDropdowns[0].onValueChanged.AddListener(delegate { SetSpawnDropdownValue(0, spawnDropdowns[0].value); });
        spawnDropdowns[1].onValueChanged.AddListener(delegate { SetSpawnDropdownValue(1, spawnDropdowns[1].value); });
        spawnDropdowns[2].onValueChanged.AddListener(delegate { SetSpawnDropdownValue(2, spawnDropdowns[2].value); });
        spawnDropdowns[3].onValueChanged.AddListener(delegate { SetSpawnDropdownValue(3, spawnDropdowns[3].value); });
        spawnDropdowns[4].onValueChanged.AddListener(delegate { SetSpawnDropdownValue(4, spawnDropdowns[4].value); });

        chanceDropdowns[0].onValueChanged.AddListener(delegate { SetChanceDropdownValue(0, chanceDropdowns[0].value); });
        chanceDropdowns[1].onValueChanged.AddListener(delegate { SetChanceDropdownValue(1, chanceDropdowns[1].value); });
        chanceDropdowns[2].onValueChanged.AddListener(delegate { SetChanceDropdownValue(2, chanceDropdowns[2].value); });
        chanceDropdowns[3].onValueChanged.AddListener(delegate { SetChanceDropdownValue(3, chanceDropdowns[3].value); });
        chanceDropdowns[4].onValueChanged.AddListener(delegate { SetChanceDropdownValue(4, chanceDropdowns[4].value); });
        chanceDropdowns[5].onValueChanged.AddListener(delegate { SetChanceDropdownValue(5, chanceDropdowns[5].value); });
        chanceDropdowns[6].onValueChanged.AddListener(delegate { SetChanceDropdownValue(6, chanceDropdowns[6].value); });

        speedDropdowns[0].onValueChanged.AddListener(delegate { SetStartingObjectSpeedDropdownValue(speedDropdowns[0].value); });
        speedDropdowns[1].onValueChanged.AddListener(delegate { SetAmountToIncreaseObjectSpeedDropdownValue(speedDropdowns[1].value); });
        speedDropdowns[2].onValueChanged.AddListener(delegate { SetStartingSpawnSpeedDropdownValue(speedDropdowns[2].value); });
        speedDropdowns[3].onValueChanged.AddListener(delegate { SetAmountToDecreaseSpawnSpeedDropdownValue(speedDropdowns[3].value); });

        // Add listeners to button
        defaultSettingsButton.onClick.AddListener(delegate { AddDefaultSettingsConfirmationListeners(); });
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    void SetChanceDropdownValue(int ndx, int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.chancesToSpawn[ndx] = valueAsFloat / 20;

        chanceDropdowns[ndx].value = value;

        // Save settings
        string tString = "Chance Dropdown " + ndx.ToString();
        PlayerPrefs.SetInt(tString, chanceDropdowns[ndx].value);
    }

    void SetSpawnDropdownValue(int ndx, int value) {
        GameManager.S.spawner.objectsToSpawn[ndx] = value;

        spawnDropdowns[ndx].value = value;

        // Save settings
        string tString = "Spawn Dropdown " + ndx.ToString();
        PlayerPrefs.SetInt(tString, spawnDropdowns[ndx].value);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //
    void SetStartingObjectSpeedDropdownValue(int value) {
        GameManager.S.spawner.startingObjectSpeed = value + 1;

        speedDropdowns[0].value = value;

        // Save settings
        string tString = "Speed Dropdown 0";
        PlayerPrefs.SetInt(tString, speedDropdowns[0].value);
    }

    void SetAmountToIncreaseObjectSpeedDropdownValue(int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.amountToIncreaseObjectSpeed = (valueAsFloat / 10);

        speedDropdowns[1].value = value;

        // Save settings
        string tString = "Speed Dropdown 1";
        PlayerPrefs.SetInt(tString, speedDropdowns[1].value);
    }

    void SetStartingSpawnSpeedDropdownValue(int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.startingSpawnSpeed = (valueAsFloat / 10) + 0.1f;

        speedDropdowns[2].value = value;

        // Save settings
        string tString = "Speed Dropdown 2";
        PlayerPrefs.SetInt(tString, speedDropdowns[2].value);
    }

    void SetAmountToDecreaseSpawnSpeedDropdownValue(int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.amountToDecreaseSpawnSpeed = (valueAsFloat / 10);

        speedDropdowns[3].value = value;

        // Save settings
        string tString = "Speed Dropdown 3";
        PlayerPrefs.SetInt(tString, speedDropdowns[3].value);
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
            SetChanceDropdownValue(0, 6); // 30%
            SetChanceDropdownValue(1, 7); // 35%
            SetChanceDropdownValue(4, 7); // 35%

            SetChanceDropdownValue(2, 10); // 50%
            SetChanceDropdownValue(3, 10); // 50%

            SetChanceDropdownValue(5, 15); // 75%
            SetChanceDropdownValue(6, 5); // 25%

            SetSpawnDropdownValue(0, 0); // Horizontal block
            SetSpawnDropdownValue(1, 1); // Vertical low block
            SetSpawnDropdownValue(2, 2); // Vertical high block
            SetSpawnDropdownValue(3, 3); // Quid pickup
            SetSpawnDropdownValue(4, 4); // Shield pickup

            SetStartingObjectSpeedDropdownValue(4); // 5
            SetAmountToIncreaseObjectSpeedDropdownValue(0); // 0
            SetStartingSpawnSpeedDropdownValue(19); // 2.0f
            SetAmountToDecreaseSpawnSpeedDropdownValue(1); // 0.1f

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Options set to their default values!");
        } else {
            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the programmer menu!");
        }
    }

    //
    void Moo() {
        // Add up chanceDropdown values
        int total = 20;

        // If 
        if (total == 20) {
            // Total == 100%

        } else {
            // Prompt user that combined dropdown values must equal 100%

            // Display text

            // Highlight dropdowns

        }
    }
}