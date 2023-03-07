using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles what happens after the player has collided with a quid pickup
public class QuidPickup : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject   explosionGO;

    [Header("Set Dynamically")]
    bool                isTriggered;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            if (!isTriggered) {
                isTriggered = true;

                // Increase score;
                GameManager.S.score.AddToScore(1);

                // Instantiate particle system
                SphereCollider sphereCollider = other.GetComponent<SphereCollider>();
                if (sphereCollider) { // Collided with hand
                    Instantiate(explosionGO, GameManager.utilities.GetMidpoint(gameObject, other.gameObject), transform.rotation);
                } else { // Collided with body
                    float xPos = other.transform.position.x + (gameObject.transform.position.x - other.transform.position.x) / 2;
                    float zPos = other.transform.position.z + (gameObject.transform.position.z - other.transform.position.z) / 2;
                    Vector3 midPoint = new Vector3(xPos, transform.position.y, zPos);
                    Instantiate(explosionGO, midPoint, transform.rotation);
                }

                // SFX
                GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxConfirm);

                // Destroy this GO
                Destroy(gameObject);
            }
        }
    }
}