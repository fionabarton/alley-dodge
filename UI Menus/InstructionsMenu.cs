using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Button                   previousPageButton;
    public Button                   nextPageButton;

    public List<GameObject>         textGO;

    public TMPro.TextMeshProUGUI    pageText;

    [Header("Set dynamically")]
    //
    public int                      currentPageNdx = 0;

    void Start() {
        // Add listeners to buttons
        previousPageButton.onClick.AddListener(delegate { GoToPreviousOrNextPage(-1); });
        nextPageButton.onClick.AddListener(delegate { GoToPreviousOrNextPage(1); });
    }

    // Displays either the previous or next 10 entries of high scores
    public void GoToPreviousOrNextPage(int amountToChange) {
        //
        currentPageNdx += amountToChange;

        // Reset
        if (currentPageNdx > 4) {
            currentPageNdx = 0;
        } else if (currentPageNdx < 0) {
            currentPageNdx = 4;
        }

        // Deactivate all text pages
        GameManager.utilities.SetActiveList(textGO, false);

        // Activate current page text
        textGO[currentPageNdx].SetActive(true);

        // Set page text
        pageText.text = "Page: " + "<color=white>" + (currentPageNdx + 1).ToString() + "/5" + "</color>";
    }
}