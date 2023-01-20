using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class QuidPickup : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject explosionGO;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            // Increase score
            //GameManager.S.score.AddToScore(GameManager.S.score.level);
            GameManager.S.score.AddToScore(1);

            // Instantiate particle system
            Instantiate(explosionGO, transform.position, transform.rotation);

            // SFX


            // Destroy this GO
            Destroy(gameObject);
        }
    }
}