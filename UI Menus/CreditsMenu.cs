using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Displays who is responsible for each aspect of this game.
public class CreditsMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Button                   previousPageButton;
    public Button                   nextPageButton;

    public List<GameObject>         textGO;

    public TMPro.TextMeshProUGUI    menuHeaderText;
    public TMPro.TextMeshProUGUI    pageText;

    [Header("Set dynamically")]
    public int                      currentPageNdx = 0;

    private void OnEnable() {
        // Display text
        if (Time.time > 0.01f) {
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the credits menu:\nRead along and learn who is responsible for each part of the game.", true);
        }
    }

    void Start() {
        // Add listeners to buttons
        previousPageButton.onClick.AddListener(delegate { GoToPreviousOrNextPage(-1); });
        nextPageButton.onClick.AddListener(delegate { GoToPreviousOrNextPage(1); });
    }

    // Displays either the previous or next page of instructions
    public void GoToPreviousOrNextPage(int amountToChange) {
        // Adjust current page index
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
        pageText.text = "Page: " + "<color=#D9D9D9>" + (currentPageNdx + 1).ToString() + "/11" + "</color>";

        // Set menu header text
        if (currentPageNdx == 0 || currentPageNdx == 1) {
            menuHeaderText.text = "Credits: <color=#D9D9D9>1st Party Developers";
        } else if (currentPageNdx == 2 || currentPageNdx == 3) {
            menuHeaderText.text = "Credits: <color=#D9D9D9>Tools";
        } else {
            menuHeaderText.text = "Credits: <color=#D9D9D9>3rd Party Assets";
        }
    }
}