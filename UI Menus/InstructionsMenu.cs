using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Displays text instructions on how the game is played and controlled
public class InstructionsMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Button                   previousPageButton;
    public Button                   nextPageButton;

    public List<GameObject>         textGO;

    public TMPro.TextMeshProUGUI    menuHeaderText;
    public TMPro.TextMeshProUGUI    pageText;

    [Header("Set dynamically")]
    //
    public int                      currentPageNdx = 0;

    private void OnEnable() {
        // Display text
        if (Time.time > 0.01f) {
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the instructions menu:\nRead along and learn how to play the game.", true);
        }
    }

    void Start() {
        // Add listeners to buttons
        previousPageButton.onClick.AddListener(delegate { GoToPreviousOrNextPage(-1); });
        nextPageButton.onClick.AddListener(delegate { GoToPreviousOrNextPage(1); });
    }

    // Displays either the previous or next page of instructions
    public void GoToPreviousOrNextPage(int amountToChange) {
        //
        currentPageNdx += amountToChange;

        // Reset
        if (currentPageNdx > textGO.Count - 1) {
            currentPageNdx = 0;
        } else if (currentPageNdx < 0) {
            currentPageNdx = textGO.Count - 1;
        }

        // Deactivate all text pages
        GameManager.utilities.SetActiveList(textGO, false);

        // Activate current page text
        textGO[currentPageNdx].SetActive(true);

        // Set page text
        pageText.text = "Page: " + "<color=white>" + (currentPageNdx + 1).ToString() + "/6" + "</color>";

        // Set menu header text
        if (currentPageNdx != 5) {
            menuHeaderText.text = "Instructions:";
        } else {
            menuHeaderText.text = "Controls:";
        }
    }
}