using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Makes the player's shadows follow its body and hands 
public class ShadowFollow : MonoBehaviour {
    [Header("Set in Inspector")]
    public Transform objectToFollow;
    
    private void Start() {
        StartCoroutine("Follow");
    }
    
    IEnumerator Follow() {
        Vector3 tPos = transform.position;
        tPos = new Vector3(objectToFollow.position.x, 0.01f, objectToFollow.position.z);
        transform.position = tPos;

        yield return null;
        StartCoroutine("Follow");
    }
}