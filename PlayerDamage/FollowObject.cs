using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script ensures the player's damage collider follows the Main Camera/HMD (XR Origin).
// Don't forget to constrain the collider’s rotation on x and z axes.
public class FollowObject : MonoBehaviour {
    [Header("Set in Inspector")]
    public Transform objectToFollow; // Main Camera

    void Update() {
        Vector3 tPos = transform.position;
        tPos = new Vector3 (objectToFollow.position.x, objectToFollow.position.y - 0.6666666f, objectToFollow.position.z);
        transform.position = tPos;
    }
}