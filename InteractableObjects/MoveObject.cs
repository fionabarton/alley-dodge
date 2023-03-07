using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move an object in a single, specified direction at a specified speed.
public class MoveObject : MonoBehaviour {
    [SerializeField] Vector3    direction = Vector3.forward;

    void FixedUpdate() {
        if (GameManager.S.spawner.objectsCanMove) {
            transform.Translate(direction * Time.fixedDeltaTime * GameManager.S.spawner.currentObjectSpeed);
        }
    }
}