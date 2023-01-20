using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script ensures the player's damage collider follows the Main Camera/HMD (XR Origin).
// Don't forget to constrain the collider’s rotation on x and z axes.
public class DamageColliderFollow : MonoBehaviour {
    [Header("Set in Inspector")]
    public Transform    objectToFollow; // Main Camera

    public float        yOffset = 0.6666666f;
    public float        yScale = 0.9f;

    private void Start() {
        // Set scale
        Invoke("SetScale", 0.1f);

        // Start following
        StartCoroutine("Follow");
    }

    public void SetScale() {
        GameManager.utilities.SetScale(gameObject, transform.localScale.x, yScale, transform.localScale.z);
    }

    IEnumerator Follow() {
        Vector3 tPos = transform.position;
        tPos = new Vector3(objectToFollow.position.x, objectToFollow.position.y - yOffset, objectToFollow.position.z);
        transform.position = tPos;

        yield return null;
        StartCoroutine("Follow");
    }
}