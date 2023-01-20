using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

//
public class PlayerDamage : MonoBehaviour {
    [Header("Set in Inspector")]
    public XRController leftXR;
    public XRController rightXR;

    public GameObject   explosionGO;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Hazard") {
            // Haptic feedback (amplitude, duration)
            leftXR.SendHapticImpulse(0.25f, 0.5f);
            rightXR.SendHapticImpulse(0.25f, 0.5f);

            // SFX

            // Instantiate explosion
            Instantiate(explosionGO, transform.position, transform.rotation);

            // Destroy hazard
            Destroy(other.gameObject);

            // If shield isn't active...
            if (!GameManager.shield.shieldIsActive) {
                // Cache ending time
                GameManager.S.score.endingTime = Time.time;

                // Freeze all objects
                GameManager.S.spawner.objectsCanMove = false;
                GameManager.S.spawner.canSpawn = false;

                // Find all hazards and pickups
                GameObject[] hazards = GameObject.FindGameObjectsWithTag("Hazard");
                GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");

                // Destroy all hazards and pickups
                for (int i = 0; i < hazards.Length; i++) {
                    Destroy(hazards[i]);
                }
                for (int i = 0; i < pickups.Length; i++) {
                    Destroy(pickups[i]);
                }

                // Display text
                GameManager.S.score.SetDisplayText("GAME OVER!", Color.red, Color.red, false);
 
                // Reset object spawner timer properties
                GameManager.S.spawner.timeDuration = 2.0f;
                GameManager.S.spawner.timeDone = GameManager.S.spawner.timeDuration + Time.time;

                //
                GameManager.S.score.timerIsOn = false;

                // Check for high score
                if (GameManager.S.highScore.CheckForNewHighScore(GameManager.S.score.score)) {
                    //
                    Invoke("AnnounceHighScore", 2.5f);
                } else {
                    // Reset score for next game
                    GameManager.S.score.ResetScore();

                    // Activate XR ray interactors
                    GameManager.utilities.SetActiveList(GameManager.S.xrRayInteractorsGO, true);

                    // Activate Start Game/Options menu
                    GameManager.S.startGameMenuGO.SetActive(true);
                }
            } else {
                // Deactivate shield
                GameManager.shield.SetActiveShield(false);

                // Display text
                GameManager.S.score.SetDisplayText(GameManager.words.GetRandomInterjection() + "!", Color.red, Color.red);
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

        //
        Invoke("ActivateKeyboardMenu", 2.5f);
    }

    //
    void ActivateKeyboardMenu() {
        // Activate keyboard input menu
        GameManager.S.keyboardMenuGO.SetActive(true);

        // Activate XR ray interactors
        GameManager.utilities.SetActiveList(GameManager.S.xrRayInteractorsGO, true);
    }
}