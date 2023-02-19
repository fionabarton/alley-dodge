using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sets the user's left & right hand's material's colors depending on their current interaction (hover, hold, release)
// with a climbable XRGrabInteractable object.
public class SetHandMaterialColor : MonoBehaviour {
    [Header("Set in Inspector")]
    public Material         handMaterial;
    public GameObject       handParticles;
    public GameObject       sparkParticles;

    public bool             isLeftHand;
    
    // Red w/ slight opacity
    //public void OnHoverEntered() {
    //    handMaterial.SetColor("_Color", new Color(1.0f, 0.0f, 0.0f, 0.25f));
    //}

    // White w/ slight opacity
    public void OnSelectEntered() {
        //handMaterial.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 0.25f));

        handParticles.SetActive(true);

        // Set animation clip
        if (isLeftHand) {
            GameManager.S.playerLeftHandAnim.CrossFade("RainbowColor", 0);
        } else {
            GameManager.S.playerRightHandAnim.CrossFade("RainbowColor", 0);
        }

        // Instantiate sparks
        Instantiate(sparkParticles, transform.position, transform.rotation);
    }

    public void OnSelectExited() {
        handParticles.SetActive(false);

        // Set animation clip
        if (isLeftHand) {
            GameManager.S.playerLeftHandAnim.CrossFade("DefaultColor", 0);
        } else {
            GameManager.S.playerRightHandAnim.CrossFade("DefaultColor", 0);
        }
    }

    // Red w/ no opacity
    public void OnExited() {
        //handMaterial.SetColor("_Color", new Color(1.0f, 0.0f, 0.0f, 1.0f));

        handParticles.SetActive(false);

        // Set animation clip
        if (isLeftHand) {
            GameManager.S.playerLeftHandAnim.CrossFade("DefaultColor", 0);
        } else {
            GameManager.S.playerRightHandAnim.CrossFade("DefaultColor", 0);
        }
    }
}