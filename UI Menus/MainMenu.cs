using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Menu that handles adjusting options (player height, level, alley amount) and starting/exiting the game.
public class MainMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    // Displays selected user height in both meters/centimeters and feet/inches
    public TMPro.TextMeshProUGUI    heightText;

    // Slider, dropdowns, & buttons
    public Slider                   playerHeightSlider;
    public TMPro.TMP_Dropdown       levelSelectDropdown;
    public TMPro.TMP_Dropdown       alleyAmountDropdown;
    public Button                   startGameButton;
    public Button                   exitGameButton;
    public Button                   resetButton;
    public Button                   optionsButton;
    public Button                   highScoresButton;

    // Delayed text display
    public DelayedTextDisplay       delayedTextDisplay;

    // 
    public AdjustObjectsHeight      adjustObjectsHeight;

    // Position at which to respawn the user if they've fallen through the floor
    public Transform                resetPosition;

    private void OnEnable() {
        // Display text
        delayedTextDisplay.DisplayText("To get started quickly,\nplease set the 'Player Height' slider to your height,\nand then press the 'Start Game' button!");

        // Set selected game object to null
        EventSystem.current.SetSelectedGameObject(null);

        // Play BGM: Soap
        Invoke("PlayBGMClip", 0.1f);
    }

    // Play BGM: Soap
    void PlayBGMClip() {
        GameManager.audioMan.PlayBGMClip(eBGM.bgmSoap);
    }
    void Start() {
        // GetPlayerPrefs
        if (PlayerPrefs.HasKey("Player Height")) {
            playerHeightSlider.value = PlayerPrefs.GetFloat("Player Height");
        } else {
            playerHeightSlider.value = 168;
        }
        if (PlayerPrefs.HasKey("Level Select")) {
            levelSelectDropdown.value = PlayerPrefs.GetInt("Level Select");
        } else {
            levelSelectDropdown.value = 0;
        }
        if (PlayerPrefs.HasKey("Alley Amount")) {
            alleyAmountDropdown.value = PlayerPrefs.GetInt("Alley Amount");
        } else {
            alleyAmountDropdown.value = 0;
        }

        // Add listener to slider and dropdowns
        playerHeightSlider.onValueChanged.AddListener(delegate { DisplayHeightInMetersAndFeet(); });
        levelSelectDropdown.onValueChanged.AddListener(delegate { SetLevel(); });
        alleyAmountDropdown.onValueChanged.AddListener(delegate { SetAlleyAmount(alleyAmountDropdown.value); });

        // Add listeners to buttons
        startGameButton.onClick.AddListener(delegate { StartGame(); });
        exitGameButton.onClick.AddListener(delegate { AddExitAppConfirmationListeners(); });
        resetButton.onClick.AddListener(delegate { AddDefaultSettingsConfirmationListeners(); });
        optionsButton.onClick.AddListener(delegate { GoToOptionsMenuButton(); });
        highScoresButton.onClick.AddListener(delegate { GoToHighScoreMenuButton(); });
        
        // Set objects' position and scale to their default values
        Invoke("SetObjects", 0.1f);
    }

    void SetObjects() {
        adjustObjectsHeight.SetObjects(playerHeightSlider.value);

        // In case the level or alley dropdowns are set to something other than 1
        SetLevel(false);
        SetAlleyAmount(alleyAmountDropdown.value);
    }

    // On value changed of playerHeightSlider, display the user's selected height in both meters and feet
    public void DisplayHeightInMetersAndFeet() {
        // Save settings
        PlayerPrefs.SetFloat("Player Height", playerHeightSlider.value);

        // Deactivate climbing interactors
        GameManager.S.EnableClimbInteractors(false);

        // Get total inches from total centimeters
        float totalInches = playerHeightSlider.value / 2.54f;

        // Get feet
        int feet = (int)totalInches / 12;

        // Get remaining inches
        double inches = System.Math.Round((totalInches - (feet * 12)), 2);

        // If height is less than 1 meter...
        if (playerHeightSlider.value < 100) {
            // Display height text
            heightText.text = playerHeightSlider.value + " cm / " + feet + " ft " + inches + " in";
        } else {
            // Get meters
            double d1 = System.Math.Round((playerHeightSlider.value / 100), 2);

            // Display height text
            heightText.text = d1 + " m / " + feet + " ft " + inches + " in";
        }

        // Set objects position and scale
        adjustObjectsHeight.SetObjects(playerHeightSlider.value);

        // Reset resetPosition height (where the user respawns if they’ve fallen through the floor) 
        GameManager.utilities.SetPosition(resetPosition.gameObject, 0, (playerHeightSlider.value / 98.82352941f));
    }

    // Called OnPointerUp() by the EventTrigger attached to the slider in the Inspector
    public void OnSliderButtonReleased() {
        // Activate climbing interactors
        GameManager.S.EnableClimbInteractors(true);

        // Delayed text display
        delayedTextDisplay.DisplayText("Player height selected!");
    }

    // On value changed of levelSelectDropdown, set level 
    void SetLevel(bool displayText = true) {
        // Save settings
        PlayerPrefs.SetInt("Level Select", levelSelectDropdown.value);

        // Set level to value of dropdown
        GameManager.S.score.level = (levelSelectDropdown.value + 1);
        GameManager.S.score.levelText.text = "Level:  <color=white>" + GameManager.S.score.level;

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
            GameManager.color.ResetPalette();
        }

        // Set display text colors
        GameManager.color.SetDisplayTextPalette();

        if (displayText) {
            if (gameObject.activeInHierarchy) {
                // Delayed text display
                delayedTextDisplay.DisplayText("Level selected!");
            }
        }        
    }

    // On value changed of alleyAmountDropdown, sets alley amount
    public void SetAlleyAmount(int amount, bool displayText = true) {
        // Save settings
        PlayerPrefs.SetInt("Alley Amount", amount);

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
        GameManager.alley.InitializeAlleys(playerHeightSlider.value);

        // Delayed text display
        if (displayText) {
            if (gameObject.activeInHierarchy) {
                delayedTextDisplay.DisplayText("Amount of alleys selected!");
            }
        }
    }

    // On click of startGameButton, starts game
    void StartGame() {
        //
        SetLevel();

        // Updates score and level GUI
        GameManager.S.score.UpdateGUI();

        // Start spawning objects
        GameManager.S.spawner.canSpawn = true;
        GameManager.S.spawner.objectsCanMove = true;

        // Deactivate XR ray interactors
        GameManager.utilities.SetActiveList(GameManager.S.xrRayInteractorsGO, false);

        // Activate climbing interactors (in case the user hasn't released the 'player height' slider)
        GameManager.S.EnableClimbInteractors(true);

        // Deactivate menu
        gameObject.SetActive(false);

        // Cache starting time
        GameManager.S.score.startingTime = Time.time;
        GameManager.S.score.timerIsOn = true;

        // Reset fall through floor count
        GameManager.S.fallBelowFloorCount = 0;

        // Display text
        GameManager.S.score.SetDisplayText("LET'S GO!", GameManager.color.alleyMaterial1.color, GameManager.color.alleyMaterial2.color);

        // Play BGM: Ninja
        GameManager.audioMan.PlayBGMClip(eBGM.bgmNinja);
    }

    //
    public void GoToOptionsMenuButton() {
        //// Activate options menu
        //GameManager.S.optionsMenuGO.SetActive(true);
        GameManager.S.moreMenuGO.SetActive(true);

        // Play BGM: Never
        GameManager.audioMan.PlayBGMClip(eBGM.bgmNever);

        // Deactivate this menu
        gameObject.SetActive(false);
    }

    //
    public void GoToHighScoreMenuButton() {
        // Activate high score menu
        GameManager.S.highScoreMenuGO.SetActive(true);

        // Load data
        GameManager.save.Load();

        // Update high score display
        GameManager.S.highScore.UpdateHighScoreDisplay(GameManager.S.highScore.currentPageNdx);

        // Play BGM: 1940
        GameManager.audioMan.PlayBGMClip(eBGM.bgm1940);

        // Deactivate keyboard input menu
        gameObject.SetActive(false);
    }

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
            // Reset slider and dropdown values
            playerHeightSlider.value = 168;
            levelSelectDropdown.value = 0;
            alleyAmountDropdown.value = 0;

            // Reset alley amount
            SetAlleyAmount(0);

            // Reset spawn speed
            GameManager.S.spawner.timeDuration = 2.0f;

            // Reset scene colors
            GameManager.color.ResetPalette();

            // Set display text colors
            GameManager.color.SetDisplayTextPalette();

            // Delayed text display
            delayedTextDisplay.DisplayText("Menu settings set to their default values!");
        } else {
            // Reset display text
            delayedTextDisplay.DisplayText("To get started quickly,\nplease set the 'Player Height' slider to your height,\nand then press the 'Start Game' button!");
        }
    }

    // Adds functions to the sub menu's yes/no buttons
    void AddExitAppConfirmationListeners() {
        GameManager.S.subMenuCS.AddListeners(ExitApp, "Are you sure that you would\nlike to close this application?");
    }
    // On 'Yes' button click, quits application
    void ExitApp(int yesOrNo = -1) {
        // Deactivate sub menu
        GameManager.S.subMenuGO.SetActive(false);

        // Reset display text
        delayedTextDisplay.DisplayText("To get started quickly,\nplease set the 'Player Height' slider to your height,\nand then press the 'Start Game' button!");

        // Quit application
        if (yesOrNo == 0) {
            Application.Quit();
        } 
    }
}