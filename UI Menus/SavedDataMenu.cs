using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Allows the user to delete all saved high scores or custom algorithms, then reset said data to their default values 
public class SavedDataMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Button resetHighScoresButton;
    public Button resetCustomAlgorithmsButton;

    private void OnEnable() {
        // Display text
        if (Time.time > 0.01f) {
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the saved data menu.", true);
        }
    }

    private void Start() {
        resetHighScoresButton.onClick.AddListener(delegate { AddResetHighScoresConfirmationListeners(); });
        resetCustomAlgorithmsButton.onClick.AddListener(delegate { AddResetCustomAlgorithmsConfirmationListeners(); });
    }

    // Adds functions to the sub menu's yes/no buttons
    void AddResetHighScoresConfirmationListeners() {
        GameManager.S.subMenuCS.AddListeners(ResetHighScores, "Would you like to\ndelete and reset all saved high scores?");
    }
    // On 'Yes' button click, deletes all saved high scores and resets them to default values
    public void ResetHighScores(int yesOrNo = -1) {
        // Deactivate sub menu
        GameManager.S.subMenuGO.SetActive(false);

        //
        if (yesOrNo == 0) {  
            GameManager.S.highScore.SetHighScoresToDefaultValues(true);
            GameManager.S.highScore.ResetNewHighScoreNdx();

            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("All saved high scores deleted and reset!", true);
        } else {
            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the saved data menu.", true);
        }
    }

    // Adds functions to the sub menu's yes/no buttons
    void AddResetCustomAlgorithmsConfirmationListeners() {
        GameManager.S.subMenuCS.AddListeners(ResetCustomAlgorithms, "Would you like to\ndelete and reset all saved custom game options?");
    }
    // On 'Yes' button click, deletes all saved custom algorithms and resets them to default values
    public void ResetCustomAlgorithms(int yesOrNo = -1) {
        // Deactivate sub menu
        GameManager.S.subMenuGO.SetActive(false);

        // 
        if (yesOrNo == 0) {
            GameManager.S.customAlgorithmMenuCS.SetToDefaultSettings();

            GameManager.S.customAlgorithmMenuCS.SaveAll();

            GameManager.S.customAlgorithmMenuCS.UpdateGUI();

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("All saved game options deleted and reset!", true);
        } else {
            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the saved data menu.", true);
        }
    }
}