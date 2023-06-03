using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rotates this game object around another game object
public class RotateObject : MonoBehaviour {
    [Header("Set in Inspector")]
    // Game object position to rotate this object around
    public GameObject   targetToRotateAround;

    [Header("Set Dynamically")]
    public bool         isRotatingAroundXAxis;

    void FixedUpdate() {
        // Rotate this object around the target
        if (isRotatingAroundXAxis) {
            transform.RotateAround(targetToRotateAround.transform.position, Vector3.left, (GameManager.S.spawner.currentObjectSpeed * 2) * Time.fixedDeltaTime);
        } else {
            transform.RotateAround(targetToRotateAround.transform.position, Vector3.up, (GameManager.S.spawner.currentObjectSpeed * 2) * Time.fixedDeltaTime);
        }
    }
}