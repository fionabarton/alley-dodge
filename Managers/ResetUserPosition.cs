using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Resets the user's position if they've fallen through the floor
public class ResetUserPosition : MonoBehaviour {
    [Header("Set in Inspector")]
    public Transform            resetPosition;
    [SerializeField] GameObject xrOriginGO;

    public Animator             faderAnim;

    public void ResetPosition() {
        faderAnim.CrossFade("Fadeout", 0);

        var distanceDiff = resetPosition.position - Camera.main.transform.position;
        xrOriginGO.transform.position += distanceDiff;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            ResetPosition();
        }
    }
}