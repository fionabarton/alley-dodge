using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A set of general functions that are HOPEFULLY useful in a multitude of projects
public class Utilities : MonoBehaviour {
    // Activate or deactivate all of the gameObject elements stored within a list 
    public void SetActiveList(List<GameObject> objects, bool isActive) {
        for (int i = 0; i < objects.Count; i++) {
            objects[i].SetActive(isActive);
        }
    }
}