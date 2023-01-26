using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSound { bgm1940, bgmLose, bgmNever, bgmNinja, bgmSoap, bgmStart_Battle, bgmThings, bgmWin,
sfxBuff1, sfxBuff2, sfxConfirm, sfxDamage1, sfxDamage2, sfxDamage3, sfxDeath, sfxDeny, sfxDialogue,
sfxFireball, sfxFireblast, sfxFlicker, sfxHighBeep1, sfxHighBeep2, sfxRun, sfxSelection2, sfxSwell };

public class AudioManager : MonoBehaviour {
    public AudioSource playerAudioSource;
    public AudioSource UIAudioSource;
    public List<AudioClip> audioClips;

    //private void Update() {
    //    if (Input.GetKeyDown(KeyCode.Space)) {
    //        PlayClip(eSound.sfxRun);
    //    }
    //}

    private void Start() {
        
    }

    public void PlayPlayerClip(eSound soundName) {
        playerAudioSource.clip = GetClip(soundName);
        playerAudioSource.Play();
    }

    public void PlayUIClip(eSound soundName) {
        UIAudioSource.clip = GetClip(soundName);
        UIAudioSource.Play();
    }

    public AudioClip GetClip(eSound soundName) {
        AudioClip clip = audioClips[(int)soundName];
        return clip;
    }
}