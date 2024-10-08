// Please note that I, Fiona Barton, did not write this code.
// It was sourced from:
// https://github.com/Fist-Full-of-Shrimp/Unity-VR-Basics-2022/tree/main/Assets/Scripts/14%20Climbing

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClimbProvider : MonoBehaviour {
    public static event Action ClimbActive;
    public static event Action ClimbInActive;

    public CharacterController characterController;
    public InputActionProperty velocityRight;
    public InputActionProperty velocityLeft;

    private bool _rightActive = false;
    private bool _leftActive = false;

    private void Start() {
        XRDirectClimbInteractor.ClimbHandActivated += HandActivated;
        XRDirectClimbInteractor.ClimbHandDeactivated += HandDeactivated;
    }

    private void OnDestroy() {
        XRDirectClimbInteractor.ClimbHandActivated -= HandActivated;
        XRDirectClimbInteractor.ClimbHandDeactivated -= HandDeactivated;
    }

    private void HandActivated(string _controllerName) {
        if (_controllerName == "LeftHand Controller") {
            _leftActive = true;
            _rightActive = false;

            GameManager.S.LeftXRSendHapticImpuse(1.0f, 0.1f);
        } else {
            _leftActive = false;
            _rightActive = true;

            GameManager.S.RightXRSendHapticImpuse(1.0f, 0.1f);
        }

        ClimbActive?.Invoke();
    }

    private void HandDeactivated(string _controllerName) {
        if (_rightActive && _controllerName == "RightHand Controller") {
            _rightActive = false;
            ClimbInActive?.Invoke();
        } else if (_leftActive && _controllerName == "LeftHand Controller") {
            _leftActive = false;
            ClimbInActive?.Invoke();
        }
    }

    private void FixedUpdate() {
        if (_rightActive || _leftActive) {
            Climb();
        }
    }

    private void Climb() {
        Vector3 velocity = _leftActive ? velocityLeft.action.ReadValue<Vector3>() : velocityRight.action.ReadValue<Vector3>();

        characterController.Move(characterController.transform.rotation * -velocity * Time.fixedDeltaTime);
    }
}