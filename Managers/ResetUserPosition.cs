using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Resets the user's position if they've fallen through the floor
public class ResetUserPosition : MonoBehaviour {
    [Header("Set in Inspector")]
    public Transform            resetPosition;
    [SerializeField] GameObject xrOriginGO;

    public Animator             faderAnim;

    [Header("Set Dynamically")]
    Vector3                     resetDestination;

    public void ResetPosition() {
        // Play teleport fadeout animation clip
        faderAnim.CrossFade("Fadeout", 0);

        // If game is playing...
        if (GameManager.S.spawner.canSpawn) {
            // Freeze objects and stop spawner
            GameManager.S.spawner.PauseSpawner();

            // Find all hazards and pickups
            GameObject[] hazards = GameObject.FindGameObjectsWithTag("Hazard");
            GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");

            // Destroy all hazards and pickups in the user's playspace
            for (int i = 0; i < hazards.Length; i++) {
                if (hazards[i].transform.position.z <= 2) {
                    Destroy(hazards[i]);
                }
            }
            for (int i = 0; i < pickups.Length; i++) {
                if (pickups[i].transform.position.z <= 2) {
                    Destroy(pickups[i]);
                }
            }

            // Pause ScoreManager timer
            GameManager.S.score.PauseTimer();

            // Wait and countdown from 3
            StartCoroutine(GameManager.S.score.Countdown());
        }

        // Set user position to reset position
        //var distanceDiff = resetPosition.position - Camera.main.transform.position;
        //xrOriginGO.transform.position += distanceDiff;

        // Cache distance between user and reset position
        resetDestination = resetPosition.position - Camera.main.transform.position;

        Invoke("SetUserPosition", 0.25f);
    }

    // Set user position to reset position
    public void SetUserPosition() {
        xrOriginGO.transform.position += resetDestination;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            ResetPosition();
        }
    }
}