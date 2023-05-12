using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Based on the user specified alleyCount, sets the size of the alley and amount of handles.
public class AlleyManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject       playerFloor;
    public GameObject       transparentAlley;
    public GameObject       baseHandle;

    public List<GameObject> handles3;
    public List<GameObject> alleys3;
    public List<GameObject> handles5;
    public List<GameObject> alleys5;

    // Amount of alleys
    public int alleyCount = 3; // 3, 5, 7

    // Depending on value of alleyCount, set:
    public void InitializeAlleys(float playerHeight = 168) {
        // Set floor length
        playerFloor.transform.localScale = new Vector3(alleyCount, 1, 2.5f);

        // Set transparent alley length
        transparentAlley.transform.localScale = new Vector3(alleyCount, 0.025f, 50f);

        // Set base handle length  3-1, 5-2, 7-3
        baseHandle.transform.localScale = new Vector3(0.2f, ((alleyCount/2)), 0.2f);

        // Set length of vertical block hazards
        GameManager.S.spawner.verticalLowBlock.transform.localScale = new Vector3(alleyCount, playerHeight / 224f, 1);
        GameManager.S.spawner.verticalHighBlock.transform.localScale = new Vector3(alleyCount, playerHeight / 48f, 1);

        // Set xPos range of horizontal block hazards & pickups
        GameManager.S.spawner.minXPos = -(alleyCount / 2);
        GameManager.S.spawner.maxXPos = (alleyCount / 2) + 1;

        // Set amount of handles & alleys
        switch (alleyCount) {
            case 3:
                GameManager.utilities.SetActiveList(handles3, false);
                GameManager.utilities.SetActiveList(alleys3, false);

                GameManager.utilities.SetActiveList(handles5, false);
                GameManager.utilities.SetActiveList(alleys5, false);

                // Set exit run button position
                GameManager.utilities.SetLocalPosition(GameManager.S.exitRunButtonLeftCS.gameObject.transform.parent.gameObject, 0f, -5.25f);
                GameManager.utilities.SetLocalPosition(GameManager.S.exitRunButtonRightCS.gameObject.transform.parent.gameObject, 0f, -5.25f);
                break;
            case 5:
                GameManager.utilities.SetActiveList(handles3, true);
                GameManager.utilities.SetActiveList(alleys3, true);

                GameManager.utilities.SetActiveList(handles5, false);
                GameManager.utilities.SetActiveList(alleys5, false);

                // Set exit run button position
                GameManager.utilities.SetLocalPosition(GameManager.S.exitRunButtonLeftCS.gameObject.transform.parent.gameObject, -1f, -5.25f);
                GameManager.utilities.SetLocalPosition(GameManager.S.exitRunButtonRightCS.gameObject.transform.parent.gameObject, 1f, -5.25f);
                break;
            case 7:
                GameManager.utilities.SetActiveList(handles3, true);
                GameManager.utilities.SetActiveList(alleys3, true);

                GameManager.utilities.SetActiveList(handles5, true);
                GameManager.utilities.SetActiveList(alleys5, true);

                // Set exit run button position
                GameManager.utilities.SetLocalPosition(GameManager.S.exitRunButtonLeftCS.gameObject.transform.parent.gameObject, -2f, -5.25f);
                GameManager.utilities.SetLocalPosition(GameManager.S.exitRunButtonRightCS.gameObject.transform.parent.gameObject, 2f, -5.25f);
                break;
        }
    }
}