using System.Collections.Generic;
using UnityEngine;

public enum eBGM {
    bgm1940, bgmLose, bgmNever, bgmNinja, bgmSoap, bgmStart_Battle, bgmThings, bgmWin
};

public enum eSFX {
    sfxBuff1, sfxBuff2, sfxConfirm, sfxDamage1, sfxDamage2, sfxDamage3, sfxDeny,
    sfxFireball, sfxFireblast, sfxSelection, sfxQuid1, sfxQuid5, sfxWhooshHigh, sfxWhooshMed, sfxWhooshLow, 
    sfxApplause, sfxScream, sfxApplauseLoop, sfxGrab, sfxHighBeep1, sfxHighBeep2,
};

public enum eVOX {
    vox1, vox1ToGo, vox2, vox2ToGo, vox3, vox3ToGo, vox4ToGo, vox5ToGo, voxGameOver, voxGetReady, voxGo, voxLetsGo, voxNewHighScore, voxNextLevel, voxShield,
    voxNull
};

public class AudioManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public AudioSource      playerSFXAudioSource;
    public AudioSource      UI_SFXAudioSource;
    public AudioSource      BGMAudioSource;
    public AudioSource      applauseSFXAudioSource;
    public AudioSource      VOXAudioSource;
    public AudioSource      kangarooAudioSource;

    public List<AudioClip>  bgmClips = new List<AudioClip>();
    public List<AudioClip>  soundtrackClips = new List<AudioClip>();
    public List<AudioClip>  sfxClips = new List<AudioClip>();
    public List<AudioClip>  voxClips = new List<AudioClip>();
    public List<AudioClip>  voxExclamationClips = new List<AudioClip>();
    public List<AudioClip>  voxInterjectionClips = new List<AudioClip>();

    [Header("Set Dynamically")]
    public float            previousVolumeLvl;
    public bool             isMuted;

    // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    public int soundtrackNdx = 0;
    public bool isLoopingGameSoundtrack = false;
    // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

    private void Start() {
        // Set previous volume level
        previousVolumeLvl = AudioListener.volume;
    }

    ////////////////////////////////////////////////////////////////////////////////////////
    // Play/get audio functions
    ////////////////////////////////////////////////////////////////////////////////////////

    //
    public void PlayPlayerSFXClip(eSFX SFXName) {
        AudioClip clip = sfxClips[(int)SFXName];
        playerSFXAudioSource.clip = clip;
        playerSFXAudioSource.Play();
    }
    //
    public void PlayUISFXClip(eSFX SFXName) {
        AudioClip clip = sfxClips[(int)SFXName];
        UI_SFXAudioSource.clip = clip;
        UI_SFXAudioSource.Play();
    }
    //
    public void PlayApplauseSFXlip(bool doesLoop = true) {
        applauseSFXAudioSource.loop = doesLoop;

        if (doesLoop) {
            applauseSFXAudioSource.clip = sfxClips[17];
        } else {
            applauseSFXAudioSource.clip = sfxClips[15];
        }

        applauseSFXAudioSource.Play();
    }

    //
    public void PlayBGMClip(eBGM BGMName, bool doesLoop = true) {
        BGMAudioSource.loop = doesLoop;

        AudioClip clip = bgmClips[(int)BGMName];
        BGMAudioSource.clip = clip;
        BGMAudioSource.Play();
    }

    // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    private void FixedUpdate() {
        // If the soundtrack is being looped
        if (isLoopingGameSoundtrack) {
            // If the audio source has STOPPED playing
            if (!BGMAudioSource.isPlaying) {
                // Increment index to next track
                if (soundtrackNdx < soundtrackClips.Count) {
                    soundtrackNdx += 1;
                } else {
                    soundtrackNdx = 0;
                }

                // Play next track of the soundtrack
                AudioClip clip = soundtrackClips[soundtrackNdx];
                BGMAudioSource.clip = clip;
                BGMAudioSource.Play();
            }
        }
    }

    public void PlaySoundtrackClip(int ndx, bool doesLoop = true) {
        BGMAudioSource.loop = doesLoop;

        // Increment index to next track
        if (soundtrackNdx < soundtrackClips.Count - 1) {
            soundtrackNdx += 1;
        } else {
            soundtrackNdx = 0;
        }

        AudioClip clip = soundtrackClips[ndx];
        BGMAudioSource.clip = clip;
        BGMAudioSource.Play();
    }
    // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

    //
    public void PlayVOXClip(eVOX VOXName) {
        AudioClip clip = voxClips[(int)VOXName];
        VOXAudioSource.clip = clip;
        VOXAudioSource.Play();
    }

    //
    public void PlayVOXExclamationClip(int ndx) {
        AudioClip clip = voxExclamationClips[ndx];
        VOXAudioSource.clip = clip;
        VOXAudioSource.Play();
    }

    //
    public void PlayVOXInterjectionClip(int ndx) {
        AudioClip clip = voxInterjectionClips[ndx];
        VOXAudioSource.clip = clip;
        VOXAudioSource.Play();
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

    ////////////////////////////////////////////////////////////////////////////////////////
    // Adjust audio settings functions
    ////////////////////////////////////////////////////////////////////////////////////////

    public void PauseAndMuteAudio() {
        // Pause and mute
        if (!isMuted) {
            previousVolumeLvl = AudioListener.volume;
            AudioListener.volume = 0;
            isMuted = true;

            // Save settings
            PlayerPrefs.SetInt("Mute Audio", 0);
        // Unpause and unmute
        } else {
            AudioListener.volume = previousVolumeLvl;
            isMuted = false;

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
        applauseSFXAudioSource.volume = volume;

        // Save settings
        PlayerPrefs.SetFloat("SFX Volume", volume);
    }

    public void SetVOXVolume(float volume) {
        VOXAudioSource.volume = volume;

        // Save settings
        PlayerPrefs.SetFloat("VOX Volume", volume);
    }
}