using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// On this UI object highlighted, play sound
public class OnHighlightPlaySound : MonoBehaviour, IPointerEnterHandler {
    [Header("Set in Inspector")]
    public AudioSource  audioSource;
    public AudioClip    clipToPlay;

    public void OnPointerEnter(PointerEventData eventData) {
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }
}