// Please note that I, Fiona Barton, did not write this code.
// It was sourced from:
// https://github.com/Fist-Full-of-Shrimp/FFOS-VR-Tutorial-Resources/tree/main/FFOSControllerData

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// Provides access to XR input data from both VR hand controllers and HMD.
public class InputData : MonoBehaviour {
    public InputDevice _rightController;
    public InputDevice _leftController;
    public InputDevice _HMD;

    void Update() {
        if (!_rightController.isValid || !_leftController.isValid || !_HMD.isValid)
            InitializeInputDevices();
    }
    private void InitializeInputDevices() {

        if (!_rightController.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref _rightController);
        if (!_leftController.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, ref _leftController);
        if (!_HMD.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.HeadMounted, ref _HMD);
    }

    private void InitializeInputDevice(InputDeviceCharacteristics inputCharacteristics, ref InputDevice inputDevice) {
        List<InputDevice> devices = new List<InputDevice>();
        //Call InputDevices to see if it can find any devices with the characteristics we're looking for
        InputDevices.GetDevicesWithCharacteristics(inputCharacteristics, devices);

        //Our hands might not be active and so they will not be generated from the search.
        //We check if any devices are found here to avoid errors.
        if (devices.Count > 0) {
            inputDevice = devices[0];
        }
    }
}