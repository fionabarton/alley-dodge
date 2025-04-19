using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// On trigger press, deactivates the disclaimer warning the user to not jump and also
// be wary of their surroundings as the playspace can shift during gameplay
public class WarningManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public Transform            objectToFollow; // Main Camera > WarningCanvasObjectToFollow

    public GameObject           explosionGO;

    public InputActionReference toggleReference = null;

    public float                speed = 1.0f;

    void Awake() {
        toggleReference.action.started += DeactivateWarning;
    }

    void OnDestroy() {
        toggleReference.action.started -= DeactivateWarning;
    }

    // Destroy this game object
    void DeactivateWarning(InputAction.CallbackContext context) {
        // Instantiate particle system
        Instantiate(explosionGO, transform.position, transform.rotation);

        // Play SFX
        GameManager.audioMan.PlayUISFXClip(eSFX.sfxConfirm);

        // Set player height based on camera's y-position
        //GameManager.S.mainMenuCS.SetPlayerHeight();

        // Destroy this game object
        Destroy(gameObject);
    }

    private void LateUpdate() {
        // Set position and rotation to that of the target game object
        var step = speed * Time.fixedDeltaTime; 
        transform.position = Vector3.MoveTowards(transform.position, objectToFollow.position, step);
        transform.rotation = objectToFollow.transform.rotation;
    }
}