using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// On this UI object released, instantiate a game object
public class OnReleaseInstantiateObject : MonoBehaviour, IPointerUpHandler {
    [Header("Set in Inspector")]
    public GameObject objectToInstantiate;

    public void OnPointerUp(PointerEventData pointerEventData) {
        // Instantiate object
        Instantiate(objectToInstantiate, pointerEventData.pointerCurrentRaycast.worldPosition, transform.rotation);
    }
}
