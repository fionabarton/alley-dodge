using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On trigger enter, if tag is "Climbable" or "Pickup" vibrate hand controller
public class OnEnterVibrateHandController : MonoBehaviour {
    [Header("Set in Inspector")]
    public bool         isLeftHandController;
    public GameObject   sparkParticles;

    private void OnTriggerEnter(Collider other) {
        // If tag is "Climbable" or "Pickup" vibrate hand controller
        if (other.gameObject.tag == "Climbable") {
            // Vibrate left or right hand controller (amplitude, duration)
            if (isLeftHandController) {
                GameManager.S.leftXR.SendHapticImpulse(0.2f, 0.1f);
            } else {
                GameManager.S.rightXR.SendHapticImpulse(0.2f, 0.1f);
            }
        } else if (other.gameObject.tag == "Pickup") {
            // Vibrate left or right hand controller (amplitude, duration)
            if (isLeftHandController) {
                GameManager.S.leftXR.SendHapticImpulse(0.25f, 0.25f);
            } else {
                GameManager.S.rightXR.SendHapticImpulse(0.25f, 0.25f);
            }
        }

        // Instantiate sparks
        Instantiate(sparkParticles, transform.position, transform.rotation);
    }
}