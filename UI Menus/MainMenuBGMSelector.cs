using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBGMSelector : MonoBehaviour {
    [Header("Set in Inspector")]
    public TMPro.TextMeshProUGUI        musicTrackText;
    public Button                       previousTrackButton;
    public Button                       nextTrackButton;

    [Header("Set Dynamically")]
    public int                          currentTrackNdx;

    private static MainMenuBGMSelector  _S;
    public static MainMenuBGMSelector   S { get { return _S; } set { _S = value; } }

    void Awake() {
        S = this;
    }

    void Start() {
        // Add listeners to buttons
        previousTrackButton.onClick.AddListener(delegate { GoToPreviousOrNextTrack(-1); });
        nextTrackButton.onClick.AddListener(delegate { GoToPreviousOrNextTrack(1); });
    }

    // Selects either the previous or next track as 'Starting Music Track'
    public void GoToPreviousOrNextTrack(int amountToChange) {
        // Change soundtrack index
        currentTrackNdx += amountToChange;

        // Reset index if out of range
        if (currentTrackNdx > 2) {
            currentTrackNdx = 0;
        } else if (currentTrackNdx < 0) {
            currentTrackNdx = 2;
        }

        // Play soundtrack sample
        PlayCurrentTrack();
    }

    public void PlayCurrentTrack() {
        switch (currentTrackNdx) {
            case 0:
                GameManager.audioMan.PlayBGMClip(eBGM.bgmSoap);
                break;
            case 1:
                GameManager.audioMan.PlayBGMClip(eBGM.bgmNinja);
                break;
            case 2:
                GameManager.audioMan.PlayBGMClip(eBGM.bgmThings);
                break;
        }

        // Set track name text
        SetStartingTrackName(currentTrackNdx);

        // Save settings
        PlayerPrefs.SetInt("Current Main Menu Audio Track Index", currentTrackNdx);
    }

    void SetStartingTrackName(int ndx) {
        switch (ndx) {
            case 0:
                musicTrackText.text = "Music: <color=#D9D9D9>Track 1</color>";
                break;
            case 1:
                musicTrackText.text = "Music: <color=#D9D9D9>Track 2</color>";
                break;
            case 2:
                musicTrackText.text = "Music: <color=#D9D9D9>Track 3</color>";
                break;
        }
    }
}