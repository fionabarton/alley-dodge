using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public ParticleSystem confettiPS1;
    public ParticleSystem confettiPS2;
    public List<ParticleSystem> particleSystems;

    // Plays both particle systems that drop confetti for approximately 3 seconds
    public void DropConfetti(bool usePlayerAudioSource = true) {
        //confettiPS1.Play();
        //confettiPS2.Play();
        for (int i = 0; i < particleSystems.Count; i++) {
            particleSystems[i].Play();
        }

        // Play SFX
        if (usePlayerAudioSource) { 
            GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxApplause);
        } else {
            GameManager.audioMan.PlayUISFXClip(eSFX.sfxApplause);
        }
    }
}