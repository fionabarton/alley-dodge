using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static System.Net.Mime.MediaTypeNames;

//
public class AudioMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Slider                   masterVolSlider;
    public Slider                   BGMVolSlider;
    public Slider                   SFXVolSlider;
    public Slider                   VOXVolSlider;
    public Button                   soundtrackMenuButton;
    public Button                   defaultSettingsButton;
    public Button                   muteAudioButton;
    public TMPro.TextMeshProUGUI    muteAudioButtonText;

    public TMPro.TextMeshProUGUI    startingMusicTrackText;
    public Button                   previousTrackButton;
    public Button                   nextTrackButton;
    public Button                   playButton;
    public TMPro.TextMeshProUGUI    playButtonText;
    public Toggle                   loopTrackToggle;
    public Button                   resetSoundtrackButton;
    public Button                   volumeMenuButton;

    public TMPro.TextMeshProUGUI    menuHeaderText;
    public GameObject               volumeMenu;
    public GameObject               soundtrackMenu;

    private void OnEnable() {
        // Display text
        if (Time.time > 0.01f) {
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the audio menu:\nSet volume levels, mute audio, & select in-game music.", true);
        }

        // Set selected game object to null
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnDisable() {
        // If not already playing 8-bit BGM: "Never", then play it
        if (GameManager.audioMan.BGMAudioSource.clip != GameManager.audioMan.bgmClips[2]) {
            GameManager.audioMan.PlayBGMClip(eBGM.bgmNever);
        }

        // Reset play/stop soundtrack sample button text
        playButtonText.text = "Play";
    }

    void Start() {
        // Add listeners to sliders
        masterVolSlider.onValueChanged.AddListener(delegate { GameManager.audioMan.SetMasterVolume((masterVolSlider.value)); });
        BGMVolSlider.onValueChanged.AddListener(delegate { GameManager.audioMan.SetBGMVolume((BGMVolSlider.value)); });
        SFXVolSlider.onValueChanged.AddListener(delegate { GameManager.audioMan.SetSFXVolume((SFXVolSlider.value)); });
        VOXVolSlider.onValueChanged.AddListener(delegate { GameManager.audioMan.SetVOXVolume((VOXVolSlider.value)); });

        // Add listeners to buttons
        defaultSettingsButton.onClick.AddListener(delegate { AddResetVolumeConfirmationListeners(); });
        muteAudioButton.onClick.AddListener(delegate { MuteAudioButton(); });
        
        previousTrackButton.onClick.AddListener(delegate { GoToPreviousOrNextTrack(-1); });
        nextTrackButton.onClick.AddListener(delegate { GoToPreviousOrNextTrack(1); });
        playButton.onClick.AddListener(delegate { PlayOrStopTrack(); });
        resetSoundtrackButton.onClick.AddListener(delegate { AddResetSoundtrackConfirmationListeners(); });

        // Add listeners to toggle
        loopTrackToggle.onValueChanged.AddListener(delegate { LoopSoundtrack(); });

        volumeMenuButton.onClick.AddListener(delegate { SwapBetweenVolumeAndSoundtrackMenus(); });
        soundtrackMenuButton.onClick.AddListener(delegate { SwapBetweenVolumeAndSoundtrackMenus(); });

        Invoke("GetPlayerPrefs", 0.1f);

        gameObject.SetActive(false);
    }

    void GetPlayerPrefs() {
        if (PlayerPrefs.HasKey("Master Volume")) {
            masterVolSlider.value = PlayerPrefs.GetFloat("Master Volume");
            GameManager.audioMan.SetMasterVolume(masterVolSlider.value);
        } else {
            GameManager.audioMan.SetMasterVolume(0.25f);
        }

        if (PlayerPrefs.HasKey("BGM Volume")) {
            BGMVolSlider.value = PlayerPrefs.GetFloat("BGM Volume");
            GameManager.audioMan.SetBGMVolume(BGMVolSlider.value);
        } else {
            GameManager.audioMan.SetBGMVolume(0.45f);
        }

        if (PlayerPrefs.HasKey("SFX Volume")) {
            SFXVolSlider.value = PlayerPrefs.GetFloat("SFX Volume");
            GameManager.audioMan.SetSFXVolume(SFXVolSlider.value);
        } else {
            GameManager.audioMan.SetSFXVolume(0.65f);
        }

        if (PlayerPrefs.HasKey("VOX Volume")) {
            VOXVolSlider.value = PlayerPrefs.GetFloat("VOX Volume");
            GameManager.audioMan.SetVOXVolume(VOXVolSlider.value);
        } else {
            GameManager.audioMan.SetVOXVolume(0.45f);
        }

        if (PlayerPrefs.HasKey("Mute Audio")) {
            if (PlayerPrefs.GetInt("Mute Audio") == 0) {
                GameManager.audioMan.PauseAndMuteAudio();
                muteAudioButtonText.text = "Unmute Audio";
            }
        }

        // Set starting soundtrack index & name
        if (PlayerPrefs.HasKey("Starting Soundtrack Index")) {
            // Set track index
            GameManager.audioMan.startingSoundtrackNdx = PlayerPrefs.GetInt("Starting Soundtrack Index");

            // Set track name text
            SetStartingTrackName();
        }

        // Set loop and loop toggle
        if (PlayerPrefs.HasKey("Soundtrack Is Looping")) {
            if (PlayerPrefs.GetInt("Soundtrack Is Looping") == 1) {
                // Enable loop toggle, which by changing its value calls LoopSoundtrack()   
                loopTrackToggle.isOn = true;
            }
        }
    }

    // Called OnPointerUp() by the EventTrigger attached to each slider in the Inspector
    public void OnSliderButtonReleased(string name) {
        // Delayed text display
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText(name + " volume set!", true);

        // For master volume, if volume is set to a value above zero, unmute
        if (name == "Master") {
            if (masterVolSlider.value > 0) {
                GameManager.audioMan.isMuted = false;

                muteAudioButtonText.text = "Mute Audio";

                // Save settings
                PlayerPrefs.SetInt("Mute Audio", 1);
            } else {
                GameManager.audioMan.isMuted = true;

                muteAudioButtonText.text = "Unmute Audio";

                // Save settings
                PlayerPrefs.SetInt("Mute Audio", 0);
            }
        }
    }

    // On click (un)mutes all audio
    public void MuteAudioButton() {
        // Delayed text display
        if (!GameManager.audioMan.isMuted) {
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
    void AddResetVolumeConfirmationListeners() {
        GameManager.S.subMenuCS.AddListeners(ResetVolume, "Are you sure that you would like to\nreset this menu's options to their default values?");
    }
    // On 'Yes' button click, returns all menu settings to their default value
    public void ResetVolume(int yesOrNo = -1) {
        // Deactivate sub menu
        GameManager.S.subMenuGO.SetActive(false);

        // 
        if (yesOrNo == 0) {
            // Reset slider values
            masterVolSlider.value = 0.25f;
            BGMVolSlider.value = 0.45f;
            SFXVolSlider.value = 0.65f;
            VOXVolSlider.value = 0.45f;

            // Reset mute
            if (GameManager.audioMan.isMuted) {
                GameManager.audioMan.PauseAndMuteAudio();
            }
            muteAudioButtonText.text = "Mute Audio";

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Menu settings reset!", true);
        } else {
            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the audio menu:\nSet volume levels, mute audio, & select in-game music.", true);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////
    // Swaps which of the 2 halves of the audio menu is active
    public void SwapBetweenVolumeAndSoundtrackMenus() {
        if (volumeMenu.activeInHierarchy) {
            // Activate menu
            volumeMenu.SetActive(false);
            soundtrackMenu.SetActive(true);

            // Set header text
            menuHeaderText.text = "Audio: <color=#D9D9D9>Soundtrack";
        } else {
            // Activate menu
            soundtrackMenu.SetActive(false);
            volumeMenu.SetActive(true);

            // Set header text
            menuHeaderText.text = "Audio: <color=#D9D9D9>Volume";

            // If not already playing 8-bit BGM: "Never", then play it
            if (GameManager.audioMan.BGMAudioSource.clip != GameManager.audioMan.bgmClips[2]) {
                GameManager.audioMan.PlayBGMClip(eBGM.bgmNever);
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////

    // Selects either the previous or next track as 'Starting Music Track'
    public void GoToPreviousOrNextTrack(int amountToChange) {
        // Change soundtrack index
        if(amountToChange == -1) {
            GameManager.audioMan.DecrementStartingSoundtrackNdx();
        } else {
            GameManager.audioMan.IncrementStartingSoundtrackNdx();
        }

        // Set track name text
        SetStartingTrackName();

        // Play soundtrack sample
        if (playButtonText.text == "Stop") {
            GameManager.audioMan.PlaySoundtrackClip(GameManager.audioMan.startingSoundtrackNdx, GameManager.audioMan.loopSoundtrackToggleIsOn);
        }

        // Save settings
        PlayerPrefs.SetInt("Starting Soundtrack Index", GameManager.audioMan.startingSoundtrackNdx);
    }

    void SetStartingTrackName() {
        switch (GameManager.audioMan.startingSoundtrackNdx) {
            case 0:
                startingMusicTrackText.text = "1) Track Name";
                break;
            case 1:
                startingMusicTrackText.text = "2) Track Name";
                break;
            case 2:
                startingMusicTrackText.text = "3) Track Name";
                break;
            case 3:
                startingMusicTrackText.text = "4) Track Name";
                break;
            case 4:
                startingMusicTrackText.text = "5) Track Name";
                break;
            case 5:
                startingMusicTrackText.text = "6) Track Name";
                break;
            case 6:
                startingMusicTrackText.text = "7) Track Name";
                break;
            case 7:
                startingMusicTrackText.text = "8) Track Name";
                break;
            case 8:
                startingMusicTrackText.text = "9) Track Name";
                break;
            case 9:
                startingMusicTrackText.text = "10) Track Name";
                break;
            case 10:
                startingMusicTrackText.text = "11) Track Name";
                break;
        }
    }

    void PlayOrStopTrack() {
        if(playButtonText.text == "Stop") {
            // Stop track
            GameManager.audioMan.BGMAudioSource.Stop();

            // Set button text
            playButtonText.text = "Play";
        } else {
            // Play track
            GameManager.audioMan.PlaySoundtrackClip(GameManager.audioMan.startingSoundtrackNdx, GameManager.audioMan.loopSoundtrackToggleIsOn);

            // Set button text
            playButtonText.text = "Stop";
        }
    }

    //
    void LoopSoundtrack() {
        if (loopTrackToggle.isOn) {
            // Enable loop background music soundtrack
            GameManager.audioMan.loopSoundtrackToggleIsOn = true;
            GameManager.audioMan.BGMAudioSource.loop = true;

            // Save settings
            PlayerPrefs.SetInt("Soundtrack Is Looping", 1);
        } else {
            // Disable loop background music soundtrack
            GameManager.audioMan.loopSoundtrackToggleIsOn = false;
            GameManager.audioMan.BGMAudioSource.loop = false;

            // Save settings
            PlayerPrefs.SetInt("Soundtrack Is Looping", 0);
        }
    }

    // Adds functions to the sub menu's yes/no buttons
    void AddResetSoundtrackConfirmationListeners() {
        GameManager.S.subMenuCS.AddListeners(ResetSoundtrack, "Are you sure that you would like to\nreset this menu's options to their default values?");
    }
    // On 'Yes' button click, returns all menu settings to their default value
    public void ResetSoundtrack(int yesOrNo = -1) {
        // Deactivate sub menu
        GameManager.S.subMenuGO.SetActive(false);

        // 
        if (yesOrNo == 0) {
            // Reset starting music track
            GameManager.audioMan.startingSoundtrackNdx = 0;
            startingMusicTrackText.text = "1) Track Name";

            // Disable loop toggle
            loopTrackToggle.isOn = false;

            // Disable loop background music soundtrack
            GameManager.audioMan.loopSoundtrackToggleIsOn = false;

            // Save settings
            PlayerPrefs.SetInt("Starting Soundtrack Index", 0);
            PlayerPrefs.SetInt("Soundtrack Is Looping", 0);

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Menu settings reset!", true);
        } else {
            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the audio menu:\nSet volume levels, mute audio, & select in-game music.", true);
        }
    }
}