// Please note that I, Fiona Barton, did not write all of this code.
// It was sourced from:
// https://github.com/Fist-Full-of-Shrimp/Unity-VR-Basics-2022/tree/main/Assets/Scripts/14%20Climbing

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class XRDirectClimbInteractor : XRDirectInteractor {
    public static event Action<string> ClimbHandActivated;
    public static event Action<string> ClimbHandDeactivated;

    private string _controllerName;

    protected override void Start() {
        base.Start();
        _controllerName = gameObject.name;
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args) {
        base.OnSelectEntered(args);

        if (args.interactableObject.transform.gameObject.tag == "Climbable") {
            ClimbHandActivated?.Invoke(_controllerName);
        }
    }
    protected override void OnSelectExited(SelectExitEventArgs args) {
        base.OnSelectExited(args);

        ClimbHandDeactivated?.Invoke(_controllerName);
    }
}