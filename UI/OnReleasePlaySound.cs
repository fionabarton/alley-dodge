using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// On this UI object released, play sound
public class OnReleasePlaySound : MonoBehaviour, IPointerClickHandler {
    [Header("Set in Inspector")]
    public AudioSource audioSource;
    public AudioClip clipToPlay;

    public void OnPointerClick(PointerEventData pointerEventData) {
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }
}
