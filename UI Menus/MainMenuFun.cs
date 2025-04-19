using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// From main menu, select or toggle fun settings:
// BGM, confetti, fireworks, & applause
public class MainMenuFun : MonoBehaviour {
    [Header("Set in Inspector")]
    public TMPro.TextMeshProUGUI        musicTrackText;
    public Button                       previousTrackButton;
    public Button                       nextTrackButton;

    public Toggle                       confettiToggle;
    public Toggle                       fireworksToggle;
    public Toggle                       applauseToggle;
    public Toggle                       allToggle;

    [Header("Set Dynamically")]
    public int                          currentTrackNdx;

    private static MainMenuFun  _S;
    public static MainMenuFun   S { get { return _S; } set { _S = value; } }

    void Awake() {
        S = this;
    }

    void Start() {
        // Add listeners to buttons
        previousTrackButton.onClick.AddListener(delegate { GoToPreviousOrNextTrack(-1); });
        nextTrackButton.onClick.AddListener(delegate { GoToPreviousOrNextTrack(1); });

        // Add listeners to toggle
        confettiToggle.onValueChanged.AddListener(delegate { DoConfetti(); });
        fireworksToggle.onValueChanged.AddListener(delegate { DoFireworks(); });
        applauseToggle.onValueChanged.AddListener(delegate { DoApplause(); });
        allToggle.onValueChanged.AddListener(delegate { DoAll(); });
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
                musicTrackText.text = "Type A";
                break;
            case 1:
                musicTrackText.text = "Type B";
                break;
            case 2:
                musicTrackText.text = "Type C";
                break;
        }
    }

    /////////////////////////////////////////////////////////////////////////
    // Confetti
    public void DoConfetti() {
        GameManager.S.confetti.DoConfetti(confettiToggle.isOn);
    }

    // Fireworks
    public void DoFireworks() {
        GameManager.S.confetti.DoFireworks(fireworksToggle.isOn);
    }

    // Applause
    public void DoApplause() {
        GameManager.S.confetti.DoApplause(applauseToggle.isOn);
    }

    // All
    public void DoAll() {
        GameManager.S.confetti.DoAll(allToggle.isOn);
    }

    public void SetAllToTrue() {
        allToggle.GetComponent<Toggle>().isOn = true;
    }
    public void SetAllToFalse() {
        allToggle.GetComponent<Toggle>().isOn = false;
    }
}