using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sets the user's left & right hand's material's colors depending on their current interaction (hover, hold, release)
// with a climbable XRGrabInteractable object.
public class SetHandMaterialColor : MonoBehaviour {
    [Header("Set in Inspector")]
    public Material handMaterial;

    // Red w/ slight opacity
    public void OnHoverEntered() {
        handMaterial.SetColor("_Color", new Color(1.0f, 0.0f, 0.0f, 0.25f));
    }

    // White w/ slight opacity
    public void OnSelectEntered() {
        handMaterial.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 0.25f));
    }

    // Red w/ no opacity
    public void OnExited() {
        handMaterial.SetColor("_Color", new Color(1.0f, 0.0f, 0.0f, 1.0f));
    }
}