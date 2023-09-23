using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// On this UI object highlighted, change the color of selected buttons
public class OnHighlightChangeButtonColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [Header("Set in Inspector")]
    public List<Button> buttons;

    [Header("Set Dynamically")]
    private Color       normalColor;
    private Color       highlightedColor;
    private Color       selectedColor;

    private void Start() {
        // Cache the buttons' default color block
        ColorBlock cb = buttons[0].colors;
        normalColor = cb.normalColor;
        highlightedColor = cb.highlightedColor;
        selectedColor = cb.selectedColor;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //Color newColor = new Color(1.0f, 0.65f, 0f, 1);
        Color newColor = Color.green;

        // Highlight buttons w/ new color
        for (int i = 0; i < buttons.Count; i++) {
            ColorBlock cb = buttons[i].colors;
            cb.normalColor = newColor;
            cb.highlightedColor = newColor;
            cb.selectedColor = newColor;
            buttons[i].colors = cb;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        // Reset to default button color block
        for (int i = 0; i < buttons.Count; i++) {
            ColorBlock cb = buttons[i].colors;
            cb.normalColor = normalColor;
            cb.highlightedColor = highlightedColor;
            cb.selectedColor = selectedColor;
            buttons[i].colors = cb;
        }
    }
}