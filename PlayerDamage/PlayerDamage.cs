using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

// Handles what happens after the user collides with a hazardous object
public class PlayerDamage : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject   damageParticleSystemGO;
    public GameObject   deathParticleSystemGO;

    public Animator     damageOverlayAnim;

    // Deactivates the player's invicibility. Is called 0.1 seconds after the player collides with an obstacle.
    void DeactivateInvicibility() {
        GameManager.S.playerIsInvincible = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (!GameManager.S.playerIsInvincible) {
            if (other.gameObject.tag == "Hazard") {
                // Make player invincible to prevent multiple collisions, then undo it after 0.1 seconds
                GameManager.S.playerIsInvincible = true;
                Invoke("DeactivateInvicibility", 0.1f);

                // Send haptic feedback (amplitude, duration) to both controllers
                GameManager.S.LeftXRSendHapticImpuse(1.0f, 0.5f);
                GameManager.S.RightXRSendHapticImpuse(1.0f, 0.5f);

                // Destroy hazard
                Destroy(other.gameObject.transform.parent.gameObject);

                // Increment damage count
                GameManager.S.damageCount += 1;

                // Play damage overlay animation clip
                damageOverlayAnim.CrossFade("Damage", 0);

                // Instantiate exploding cubes
                OnDestroyInstantiateExplodingCubes cubes = other.gameObject.GetComponent<OnDestroyInstantiateExplodingCubes>();
                if (cubes) {
                    cubes.InstantiateCubes();
                }

                // If shield isn't active...
                if (!GameManager.shield.shieldIsActive) {
                    // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    // Stop looping over background music soundtrack
                    GameManager.audioMan.checkForBGMListenerNotPlaying = false;
                    // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

                    // Cache ending time
                    GameManager.S.score.endingTime = Time.time;

                    // Cache ending move and spawn speeds
                    GameManager.S.spawner.SetEndingMoveSpeedForHighScoreEntry();
                    GameManager.S.spawner.SetEndingSpawnSpeedForHighScoreEntry();

                    // Freeze all objects
                    GameManager.S.spawner.objectsCanMove = false;
                    GameManager.S.spawner.canSpawn = false;

                    // Find and destroy all hazard and pickup game objects
                    GameManager.S.DestroyAllObject();

                    // Display text
                    GameManager.S.score.SetDisplayText("GAME OVER!", Color.red, Color.red, eVOX.voxGameOver, false);

                    // Reset object spawner timer properties
                    GameManager.S.spawner.currentSpawnSpeed = GameManager.S.spawner.startingSpawnSpeed;
                    GameManager.S.spawner.timeDone = GameManager.S.spawner.startingSpawnSpeed + Time.time;

                    // Reset object speed 
                    GameManager.S.spawner.currentObjectSpeed = GameManager.S.spawner.startingObjectSpeed;

                    //
                    GameManager.S.score.timerIsOn = false;

                    // Instantiate explosion
                    Instantiate(deathParticleSystemGO, transform.position, transform.rotation);

                    // SFX
                    GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxFireblast);

                    // Play BGM: Lose
                    GameManager.audioMan.PlayBGMClip(eBGM.bgmLose, false);

                    // Stop smoke particle system 
                    GameManager.S.smokePS.Stop();

                    // Activate move UI menu podium
                    GameManager.S.podiums.ActivateMenus(false, true);

                    // Check for high score
                    if (GameManager.S.highScore.CheckForNewHighScore(GameManager.S.score.score)) {
                        // 
                        Invoke("AnnounceHighScore", 2.5f);
                    } else {
                        //
                        Invoke("ActivateStartMenu", 2.5f);
                    }
                } else {
                    // Instantiate explosion
                    Instantiate(damageParticleSystemGO, transform.position, transform.rotation);

                    // SFX
                    GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxFireball);

                    // Deactivate shield
                    GameManager.shield.SetActiveShield(false);

                    // Display text
                    GameManager.S.score.SetDisplayText(GameManager.words.GetRandomInterjection(true) + "!", Color.red, Color.red);
                }
            } else if (other.gameObject.tag == "Pickup") {
                // Send haptic feedback (amplitude, duration) to both controllers
                GameManager.S.LeftXRSendHapticImpuse(0.3333f, 0.25f);
                GameManager.S.RightXRSendHapticImpuse(0.3333f, 0.25f);
            }
        }
    }

    //
    void AnnounceHighScore() {
        // Play confetti particle systems
        GameManager.S.confetti.DropConfetti();

        // Set display text colors
        GameManager.color.SetDisplayTextPalette();

        // Display text
        GameManager.S.score.displayText.text = "NEW\nHIGH SCORE!";

        // Play VOX audio clip
        GameManager.audioMan.PlayVOXClip(eVOX.voxNewHighScore);

        // Play BGM: Win
        GameManager.audioMan.PlayBGMClip(eBGM.bgmWin, false);

        // Set player body & hand color to rainbow cycle
        GameManager.S.playerDamageColldierAnim.CrossFade("PlayerDamageColliderRainbow", 0);
        GameManager.S.playerLeftHandAnim1.CrossFade("RainbowColor", 0);
        GameManager.S.playerLeftHandAnim2.CrossFade("RainbowColor", 0);
        GameManager.S.playerRightHandAnim1.CrossFade("RainbowColor", 0);
        GameManager.S.playerRightHandAnim2.CrossFade("RainbowColor", 0);

        //
        Invoke("ActivateKeyboardMenu", 2.5f);
    }

    //
    void ActivateKeyboardMenu() {
        // Activate keyboard input menu
        GameManager.S.keyboardMenuCS.Activate("NameHighScoreEntry");

        // Activate XR ray interactors
        GameManager.utilities.SetActiveList(GameManager.S.xrRayInteractorsGO, true);

        //// Activate move UI menu podium
        //GameManager.S.podiums.ActivateMenus(false, true);

        // Play BGM: 1940
        GameManager.audioMan.PlayBGMClip(eBGM.bgm1940);
    }

    //
    void ActivateStartMenu() {
        // Reset score for next game
        GameManager.S.score.ResetScore();

        // Activate XR ray interactors
        GameManager.utilities.SetActiveList(GameManager.S.xrRayInteractorsGO, true);

        //// Activate move UI menu podium
        //GameManager.S.podiums.ActivateMenus(false, true);

        // Activate main menu
        GameManager.S.mainMenuGO.SetActive(true);
    }
}