using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// On grip input when not climbing, rotates claw hands open or shut.
[RequireComponent(typeof(InputData))]
public class ClawHandRotation : MonoBehaviour {
    private InputData   _inputData;

    private float       leftCachedVal = 999;
    private float       rightCachedVal = 999;

    private void Start() {
        _inputData = GetComponent<InputData>();
    }

    void Update() {
        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.grip, out float leftTriggerValue)) {
            if(leftTriggerValue != leftCachedVal) {
                if(leftTriggerValue >= 1) {
                    // Rotate hands closed
                    GameManager.S.playerLeftHandTrans1.localEulerAngles = new Vector3(
                        -89,
                        GameManager.S.playerLeftHandTrans1.localEulerAngles.y,
                        GameManager.S.playerLeftHandTrans1.localEulerAngles.z
                    );
                    GameManager.S.playerLeftHandTrans2.localEulerAngles = new Vector3(
                        -89,
                        GameManager.S.playerLeftHandTrans2.localEulerAngles.y,
                        GameManager.S.playerLeftHandTrans2.localEulerAngles.z
                    );
                } else {
                    // Rotate hands open
                    GameManager.S.playerLeftHandTrans1.localEulerAngles = new Vector3(
                        -60,
                        GameManager.S.playerLeftHandTrans1.localEulerAngles.y,
                        GameManager.S.playerLeftHandTrans1.localEulerAngles.z
                    );
                    GameManager.S.playerLeftHandTrans2.localEulerAngles = new Vector3(
                        -60,
                        GameManager.S.playerLeftHandTrans2.localEulerAngles.y,
                        GameManager.S.playerLeftHandTrans2.localEulerAngles.z
                    );
                }

                leftCachedVal = leftTriggerValue;
            }
        }

        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.grip, out float rightTriggerValue)) {
            if (rightTriggerValue != rightCachedVal) {
                if (rightTriggerValue >= 1) {
                    // Rotate hands closed
                    GameManager.S.playerRightHandTrans1.localEulerAngles = new Vector3(
                        -89,
                        GameManager.S.playerRightHandTrans1.localEulerAngles.y,
                        GameManager.S.playerRightHandTrans1.localEulerAngles.z
                    );
                    GameManager.S.playerRightHandTrans2.localEulerAngles = new Vector3(
                        -89,
                        GameManager.S.playerRightHandTrans2.localEulerAngles.y,
                        GameManager.S.playerRightHandTrans2.localEulerAngles.z
                    );
                } else {
                    // Rotate hands open
                    GameManager.S.playerRightHandTrans1.localEulerAngles = new Vector3(
                        -60,
                        GameManager.S.playerRightHandTrans1.localEulerAngles.y,
                        GameManager.S.playerRightHandTrans1.localEulerAngles.z
                    );
                    GameManager.S.playerRightHandTrans2.localEulerAngles = new Vector3(
                        -60,
                        GameManager.S.playerRightHandTrans2.localEulerAngles.y,
                        GameManager.S.playerRightHandTrans2.localEulerAngles.z
                    );
                }

                rightCachedVal = rightTriggerValue;
            }
        }
    }
}