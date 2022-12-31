using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroys this object within a specified amount of seconds
public class DestroyOverTime : MonoBehaviour {
    [SerializeField] float seconds;

    void Start() {
        Destroy(gameObject, seconds);
    }
}