using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// On this UI object highlighted, "pop up" towards the player
public class OnHighlightPopUpUIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [Header("Set Dynamically")]
    RectTransform   rectTrans;
    float           defaultZPos;
    float           popUpZPos;

    void Start() {
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

        // Set this UI object's z pos to default value
        if (Time.time > 0.5f) {
            Vector3 pos = rectTrans.position;
            pos.z = defaultZPos;
            rectTrans.position = pos;
        }
    }

    // Cache default and "pop up" z positions
    void SetFields() {
        defaultZPos = rectTrans.position.z;
        popUpZPos = defaultZPos - 0.15f;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        // Set this UI object's z pos to "pop up" value
        Vector3 pos = rectTrans.position;
        pos.z = popUpZPos;
        rectTrans.position = pos;
    }

    public void OnPointerExit(PointerEventData eventData) {
        // Set this UI object's z pos to default value
        Vector3 pos = rectTrans.position;
        pos.z = defaultZPos;
        rectTrans.position = pos;
    }
}