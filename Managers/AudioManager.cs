using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBGM {
    bgm1940, bgmLose, bgmNever, bgmNinja, bgmSoap, bgmStart_Battle, bgmThings, bgmWin
};

public enum eSFX {
    sfxBuff1, sfxBuff2, sfxConfirm, sfxDamage1, sfxDamage2, sfxDamage3, sfxDeath, sfxDeny, sfxDialogue,
    sfxFireball, sfxFireblast, sfxFlicker, sfxHighBeep1, sfxHighBeep2, sfxRun, sfxSelection2, sfxSwell,
    sfxQuid1, sfxQuid2, sfxQuid3, sfxQuid4, sfxQuid5, sfxWhooshHigh, sfxWhooshMed, sfxWhooshLow, sfxApplause, sfxScream
};

public class AudioManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public AudioSource      playerSFXAudioSource;
    public AudioSource      UI_SFXAudioSource;
    public AudioSource      BGMAudioSource;

    public List<AudioClip>  bgmClips = new List<AudioClip>();
    public List<AudioClip>  sfxClips = new List<AudioClip>();

    [Header("Set Dynamically")]
    public float            previousVolumeLvl;

    private void Start() {
        // Set previous volume level
        previousVolumeLvl = AudioListener.volume;
    }

    //
    public void PlayPlayerSFXClip(eSFX SFXName) {
        playerSFXAudioSource.clip = GetSFXClip(SFXName);
        playerSFXAudioSource.Play();
    }

    //
    public void PlayUISFXClip(eSFX SFXName) {
        UI_SFXAudioSource.clip = GetSFXClip(SFXName);
        UI_SFXAudioSource.Play();
    }

    //
    public AudioClip GetSFXClip(eSFX SFXName) {
        AudioClip clip = sfxClips[(int)SFXName];
        return clip;
    }

    //
    public void PlayBGMClip(eBGM BGMName, bool doesLoop = true) {
        BGMAudioSource.loop = doesLoop;

        BGMAudioSource.clip = GetBGMClip(BGMName);
        BGMAudioSource.Play();
    }

    //
    public AudioClip GetBGMClip(eBGM BGMName) {
        AudioClip clip = bgmClips[(int)BGMName];
        return clip;
    }

    public void PauseAndMuteAudio() {
        // Pause and mute
        if (!AudioListener.pause) {
            previousVolumeLvl = AudioListener.volume;
            AudioListener.pause = true;

            // Save settings
            PlayerPrefs.SetInt("Mute Audio", 0);
        // Unpause and unmute
        } else {
            AudioListener.volume = previousVolumeLvl;
            AudioListener.pause = false;

            // Save settings
            PlayerPrefs.SetInt("Mute Audio", 1);
        }
    }

    public void SetMasterVolume(float volume) {
        AudioListener.volume = volume;

        // Save settings
        PlayerPrefs.SetFloat("Master Volume", volume);

        // Set previous volume level
        previousVolumeLvl = volume;
    }

    public void SetBGMVolume(float volume) {
        BGMAudioSource.volume = volume;

        // Save settings
        PlayerPrefs.SetFloat("BGM Volume", volume);
    }

    public void SetSFXVolume(float volume) {
        playerSFXAudioSource.volume = volume;
        UI_SFXAudioSource.volume = volume;

        // Save settings
        PlayerPrefs.SetFloat("SFX Volume", volume);
    }

    //
    public void PlayRandomDamageSFX() {
        int randomInt = Random.Range(0, 3);
        if (randomInt == 0) {
            PlayPlayerSFXClip(eSFX.sfxDamage1);
        } else if (randomInt == 1) {
            PlayPlayerSFXClip(eSFX.sfxDamage2);
        } else {
            PlayPlayerSFXClip(eSFX.sfxDamage3);
        }
    }
}