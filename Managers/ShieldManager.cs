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

        if (isActive) {
            // Set player body color to blue cycle
            GameManager.S.playerDamageColldierAnim.CrossFade("PlayerDamageColliderBlue", 0);
        } else {
            // Reset player body color to default
            GameManager.S.playerDamageColldierAnim.CrossFade("PlayerDamageColliderWhite", 0);
        }
    }
}