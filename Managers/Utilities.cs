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
    public void SetPosition(GameObject tGO, float x, float y, float z) {
        Vector3 tPos = tGO.transform.position;
        tPos.x = x;
        tPos.y = y;
        tPos.z = z;
        tGO.transform.position = tPos;
    }
    public void SetLocalPosition(GameObject tGO, float x, float y) {
        Vector3 tPos = tGO.transform.localPosition;
        tPos.x = x;
        tPos.y = y;
        tGO.transform.localPosition = tPos;
    }

    // Set GameObject Scale
    public void SetScale(GameObject tGO, float x, float y, float z = -1) {
        Vector3 tScale = tGO.transform.localScale;
        tScale.x = x;
        tScale.y = y;
        if (z != -1) tScale.z = z;
        tGO.transform.localScale = tScale;
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

    // Get midpoint between two gameObjects
    public Vector3 GetMidpoint(GameObject object1, GameObject object2) {
        Vector3 midpoint = object2.transform.position + (object1.transform.position - object2.transform.position) / 2;
        return midpoint;
    }

    // Map a value within a set of numbers to a different set of numbers
    public float Map(float OldMin, float OldMax, float NewMin, float NewMax, float valueToMap) {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((valueToMap - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }
}