using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Plays "Fadeout" animation clip, which first obscures the user's view with a black image,
// then gradually drops its alpha channel to 0 (total transparency) in the span of one second.
// This is useful for reducing motion sickness associated with VR teleportation.
public class Fader : MonoBehaviour {
    [Header("Set dynamically")]
    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();    
    }

    public void Fadeout() {
        anim.Play("Fadeout", -1, 0);
    }
}