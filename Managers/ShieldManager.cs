using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject shieldPS;
    public GameObject shieldUI;

    [Header("Set Dynamically")]
    public bool shieldIsActive = false;

    public void SetActiveShield(bool isActive) {
        shieldIsActive = isActive;
        shieldPS.SetActive(isActive);
        shieldUI.SetActive(isActive);
    }
}