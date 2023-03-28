using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public List<ParticleSystem> confettiPS;

    public ParticleSystem       fireworksPS;

    // Plays particle systems that drop confetti for approximately 3 seconds
    public void DropConfetti(bool usePlayerAudioSource = true, bool playFireworks = false) {
        for (int i = 0; i < confettiPS.Count; i++) {
            confettiPS[i].Play();
        }

        // Play SFX
        if (usePlayerAudioSource) { 
            GameManager.audioMan.PlayPlayerSFXClip(eSFX.sfxApplause);
        } else {
            GameManager.audioMan.PlayUISFXClip(eSFX.sfxApplause);
        }

        // Play fireworks
        if (playFireworks) {
            fireworksPS.Play();
        }
    }

    // Sets whether all particle systems are looping or not
    public void IsLooping(bool isLooping = false) {
        for (int i = 0; i < confettiPS.Count; i++) {
            var confettiMain = confettiPS[i].main;
            confettiMain.loop = isLooping;
        }

        var fireworksMain = fireworksPS.main;
        fireworksMain.loop = isLooping;
    }
}