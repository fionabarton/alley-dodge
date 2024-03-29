using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Allows the user to reposition all UI menus
public class MoveUIMenus : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject           UImenusParentGO;
    public GameObject           alleywayGO;

    // Sliders
    public Slider               moveDownUpSlider;
    public Slider               moveInOutSlider;

    // Reset to default settings button
    public Button               resetButton;

    // Move UI menus in/out event
    public delegate void MoveMenusInOut();
    public static event MoveMenusInOut onMoveMenusInOut;

    void Start() {
        Invoke("OnStart", 0.1f);
    }

    void OnStart() {
        // GetPlayerPrefs
        if (PlayerPrefs.HasKey("Move Down/Up Slider")) {
            MoveDownUp(PlayerPrefs.GetFloat("Move Down/Up Slider"));
        } else {
            moveDownUpSlider.value = 0;
        }
        if (PlayerPrefs.HasKey("Move In/Out Slider")) {
            MoveInOut(PlayerPrefs.GetFloat("Move In/Out Slider"));
        } else {
            moveInOutSlider.value = 0;
        }

        // Add listener to sliders
        moveDownUpSlider.onValueChanged.AddListener(delegate { MoveDownUp(moveDownUpSlider.value); });
        moveInOutSlider.onValueChanged.AddListener(delegate { MoveInOut(moveInOutSlider.value); });

        // Add listener to button
        resetButton.onClick.AddListener(delegate { DefaultSettings(); });
    }

    // On value changed of moveDownUpSlider, move UI menus down/up
    public void MoveDownUp(float yPos = 0) {
        moveDownUpSlider.value = yPos;

        // Save settings
        PlayerPrefs.SetFloat("Move Down/Up Slider", yPos);

        // Set UI menus position
        GameManager.utilities.SetPosition(UImenusParentGO, UImenusParentGO.transform.position.x, yPos, UImenusParentGO.transform.position.z);

        // Set alleyway position
        GameManager.utilities.SetPosition(alleywayGO, alleywayGO.transform.position.x, yPos, alleywayGO.transform.position.z);
    }

    // On value changed of moveInOutSlider, , move UI menus in/out
    public void MoveInOut(float zPos = 0) {
        moveInOutSlider.value = zPos;

        // Save settings
        PlayerPrefs.SetFloat("Move In/Out Slider", zPos);

        // Set UI menus position
        GameManager.utilities.SetPosition(UImenusParentGO, UImenusParentGO.transform.position.x, UImenusParentGO.transform.position.y, zPos);

        // Invoke event
        onMoveMenusInOut?.Invoke();
    }

    // On click, returns all menu settings to their default value
    public void DefaultSettings() {
        // Reset slider values
        moveDownUpSlider.value = 0;
        moveInOutSlider.value = 0;

        // Reset UI menus position
        GameManager.utilities.SetPosition(UImenusParentGO, 0, 0, 0);
    }
}