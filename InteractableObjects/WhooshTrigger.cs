using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On collision with a hazard or pickup plays an audio clip of an object whooshing by.
public class WhooshTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Hazard" || other.gameObject.tag == "Pickup") {
            AudioSource audio = other.gameObject.GetComponent<AudioSource>();
            if (audio) {
                // Set volume to current SFX volume
                audio.volume = GameManager.audioMan.UI_SFXAudioSource.volume;

                // Play audio
                audio.Play();
            }
        }
    }
}