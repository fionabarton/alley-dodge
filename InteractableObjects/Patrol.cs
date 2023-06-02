using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Repeatedly moves a game object back and forth between two points
public class Patrol : MonoBehaviour {
    [Header("Set in Inspector")]
    public Transform    startPoint;
    public Transform    endPoint;
    
    [Header("Set Dynamically")]
    public bool         isMovingToEndPoint;

    void FixedUpdate() {
        var step = (GameManager.S.spawner.currentObjectSpeed * 0.5f) * Time.deltaTime; // calculate distance to move

        if (isMovingToEndPoint) {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, step);

            if (Vector3.Distance(transform.position, endPoint.position) < 0.001f) {
                isMovingToEndPoint = !isMovingToEndPoint;
            }
        } else {
            transform.position = Vector3.MoveTowards(transform.position, startPoint.position, step);

            if (Vector3.Distance(transform.position, startPoint.position) < 0.001f) {
                isMovingToEndPoint = !isMovingToEndPoint;
            }
        }
    }
}