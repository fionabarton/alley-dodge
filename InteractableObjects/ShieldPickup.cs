using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class ShieldPickup : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject explosionGO;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            // Instantiate particle system
            Instantiate(explosionGO, transform.position, transform.rotation);

            // SFX

            // Activate shield
            GameManager.shield.SetActiveShield(true);

            // Display text
            GameManager.S.score.SetDisplayText("SHIELD!", Color.blue, Color.blue);

            // Destroy this GO
            Destroy(gameObject);
        }
    }
}