using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles adjusting monitor camera properties (position, rotation, & target display) via keyboard input
public class SpectatorCam : MonoBehaviour {
    [Header("Set in Inspector")]
    public float speed = 20;

    [Header("Set Dynamically")]
    private Camera cam;

    private void Start() {
        cam = GetComponent<Camera>();

        // Set target display by its display index
        cam.targetDisplay = 0;
    }

    void Update() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Move camera
        //Vector3 movement = new Vector3(x, 0, z);
        //movement = Vector3.ClampMagnitude(movement, 1);
        //transform.Translate(movement * speed * Time.deltaTime);

        // Rotate camera
        Vector3 movement = new Vector3(-z, x, 0);
        movement = Vector3.ClampMagnitude(movement, 1);
        transform.Rotate(movement * speed * Time.deltaTime, Space.Self);
    }
}