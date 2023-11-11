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

    public List<GameObject>         gameObjectsInvisibleToHMD;

    [Header("Set Dynamically")]
    private Camera                  cam;

    public List<Vector3>            defaultTransformPositions;
    public List<Quaternion>         defaultTransformRotations;

    public List<Vector3>            currentTransformPositions;
    public List<Quaternion>         currentTransformRotations;

    public int                      currentDisplayNdx = 0;

    public float autoCamStartingPosX = 0;

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

        // Display text
        displayText.text = "Display: <color=white>Look at Spawner</color>";

        // Set transform to look at
        targetTrans = transforms[0];
    }

    void Update() {
        if (currentDisplayNdx != 2) {
            float x = Input.GetAxis("Horizontal");
            //autoCamStartingPosX += Time.fixedDeltaTime;
            float y = Input.GetAxis("Vertical");

            // Move camera
            Vector3 movement = new Vector3(x, y, 0);
            //Vector3 movement = new Vector3(autoCamStartingPosX, y, 0);
            movement = Vector3.ClampMagnitude(movement, 1);
            transform.Translate(movement * speed * Time.fixedDeltaTime);
        }

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
                // Set parent transform
                gameObject.transform.SetParent(null);

                // Make objects visible to spectator cam
                //SetObjectsLayer("Invisible to HMD");
                // Activate objects invisible to HMD
                GameManager.utilities.SetActiveList(gameObjectsInvisibleToHMD, true);

                // Set transform to look at
                targetTrans = transforms[0];

                // Set transform position & rotation to cached values
                gameObject.transform.position = currentTransformPositions[0];
                gameObject.transform.rotation = currentTransformRotations[0];

                // Display text
                displayText.text = "Display: <color=white>Look at Spawner</color>";
            } else if (currentDisplayNdx == 1) {
                // Set transform to look at
                targetTrans = transforms[1];

                // Make objects visible to spectator cam
                //SetObjectsLayer("Invisible to HMD");
                // Activate objects invisible to HMD
                GameManager.utilities.SetActiveList(gameObjectsInvisibleToHMD, true);

                // Set transform position & rotation to cached values
                gameObject.transform.position = currentTransformPositions[1];
                gameObject.transform.rotation = currentTransformRotations[1];

                // Display text
                displayText.text = "Display: <color=white>Look at Player</color>";
            } else {
                // Set parent transform
                gameObject.transform.SetParent(Camera.main.transform);

                // Make objects invisible to spectator cam
                //SetObjectsLayer("Invisible to Spectator Cam");
                //SetObjectsLayer("Invisible to HMD");
                // Deactivate objects invisible to HMD
                GameManager.utilities.SetActiveList(gameObjectsInvisibleToHMD, false);

                // Set position and rotation to 0
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localRotation = Quaternion.identity;

                // Display text
                displayText.text = "Display: <color=white>Player HMD</color>";
            }
        }

        if (currentDisplayNdx != 2) {
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
            if (Vector3.Distance(targetTrans.position, transform.position) >= 7.5f) {
                if (Input.GetButton("CamForward")) {
                    transform.Translate(Vector3.forward * Time.fixedDeltaTime * speed);
                }
            }

            // Zoom out
            if (Input.GetButton("CamBackward")) {
                transform.Translate(Vector3.back * Time.fixedDeltaTime * speed);
            }
        } 
    }

    // Make objects (in)visible to spectator cam
    //void SetObjectsLayer(string layerName) {
    //    int layerNdx = LayerMask.NameToLayer(layerName);
    //    gameObject.layer = layerNdx;
    //    for (int i = 0; i < gameObjectsInvisibleToHMD.Count; i++) {
    //        gameObjectsInvisibleToHMD[i].layer = layerNdx;
    //    }
    //}

    private void LateUpdate() {
        if (currentDisplayNdx != 2) {
            // Aim camera at target
            transform.LookAt(targetTrans);
        }
    }
}