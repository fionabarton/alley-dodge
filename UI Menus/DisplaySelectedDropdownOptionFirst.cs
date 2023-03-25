using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On dropdown value changed, display its currently selected option first
public class DisplaySelectedDropdownOptionFirst : MonoBehaviour {
    [Header("Set in Inspector")]
    public TMPro.TMP_Dropdown   dropdown;

    public RectTransform        dropdownContentRT;
    public RectTransform        dropdownItemRT;

    void Start() {
        // Add listener to dropdown
        dropdown.onValueChanged.AddListener(delegate { SetDropdownContentPosition(); });

        SetDropdownContentPosition();
    }

    // Adjust the dropdown content's anchored position to display its currently selected option first
    void SetDropdownContentPosition() {
        dropdownContentRT.anchoredPosition = new Vector2(dropdownContentRT.anchoredPosition.x,
                                                         dropdownItemRT.sizeDelta.y * dropdown.value);
    }
}