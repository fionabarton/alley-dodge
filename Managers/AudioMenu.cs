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
    public Slider                   VOXVolSlider;
    public Button                   defaultSettingsButton;
    public Button                   muteAudioButton;
    public TMPro.TextMeshProUGUI    muteAudioButtonText;

    private void OnEnable() {
        // Display text
        if (Time.time > 0.01f) {
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the audio menu:\nAdjust volume levels, mute audio, etc.", true);
        }

        // Set selected game object to null
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Start() {
        // Add listeners to sliders
        masterVolSlider.onValueChanged.AddListener(delegate { GameManager.audioMan.SetMasterVolume((masterVolSlider.value)); });
        BGMVolSlider.onValueChanged.AddListener(delegate { GameManager.audioMan.SetBGMVolume((BGMVolSlider.value)); });
        SFXVolSlider.onValueChanged.AddListener(delegate { GameManager.audioMan.SetSFXVolume((SFXVolSlider.value)); });
        VOXVolSlider.onValueChanged.AddListener(delegate { GameManager.audioMan.SetVOXVolume((VOXVolSlider.value)); });

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

        if (PlayerPrefs.HasKey("VOX Volume")) {
            VOXVolSlider.value = PlayerPrefs.GetFloat("VOX Volume");
            GameManager.audioMan.SetVOXVolume(VOXVolSlider.value);
        } else {
            GameManager.audioMan.SetVOXVolume(0.5f);
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
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText(name + " volume set!", true);
    }

    // On click (un)mutes all audio
    public void MuteAudioButton() {
        // Delayed text display
        if (!AudioListener.pause) {
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Audio has been muted!", true);
            muteAudioButtonText.text = "Unmute Audio";

            // Save settings
            PlayerPrefs.SetInt("Mute Audio", 0);
        } else {
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Audio has been unmuted!", true);
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
            VOXVolSlider.value = 0.5f;

            // Reset mute
            AudioListener.pause = true;
            GameManager.audioMan.PauseAndMuteAudio();
            muteAudioButtonText.text = "Mute Audio";

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Options set to their default values!", true);
        } else {
            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the audio menu:\nAdjust volume levels, mute audio, etc.", true);
        }
    }
}