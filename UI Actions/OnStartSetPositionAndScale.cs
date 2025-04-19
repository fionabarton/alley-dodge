using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to ONLY the two middle buttons on the NumericalSelectionMenu game object
// I don't know why, but all of a sudden on start their position and scale are set to invalid values;
// can't figure out why (and would rather not waste time on things I cannot fix), so I created this bullshit solution.
public class OnStartSetPositionAndScale : MonoBehaviour {
    [Header("Set in Inspector")]
    public int xPos;
    public int yPos;

    void Start() {
        Invoke("OnStart", 0.25f);
    }

    void OnStart() {
        // Set position (z)
        GameManager.utilities.SetLocalPosition(gameObject, gameObject.transform.position.x, gameObject.transform.position.y, -30);

        // Set position (x, y)
        GameManager.utilities.SetRectPosition(gameObject, xPos, yPos);

        // Reset scale
        GameManager.utilities.SetScale(gameObject, 1, 1, 1);
    }
}