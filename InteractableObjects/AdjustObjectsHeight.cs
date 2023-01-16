using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adjusts the position and scale of climbing handles, pickups, and hazards based on the player’s height 
public class AdjustObjectsHeight : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject row1Handles;
    public GameObject row2Handles;
    public GameObject row3Handles;

    public GameObject horizontalHazard;
    public GameObject verticalHighHazard;
    public GameObject verticalLowHazard;

    public void SetObjects(float playerHeight = 168) {
        // Get lowest row height
        float lowestRowYPos = playerHeight / 84;

        // Get distance between rows
        float distanceBetweenRows = playerHeight / 168;

        // Handles
        GameManager.utilities.SetPosition(row1Handles, 0, lowestRowYPos);
        GameManager.utilities.SetPosition(row2Handles, 0, lowestRowYPos + distanceBetweenRows);
        GameManager.utilities.SetPosition(row3Handles, 0, lowestRowYPos + (distanceBetweenRows * 2));

        // Pickups
        GameManager.S.spawner.pickupMaxYPos = playerHeight / 30.54545455f;

        // Hazards

    }
}