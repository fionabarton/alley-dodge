using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sets the user's left & right hand's material's colors depending on their current interaction (hold, release)
// with a climbable XRGrabInteractable object.

// Is set on XRDirectClimbInteractor's "Interactor Events" (in Inspector) attached to both hand controller game objects 
public class SetHandMaterialColor : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject       handParticles;
    public GameObject       sparkParticles;

    public List<Animator>   anims = new List<Animator>();   

    public void OnSelectEntered() {
        handParticles.SetActive(true);

        // Set hand material color animation clip
        for (int i = 0; i < anims.Count; i++) {
            anims[i].CrossFade("RainbowColor", 0);
        }

        // Instantiate sparks
        Instantiate(sparkParticles, transform.position, transform.rotation);

        // Audio: Grab
        GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxGrab);
    }

    public void OnSelectExited() {
        handParticles.SetActive(false);

        // Set hand material color animation clip
        for (int i = 0; i < anims.Count; i++) {
            anims[i].CrossFade("DefaultColor", 0);
        }
    }
}