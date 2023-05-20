using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// On this UI object highlighted, "pop up" towards the player & increase its scale
public class OnHighlightPopUpUIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [Header("Set Dynamically")]
    RectTransform   rectTrans;

    // Cached position values
    float           defaultZPos;
    float           popUpZPos;

    // Cached scale values
    Vector3         defaultScale;
    Vector3         popUpScale;

    void Awake() {
        rectTrans = GetComponent<RectTransform>();
    }

    void OnEnable() {
        // Add event listener
        MoveUIMenus.onMoveMenusInOut += SetFields;

        // Cache default and "pop up" z positions
        if (Time.time > 0.5f) {
            SetFields();
        }
    }

    void OnDisable() {
        // Remove event listener
        MoveUIMenus.onMoveMenusInOut -= SetFields;

        if (Time.time > 0.5f) {
            // Set z-pos to default value
            Vector3 pos = rectTrans.position;
            pos.z = defaultZPos;
            rectTrans.position = pos;

            // Set scale to default value
            rectTrans.localScale = defaultScale;
        }
    }

    // Cache default and "pop up" z position and scale values
    void SetFields() {
        defaultZPos = rectTrans.position.z;
        popUpZPos = defaultZPos - 0.15f;

        defaultScale = rectTrans.localScale;
        popUpScale = defaultScale * 1.1f;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        // Set z-pos to "pop up" value
        Vector3 pos = rectTrans.position;
        pos.z = popUpZPos;
        rectTrans.position = pos;

        // Set scale to "pop up" value
        rectTrans.localScale = popUpScale;
    }

    public void OnPointerExit(PointerEventData eventData) {
        // Set z-pos to default value
        Vector3 pos = rectTrans.position;
        pos.z = defaultZPos;
        rectTrans.position = pos;

        // Set scale to default value
        rectTrans.localScale = defaultScale;
    }
}