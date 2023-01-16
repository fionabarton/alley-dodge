using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A set of general functions that are HOPEFULLY useful in a multitude of projects
public class Utilities : MonoBehaviour {
    // Set GameObject Position
    public void SetPosition(GameObject tGO, float x, float y) {
        Vector3 tPos = tGO.transform.position;
        tPos.x = x;
        tPos.y = y;
        tGO.transform.position = tPos;
    }

    // Activate or deactivate all of the gameObject elements stored within a list 
    public void SetActiveList(List<GameObject> objects, bool isActive) {
        for (int i = 0; i < objects.Count; i++) {
            objects[i].SetActive(isActive);
        }
    }

    // Set cursor position to currently selected button/gameObject
    public void PositionCursor(GameObject cursorGO, GameObject selectedGO,
        float xAxisDistanceFromCenter, float yAxisDistanceFromCenter = 0, int directionToFace = 2) {
        // Get position
        float tPosX = selectedGO.GetComponent<RectTransform>().anchoredPosition.x;
        float tPosY = selectedGO.GetComponent<RectTransform>().anchoredPosition.y;
        float tParentX = selectedGO.transform.parent.GetComponent<RectTransform>().anchoredPosition.x;
        float tParentY = selectedGO.transform.parent.GetComponent<RectTransform>().anchoredPosition.y;

        // Set position
        RectTransform rectTrans = cursorGO.GetComponent<RectTransform>();
        rectTrans.anchoredPosition = new Vector2(
            (tPosX + tParentX + xAxisDistanceFromCenter),
            (tPosY + tParentY + yAxisDistanceFromCenter)
        );

        // Set rotation
        int angle = (directionToFace + 1) * 90;
        cursorGO.transform.localEulerAngles = new Vector3(0, 0, angle);
    }
}