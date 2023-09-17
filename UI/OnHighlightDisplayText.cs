using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// On this UI object highlighted, display delayed text
public class OnHighlightDisplayText : MonoBehaviour, IPointerEnterHandler {
    [Header("Set in Inspector")]
    public DelayedTextDisplay   delayedTextDisplay;
    
    [TextArea]
    public string               messageToDisplay;

    public void OnPointerEnter(PointerEventData eventData) {
        //if (delayedTextDisplay.dialogueFinished) {
            if (GameManager.S.previouslyHighlightedGO != gameObject) {
                delayedTextDisplay.DisplayText(messageToDisplay);
                GameManager.S.previouslyHighlightedGO = gameObject;
            }
        //}
    }
}