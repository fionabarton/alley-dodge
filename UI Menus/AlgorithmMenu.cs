using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlgorithmMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    // Chance selection
    public List<Button>             chanceButtons;

    // Object selection
    public List<Button>             objectButtons;
    public List<Button>             objectSubMenuButtons;
    public List<Sprite>             objectSprites;
    public List<int>                objectButtonSpriteNdx;

    public GameObject               objectSelectionSubMenu;

    // Speed selection
    public Button                   objectSpeedButton;
    public Button                   amountToIncreaseButton;
    public Button                   spawnSpeedButton;
    public Button                   amountToDecreaseButton;

    public Button                   loadButton;
    public Button                   saveButton;
    public Button                   goBackButton;

    public Button                   defaultSettingsButton;
    public Button                   randomizeAllButton;
    public Button                   randomizeChancesButton;
    public Button                   randomizeObjectsButton;
    public Button                   randomizeSpeedsButton;

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

        // Get speedDropdowns PlayerPrefs
        if (PlayerPrefs.HasKey("Speed Dropdown 0")) {
            SetStartingObjectSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 0"));
        } else {
            SetStartingObjectSpeedDropdownValue(9); // 10
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 1")) {
            SetAmountToIncreaseObjectSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 1"));
        } else {
            SetAmountToIncreaseObjectSpeedDropdownValue(2); // 0.2f
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 2")) {
            SetStartingSpawnSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 2"));
        } else {
            SetStartingSpawnSpeedDropdownValue(10); // 2.0f
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 3")) {
            SetAmountToDecreaseSpawnSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 3"));
        } else {
            SetAmountToDecreaseSpawnSpeedDropdownValue(1); // 0.1f
        }

        // In case game was closed before ensuring connected chance values are valid (have a sum of exactly 100%),
        // otherwise the menu will not open correctly
        if (!GameManager.S.algorithmMenuCS.CheckAllButtonsForValidValues()) {
            SetChanceValuesToDefaultSetting();
        }

        // Add listeners to buttons
        for (int i = 0; i < objectButtons.Count; i++) {
            int copy = i;
            objectButtons[copy].onClick.AddListener(delegate { OpenObjectSelectionSubMenu(copy); });
        }

        loadButton.onClick.AddListener(delegate { GameManager.S.customAlgorithmMenuCS.ActivateMenu("Load"); });
        saveButton.onClick.AddListener(delegate { GameManager.S.customAlgorithmMenuCS.ActivateMenu("Save"); });
        defaultSettingsButton.onClick.AddListener(delegate { AddDefaultSettingsConfirmationListeners(); });
        randomizeAllButton.onClick.AddListener(delegate { AddRandomizeSettingsConfirmationListeners(); });
        randomizeChancesButton.onClick.AddListener(delegate { AddRandomizeChancesSettingsConfirmationListeners(); });
        randomizeObjectsButton.onClick.AddListener(delegate { AddRandomizeObjectsSettingsConfirmationListeners(); });
        randomizeSpeedsButton.onClick.AddListener(delegate { AddRandomizeSpeedsSettingsConfirmationListeners(); });
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

        // Delayed text display
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Chance of the flow chart\nfollowing this path selected!", true);

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

        // Delayed text display
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Object to spawn selected!", true);

        // Close object (obstacles & items) sub menu
        CloseObjectSelectionSubMenu();
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //
    public void SetStartingObjectSpeedDropdownValue(int value) {
        // Set value 
        GameManager.S.spawner.startingObjectSpeed = value + 1;

        // Update button text
        objectSpeedButton.GetComponentInChildren<TextMeshProUGUI>().text = (value + 1).ToString();

        // Save value
        PlayerPrefs.SetInt("Speed Dropdown 0", value);

        // Delayed text display
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Starting object speed selected!", true);
    }

    public void SetAmountToIncreaseObjectSpeedDropdownValue(int value) {
        float valueAsFloat = value;

        // Set value
        GameManager.S.spawner.amountToIncreaseObjectSpeed = (valueAsFloat / 10);

        // Update button text
        amountToIncreaseButton.GetComponentInChildren<TextMeshProUGUI>().text = (valueAsFloat / 10).ToString();

        // Save value
        PlayerPrefs.SetInt("Speed Dropdown 1", value);

        // Delayed text display
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Amount to increase object speed\nper level selected!", true);
    }

    public void SetStartingSpawnSpeedDropdownValue(int value) {
        float valueAsFloat = value;

        // Set value
        GameManager.S.spawner.startingSpawnSpeed = (valueAsFloat / 10) + 1.0f;

        // Update button text
        spawnSpeedButton.GetComponentInChildren<TextMeshProUGUI>().text = ((valueAsFloat / 10) + 1.0f).ToString();

        // Save value
        PlayerPrefs.SetInt("Speed Dropdown 2", value);

        // Delayed text display
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Starting spawn speed selected!", true);
    }

    public void SetAmountToDecreaseSpawnSpeedDropdownValue(int value) {
        float valueAsFloat = value;

        // Set value
        GameManager.S.spawner.amountToDecreaseSpawnSpeed = (valueAsFloat / 10);

        // Update button text
        amountToDecreaseButton.GetComponentInChildren<TextMeshProUGUI>().text = (valueAsFloat / 10).ToString();

        // Save value
        PlayerPrefs.SetInt("Speed Dropdown 3", value);

        // Delayed text display
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Amount to decrease spawn speed\nper level selected!", true);
    }

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
            // Set to default chance values
            SetChanceValuesToDefaultSetting();

            //SetObjectToSpawn(3, 0); // Horizontal block
            //SetObjectToSpawn(0, 1); // Vertical low block
            //SetObjectToSpawn(17, 2); // Vertical high block
            //SetObjectToSpawn(43, 3); // Quid pickup
            //SetObjectToSpawn(44, 4); // Shield pickup

            SetObjectToSpawn(7, 0); // Horizontal block
            SetObjectToSpawn(0, 1); // Vertical low block
            SetObjectToSpawn(20, 2); // Vertical high block
            SetObjectToSpawn(47, 3); // Quid pickup
            SetObjectToSpawn(48, 4); // Shield pickup

            // Set to default speed values
            SetStartingObjectSpeedDropdownValue(9); // 10
            SetAmountToIncreaseObjectSpeedDropdownValue(2); // 0.2f
            SetStartingSpawnSpeedDropdownValue(10); // 2.0f
            SetAmountToDecreaseSpawnSpeedDropdownValue(1); // 0.1f

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Options set to their default values!", true);
        } else {
            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the algorithm menu:\nView and adjust the flow chart of how likely\nand which objects will be randomly generated\nduring gameplay.", true);
        }
    }

    // Adds functions to the sub menu's yes/no buttons
    void AddRandomizeSettingsConfirmationListeners() {
        GameManager.S.subMenuCS.AddListeners(RandomizeSettings, "Are you sure that you would like to\nset this menu's options to totally random values?");
    }
    // On 'Yes' button click, sets all menu settings to random values
    void RandomizeSettings(int yesOrNo = -1) {
        // Deactivate sub menu
        GameManager.S.subMenuGO.SetActive(false);

        // 
        if (yesOrNo == 0) {
            // Chance values (set 1)
            int randomInt = Random.Range(0, 19);
            SetChanceButtonValue(0, randomInt);
            int difference = 20 - randomInt;
            randomInt = Random.Range(0, difference);
            SetChanceButtonValue(1, randomInt);
            difference -= randomInt;
            SetChanceButtonValue(2, difference);

            // Chance values (set 2)
            randomInt = Random.Range(0, 19);
            SetChanceButtonValue(3, randomInt);
            difference = 20 - randomInt;
            SetChanceButtonValue(4, difference);

            // Chance values (set 3)
            randomInt = Random.Range(0, 19);
            SetChanceButtonValue(5, randomInt);
            difference = 20 - randomInt;
            SetChanceButtonValue(6, difference); 

            // Objects to spawn
            for (int i = 0; i < 5; i++) {
                randomInt = Random.Range(0, 48);
                SetObjectToSpawn(randomInt, i);
            }

            // Speed values
            SetStartingObjectSpeedDropdownValue(Random.Range(0, 18)); 
            SetAmountToIncreaseObjectSpeedDropdownValue(Random.Range(0, 19)); 
            SetStartingSpawnSpeedDropdownValue(Random.Range(0, 9)); 
            SetAmountToDecreaseSpawnSpeedDropdownValue(Random.Range(0, 9)); 

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Options set to totally random values!", true);
        } else {
            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the algorithm menu:\nView and adjust the flow chart of how likely\nand which objects will be randomly generated\nduring gameplay.", true);
        }
    }

    // Adds functions to the sub menu's yes/no buttons
    void AddRandomizeAllSettingsConfirmationListeners() {
        GameManager.S.subMenuCS.AddListeners(RandomizeSettings, "Are you sure that you would like to\nrandomize the all of the displayed algorithm's values?");
    }


    // Adds functions to the sub menu's yes/no buttons
    void AddRandomizeChancesSettingsConfirmationListeners() {
        GameManager.S.subMenuCS.AddListeners(RandomizeChancesSettings, "Are you sure that you would like to\nrandomize the displayed algorithm's chance values?");
    }
    // On 'Yes' button click, sets chance settings to random values
    void RandomizeChancesSettings(int yesOrNo = -1) {
        // Deactivate sub menu
        GameManager.S.subMenuGO.SetActive(false);

        // 
        if (yesOrNo == 0) {
            // Chance values (set 1)
            int randomInt = Random.Range(0, 19);
            SetChanceButtonValue(0, randomInt);
            int difference = 20 - randomInt;
            randomInt = Random.Range(0, difference);
            SetChanceButtonValue(1, randomInt);
            difference -= randomInt;
            SetChanceButtonValue(2, difference);

            // Chance values (set 2)
            randomInt = Random.Range(0, 19);
            SetChanceButtonValue(3, randomInt);
            difference = 20 - randomInt;
            SetChanceButtonValue(4, difference);

            // Chance values (set 3)
            randomInt = Random.Range(0, 19);
            SetChanceButtonValue(5, randomInt);
            difference = 20 - randomInt;
            SetChanceButtonValue(6, difference);

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Chance values set to totally random values!", true);
        } else {
            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the algorithm menu:\nView and adjust the flow chart of how likely\nand which objects will be randomly generated\nduring gameplay.", true);
        }
    }

    // Adds functions to the sub menu's yes/no buttons
    void AddRandomizeObjectsSettingsConfirmationListeners() {
        GameManager.S.subMenuCS.AddListeners(RandomizeObjectsSettings, "Are you sure that you would like to\nrandomize the displayed algorithm's object values?");
    }
    // On 'Yes' button click, sets object settings to random values
    void RandomizeObjectsSettings(int yesOrNo = -1) {
        // Deactivate sub menu
        GameManager.S.subMenuGO.SetActive(false);

        // 
        if (yesOrNo == 0) {
            // Objects to spawn
            for (int i = 0; i < 5; i++) {
                SetObjectToSpawn(Random.Range(0, 48), i);
            }

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Object values set to totally random values!", true);
        } else {
            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the algorithm menu:\nView and adjust the flow chart of how likely\nand which objects will be randomly generated\nduring gameplay.", true);
        }
    }

    // Adds functions to the sub menu's yes/no buttons
    void AddRandomizeSpeedsSettingsConfirmationListeners() {
        GameManager.S.subMenuCS.AddListeners(RandomizeSpeedSettings, "Are you sure that you would like to\nrandomize the displayed algorithm's speed values?");
    }
    // On 'Yes' button click, sets speed settings to random values
    void RandomizeSpeedSettings(int yesOrNo = -1) {
        // Deactivate sub menu
        GameManager.S.subMenuGO.SetActive(false);

        // 
        if (yesOrNo == 0) {
            // Speed values
            SetStartingObjectSpeedDropdownValue(Random.Range(0, 18));
            SetAmountToIncreaseObjectSpeedDropdownValue(Random.Range(0, 19));
            SetStartingSpawnSpeedDropdownValue(Random.Range(0, 9));
            SetAmountToDecreaseSpawnSpeedDropdownValue(Random.Range(0, 9));

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Speed values set to totally random values!", true);
        } else {
            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the algorithm menu:\nView and adjust the flow chart of how likely\nand which objects will be randomly generated\nduring gameplay.", true);
        }
    }

    void SetChanceValuesToDefaultSetting() {
        SetChanceButtonValue(0, 6); // 30%
        SetChanceButtonValue(1, 7); // 35%
        SetChanceButtonValue(2, 7); // 35%

        SetChanceButtonValue(3, 10); // 50%
        SetChanceButtonValue(4, 10); // 50%

        SetChanceButtonValue(5, 15); // 75%
        SetChanceButtonValue(6, 5); // 25%
    }

    public bool CheckAllButtonsForValidValues() {
        List<bool> isTrue = new List<bool>();

        List<float> aFloats = new List<float>() { GameManager.S.spawner.chancesToSpawn[0], GameManager.S.spawner.chancesToSpawn[1], GameManager.S.spawner.chancesToSpawn[2] };
        List<Button> aButtons = new List<Button>() { chanceButtons[0], chanceButtons[1], chanceButtons[2] };
        if (!CheckButtonForValidValues(aFloats)) {
            ButtonValuesInvalid(aButtons, aFloats, Color.red, "red");
            isTrue.Add(CheckButtonForValidValues(aFloats));
        }
        List<float> bFloats = new List<float>() { GameManager.S.spawner.chancesToSpawn[3], GameManager.S.spawner.chancesToSpawn[4] };
        List<Button> bButtons = new List<Button>() { chanceButtons[3], chanceButtons[4] };
        if (!CheckButtonForValidValues(bFloats)) {
            ButtonValuesInvalid(bButtons, bFloats, new Color(0.2f, 0.4f, 1.0f, 1), "blue");
            isTrue.Add(CheckButtonForValidValues(bFloats));
        }
        List<float> cFloats = new List<float>() { GameManager.S.spawner.chancesToSpawn[5], GameManager.S.spawner.chancesToSpawn[6] };
        List<Button> cButtons = new List<Button>() { chanceButtons[5], chanceButtons[6] };
        if (!CheckButtonForValidValues(cFloats)) {
            ButtonValuesInvalid(cButtons, cFloats, Color.yellow, "yellow");
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

    void ButtonValuesInvalid(List<Button> chanceButtons, List<float> chanceValues, Color color, string colorName) {
        // Get sum of connected chance values as a string
        float sumFloat = 0;
        for(int i = 0; i< chanceValues.Count; i++) {
            sumFloat += chanceValues[i];
        }
        string sumString = (sumFloat * 100).ToString() + "%";

        // Display text
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Please ensure the total value of\nthe connected " + colorName + " buttons is 100%, not " + sumString + ".", true);

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
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the algorithm menu:\nView and adjust the flow chart of how likely\nand which objects will be randomly generated\nduring gameplay.", true);
    }
}