using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlgorithmMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public List<Button>             chanceButtons;

    public List<Button>             objectButtons;
    public List<Button>             objectSubMenuButtons;
    public List<Sprite>             objectSprites;
    public List<int>                objectButtonSpriteNdx;

    public GameObject               objectSelectionSubMenu;

    public Button                   loadButton;
    public Button                   saveButton;
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
        if (PlayerPrefs.HasKey("Chance Value 0")) {
            SetChanceButtonValue(0, PlayerPrefs.GetInt("Chance Value 0"));
        } else {
            SetChanceButtonValue(0, 6); // 30%
        }
        if (PlayerPrefs.HasKey("Chance Value 1")) {
            SetChanceButtonValue(1, PlayerPrefs.GetInt("Chance Value 1"));
        } else {
            SetChanceButtonValue(1, 7); // 35%
        }
        if (PlayerPrefs.HasKey("Chance Value 2")) {
            SetChanceButtonValue(2, PlayerPrefs.GetInt("Chance Value 2"));
        } else {
            SetChanceButtonValue(2, 7); // 35%
        }

        // Get mid level chanceDropdowns PlayerPrefs
        if (PlayerPrefs.HasKey("Chance Value 3")) {
            SetChanceButtonValue(3, PlayerPrefs.GetInt("Chance Value 3"));
        } else {
            SetChanceButtonValue(3, 10); // 50%
        }
        if (PlayerPrefs.HasKey("Chance Value 4")) {
            SetChanceButtonValue(4, PlayerPrefs.GetInt("Chance Value 4"));
        } else {
            SetChanceButtonValue(4, 10); // 50%
        }
        if (PlayerPrefs.HasKey("Chance Value 5")) {
            SetChanceButtonValue(5, PlayerPrefs.GetInt("Chance Value 5"));
        } else {
            SetChanceButtonValue(5, 15); // 75%
        }
        if (PlayerPrefs.HasKey("Chance Value 6")) {
            SetChanceButtonValue(6, PlayerPrefs.GetInt("Chance Value 6"));
        } else {
            SetChanceButtonValue(6, 5); // 25%
        }

        // Get objectButtons PlayerPrefs
        if (PlayerPrefs.HasKey("Object Button 0")) {
            SetObjectToSpawn(PlayerPrefs.GetInt("Object Button 0"), 0);
        } else {
            SetObjectToSpawn(3, 0); // Horizontal block
        }
        if (PlayerPrefs.HasKey("Object Button 1")) {
            SetObjectToSpawn(PlayerPrefs.GetInt("Object Button 1"), 1);
        } else {
            SetObjectToSpawn(0, 1); // Vertical low block
        }
        if (PlayerPrefs.HasKey("Object Button 2")) {
            SetObjectToSpawn(PlayerPrefs.GetInt("Object Button 2"), 2);
        } else {
            SetObjectToSpawn(17, 2); // Vertical high block
        }
        if (PlayerPrefs.HasKey("Object Button 3")) {
            SetObjectToSpawn(PlayerPrefs.GetInt("Object Button 3"), 3);
        } else {
            SetObjectToSpawn(43, 3); // Quid pickup
        }
        if (PlayerPrefs.HasKey("Object Button 4")) {
            SetObjectToSpawn(PlayerPrefs.GetInt("Object Button 4"), 4);
        } else {
            SetObjectToSpawn(44, 4); // Shield pickup
        }

        // Add listeners to buttons
        for (int i = 0; i < objectButtons.Count; i++) {
            int copy = i;
            objectButtons[copy].onClick.AddListener(delegate { OpenObjectSelectionSubMenu(copy); });
        }

        loadButton.onClick.AddListener(delegate { GameManager.S.customAlgorithmMenuCS.ActivateMenu("Load"); });
        saveButton.onClick.AddListener(delegate { GameManager.S.customAlgorithmMenuCS.ActivateMenu("Save"); });
        defaultSettingsButton.onClick.AddListener(delegate { AddDefaultSettingsConfirmationListeners(); });
        goBackButton.onClick.AddListener(delegate { CloseObjectSelectionSubMenu(); });

        // Add listeners to object selection sub menu buttons
        for (int i = 0; i < objectSubMenuButtons.Count; i++) {
            int copy = i;
            objectSubMenuButtons[copy].onClick.AddListener(delegate { SetObjectToSpawn(copy); });
        }
    }

    public void SetChanceButtonValue(int ndx, int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.chancesToSpawn[ndx] = valueAsFloat / 20f;

        // If dropdown values total == 100%, reset colors
        if (ndx == 0 || ndx == 1 || ndx == 2) {
            List<float> a = new List<float>() { GameManager.S.spawner.chancesToSpawn[0], GameManager.S.spawner.chancesToSpawn[1], GameManager.S.spawner.chancesToSpawn[2] };
            if (CheckButtonForValidValues(a)) {
                ResetButtonColor(chanceButtons[0]);
                ResetButtonColor(chanceButtons[1]);
                ResetButtonColor(chanceButtons[2]);
            }
        } else if (ndx == 3 || ndx == 4) {
            List<float> b = new List<float>() { GameManager.S.spawner.chancesToSpawn[3], GameManager.S.spawner.chancesToSpawn[4] };
            if (CheckButtonForValidValues(b)) {
                ResetButtonColor(chanceButtons[3]);
                ResetButtonColor(chanceButtons[4]);
            }
        } else if (ndx == 5 || ndx == 6) {
            List<float> c = new List<float>() { GameManager.S.spawner.chancesToSpawn[5], GameManager.S.spawner.chancesToSpawn[6] };
            if (CheckButtonForValidValues(c)) {
                ResetButtonColor(chanceButtons[5]);
                ResetButtonColor(chanceButtons[6]);
            }
        }

        // Set button text
        chanceButtons[ndx].GetComponentInChildren<TextMeshProUGUI>().text = (valueAsFloat * 5f).ToString() + "%";

        // Save
        string tString = "Chance Value " + ndx.ToString();
        float tFloat = GameManager.S.spawner.chancesToSpawn[ndx] * 20f;
        PlayerPrefs.SetInt(tString, (int)tFloat);
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
    void CloseObjectSelectionSubMenu() {
        objectSelectionSubMenu.SetActive(false);
    }

    public void SetObjectToSpawn(int objectNdx, int buttonNdx = -1) {
        if(buttonNdx == -1) {
            buttonNdx = selectedObjectButtonNdx;
        }

        // Set object to spawn
        GameManager.S.spawner.objectsToSpawn[buttonNdx] = objectNdx;

        // Set button image
        objectButtonSpriteNdx[buttonNdx] = objectNdx;
        objectButtons[buttonNdx].GetComponent<Image>().sprite = objectSprites[objectNdx];

        // Save selected object button value
        string tString = "Object Button " + buttonNdx.ToString();
        PlayerPrefs.SetInt(tString, GameManager.S.spawner.objectsToSpawn[buttonNdx]);

        // Close object (obstacles & items) sub menu
        CloseObjectSelectionSubMenu();
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
            SetChanceButtonValue(0, 6); // 30%
            SetChanceButtonValue(1, 7); // 35%
            SetChanceButtonValue(2, 7); // 35%

            SetChanceButtonValue(3, 10); // 50%
            SetChanceButtonValue(4, 10); // 50%

            SetChanceButtonValue(5, 15); // 75%
            SetChanceButtonValue(6, 5); // 25%

            SetObjectToSpawn(3, 0); // Horizontal block
            SetObjectToSpawn(0, 1); // Vertical low block
            SetObjectToSpawn(17, 2); // Vertical high block
            SetObjectToSpawn(43, 3); // Quid pickup
            SetObjectToSpawn(44, 4); // Shield pickup

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Options set to their default values!");
        } else {
            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the algorithm menu!");
        }
    }

    public bool CheckAllButtonsForValidValues() {
        List<bool> isTrue = new List<bool>();

        List<float> aFloats = new List<float>() { GameManager.S.spawner.chancesToSpawn[0], GameManager.S.spawner.chancesToSpawn[1], GameManager.S.spawner.chancesToSpawn[2] };
        List<Button> aButtons = new List<Button>() { chanceButtons[0], chanceButtons[1], chanceButtons[2] };
        if (!CheckButtonForValidValues(aFloats)) {
            ButtonValuesInvalid(aButtons, Color.red);
            isTrue.Add(CheckButtonForValidValues(aFloats));
        }
        List<float> bFloats = new List<float>() { GameManager.S.spawner.chancesToSpawn[3], GameManager.S.spawner.chancesToSpawn[4] };
        List<Button> bButtons = new List<Button>() { chanceButtons[3], chanceButtons[4] };
        if (!CheckButtonForValidValues(bFloats)) {
            ButtonValuesInvalid(bButtons, new Color(1, 0.25f, 0, 1));
            isTrue.Add(CheckButtonForValidValues(bFloats));
        }
        List<float> cFloats = new List<float>() { GameManager.S.spawner.chancesToSpawn[5], GameManager.S.spawner.chancesToSpawn[6] };
        List<Button> cButtons = new List<Button>() { chanceButtons[5], chanceButtons[6] };
        if (!CheckButtonForValidValues(cFloats)) {
            ButtonValuesInvalid(cButtons, Color.yellow);
            isTrue.Add(CheckButtonForValidValues(cFloats));
        }

        for (int i = 0; i < isTrue.Count; i++) {
            if (!isTrue[i]) {
                // Input box shake animation
                messageDisplayAnim.CrossFade("DisplayTextShake", 0);

                return false;
            }
        }

        return true;
    }

    public void SavePlayerPrefs() {
        // Chance Values
        for (int i = 0; i < chanceButtons.Count; i++) {
            int copy = i;
            string tString = "Chance Value " + copy.ToString();
            float tFloat = GameManager.S.spawner.chancesToSpawn[copy] * 20f;
            PlayerPrefs.SetInt(tString, (int)tFloat);
        }
    }

    void ButtonValuesInvalid(List<Button> chanceButtons, Color color) {
        // Display text
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Please ensure the total value of\nthe highlighted linked dropdowns is 100%.");

        // Highlight dropdowns
        for (int i = 0; i < chanceButtons.Count; i++) {
            // Set colors
            ColorBlock cb = chanceButtons[i].colors;
            cb.normalColor = color;
            cb.highlightedColor = color;
            cb.selectedColor = color;
            chanceButtons[i].colors = cb;

            // Shake animation clip
            Animator anim = chanceButtons[i].GetComponent<Animator>();
            if (anim) {
                anim.CrossFade("Shake", 0);
            }
        }
    }

    bool CheckButtonForValidValues(List<float> chanceValues) {
        // Add up chance values
        float total = 0;
        for (int i = 0; i < chanceValues.Count; i++) {
            total += chanceValues[i];
        }

        // Prompt user that combined dropdown values must equal 100%
        if (total == 1.0) {
            return true;
        }

        return false;
    }

    void ResetButtonColor(Button chanceButton) {
        ColorBlock cb = chanceButton.colors;
        cb.normalColor = Color.white;
        cb.highlightedColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 1);
        cb.selectedColor = new Color(0.9607843f, 0.9607843f, 0.9607843f, 1);
        chanceButton.colors = cb;
    }

    // Sets the DisplayText's message depending on which page of the menu is visible
    void SetGeneralDisplayTextMessage() {
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the algorithm menu:\nView and adjust the flow chart of of how likely\nand which objects will be randomly generated\nduring gameplay.");
    }
}