using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Menu that handles adjusting options (player height, level, alley amount) and starting/exiting the game.
public class MenuManager : MonoBehaviour {
    [Header("Set in Inspector")]
    // Displays selected user height in both meters/centimeters and feet/inches
    public TMPro.TextMeshProUGUI heightText;

    // Slider, dropdowns, & buttons
    public Slider playerHeightSlider;
    public TMPro.TMP_Dropdown levelSelectDropdown;
    public TMPro.TMP_Dropdown alleyAmountDropdown;
    public Button startGameButton;
    public Button exitGameButton;
    public Button defaultSettingsButton;
    
    // Objects to be activated/deactivated
    public GameObject handlesGO;
    public List<GameObject> xrRayInteractorsGO;

    void Start() {
        // Add listener to slider and dropdowns
        playerHeightSlider.onValueChanged.AddListener(delegate { DisplayHeightInMetersAndFeet(); });
        levelSelectDropdown.onValueChanged.AddListener(delegate { SetLevel(); });
        alleyAmountDropdown.onValueChanged.AddListener(delegate { SetAlleyAmount(alleyAmountDropdown.value); });

        // Add listeners to buttons
        startGameButton.onClick.AddListener(delegate { StartGame(); });
        exitGameButton.onClick.AddListener(delegate { ExitApp(); });
        defaultSettingsButton.onClick.AddListener(delegate { DefaultSettings(); });
    }

    // On value changed of playerHeightSlider, display the user's selected height in both meters and feet
    public void DisplayHeightInMetersAndFeet() {
        // Get total inches from total centimeters
        float totalInches = playerHeightSlider.value / 2.54f;

        // Get feet
        int feet = (int)totalInches / 12;

        // Get remaining inches
        double inches = System.Math.Round((totalInches - (feet * 12)), 2);

        // If height is less than 1 meter...
        if (playerHeightSlider.value < 100) {
            // Display height text
            heightText.text = "Player Height:\n" + playerHeightSlider.value + " cm / " + feet + " ft " + inches + " in";
        } else {
            // Get meters
            double d1 = System.Math.Round((playerHeightSlider.value / 100), 2);

            // Display height text
            heightText.text = "Player Height:\n" + d1 + " m / " + feet + " ft " + inches + " in";
        }
    }

    // On value changed of levelSelectDropdown, set level 
    void SetLevel() {
        // Set level to value of dropdown
        GameManager.S.score.level = (levelSelectDropdown.value + 1);
        GameManager.S.score.levelText.text = "Level: " + GameManager.S.score.level;

        // Set spawn speed
        float defaultSpawnSpeed = 2.0f;
        if(GameManager.S.score.level != 1) {
            GameManager.S.spawner.timeDuration = defaultSpawnSpeed - ((float)(GameManager.S.score.level - 1) / 10);
        } else {
            GameManager.S.spawner.timeDuration = defaultSpawnSpeed;
        }

        // Set colorNdx
        if (GameManager.S.score.level != 1) {
            GameManager.color.colorNdx = GameManager.S.score.level - 2;
            GameManager.color.GetNewColorPalette();
        } else {
            GameManager.color.colorNdx = 0;
            GameManager.color.Start();
        }

        // Set display text colors
        GameManager.S.score.displayText.color = GameManager.color.alleyMaterial1.color;
        GameManager.S.score.displayMessageFrame.color = GameManager.color.alleyMaterial2.color;
    }

    // On value changed of alleyAmountDropdown, sets alley amount
    void SetAlleyAmount(int amount) {
        // Set alleyAmount to value of dropdown
        switch (amount) {
            case 0:
                GameManager.alley.alleyCount = 3;
                break;
            case 1:
                GameManager.alley.alleyCount = 5;
                break;
            case 2:
                GameManager.alley.alleyCount = 7;
                break;
        }

        // Initialize alleys
        GameManager.alley.InitializeAlleys();
    }

    // On click of startGameButton, starts game
    void StartGame() {
        // Activate handles
        handlesGO.SetActive(true);

        // Start spawning objects
        GameManager.S.spawner.canSpawn = true;

        // Deactivate XR ray interactors
        xrRayInteractorsGO[0].SetActive(false);
        xrRayInteractorsGO[1].SetActive(false);

        // Deactivate menu
        gameObject.SetActive(false);
    }

    // On click of exitGameButton, quits application
    void ExitApp() {
        Application.Quit();
    }

    // On click of defaultSettingsButton, returns all menu settings to their default value
    void DefaultSettings() {
        // Reset slider and dropdown values
        playerHeightSlider.value = 168;
        levelSelectDropdown.value = 0;
        alleyAmountDropdown.value = 0;

        // Reset alley amount
        SetAlleyAmount(3);

        // Reset spawn speed
        GameManager.S.spawner.timeDuration = 2.0f;

        // Reset scene colors
        GameManager.color.colorNdx = 0;
        GameManager.color.Start();

        // Set display text colors
        GameManager.S.score.displayText.color = GameManager.color.alleyMaterial1.color;
        GameManager.S.score.displayMessageFrame.color = GameManager.color.alleyMaterial2.color;
    }
}