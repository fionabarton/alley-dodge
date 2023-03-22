using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On collision  with player, plays SFX scream audio clip.
public class PlaySFXScreamTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            // Play SFX
            GameManager.audioMan.PlayUISFXClip(eSFX.sfxScream);
        }
    }
}