using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Confetti, fireworks, applause
public class ConfettiManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public List<ParticleSystem> confettiPS;
    public List<ParticleSystem> fireworksPS;
    //public ParticleSystem       fireworksPS;

    // Plays particle systems that drop confetti for approximately 3 seconds
    public void DropConfetti(bool loopAudio = false, bool playFireworks = false) {
        for (int i = 0; i < confettiPS.Count; i++) {
            confettiPS[i].Play();
        }

        // Play SFX
        GameManager.audioMan.PlayApplauseSFXlip(loopAudio);

        // Play fireworks
        if (playFireworks) {
            //fireworksPS.Play();
            for (int i = 0; i < fireworksPS.Count; i++) {
                fireworksPS[i].Play();
            }
        }
    }

    // Sets whether all particle systems are looping or not
    public void IsLooping(bool isLooping = false) {
        for (int i = 0; i < confettiPS.Count; i++) {
            var confettiMain = confettiPS[i].main;
            confettiMain.loop = isLooping;
        }

        //var fireworksMain = fireworksPS.main;
        //fireworksMain.loop = isLooping;
        for (int i = 0; i < fireworksPS.Count; i++) {
            var fireworksMain = fireworksPS[i].main;
            fireworksMain.loop = isLooping;
        }

        // Stop looping applause SFX
        if (!isLooping) {
            GameManager.audioMan.applauseSFXAudioSource.Stop();
        }
    }

    /////////////////////////////////////////////////////////////////////////

    // Confetti
    public void DoConfetti(bool enable) {
        // Enable loop
        for (int i = 0; i < confettiPS.Count; i++) {
            var confettiMain = confettiPS[i].main;
            confettiMain.loop = enable;
        }

        // Play confetti
        if (enable) {
            for (int i = 0; i < confettiPS.Count; i++) {
                confettiPS[i].Play();
            }
        }
    }

    // Fireworks
    public void DoFireworks(bool enable) {
        //// Enable loop
        //var fireworksMain = fireworksPS.main;
        //fireworksMain.loop = enable;

        //// Play fireworks
        //if (enable) {
        //    fireworksPS.Play();
        //}
        // Enable loop
        for (int i = 0; i < fireworksPS.Count; i++) {
            var fireworksMain = fireworksPS[i].main;
            fireworksMain.loop = enable;
        }

        // Play fireworks
        if (enable) {
            for (int i = 0; i < fireworksPS.Count; i++) {
                fireworksPS[i].Play();
            }
        }
    }

    // Applause
    public void DoApplause(bool enable) {
        if (enable) {
            // Play SFX
            GameManager.audioMan.PlayApplauseSFXlip();
        } else {
            // Stop SFX
            GameManager.audioMan.applauseSFXAudioSource.Stop();
        }
    }

    // All
    public void DoAll(bool enable) {
        DoConfetti(enable);
        DoFireworks(enable);
        DoApplause(enable);
    }
}