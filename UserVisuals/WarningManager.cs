using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// On trigger press, deactivates the disclaimer warning the user to not jump and also
// be wary of their surroundings as the playspace can shift during gameplay
public class WarningManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public InputActionReference toggleReference = null;

    void Awake() {
        toggleReference.action.started += DeactivateWarning;
    }

    void OnDestroy() {
        toggleReference.action.started -= DeactivateWarning;
    }

    void DeactivateWarning(InputAction.CallbackContext context) {
        Destroy(gameObject);
    }
}