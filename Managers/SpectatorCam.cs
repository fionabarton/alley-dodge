using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles adjusting monitor camera properties (position, rotation, & target display) via keyboard input
public class SpectatorCam : MonoBehaviour {
    [Header("Set in Inspector")]
    public float                    speed = 10;

    public TMPro.TextMeshProUGUI    displayText;

    public Transform                targetTrans;
    public List<Transform>          transforms; // 0: (Room > ObjectSpawner), 1: (Room > Handles > Row1 > Handle(2))

    [Header("Set Dynamically")]
    private Camera                  cam;

    public List<Vector3>            defaultTransformPositions;
    public List<Quaternion>         defaultTransformRotations;

    public List<Vector3>            currentTransformPositions;
    public List<Quaternion>         currentTransformRotations;

    public int                      currentDisplayNdx = 0;

    private void Start() {
        cam = GetComponent<Camera>();

        // Cache transform's starting position & rotation
        defaultTransformPositions[0] = gameObject.transform.position;
        defaultTransformRotations[0] = gameObject.transform.rotation;
        defaultTransformPositions[1] = gameObject.transform.position;
        defaultTransformRotations[1] = gameObject.transform.rotation;

        currentTransformPositions[0] = gameObject.transform.position;
        currentTransformRotations[0] = gameObject.transform.rotation;
        currentTransformPositions[1] = gameObject.transform.position;
        currentTransformRotations[1] = gameObject.transform.rotation;

        // Set target display by its display index
        cam.targetDisplay = 0;

        //
        displayText.text = "Display: <color=white>Look at Spawner</color>";

        // Set transform to look at
        targetTrans = transforms[0];
    }

    void Update() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // Move camera
        Vector3 movement = new Vector3(x, y, 0);
        movement = Vector3.ClampMagnitude(movement, 1);
        transform.Translate(movement * speed * Time.fixedDeltaTime);

        // Switch monitor's target display
        if (Input.GetKeyDown(KeyCode.V)) {
            // Cache transform position & rotation
            if (currentDisplayNdx == 0) {
                currentTransformPositions[0] = gameObject.transform.position;
                currentTransformRotations[0] = gameObject.transform.rotation;
            } else if (currentDisplayNdx == 1) {
                currentTransformPositions[1] = gameObject.transform.position;
                currentTransformRotations[1] = gameObject.transform.rotation;
            } 

            // Increment currentDisplayNdx
            if (currentDisplayNdx < 2) {
                currentDisplayNdx += 1;
            } else {
                currentDisplayNdx = 0;
            }

            // Set display properties
            if (currentDisplayNdx == 0) {
                cam.targetDisplay = 0;
                displayText.text = "Display: <color=white>Look at Spawner</color>";

                // Set transform to look at
                targetTrans = transforms[0];

                // Set transform position & rotation to cached values
                gameObject.transform.position = currentTransformPositions[0];
                gameObject.transform.rotation = currentTransformRotations[0];
            } else if (currentDisplayNdx == 1) {
                cam.targetDisplay = 0;
                displayText.text = "Display: <color=white>Look at Player</color>";

                // Set transform to look at
                targetTrans = transforms[1];

                // Set transform position & rotation to cached values
                gameObject.transform.position = currentTransformPositions[1];
                gameObject.transform.rotation = currentTransformRotations[1];
            } else {
                cam.targetDisplay = 1;
                displayText.text = "Display: <color=white>Player HMD</color>";
            }
        }

        // Reset transform position & rotation to their default values
        if (Input.GetKeyDown(KeyCode.R)) {
            if (currentDisplayNdx == 0) {
                gameObject.transform.position = defaultTransformPositions[0];
                gameObject.transform.rotation = defaultTransformRotations[0];
            } else if (currentDisplayNdx == 1) {
                gameObject.transform.position = defaultTransformPositions[1];
                gameObject.transform.rotation = defaultTransformRotations[1];
            }
        }

        // Zoom in
        if(Vector3.Distance(targetTrans.position, transform.position) >= 7.5f) {
            if (Input.GetButton("CamForward")) {
                transform.Translate(Vector3.forward * Time.fixedDeltaTime * speed);
            }
        }

        // Zoom out
        if (Input.GetButton("CamBackward")) {
            transform.Translate(Vector3.back * Time.fixedDeltaTime * speed);
        }
    }

    private void LateUpdate() {
        // Aim camera at target
        transform.LookAt(targetTrans);
    }
}