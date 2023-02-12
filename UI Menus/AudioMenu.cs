using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//
public class AudioMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Slider                   masterVolSlider;
    public Slider                   BGMVolSlider;
    public Slider                   SFXVolSlider;
    public Button                   defaultSettingsButton;
    public Button                   muteAudioButton;
    public TMPro.TextMeshProUGUI    muteAudioButtonText;

    // Delayed text display
    public DelayedTextDisplay       delayedTextDisplay;

    private void OnEnable() {
        // Display text
        delayedTextDisplay.DisplayText("Welcome to the audio menu!");

        // Set selected game object to null
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Start() {
        // Add listeners to sliders
        masterVolSlider.onValueChanged.AddListener(delegate { GameManager.audioMan.SetMasterVolume((masterVolSlider.value)); });
        BGMVolSlider.onValueChanged.AddListener(delegate { GameManager.audioMan.SetBGMVolume((BGMVolSlider.value)); });
        SFXVolSlider.onValueChanged.AddListener(delegate { GameManager.audioMan.SetSFXVolume((SFXVolSlider.value)); });

        // Add listeners to buttons
        defaultSettingsButton.onClick.AddListener(delegate { AddDefaultSettingsConfirmationListeners(); });
        muteAudioButton.onClick.AddListener(delegate { MuteAudioButton(); });

        Invoke("GetPlayerPrefs", 0.1f);

        gameObject.SetActive(false);
    }

    void GetPlayerPrefs() {
        if (PlayerPrefs.HasKey("Master Volume")) {
            masterVolSlider.value = PlayerPrefs.GetFloat("Master Volume");
            GameManager.audioMan.SetMasterVolume(masterVolSlider.value);
        } else {
            GameManager.audioMan.SetMasterVolume(0.5f);
        }

        if (PlayerPrefs.HasKey("BGM Volume")) {
            BGMVolSlider.value = PlayerPrefs.GetFloat("BGM Volume");
            GameManager.audioMan.SetBGMVolume(BGMVolSlider.value);
        } else {
            GameManager.audioMan.SetBGMVolume(0.5f);
        }

        if (PlayerPrefs.HasKey("SFX Volume")) {
            SFXVolSlider.value = PlayerPrefs.GetFloat("SFX Volume");
            GameManager.audioMan.SetSFXVolume(SFXVolSlider.value);
        } else {
            GameManager.audioMan.SetSFXVolume(0.5f);
        }

        if (PlayerPrefs.HasKey("Mute Audio")) {
            if (PlayerPrefs.GetInt("Mute Audio") == 0) {
                GameManager.audioMan.PauseAndMuteAudio();
                muteAudioButtonText.text = "Unmute Audio";
            }
        }
    }

    // Called OnPointerUp() by the EventTrigger attached to each slider in the Inspector
    public void OnSliderButtonReleased(string name) {
        // Delayed text display
        delayedTextDisplay.DisplayText(name + " volume set!");
    }

    // On click (un)mutes all audio
    public void MuteAudioButton() {
        // Delayed text display
        if (!AudioListener.pause) {
            delayedTextDisplay.DisplayText("Audio has been muted!");
            muteAudioButtonText.text = "Unmute Audio";

            // Save settings
            PlayerPrefs.SetInt("Mute Audio", 0);
        } else {
            delayedTextDisplay.DisplayText("Audio has been unmuted!");
            muteAudioButtonText.text = "Mute Audio";

            // Save settings
            PlayerPrefs.SetInt("Mute Audio", 1);
        }
        GameManager.audioMan.PauseAndMuteAudio();
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
            // Reset slider values
            masterVolSlider.value = 0.5f;
            BGMVolSlider.value = 0.5f;
            SFXVolSlider.value = 0.5f;

            // Reset mute
            AudioListener.pause = true;
            GameManager.audioMan.PauseAndMuteAudio();
            muteAudioButtonText.text = "Mute Audio";

            // Delayed text display
            delayedTextDisplay.DisplayText("Options set to their default values!");
        } else {
            // Display text
            delayedTextDisplay.DisplayText("Welcome to the audio menu!");
        }
    }
}