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
                string scoreStr = GameManager.S.score.score.ToString();
                //if (scoreStr.EndsWith("1") || scoreStr.EndsWith("6")) {
                //    GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxQuid1);
                //} else if (scoreStr.EndsWith("2") || scoreStr.EndsWith("7")) {
                //    GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxQuid2);
                //} else if (scoreStr.EndsWith("3") || scoreStr.EndsWith("8")) {
                //    GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxQuid3);
                //} else if (scoreStr.EndsWith("4") || scoreStr.EndsWith("9")) {
                //    GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxQuid4);
                //} else if (scoreStr.EndsWith("5") || scoreStr.EndsWith("0")) {
                //    GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxQuid5);
                //}

                // SFX
                if(scoreStr.EndsWith("0") || scoreStr.EndsWith("5")) {
                    GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxBuff1);
                } else {
                    GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxConfirm);
                }

                // Destroy this GO
                Destroy(gameObject);
            }
        }
    }
}