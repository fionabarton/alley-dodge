using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class DisplayPodiumManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject exitRunPodiumLeft;
    public GameObject exitRunPodiumRight;
    public GameObject moveUIPodium;

    void Start() {
        // Activate move UI menu podium
        ActivateMenus(false, true);
    }

    // Start game, pause game, game over, 

    // (De)activate specific menu podiums (exit run, move UI)
    public void ActivateMenus(bool activateExitRunPodiums, bool activateMoveUIPodium) {
        exitRunPodiumLeft.SetActive(activateExitRunPodiums);
        exitRunPodiumRight.SetActive(activateExitRunPodiums);
        moveUIPodium.SetActive(activateMoveUIPodium);
    }
}