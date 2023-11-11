using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On trigger enter, if tag is "Climbable" or "Pickup" vibrate hand controller
public class OnEnterVibrateHandController : MonoBehaviour {
    [Header("Set in Inspector")]
    public bool isLeftHandController;

    [Header("Set dynamically")]
    float       amplitude = 0;
    float       duration = 0;

    private void OnTriggerEnter(Collider other) {
        // If tag is "Climbable" or "Pickup" vibrate hand controller
        if (other.gameObject.tag == "Climbable") {
            amplitude = 0.15f;
            duration = 0.15f;

            VibrateController();
        } else if (other.gameObject.tag == "Pickup") {
            amplitude = 0.25f;
            duration = 0.25f;

            VibrateController();
        }
    }

    // Vibrate left or right hand controller (amplitude, duration)
    void VibrateController() {
        if (isLeftHandController) {
            GameManager.S.leftXR.SendHapticImpulse(amplitude, duration);
        } else {
            GameManager.S.rightXR.SendHapticImpulse(amplitude, duration);
        }
    }
}