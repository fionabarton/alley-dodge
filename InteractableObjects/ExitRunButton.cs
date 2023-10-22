using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On collision with player hands, asks user if they'd like to quit run and return to main menu
public class ExitRunButton : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject   sparksGO;

    [Header("Set dynamically")]
    public bool         isPressed;

    public float        bgmAudioSourceVolume;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "PlayerHand") {
            if (!isPressed) {
                isPressed = true;
                AddExitRunConfirmationListeners();
            }
        }
    }

    // Adds functions to the sub menu's yes/no buttons
    void AddExitRunConfirmationListeners() {
        // Instantiate sparks
        Instantiate(sparksGO, transform.position, transform.rotation);

        // Prevent player from interacting with hazards & pickups
        GameManager.S.playerIsInvincible = true;

        // Set exit run button position
        GameManager.utilities.SetLocalPosition(GameManager.S.exitRunButtonLeftCS.gameObject, -1.624f, 1.142f);
        GameManager.utilities.SetLocalPosition(GameManager.S.exitRunButtonRightCS.gameObject, -1.624f, 1.142f);

        // Audio: Damage
        GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxDamage2);

        // Cache BGM audio source volume
        bgmAudioSourceVolume = GameManager.audioMan.BGMAudioSource.volume;

        // Mute music
        GameManager.audioMan.BGMAudioSource.volume = 0;

        // Freeze objects and stop spawner
        GameManager.S.spawner.PauseSpawner();

        // Activate XR ray interactors
        GameManager.utilities.SetActiveList(GameManager.S.xrRayInteractorsGO, true);

        // Find all DestroyOverTime
        DestroyOverTime[] destroyOverTime = FindObjectsOfType<DestroyOverTime>();

        // Pause all DestroyOverTime timers
        for (int i = 0; i < destroyOverTime.Length; i++) {
            destroyOverTime[i].PauseTimer();
        }

        // Pause ScoreManager timer
        GameManager.S.score.PauseTimer();

        // Increment exit run count
        GameManager.S.pauseCount += 1;

        // Activate move UI menu podium
        GameManager.S.podiums.ActivateMenus(true, true);

        // Activate sub menu
        GameManager.S.subMenuCS.AddListeners(ExitRun, "Are you sure that you would like to end this run\nand return to the main menu?");
    }
    // On 'Yes' button click, quits run and returns to main menu
    void ExitRun(int yesOrNo = -1) {
        // Deactivate sub menu
        GameManager.S.subMenuGO.SetActive(false);

        // Unmute audio
        GameManager.audioMan.BGMAudioSource.volume = bgmAudioSourceVolume;

        // 
        if (yesOrNo == 0) {
            // Reset exit run button positions and allow them to be pressed again
            GameManager.S.exitRunButtonLeftCS.ResetButton();
            GameManager.S.exitRunButtonRightCS.ResetButton();

            // Reset score for next game
            GameManager.S.score.ResetScore();

            // Find and destroy all hazard and pickup game objects
            GameManager.S.DestroyAllObject();

            // Activate move UI menu podium
            GameManager.S.podiums.ActivateMenus(false, true);

            // Activate main menu
            GameManager.S.mainMenuGO.SetActive(true);

            // Deactivate shield
            GameManager.shield.SetActiveShield(false);
        } else {
            // Deactivate XR ray interactors
            GameManager.utilities.SetActiveList(GameManager.S.xrRayInteractorsGO, false);

            // Find all DestroyOverTime
            DestroyOverTime[] destroyOverTime = FindObjectsOfType<DestroyOverTime>();

            // Unpause all DestroyOverTime timers
            for (int i = 0; i < destroyOverTime.Length; i++) {
                destroyOverTime[i].UnpauseTimer();
            }

            // Wait and countdown from 3
            StartCoroutine(GameManager.S.score.Countdown());
        }
    }

    // Resets button's position and allows it to be pressed again
    public void ResetButton() {
        isPressed = false;

        // Reset button position
        GameManager.utilities.SetLocalPosition(gameObject, -1.575f, 1.191f);
    }
}