using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Allows users to generate and submit their name for the high score leaderboard via keyboard input
public class KeyboardInputMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    // Each slots represents one char 
    public List<Text>           charSlotsText;

    //
    public Text                 displayText;

    //
    public GameObject           cursorGO;

    // Used to set cursor position
    public GameObject           okButtonGO;

    // Shake display text animator
    public Animator             messageDisplayAnim;

    //
    public DelayedTextDisplay   messageDisplay;

    //
    public GameObject           subMenuGO;
    public Button               subMenuYesButton;
    public Button               subMenuNoButton;

    [Header("Set Dynamically")]
    private string              inputString = "";

    // Uppercase: 0-25, Lowercase: 26-51, Numbers: 52-61, Symbols: 62-65, 66-69, 70-89
    private string              characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789()[]!?.,~@#$%^&*+-=_\"'` :;/\\";

    // Variables related to predetermined default names
    private int                 dontCareNdx;
    private List<string>        dontCareNames = new List<string>() { "Butthead", "Mildew", "Pee Wee", "Disappointment", "Moon Unit" };
    
    void Start() {
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        // Display text
        messageDisplay.DisplayText("Congratulations!\nPlease enter your name!");
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
                    PlayerPrefs.SetString("Keyboard Input String", inputString);

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
                    GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxConfirm);
                }
            }
        } else {
            // Input box shake animation
            messageDisplayAnim.CrossFade("DisplayTextShake", 0);

            // Audio: Damage
            GameManager.audioMan.PlayRandomDamageSFX();

            // Display text
            messageDisplay.DisplayText(GameManager.words.GetRandomInterjection() + "!\nYa can't add anymore characters;\nthere's no more room!");
        }
    }

    // Remove the last char from the displayed name
    public void Backspace() {
        if (inputString.Length > 0) {
            // Remove last char from the string
            inputString = inputString.Remove(inputString.Length - 1);
            DisplayText(inputString + GetRemainingWhitespace());

            // Save settings
            PlayerPrefs.SetString("Keyboard Input String", inputString);

            // Set active char cursor position
            cursorGO.SetActive(true);
            GameManager.utilities.PositionCursor(cursorGO, charSlotsText[inputString.Length].gameObject, 0, -160, 3);

            // Audio: Deny
            GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxDeny);

            // Display text
            messageDisplay.DisplayText(GameManager.words.GetRandomExclamation() + "!\nYeah, you delete that character!");
        } else {
            // Input box shake animation
            messageDisplayAnim.CrossFade("DisplayTextShake", 0);

            // Audio: Damage
            GameManager.audioMan.PlayRandomDamageSFX();

            // Display text
            messageDisplay.DisplayText(GameManager.words.GetRandomInterjection() + "!\nYa can't delete anymore characters;\nthere's nothing left to delete!");
        }
    }

    // Sets the displayed name to a predetermined default name
    public void DontCare() {
        // Get a default name
        inputString = dontCareNames[dontCareNdx];
        DisplayText(inputString + GetRemainingWhitespace());

        // Save settings
        PlayerPrefs.SetString("Keyboard Input String", inputString);

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
        messageDisplay.DisplayText(GameManager.words.GetRandomExclamation() + "!\nNice choice, lazy bones!");

        // Audio: Confirm
        GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxConfirm);
    }

    public void OK() {
        // Audio: Confirm
        GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxConfirm);

        // Activate sub menu
        subMenuGO.SetActive(true);

        // Display text
        messageDisplay.DisplayText("Are you sure about this name?\nWell, are ya?");

        // Set OnClick Methods
        subMenuYesButton.onClick.RemoveAllListeners();
        subMenuNoButton.onClick.RemoveAllListeners();
        subMenuYesButton.onClick.AddListener(Yes);
        subMenuNoButton.onClick.AddListener(No);
    }

    public void Yes() {
        // Create new HighScore
        HighScore newHighScore = new HighScore(
            inputString,
            GameManager.S.score.score, GameManager.S.score.level,
            GameManager.S.score.objectCount,
            GameManager.S.score.GetTime(GameManager.S.score.endingTime),
            System.DateTime.UtcNow.ToString("HH:mm dd MMMM, yyyy"),
            GameManager.alley.alleyCount,
            GameManager.S.mainMenuCS.playerHeightSlider.value,
            GameManager.S.fallBelowFloorCount);

        // Activate high score menu
        GameManager.S.highScoreMenuGO.SetActive(true);

        // Update high score display
        GameManager.S.highScore.AddNewHighScore(newHighScore);

        // Deactivate sub menu
        subMenuGO.SetActive(false);

        // Deactivate keyboard input menu
        GameManager.S.keyboardMenuGO.SetActive(false);

        // Play confetti particle systems
        GameManager.S.confetti.DropConfetti();

        // Audio: Win
        GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxBuff2);
    }
    public void No() {
        // Deactivate sub menu
        subMenuGO.SetActive(false);

        // Audio: Deny
        GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxDeny);

        // Display text
        messageDisplay.DisplayText("Oh, okay. That's cool.\nSo what's the name?");
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
    public void GetInputString() {
        // GetPlayerPrefs
        if (PlayerPrefs.HasKey("Keyboard Input String")) {
            inputString = PlayerPrefs.GetString("Keyboard Input String");

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