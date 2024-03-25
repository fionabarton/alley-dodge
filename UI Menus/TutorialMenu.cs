using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Guides the player on how to play the game via text instructions 
public class TutorialMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Button                   previousPageButton;
    public Button                   nextPageButton;
    public Button                   exitTutorialButton;

    // Each is a "page" of instructions
    public List<GameObject>         textGO;

    public TMPro.TextMeshProUGUI    pageText;

    [Header("Set dynamically")]
    public int                      currentPageNdx = 0;

    void Start() {
        // Add listeners to buttons
        previousPageButton.onClick.AddListener(delegate { GoToPreviousOrNextPage(-1); });
        nextPageButton.onClick.AddListener(delegate { GoToPreviousOrNextPage(1); });
        exitTutorialButton.onClick.AddListener(delegate { AddExitTutorialConfirmationListeners(); });
    }

    // Displays either the previous or next page of instructions
    public void GoToPreviousOrNextPage(int amountToChange) {
        // Increment pageNdx
        currentPageNdx += amountToChange;

        // Ensure pageNdx stays within valid range
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
        pageText.text = "Page: " + "<color=#D9D9D9>" + (currentPageNdx + 1).ToString() + "/7" + "</color>";

        // Set menu subheader text

    }

    // Adds functions to the sub menu's yes/no buttons
    void AddExitTutorialConfirmationListeners() {
        GameManager.S.subMenuCS.AddListeners(ExitTutorial, "Are you sure that you would like to\nexit this tutorial and figure out the game for yourself?");
    }
    // On 'Yes' button click, exits the tutorial and starts the app as it normally would
    public void ExitTutorial(int yesOrNo = -1) {
        // Deactivate sub menu
        GameManager.S.subMenuGO.SetActive(false);

        //
        if (yesOrNo == 0) {
            //
            
        } else {

        }
    }
}