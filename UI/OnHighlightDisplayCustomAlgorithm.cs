using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// On this UI object highlighted, displays the properties and values of this entry's custom game algorithm
public class OnHighlightDisplayCustomAlgorithm : MonoBehaviour, IPointerEnterHandler {
    [Header("Set in Inspector")]
    public int buttonNdx;

    public void OnPointerEnter(PointerEventData eventData) {
        GameManager.S.customAlgorithmMenuCS.DisplaySelectedCustomAlgorithm(buttonNdx);
    }
}