using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Allows users to generate and submit their name for the high score leaderboard via keyboard input
public class KeyboardInputMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    // Each slots represents one char 
    public List<Text>           charSlotsText;

    //
    public GameObject           cursorGO;

    // Used to set cursor position
    public GameObject           okButtonGO;

    // Used to add either set high score entry name or custom algorithm name
    public Button               okButtonCS;

    public GameObject           goBackButtonGO;

    // Shake display text animator
    public Animator             messageDisplayAnim;

    //
    public DelayedTextDisplay   messageDisplay;

    [Header("Set Dynamically")]
    public string               inputString = "";

    // Uppercase: 0-25, Lowercase: 26-51, Numbers: 52-61, Symbols: 62-65, 66-69, 70-89
    private string              characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789()[]!?.,~@#$%^&*+-=_\"'` :;/\\";

    // Variables related to predetermined default names
    private int                 dontCareNdx;
    private List<string>        dontCareNames = new List<string>() { "Bud", "Champ", "Chief", "Cookie", "Cupcake", "Darling", "Deadbeat", "Dude", "Dumpling", "Dunce", "Giggles", "Goon", "Junior", "Kiddo", "Killer", "Lazy Bones", "Muscles", "Peanut", "Pumpkin", "Princess", "Slacker", "Slick", "Sport", "Stud", "Trouble" };

    private bool                isSettingHighScoreEntryName;

    void Start() {
        gameObject.SetActive(false);
    }

    public void Activate(string actionToBePerformed) {
        // Remove listeners
        okButtonCS.onClick.RemoveAllListeners();

        // Activated here in order to ensure that following display text code below gets a chance to run
        gameObject.SetActive(true);

        //
        if (actionToBePerformed == "NameHighScoreEntry") {
            isSettingHighScoreEntryName = true;

            // Add listener 
            okButtonCS.onClick.AddListener(delegate { AddSetHighScoreEntryNameConfirmationListeners(); });

            // Display saved name input string
            GameManager.S.keyboardMenuCS.GetInputString("HighScoreInputString");

            // Display text
            messageDisplay.DisplayText("Congratulations!\nPlease enter your name!");

            goBackButtonGO.SetActive(false);
        } else if (actionToBePerformed == "NameCustomAlgorithmEntry") {
            isSettingHighScoreEntryName = false;

            // Add listener
            okButtonCS.onClick.AddListener(delegate { AddSetCustomAlgorithmEntryNameConfirmationListeners(); });

            // Display saved name input string
            GameManager.S.keyboardMenuCS.GetInputString("CustomAlgorithmInputString");

            // Display text
            messageDisplay.DisplayText("Please name your custom game mode to be saved!");

            goBackButtonGO.SetActive(true);
        }
    }

    // Add a char to the displayed name
    public void PressedKey(int ndx) {
        if (inputString.Length < 15) {
            for (int i = 0; i < characters.Length; i++) {
                if (i == ndx) {
                    // Add user keyboard input to string
                    inputString += characters[ndx];
                    DisplayText(inputString + GetRemainingWhitespace());

                    // Save settings
                    if (isSettingHighScoreEntryName) {
                        PlayerPrefs.SetString("HighScoreInputString", inputString);
                    } else {
                        PlayerPrefs.SetString("CustomAlgorithmInputString", inputString);
                    }

                    if (inputString.Length < 15) {
                        // Set active char cursor position
                        GameManager.utilities.PositionCursor(cursorGO, charSlotsText[inputString.Length].gameObject, 0, -160, 3);
                    } else {
                        // Set active char cursor position
                        GameManager.utilities.PositionCursor(cursorGO, okButtonGO, 100, 0, 2);
                    }

                    // Display text
                    messageDisplay.DisplayText(GameManager.words.GetRandomExclamation() + "!\nYeah, you add that character!");

                    // Audio: Confirm
                    GameManager.audioMan.PlayUISFXClip(eSFX.sfxConfirm);
                }
            }
        } else {
            // Input box shake animation
            messageDisplayAnim.CrossFade("DisplayTextShake", 0);

            // Audio: Damage
            GameManager.audioMan.PlayRandomDamageSFX();

            // Display text
            messageDisplay.DisplayText(GameManager.words.GetRandomInterjection() + "!\nYou can't add anymore characters;\nthere's no more room left!");
        }
    }

    // Remove the last char from the displayed name
    public void Backspace() {
        if (inputString.Length > 0) {
            // Remove last char from the string
            inputString = inputString.Remove(inputString.Length - 1);
            DisplayText(inputString + GetRemainingWhitespace());

            // Save settings
            if (isSettingHighScoreEntryName) {
                PlayerPrefs.SetString("HighScoreInputString", inputString);
            } else {
                PlayerPrefs.SetString("CustomAlgorithmInputString", inputString);
            }

            // Set active char cursor position
            cursorGO.SetActive(true);
            GameManager.utilities.PositionCursor(cursorGO, charSlotsText[inputString.Length].gameObject, 0, -160, 3);

            // Audio: Deny
            GameManager.audioMan.PlayUISFXClip(eSFX.sfxDeny);

            // Display text
            messageDisplay.DisplayText(GameManager.words.GetRandomExclamation() + "!\nYeah, you delete that character!");
        } else {
            // Input box shake animation
            messageDisplayAnim.CrossFade("DisplayTextShake", 0);

            // Audio: Damage
            GameManager.audioMan.PlayRandomDamageSFX();

            // Display text
            messageDisplay.DisplayText(GameManager.words.GetRandomInterjection() + "!\nYou can't delete anymore characters;\nthere's nothing left to delete!");
        }
    }

    // Sets the displayed name to a predetermined default name
    public void DontCare() {
        // Get a default name
        inputString = dontCareNames[dontCareNdx];
        DisplayText(inputString + GetRemainingWhitespace());

        // Save settings
        if (isSettingHighScoreEntryName) {
            PlayerPrefs.SetString("HighScoreInputString", inputString);
        } else {
            PlayerPrefs.SetString("CustomAlgorithmInputString", inputString);
        }

        // Set active char cursor position
        cursorGO.SetActive(true);
        GameManager.utilities.PositionCursor(cursorGO, charSlotsText[inputString.Length].gameObject, 0, -160, 3);

        // Increment index
        if (dontCareNdx < dontCareNames.Count - 1) {
            dontCareNdx += 1;
        } else {
            dontCareNdx = 0;
        }

        // Display text
        messageDisplay.DisplayText(GameManager.words.GetRandomExclamation() + "!\nNice choice!");

        // Audio: Confirm
        GameManager.audioMan.PlayUISFXClip(eSFX.sfxConfirm);
    }

    public void GoBackButton() {
        gameObject.SetActive(false);
    }

    // On 'OK' button click, adds functions to the sub menu's yes/no buttons
    // - Sets high score entry name
    public void AddSetHighScoreEntryNameConfirmationListeners() {
        // Audio: Confirm
        GameManager.audioMan.PlayUISFXClip(eSFX.sfxConfirm);

        GameManager.S.subMenuCS.AddListeners(SetHighScoreEntryName, "Are you sure about this name?\nWell, are you?");
    }

    // On 'OK' button click, adds functions to the sub menu's yes/no buttons
    // - Sets custom algorithm entry name
    public void AddSetCustomAlgorithmEntryNameConfirmationListeners() {
        // Audio: Confirm
        GameManager.audioMan.PlayUISFXClip(eSFX.sfxConfirm);

        GameManager.S.subMenuCS.AddListeners(GameManager.S.customAlgorithmMenuCS.SaveAlgorithm, "Are you sure about this name?\nAND overwriting this custom game mode slot?\nWell, are you?");
    }

    // On 'Yes' button click, creates and stores a new HighScore based on the user's performance
    void SetHighScoreEntryName(int yesOrNo = -1) {
        // Deactivate sub menu
        GameManager.S.subMenuGO.SetActive(false);

        // 
        if (yesOrNo == 0) {
            // Create new HighScore
            HighScore newHighScore = new HighScore(
                inputString,
                GameManager.S.score.score, 
                GameManager.S.score.level,
                GameManager.S.score.objectCount,
                GameManager.S.score.GetTime(GameManager.S.score.endingTime),
                System.DateTime.UtcNow.ToString("dd MMMM, yyyy"),
                System.DateTime.UtcNow.ToString("HH:mm"),
                GameManager.alley.alleyCount,
                GameManager.S.mainMenuCS.GetPlayerHeightInMetersAndFeet(GameManager.S.mainMenuCS.playerHeightSlider.value),
                GameManager.S.fallBelowFloorCount,
                GameManager.S.damageCount,
                GameManager.S.pauseCount,
                GameManager.S.objectSpeedDisplayedValues[PlayerPrefs.GetInt("Speed Dropdown 0")].ToString(),
                GameManager.S.amountToIncreaseDisplayedValues[PlayerPrefs.GetInt("Speed Dropdown 1")].ToString(),
                GameManager.S.spawnSpeedDisplayedValues[PlayerPrefs.GetInt("Speed Dropdown 2")].ToString(),
                GameManager.S.amountToDecreaseDisplayedValues[PlayerPrefs.GetInt("Speed Dropdown 3")].ToString(),
                (GameManager.S.spawner.chancesToSpawn[0] * 100).ToString() + "%",
                (GameManager.S.spawner.chancesToSpawn[1] * 100).ToString() + "%",
                (GameManager.S.spawner.chancesToSpawn[2] * 100).ToString() + "%",
                (GameManager.S.spawner.chancesToSpawn[3] * 100).ToString() + "%",
                (GameManager.S.spawner.chancesToSpawn[4] * 100).ToString() + "%",
                (GameManager.S.spawner.chancesToSpawn[5] * 100).ToString() + "%",
                (GameManager.S.spawner.chancesToSpawn[6] * 100).ToString() + "%",
                (GameManager.S.spawner.chancesToSpawn[7] * 100).ToString() + "%",
                (GameManager.S.spawner.chancesToSpawn[8] * 100).ToString() + "%",
                (GameManager.S.spawner.chancesToSpawn[9] * 100).ToString() + "%",
                GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[0],
                GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[1],
                GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[2],
                GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[3],
                GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[4],
                GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[5],
                GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[6],
                GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[7],
                GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[8],
                GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[9]
                );

            // Activate selected high score menu
            GameManager.S.highScoreMenuGO.SetActive(true);

            // Update high score display
            GameManager.S.highScore.AddNewHighScore(newHighScore);

            // Deactivate keyboard input menu
            GameManager.S.keyboardMenuGO.SetActive(false);

            // Play confetti particle systems
            GameManager.S.confetti.IsLooping(true);
            GameManager.S.confetti.DropConfetti(true, true);
        } else {
            // Display text
            messageDisplay.DisplayText("Oh, okay. That's cool.\nSo what's the name?");
        }
    }

    // Returns a string of whitespace as long as the amount of remaining empty chars 
    public string GetRemainingWhitespace() {
        string remainingChars = "";
        if (inputString.Length < 15) {
            // Get amount of remaining space
            int amountOfRemainingChars = 15 - inputString.Length;

            // Populate string with whitespace 
            for (int j = 0; j < amountOfRemainingChars; j++) {
                remainingChars += " ";
            }
        }

        return remainingChars;
    }

    // Display each char of the 'text' argument in an individual Text object
    public void DisplayText(string text) {
        for (int i = 0; i < charSlotsText.Count; i++) {
            charSlotsText[i].text = text[i].ToString();
        }
    }

    // Display saved name input string
    public void GetInputString(string playerPrefsKeyName) {
        // GetPlayerPrefs
        if (PlayerPrefs.HasKey(playerPrefsKeyName)) {
            inputString = PlayerPrefs.GetString(playerPrefsKeyName);

            if (inputString != "") {
                DisplayText(inputString + GetRemainingWhitespace());

                // Set active char cursor position
                cursorGO.SetActive(true);
                if (inputString.Length < 15) {
                    // Set active char cursor position
                    GameManager.utilities.PositionCursor(cursorGO, charSlotsText[inputString.Length].gameObject, 0, -160, 3);
                } else {
                    // Set active char cursor position
                    GameManager.utilities.PositionCursor(cursorGO, okButtonGO, 100, 0, 2);
                }
            }
        } else {
            inputString = "";
        }
    }
}