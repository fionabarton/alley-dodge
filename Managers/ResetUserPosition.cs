using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Resets the user's position if they've fallen through the floor
public class ResetUserPosition : MonoBehaviour {
    [Header("Set in Inspector")]
    public Transform            resetPosition;
    [SerializeField] GameObject xrOriginGO;

    public Animator             faderAnim;

    public void ResetPosition() {
        // Play teleport fadeout animation clip
        faderAnim.CrossFade("Fadeout", 0);

        // If game is playing...
        if (GameManager.S.spawner.canSpawn) {
            // Freeze objects and stop spawner
            GameManager.S.spawner.canSpawn = false;
            GameManager.S.spawner.objectsCanMove = false;

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

            // Wait and countdown from 3
            StartCoroutine(GameManager.S.score.Countdown());
        }

        // Set user position to reset position
        var distanceDiff = resetPosition.position - Camera.main.transform.position;
        xrOriginGO.transform.position += distanceDiff;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            ResetPosition();
        }
    }
}