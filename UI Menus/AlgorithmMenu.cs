using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlgorithmMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    // Dropdowns, & buttons
    public List<TMPro.TMP_Dropdown> chanceDropdowns;
    public List<TMPro.TMP_Dropdown> objectDropdowns;

    public List<Button>             objectButtons;
    public List<Button>             objectSubMenuButtons;
    public List<Sprite>             objectSprites;
    public List<int>                objectButtonSpriteNdx;

    public GameObject               objectSelectionSubMenu;

    public Button                   loadButton;
    public Button                   saveButton;
    public Button                   deleteButton;
    public Button                   goBackButton;

    public Button                   defaultSettingsButton;

    // Shake display text animator
    public Animator                 messageDisplayAnim;

    [Header("Set Dynamically")]
    public int                      selectedObjectButtonNdx = 0;

    private void OnEnable() {
        // Display text
        if (Time.time > 0.01f) {
            SetGeneralDisplayTextMessage();
        }
    }

    void Start() {
        // Get top level chanceDropdowns PlayerPrefs
        if (PlayerPrefs.HasKey("Chance Dropdown 0")) {
            SetChanceDropdownValue(0, PlayerPrefs.GetInt("Chance Dropdown 0"));
        } else {
            chanceDropdowns[0].value = 6; // 30%
        }
        if (PlayerPrefs.HasKey("Chance Dropdown 1")) {
            SetChanceDropdownValue(1, PlayerPrefs.GetInt("Chance Dropdown 1"));
        } else {
            chanceDropdowns[1].value = 7; // 35%
        }
        if (PlayerPrefs.HasKey("Chance Dropdown 4")) {
            SetChanceDropdownValue(4, PlayerPrefs.GetInt("Chance Dropdown 4"));
        } else {
            chanceDropdowns[4].value = 7; // 35%
        }

        // Get mid level chanceDropdowns PlayerPrefs
        if (PlayerPrefs.HasKey("Chance Dropdown 2")) {
            SetChanceDropdownValue(2, PlayerPrefs.GetInt("Chance Dropdown 2"));
        } else {
            chanceDropdowns[2].value = 10; // 50%
        }
        if (PlayerPrefs.HasKey("Chance Dropdown 3")) {
            SetChanceDropdownValue(3, PlayerPrefs.GetInt("Chance Dropdown 3"));
        } else {
            chanceDropdowns[3].value = 10; // 50%
        }
        if (PlayerPrefs.HasKey("Chance Dropdown 5")) {
            SetChanceDropdownValue(5, PlayerPrefs.GetInt("Chance Dropdown 5"));
        } else {
            chanceDropdowns[5].value = 15; // 75%
        }
        if (PlayerPrefs.HasKey("Chance Dropdown 6")) {
            SetChanceDropdownValue(6, PlayerPrefs.GetInt("Chance Dropdown 6"));
        } else {
            chanceDropdowns[6].value = 5; // 25%
        }

        // Get objectButtons PlayerPrefs
        if (PlayerPrefs.HasKey("Object Button 0")) {
            SetObjectButtonValue(0, PlayerPrefs.GetInt("Object Button 0"));
        } else {
            SetObjectButtonValue(0, 3); // Horizontal block
        }
        if (PlayerPrefs.HasKey("Object Button 1")) {
            SetObjectButtonValue(1, PlayerPrefs.GetInt("Object Button 1"));
        } else {
            SetObjectButtonValue(1, 0); // Vertical low block
        }
        if (PlayerPrefs.HasKey("Object Button 2")) {
            SetObjectButtonValue(2, PlayerPrefs.GetInt("Object Button 2"));
        } else {
            SetObjectButtonValue(2, 17); // Vertical high block
        }
        if (PlayerPrefs.HasKey("Object Button 3")) {
            SetObjectButtonValue(3, PlayerPrefs.GetInt("Object Button 3"));
        } else {
            SetObjectButtonValue(3, 43); // Quid pickup
        }
        if (PlayerPrefs.HasKey("Object Button 4")) {
            SetObjectButtonValue(4, PlayerPrefs.GetInt("Object Button 4"));
        } else {
            SetObjectButtonValue(4, 44); // Shield pickup
        }

        // Add listener to dropdowns
        for (int i = 0; i < chanceDropdowns.Count; i++) {
            int copy = i;
            chanceDropdowns[copy].onValueChanged.AddListener(delegate { SetChanceDropdownValue(copy, chanceDropdowns[copy].value); });
        }

        // Add listeners to buttons
        for (int i = 0; i < objectButtons.Count; i++) {
            int copy = i;
            objectButtons[copy].onClick.AddListener(delegate { OpenObjectSelectionSubMenu(copy); });
        }

        loadButton.onClick.AddListener(delegate { LoadCustomAlgorithmButton(); });
        saveButton.onClick.AddListener(delegate { SaveCustomAlgorithmButton(); });
        deleteButton.onClick.AddListener(delegate { DeleteCustomAlgorithmButton(); });
        defaultSettingsButton.onClick.AddListener(delegate { AddDefaultSettingsConfirmationListeners(); });
        goBackButton.onClick.AddListener(delegate { CloseObjectSelectionSubMenu(); });

        // Add listeners to object selection sub menu buttons
        for (int i = 0; i < objectSubMenuButtons.Count; i++) {
            int copy = i;
            objectSubMenuButtons[copy].onClick.AddListener(delegate { SetObjectToSpawn(copy); });
        }
    }

    void SetChanceDropdownValue(int ndx, int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.chancesToSpawn[ndx] = valueAsFloat / 20;

        chanceDropdowns[ndx].value = value;

        // If dropdown values total == 100%, reset colors
        if (ndx == 0 || ndx == 1 || ndx == 4) {
            List<TMPro.TMP_Dropdown> a = new List<TMPro.TMP_Dropdown>() { chanceDropdowns[0], chanceDropdowns[1], chanceDropdowns[4] };
            if (CheckDropdownForValidValues(a)) {
                ResetDropdownColor(chanceDropdowns[0]);
                ResetDropdownColor(chanceDropdowns[1]);
                ResetDropdownColor(chanceDropdowns[4]);
            }
        } else if (ndx == 2 || ndx == 3) {
            List<TMPro.TMP_Dropdown> b = new List<TMPro.TMP_Dropdown>() { chanceDropdowns[2], chanceDropdowns[3] };
            if (CheckDropdownForValidValues(b)) {
                ResetDropdownColor(chanceDropdowns[2]);
                ResetDropdownColor(chanceDropdowns[3]);
            }
        } else if (ndx == 5 || ndx == 6) {
            List<TMPro.TMP_Dropdown> c = new List<TMPro.TMP_Dropdown>() { chanceDropdowns[5], chanceDropdowns[6] };
            if (CheckDropdownForValidValues(c)) {
                ResetDropdownColor(chanceDropdowns[5]);
                ResetDropdownColor(chanceDropdowns[6]);
            }
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    //
    void OpenObjectSelectionSubMenu(int ndx) {
        // Cache button index
        selectedObjectButtonNdx = ndx;

        // Open object (obstacles & items) sub menu
        objectSelectionSubMenu.SetActive(true);
    }

    //
    void SetObjectToSpawn(int value) {
        // Set object to spawn
        GameManager.S.spawner.objectsToSpawn[selectedObjectButtonNdx] = value;

        // Set button image
        objectButtonSpriteNdx[selectedObjectButtonNdx] = value;
        objectButtons[selectedObjectButtonNdx].GetComponent<Image>().sprite = objectSprites[value];

        // Save selected object button value
        string tString = "Object Button " + selectedObjectButtonNdx.ToString();
        PlayerPrefs.SetInt(tString, GameManager.S.spawner.objectsToSpawn[selectedObjectButtonNdx]);

        // Close object (obstacles & items) sub menu
        CloseObjectSelectionSubMenu();
    }

    //
    void CloseObjectSelectionSubMenu() {
        objectSelectionSubMenu.SetActive(false);
    }

    //
    void SetObjectButtonValue(int buttonNdx, int objectNdx) {
        GameManager.S.spawner.objectsToSpawn[buttonNdx] = objectNdx;

        // Set button sprite
        objectButtonSpriteNdx[buttonNdx] = objectNdx;
        objectButtons[buttonNdx].GetComponent<Image>().sprite = objectSprites[objectNdx];
    }

    void LoadCustomAlgorithmButton() {
        GameManager.S.customAlgorithmMenuCS.gameObject.SetActive(true);
    }

    void SaveCustomAlgorithmButton() {
        GameManager.S.customAlgorithmMenuCS.gameObject.SetActive(true);
    }

    void DeleteCustomAlgorithmButton() {
        GameManager.S.customAlgorithmMenuCS.gameObject.SetActive(true);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Adds functions to the sub menu's yes/no buttons
    void AddDefaultSettingsConfirmationListeners() {
        GameManager.S.subMenuCS.AddListeners(DefaultSettings, "Are you sure that you would like to\nreset this menu's options to their default values?");
    }
    // On 'Yes' button click, returns all menu settings to their default value
    public void DefaultSettings(int yesOrNo = -1) {
        // Deactivate sub menu
        GameManager.S.subMenuGO.SetActive(false);

        // 
        if (yesOrNo == 0) {
            // Set dropdowns to default values
            SetChanceDropdownValue(0, 6); // 30%
            SetChanceDropdownValue(1, 7); // 35%
            SetChanceDropdownValue(4, 7); // 35%

            SetChanceDropdownValue(2, 10); // 50%
            SetChanceDropdownValue(3, 10); // 50%

            SetChanceDropdownValue(5, 15); // 75%
            SetChanceDropdownValue(6, 5); // 25%

            SetObjectButtonValue(0, 3); // Horizontal block
            SetObjectButtonValue(1, 0); // Vertical low block
            SetObjectButtonValue(2, 17); // Vertical high block
            SetObjectButtonValue(3, 43); // Quid pickup
            SetObjectButtonValue(4, 44); // Shield pickup

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Options set to their default values!");
        } else {
            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the algorithm menu!");
        }
    }

    public bool CheckAllDropdownsForValidValues() {
        List<bool> isTrue = new List<bool>();

        List<TMPro.TMP_Dropdown> a = new List<TMPro.TMP_Dropdown>() { chanceDropdowns[0], chanceDropdowns[1], chanceDropdowns[4] };
        if (!CheckDropdownForValidValues(a)) {
            DropdownValuesInvalid(a, Color.red);
            isTrue.Add(CheckDropdownForValidValues(a));
        }
        List<TMPro.TMP_Dropdown> b = new List<TMPro.TMP_Dropdown>() { chanceDropdowns[2], chanceDropdowns[3] };
        if (!CheckDropdownForValidValues(b)) {
            DropdownValuesInvalid(b, new Color(1, 0.25f, 0, 1));
            isTrue.Add(CheckDropdownForValidValues(b));
        }
        List<TMPro.TMP_Dropdown> c = new List<TMPro.TMP_Dropdown>() { chanceDropdowns[5], chanceDropdowns[6] };
        if (!CheckDropdownForValidValues(c)) {
            DropdownValuesInvalid(c, Color.yellow);
            isTrue.Add(CheckDropdownForValidValues(c));
        }

        for (int i = 0; i < isTrue.Count; i++) {
            if (!isTrue[i]) {
                // Input box shake animation
                messageDisplayAnim.CrossFade("DisplayTextShake", 0);

                return false;
            }
        }

        //
        SavePlayerPrefs();

        return true;
    }

    void SavePlayerPrefs() {
        // Chance dropdowns
        for (int i = 0; i < chanceDropdowns.Count; i++) {
            string tString = "Chance Dropdown " + i.ToString();
            PlayerPrefs.SetInt(tString, chanceDropdowns[i].value);
        }
    }

    void DropdownValuesInvalid(List<TMPro.TMP_Dropdown> chanceDropdowns, Color color) {
        // Display text
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Please ensure the total value of\nthe highlighted linked dropdowns is 100%.");

        // Highlight dropdowns
        for (int i = 0; i < chanceDropdowns.Count; i++) {
            // Set colors
            ColorBlock cb = chanceDropdowns[i].colors;
            cb.normalColor = color;
            cb.highlightedColor = color;
            cb.selectedColor = color;
            chanceDropdowns[i].colors = cb;

            // Shake animation clip
            Animator anim = chanceDropdowns[i].GetComponent<Animator>();
            if (anim) {
                anim.CrossFade("Shake", 0);
            }
        }
    }

    bool CheckDropdownForValidValues(List<TMPro.TMP_Dropdown> chanceDropdowns) {
        // Add up chanceDropdown values
        int total = 0;
        for (int i = 0; i < chanceDropdowns.Count; i++) {
            total += chanceDropdowns[i].value;
        }

        // Prompt user that combined dropdown values must equal 100%
        if (total == 20) {
            return true;
        }

        return false;
    }

    void ResetDropdownColor(TMPro.TMP_Dropdown chanceDropdown) {
        ColorBlock cb = chanceDropdown.colors;
        cb.normalColor = Color.white;
        cb.highlightedColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 1);
        cb.selectedColor = new Color(0.9607843f, 0.9607843f, 0.9607843f, 1);
        chanceDropdown.colors = cb;
    }

    // Sets the DisplayText's message depending on which page of the menu is visible
    void SetGeneralDisplayTextMessage() {
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the algorithm menu:\nView and adjust the flow chart of of how likely\nand which objects will be randomly generated\nduring gameplay.");
    }
}