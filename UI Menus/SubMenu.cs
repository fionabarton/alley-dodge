using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// Prompts the user to select between two different UI option buttons, yes and no.
public class SubMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Button               yesButton;
    public Button               noButton;

    //
    public DelayedTextDisplay   messageDisplay;

    void Start() {
        gameObject.SetActive(false);
    }

    public void AddListeners(Action<int> functionToPass, string message) {
        // Activate sub menu
        gameObject.SetActive(true);

        // Display text
        messageDisplay.DisplayText(message);

        // Remove listeners
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        // Add listeners
        yesButton.onClick.AddListener(delegate { functionToPass(0); });
        noButton.onClick.AddListener(delegate { functionToPass(1); });
    }
}