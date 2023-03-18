using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Applies random force and torque to an exploding cube
public class LaunchExplodingCube : MonoBehaviour {
    [Header("Set in Inspector")]
    public Rigidbody    rigid;

    public float        thrust = 2;

    [Header("Set dynamically")]
    float               xTorque;
    float               yTorque;
    float               zTorque;

    private void Start() {
        rigid.isKinematic = false;

        // Get random force
        float xForce = Random.Range(-1.0f, 2.0f);
        float yForce = Random.Range(2.0f, 5.0f);
        float zForce = Random.Range(-1.0f, 2.0f);

        // Get random torque
        xTorque = Random.Range(-1.0f, 2.0f);
        yTorque = Random.Range(-1.0f, 2.0f);
        zTorque = Random.Range(-1.0f, 2.0f);

        // Add force
        rigid.AddForce((new Vector3(xForce, yForce, zForce) * thrust), ForceMode.Impulse);
    }

    private void FixedUpdate() {
        // Add torque
        rigid.AddTorque((new Vector3(xTorque, yTorque, zTorque) * thrust));
    }
}