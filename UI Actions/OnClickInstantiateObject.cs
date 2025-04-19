using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// On this UI object clicked, instantiate a game object
public class OnClickInstantiateObject : MonoBehaviour, IPointerDownHandler {
    [Header("Set in Inspector")]
    public GameObject objectToInstantiate;

    public void OnPointerDown(PointerEventData pointerEventData) {
        // Instantiate object
        Instantiate(objectToInstantiate, pointerEventData.pointerCurrentRaycast.worldPosition, transform.rotation);
    }
}
