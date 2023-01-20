using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adjusts the position and scale of climbing handles, pickups, and hazards based on the player�s height 
public class AdjustObjectsHeight : MonoBehaviour {
    [Header("Set in Inspector")]
    // Horizontal climbing handles
    public GameObject       row1Handles;
    public GameObject       row2Handles;
    public GameObject       row3Handles;

    // Hazards
    public GameObject       horizontalHazard;
    public GameObject       verticalHighHazard;
    public GameObject       verticalLowHazard;

    // Vertical climbing handles
    public List<GameObject> longVerticalHandles;
    public List<GameObject> shortVerticalHandles;

    public void SetObjects(float playerHeight = 168) {
        // Get lowest row height
        float lowestRowYPos = playerHeight / 84;

        // Get distance between rows
        float distanceBetweenRows = playerHeight / 168;

        // Handles (yPos)
        GameManager.utilities.SetPosition(row1Handles, 0, lowestRowYPos);
        GameManager.utilities.SetPosition(row2Handles, 0, lowestRowYPos + distanceBetweenRows);
        GameManager.utilities.SetPosition(row3Handles, 0, lowestRowYPos + (distanceBetweenRows * 2));

        // Pickups (yPos)
        GameManager.S.spawner.pickupMaxYPos = playerHeight / 30.54545455f;

        // Hazards (yPos)
        GameManager.S.spawner.horizontalYPos = playerHeight / 67.2f;
        GameManager.S.spawner.verticalLowYPos = playerHeight / 448f;
        GameManager.S.spawner.verticalHighYPos = playerHeight / 51.69230769f;

        // Hazards (yScale)
        GameManager.utilities.SetScale(GameManager.S.spawner.horizontalBlock, 1, (playerHeight / 33.6f), 1);
        GameManager.utilities.SetScale(GameManager.S.spawner.verticalLowBlock, 3, (playerHeight / 224f), 1);
        GameManager.utilities.SetScale(GameManager.S.spawner.verticalHighBlock, 3, (playerHeight / 48f), 1);
    
        // Vertical climbing handles
        for(int i = 0; i < longVerticalHandles.Count; i++) {
            GameManager.utilities.SetPosition(longVerticalHandles[i], longVerticalHandles[i].transform.position.x, (playerHeight / 84f));
            GameManager.utilities.SetScale(longVerticalHandles[i], 0.2f, (playerHeight / 84f), 0.2f);
        }
        for (int i = 0; i < shortVerticalHandles.Count; i++) {
            GameManager.utilities.SetPosition(shortVerticalHandles[i], shortVerticalHandles[i].transform.position.x, (playerHeight / 112f));
            GameManager.utilities.SetScale(shortVerticalHandles[i], 0.2f, (playerHeight / 112f), 0.2f);
        }

        // Player damage collider
        GameManager.S.damageColl.yScale = playerHeight / 186.6666667f;
        GameManager.S.damageColl.yOffset = playerHeight / 252.0000252f;
        GameManager.S.damageColl.SetScale();
    }
}