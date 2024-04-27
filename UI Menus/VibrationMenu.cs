using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Allows users to adjust the level of hand controller vibration 
public class VibrationMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Slider                   vibrationSlider;
    public Button                   defaultSettingsButton;
    public Button                   muteVibrationButton;
    public TMPro.TextMeshProUGUI    muteVibrationButtonText;

    [Header("Set Dynamically")]
    public float                    vibrationMultiplier = 0.75f;
    public float                    cachedVibrationMultiplier = 0.75f;

    private void OnEnable() {
        // Display text
        if (Time.time > 0.01f) {
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the vibration menu:\nAdjust the amount of vibration to your hand controllers.", true);
        }

        // Set selected game object to null
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Start() {
        // Add listeners to slider
        vibrationSlider.onValueChanged.AddListener(delegate { SetVibrationVolume(vibrationSlider.value); });

        // Add listeners to buttons
        defaultSettingsButton.onClick.AddListener(delegate { AddDefaultSettingsConfirmationListeners(); });
        muteVibrationButton.onClick.AddListener(delegate { MuteVibrationButton(); });

        Invoke("GetPlayerPrefs", 0.1f);

        gameObject.SetActive(false);
    }

    void GetPlayerPrefs() {
        if (PlayerPrefs.HasKey("Vibration Volume")) {
            vibrationSlider.value = PlayerPrefs.GetFloat("Vibration Volume");
            vibrationMultiplier = vibrationSlider.value;
        } else {
            vibrationMultiplier = 0.75f;
        }

        if (PlayerPrefs.HasKey("Mute Vibration")) {
            if (PlayerPrefs.GetInt("Mute Vibration") == 0) {
                MuteVibration();
            }
        }
    }

    // Called OnPointerUp() by the EventTrigger attached to each slider in the Inspector
    public void OnSliderButtonReleased(string name) {
        // Delayed text display
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText(name + " volume set!", true);

        // Vibrate controllers
        GameManager.S.LeftXRSendHapticImpuse(1.0f, 0.5f);
        GameManager.S.RightXRSendHapticImpuse(1.0f, 0.5f);

        // If volume is set to a value above zero, unmute
        if (vibrationSlider.value > 0) {
            muteVibrationButtonText.text = "Mute Vibration";

            // Save settings
            PlayerPrefs.SetInt("Mute Vibration", 1);
        } else {
            muteVibrationButtonText.text = "Unmute Vibration";

            // Save settings
            PlayerPrefs.SetInt("Mute Vibration", 0);
        }
    }

    public void SetVibrationVolume(float volume) {
        // Set vibration volume
        vibrationSlider.value = volume;
        vibrationMultiplier = vibrationSlider.value;

        // Cache vibration value
        cachedVibrationMultiplier = vibrationSlider.value;

        // Save settings
        PlayerPrefs.SetFloat("Vibration Volume", volume);
    }

    public void MuteVibrationButton() {
        if (PlayerPrefs.HasKey("Mute Vibration")) {
            if (PlayerPrefs.GetInt("Mute Vibration") == 0) {
                UnmuteVibration();
            } else {
                MuteVibration();
            }
        }
    }

    // On click mutes all vibration
    public void MuteVibration() {
        // Cache vibration value and mute volume
        cachedVibrationMultiplier = vibrationMultiplier;
        vibrationMultiplier = 0;

        // Delayed text display
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Vibration has been muted!", true);
        muteVibrationButtonText.text = "Unmute Vibration";

        // Save settings
        PlayerPrefs.SetInt("Mute Vibration", 0);
    }

    // On click unmutes all vibration
    public void UnmuteVibration() {
        // Set vibration to cached value
        vibrationMultiplier = cachedVibrationMultiplier;

        // Vibrate controllers
        GameManager.S.LeftXRSendHapticImpuse(1.0f, 0.5f);
        GameManager.S.RightXRSendHapticImpuse(1.0f, 0.5f);

        // Delayed text display
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Vibration has been unmuted!", true);
        muteVibrationButtonText.text = "Mute Vibration";

        // Save settings
        PlayerPrefs.SetInt("Mute Vibration", 1);
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
            // Reset values
            vibrationSlider.value = 0.75f;
            vibrationMultiplier = 0.75f;

            // Reset mute
            UnmuteVibration();

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Menu settings reset!", true);
        } else {
            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the vibration menu:\nAdjust the amount of vibration to your hand controllers.", true);
        }
    }
}