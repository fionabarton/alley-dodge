using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sets the user's left & right hand's material's colors depending on their current interaction (hover, hold, release)
// with a climbable XRGrabInteractable object.

// Is set on XRDirectClimbInteractor's "Interactor Events" (in Inspector) attached to both hand controller game objects 
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
            // Set hand material color
            GameManager.S.playerLeftHandAnim1.CrossFade("RainbowColor", 0);
            GameManager.S.playerLeftHandAnim2.CrossFade("RainbowColor", 0);
        } else {
            // Set hand material color
            GameManager.S.playerRightHandAnim1.CrossFade("RainbowColor", 0);
            GameManager.S.playerRightHandAnim2.CrossFade("RainbowColor", 0);
        }

        // Instantiate sparks
        Instantiate(sparkParticles, transform.position, transform.rotation);

        // Audio: Damage
        GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxGrab);
    }

    public void OnSelectExited() {
        handParticles.SetActive(false);

        // Set animation clip
        if (isLeftHand) {
            // Set hand material color
            GameManager.S.playerLeftHandAnim1.CrossFade("DefaultColor", 0);
            GameManager.S.playerLeftHandAnim2.CrossFade("DefaultColor", 0);
        } else {
            // Set hand material color
            GameManager.S.playerRightHandAnim1.CrossFade("DefaultColor", 0);
            GameManager.S.playerRightHandAnim2.CrossFade("DefaultColor", 0);
        }

        // Audio: Damage
        //GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxGrab);
    }

    // Red w/ no opacity
    public void OnExited() {
        //handMaterial.SetColor("_Color", new Color(1.0f, 0.0f, 0.0f, 1.0f));

        handParticles.SetActive(false);

        // Set animation clip
        if (isLeftHand) {
            // Set hand material color
            GameManager.S.playerLeftHandAnim1.CrossFade("DefaultColor", 0);
            GameManager.S.playerLeftHandAnim2.CrossFade("DefaultColor", 0);
        } else {
            // Set hand material color
            GameManager.S.playerRightHandAnim1.CrossFade("DefaultColor", 0);
            GameManager.S.playerRightHandAnim2.CrossFade("DefaultColor", 0);
        }
    }
}