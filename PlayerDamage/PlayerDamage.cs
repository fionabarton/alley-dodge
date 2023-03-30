using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

// Handles what happens after the user collides with a hazardous object
public class PlayerDamage : MonoBehaviour {
    [Header("Set in Inspector")]
    public XRController leftXR;
    public XRController rightXR;

    public GameObject   damageParticleSystemGO;
    public GameObject   deathParticleSystemGO;

    public Animator     damageOverlayAnim;

    private void OnTriggerEnter(Collider other) {
        if (!GameManager.S.playerIsInvincible) {
            if (other.gameObject.tag == "Hazard") {
                // Haptic feedback (amplitude, duration)
                leftXR.SendHapticImpulse(0.25f, 0.5f);
                rightXR.SendHapticImpulse(0.25f, 0.5f);

                // Destroy hazard
                Destroy(other.gameObject);

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
                    // Cache ending time
                    GameManager.S.score.endingTime = Time.time;

                    // Freeze all objects
                    GameManager.S.spawner.objectsCanMove = false;
                    GameManager.S.spawner.canSpawn = false;

                    // Find and destroy all hazard and pickup game objects
                    GameManager.S.DestroyAllObject();

                    // Display text
                    GameManager.S.score.SetDisplayText("GAME OVER!", Color.red, Color.red, false);

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
                    GameManager.S.score.SetDisplayText(GameManager.words.GetRandomInterjection() + "!", Color.red, Color.red);
                }
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

        // Play BGM: Win
        GameManager.audioMan.PlayBGMClip(eBGM.bgmWin, false);

        // Set player color to rainbow cycle
        GameManager.S.playerDamageColldierAnim.CrossFade("RainbowColor", 0);
        GameManager.S.playerLeftHandAnim.CrossFade("RainbowColor", 0);
        GameManager.S.playerRightHandAnim.CrossFade("RainbowColor", 0);

        //
        Invoke("ActivateKeyboardMenu", 2.5f);
    }

    //
    void ActivateKeyboardMenu() {
        // Activate keyboard input menu
        GameManager.S.keyboardMenuGO.SetActive(true);

        // Display saved name input string
        GameManager.S.keyboardMenuCS.GetInputString();

        // Activate XR ray interactors
        GameManager.utilities.SetActiveList(GameManager.S.xrRayInteractorsGO, true);

        // Play BGM: 1940
        GameManager.audioMan.PlayBGMClip(eBGM.bgm1940);
    }

    //
    void ActivateStartMenu() {
        // Reset score for next game
        GameManager.S.score.ResetScore();

        // Activate XR ray interactors
        GameManager.utilities.SetActiveList(GameManager.S.xrRayInteractorsGO, true);

        // Activate main menu
        GameManager.S.mainMenuGO.SetActive(true);
    }
}