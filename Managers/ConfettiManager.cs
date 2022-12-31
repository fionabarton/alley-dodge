using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public ParticleSystem confettiPS1;
    public ParticleSystem confettiPS2;

    // Plays both particle systems that drop confetti for approximately 3 seconds
    public void DropConfetti() {
        confettiPS1.Play();
        confettiPS2.Play();
    }
}