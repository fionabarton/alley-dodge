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

    //public Button                   defaultSettingsButton;
    //public Button                   randomizeAllButton;
    //public Button                   randomizeChancesButton;
    //public Button                   randomizeObjectsButton;
    //public Button                   randomizeSpeedsButton;

    /// Basic Options //////////////////////////////////////////////
    public Button                   normalModeButton;
    public Button                   randomModeButton;
    public Button                   advancedOptionsMenuButton;
    public TextMeshProUGUI          menuHeaderText;

    public GameObject               basicOptionsMenu;
    public GameObject               advancedOptionsMenu;

    public GameObject               cursorGO;
    public List<Button>             gameModeButtons;

    public TextMeshProUGUI          gameModeTypeText;
    public TextMeshProUGUI          modeDescriptionText;
    ////////////////////////////////////////////////////////////////

    public GameObject               normalModeImage;
    public GameObject               randomModeImage;
    public GameObject               customModeImage;

    /// Advanced Options ///////////////////////////////////////////
    public Button                   goBackToBasicOptionsButton;
    ////////////////////////////////////////////////////////////////

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
        // Get chanceDropdowns PlayerPrefs
        if (PlayerPrefs.HasKey("Chance Value 0")) {
            SetChanceButtonValue(0, PlayerPrefs.GetInt("Chance Value 0"));
        } else {
            SetChanceButtonValue(0, 4); // 20%
        }
        if (PlayerPrefs.HasKey("Chance Value 1")) {
            SetChanceButtonValue(1, PlayerPrefs.GetInt("Chance Value 1"));
        } else {
            SetChanceButtonValue(1, 4); // 20%
        }
        if (PlayerPrefs.HasKey("Chance Value 2")) {
            SetChanceButtonValue(2, PlayerPrefs.GetInt("Chance Value 2"));
        } else {
            SetChanceButtonValue(2, 4); // 20%
        }
        if (PlayerPrefs.HasKey("Chance Value 3")) {
            SetChanceButtonValue(3, PlayerPrefs.GetInt("Chance Value 3"));
        } else {
            SetChanceButtonValue(3, 6); // 30%
        }
        if (PlayerPrefs.HasKey("Chance Value 4")) {
            SetChanceButtonValue(4, PlayerPrefs.GetInt("Chance Value 4"));
        } else {
            SetChanceButtonValue(4, 2); // 10%
        }
        if (PlayerPrefs.HasKey("Chance Value 5")) {
            SetChanceButtonValue(5, PlayerPrefs.GetInt("Chance Value 5"));
        } else {
            SetChanceButtonValue(5, 0); // 0%
        }
        if (PlayerPrefs.HasKey("Chance Value 6")) {
            SetChanceButtonValue(6, PlayerPrefs.GetInt("Chance Value 6"));
        } else {
            SetChanceButtonValue(6, 0); // 0%
        }
        if (PlayerPrefs.HasKey("Chance Value 7")) {
            SetChanceButtonValue(7, PlayerPrefs.GetInt("Chance Value 7"));
        } else {
            SetChanceButtonValue(7, 0); // 0%
        }
        if (PlayerPrefs.HasKey("Chance Value 8")) {
            SetChanceButtonValue(8, PlayerPrefs.GetInt("Chance Value 8"));
        } else {
            SetChanceButtonValue(8, 0); // 0%
        }
        if (PlayerPrefs.HasKey("Chance Value 9")) {
            SetChanceButtonValue(9, PlayerPrefs.GetInt("Chance Value 9"));
        } else {
            SetChanceButtonValue(9, 0); // 0%
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
        if (PlayerPrefs.HasKey("Object Button 5")) {
            SetObjectToSpawn(PlayerPrefs.GetInt("Object Button 5"), 5);
        } else {
            SetObjectToSpawn(50, 5); // Nothing
        }
        if (PlayerPrefs.HasKey("Object Button 6")) {
            SetObjectToSpawn(PlayerPrefs.GetInt("Object Button 6"), 6);
        } else {
            SetObjectToSpawn(50, 6); // Nothing
        }
        if (PlayerPrefs.HasKey("Object Button 7")) {
            SetObjectToSpawn(PlayerPrefs.GetInt("Object Button 7"), 7);
        } else {
            SetObjectToSpawn(50, 7); // Nothing
        }
        if (PlayerPrefs.HasKey("Object Button 8")) {
            SetObjectToSpawn(PlayerPrefs.GetInt("Object Button 8"), 8);
        } else {
            SetObjectToSpawn(50, 8); // Nothing
        }
        if (PlayerPrefs.HasKey("Object Button 9")) {
            SetObjectToSpawn(PlayerPrefs.GetInt("Object Button 9"), 9);
        } else {
            SetObjectToSpawn(50, 9); // Nothing
        }

        // Get speedDropdowns PlayerPrefs
        if (PlayerPrefs.HasKey("Speed Dropdown 0")) {
            SetStartingObjectSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 0"));
        } else {
            SetStartingObjectSpeedDropdownValue(2); // 6 MPH 
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 1")) {
            SetAmountToIncreaseObjectSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 1"));
        } else {
            SetAmountToIncreaseObjectSpeedDropdownValue(1); // 1 MPH / 0.4469444444f            
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 2")) {
            SetStartingSpawnSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 2"));
        } else {
            SetStartingSpawnSpeedDropdownValue(9); // 20 OPM / 3.0f
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 3")) {
            SetAmountToDecreaseSpawnSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 3"));
        } else {
            SetAmountToDecreaseSpawnSpeedDropdownValue(3); // 3 OPM
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
        goBackButton.onClick.AddListener(delegate { CloseObjectSelectionSubMenu(); });

        /// Basic Options //////////////////////////////////////////////
        normalModeButton.onClick.AddListener(delegate { SetGameToNormalMode(); });
        randomModeButton.onClick.AddListener(delegate { SetGameToRandomMode(); });
        
        advancedOptionsMenuButton.onClick.AddListener(delegate { SwapBetweenBasicAndAdvancedMenus(); });
        goBackToBasicOptionsButton.onClick.AddListener(delegate { SwapBetweenBasicAndAdvancedMenus(); });
        ////////////////////////////////////////////////////////////////

        // Add listeners to object selection sub menu buttons
        for (int i = 0; i < objectSubMenuButtons.Count; i++) {
            int copy = i;
            objectSubMenuButtons[copy].onClick.AddListener(delegate { SetObjectToSpawn(copy); });
        }

        if (PlayerPrefs.HasKey("Game Mode")) {
            switch (PlayerPrefs.GetInt("Game Mode")) {
                case 0:
                    SetUIToNormalMode();
                    break;
                case 1:
                    SetUIToRandomMode();
                    break;
                case 2:
                    SetUIToCustomMode();
                    break;
            }
        } else {
            SetUIToNormalMode();
        }
    }

    //
    void HighlightSelectedButton(int ndx) {
        // Reset all menu buttons' colors
        for (int i = 0; i < gameModeButtons.Count; i++) {
            ColorBlock colors = gameModeButtons[i].colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = new Color(0.45f, 0.85f, 0.3f, 1);
            gameModeButtons[i].colors = colors;
        }

        // Set selected menu button's colors
        ColorBlock c = gameModeButtons[ndx].colors;
        c.normalColor = new Color(0.805353f, 0.9333333f, 0.372549f, 1);
        c.highlightedColor = new Color(0.45f, 0.85f, 0.3f, 1);
        gameModeButtons[ndx].colors = c;

        // Set cursor position
        GameManager.utilities.PositionCursor(cursorGO, gameModeButtons[ndx].gameObject, 0, 60f, 3);
    }

    /// Basic Options //////////////////////////////////////////////
    public void SetGameToNormalMode() {
        // Set game options
        NormalModeSettings();

        // Save current mode
        PlayerPrefs.SetInt("Game Mode", 0);

        // Delayed text display
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Game set to normal mode!", true);

        SetUIToNormalMode();

        Debug.Log("Normal called");
    }
    void SetUIToNormalMode() {
        // Set button color
        HighlightSelectedButton(0);

        // Set text
        gameModeTypeText.text = "Game Mode: <color=#D9D9D9>Normal</color>";
        modeDescriptionText.text = "Description:\n<color=#D9D9D9>3 different obstacle shapes.\nSidestep, duck, or climb!</color>";

        // Activate algorithm image
        normalModeImage.SetActive(true);
        randomModeImage.SetActive(false);
        customModeImage.SetActive(false);
    }

    public void SetGameToRandomMode() {
        // Set game options
        RandomModeSettings();

        // Save current mode
        PlayerPrefs.SetInt("Game Mode", 1);

        // Delayed text display
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Game set to hard mode!", true);

        SetUIToRandomMode();

        Debug.Log("Random called");
    }
    void SetUIToRandomMode() {
        // Set button color
        HighlightSelectedButton(1);

        // Set text
        gameModeTypeText.text = "Game Mode: <color=#D9D9D9>Hard</color>";
        modeDescriptionText.text = "Description:\n<color=#D9D9D9>All 46 different obstacle shapes.\nSqueeze your body all over the game.</color>";

        // Activate algorithm image
        normalModeImage.SetActive(false);
        randomModeImage.SetActive(true);
        customModeImage.SetActive(false);
    }
    ////////////////////////////////////////////////////////////////

    /// Advanced Options ///////////////////////////////////////////
    // Swaps which of the 2 halves of the algorithm menu is active
    public void SwapBetweenBasicAndAdvancedMenus() {
        if (basicOptionsMenu.activeInHierarchy) {
            // Save current mode
            PlayerPrefs.SetInt("Game Mode", 2);

            Debug.Log("Custom called");

            // Activate menu
            basicOptionsMenu.SetActive(false);
            advancedOptionsMenu.SetActive(true);

            // Set header text
            menuHeaderText.text = "Game Options: <color=#D9D9D9>Advanced";

            SetUIToCustomMode();
        } else {
            // Activate menu
            advancedOptionsMenu.SetActive(false);
            basicOptionsMenu.SetActive(true);

            // Set header text
            menuHeaderText.text = "Game Options: <color=#D9D9D9>Basic";
        }
    }
    void SetUIToCustomMode() {
        // Set button color
        HighlightSelectedButton(2);

        // Set text
        gameModeTypeText.text = "Game Mode: <color=#D9D9D9>Custom</color>";
        modeDescriptionText.text = "Description:\n<color=#D9D9D9>Customized set of game options.\nAdjust everything from game speed to objects to spawn. </color>";

        // Activate algorithm image
        normalModeImage.SetActive(false);
        randomModeImage.SetActive(false);
        customModeImage.SetActive(true);
    }

    ////////////////////////////////////////////////////////////////

    public void SetChanceButtonValue(int ndx, int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.chancesToSpawn[ndx] = valueAsFloat / 20f;

        // If button values total == 100%, reset colors
        if (CheckButtonForValidValues(GameManager.S.spawner.chancesToSpawn)) {
            for(int i = 0; i < chanceButtons.Count; i++) {
                ResetButtonColor(chanceButtons[i]);
            }
        }

        // Set button text
        chanceButtons[ndx].GetComponentInChildren<TextMeshProUGUI>().text = (valueAsFloat * 5f).ToString() + "%";

        // Delayed text display
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Chance of this object being spawned selected!", true);

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
        GameManager.S.spawner.startingObjectSpeed = GameManager.S.objectSpeedValues[value];

        // Update button text
        objectSpeedButton.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.S.objectSpeedDisplayedValues[value].ToString();

        // Save value
        PlayerPrefs.SetInt("Speed Dropdown 0", value);

        // Delayed text display
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Starting object speed selected!", true);
    }

    public void SetAmountToIncreaseObjectSpeedDropdownValue(int value) {
        // Set value
        GameManager.S.spawner.amountToIncreaseObjectSpeed = GameManager.S.amountToIncreaseValues[value];

        // Update button text
        amountToIncreaseButton.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.S.amountToIncreaseDisplayedValues[value].ToString();

        // Save value
        PlayerPrefs.SetInt("Speed Dropdown 1", value);

        // Delayed text display
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Amount to increase object speed per level selected!", true);
    }

    public void SetStartingSpawnSpeedDropdownValue(int value) {
        // Set value
        GameManager.S.spawner.startingSpawnSpeed = GameManager.S.spawnSpeedValues[value];

        // Update button text
        spawnSpeedButton.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.S.spawnSpeedDisplayedValues[value].ToString();

        // Save value
        PlayerPrefs.SetInt("Speed Dropdown 2", value);

        // Delayed text display
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Starting spawn speed selected!", true);
    }

    public void SetAmountToDecreaseSpawnSpeedDropdownValue(int value) {
        // Set value
        GameManager.S.spawner.amountToDecreaseSpawnSpeed = GameManager.S.amountToDecreaseValues[value];

        // Update button text
        amountToDecreaseButton.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.S.amountToDecreaseDisplayedValues[value].ToString();

        // Save value
        PlayerPrefs.SetInt("Speed Dropdown 3", value);

        // Delayed text display
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Amount to increase spawn speed per level selected!", true);
    }

    public void NormalModeSettings() {
        // Set to default chance values
        SetChanceValuesToDefaultSetting();

        SetObjectToSpawn(7, 0); // Horizontal block
        SetObjectToSpawn(0, 1); // Vertical low block
        SetObjectToSpawn(20, 2); // Vertical high block
        SetObjectToSpawn(47, 3); // Quid pickup
        SetObjectToSpawn(48, 4); // Shield pickup
        SetObjectToSpawn(50, 5); // Nothing
        SetObjectToSpawn(50, 6); // Nothing
        SetObjectToSpawn(50, 7); // Nothing
        SetObjectToSpawn(50, 8); // Nothing
        SetObjectToSpawn(50, 9); // Nothing

        // Set to default speed values
        SetStartingObjectSpeedDropdownValue(2);         // 6 MPH 
        SetAmountToIncreaseObjectSpeedDropdownValue(1); // 1 MPH / 0.4469444444f
        SetStartingSpawnSpeedDropdownValue(9);          // 20 OPM / 3.0f
        SetAmountToDecreaseSpawnSpeedDropdownValue(3);  // 3 OPM
    }
    public void RandomModeSettings() {
        // Set chance values
        SetChanceButtonValue(0, 15); // 75%
        SetChanceButtonValue(1, 4);  // 20%
        SetChanceButtonValue(2, 1);  // 5%

        SetChanceButtonValue(3, 0); // 0%
        SetChanceButtonValue(4, 0); // 0%
        SetChanceButtonValue(5, 0); // 0%
        SetChanceButtonValue(6, 0); // 0%
        SetChanceButtonValue(7, 0); // 0%
        SetChanceButtonValue(8, 0); // 0%
        SetChanceButtonValue(9, 0); // 0%

        // Set object to spawn values
        SetObjectToSpawn(46, 0); // Random block
        SetObjectToSpawn(47, 1); // Yellow coin
        SetObjectToSpawn(48, 2); // Blue shield
        SetObjectToSpawn(50, 3); // Nothing
        SetObjectToSpawn(50, 4); // Nothing
        SetObjectToSpawn(50, 5); // Nothing
        SetObjectToSpawn(50, 6); // Nothing
        SetObjectToSpawn(50, 7); // Nothing
        SetObjectToSpawn(50, 8); // Nothing
        SetObjectToSpawn(50, 9); // Nothing

        // Set speed values
        SetStartingObjectSpeedDropdownValue(2);         // 6 MPH 
        SetAmountToIncreaseObjectSpeedDropdownValue(1); // 1 MPH / 0.4469444444f
        SetStartingSpawnSpeedDropdownValue(9);          // 20 OPM / 3.0f
        SetAmountToDecreaseSpawnSpeedDropdownValue(3);  // 3 OPM
    }

    void SetChanceValuesToDefaultSetting() {
        SetChanceButtonValue(0, 4); // 20%
        SetChanceButtonValue(1, 4); // 20%
        SetChanceButtonValue(2, 4); // 20%

        SetChanceButtonValue(3, 6); // 30%
        SetChanceButtonValue(4, 2); // 10%

        SetChanceButtonValue(5, 0); // 0%
        SetChanceButtonValue(6, 0); // 0%
        SetChanceButtonValue(7, 0); // 0%
        SetChanceButtonValue(8, 0); // 0%
        SetChanceButtonValue(9, 0); // 0%
    }

    public bool CheckAllButtonsForValidValues() {
        if (!CheckButtonForValidValues(GameManager.S.spawner.chancesToSpawn)) {
            ButtonValuesInvalid(chanceButtons, GameManager.S.spawner.chancesToSpawn, Color.red, "red");
            //isTrue.Add(CheckButtonForValidValues(GameManager.S.spawner.chancesToSpawn));
            if (!CheckButtonForValidValues(GameManager.S.spawner.chancesToSpawn)) {
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
        for (int i = 0; i < chanceValues.Count; i++) {
            sumFloat += chanceValues[i];
        }
        string sumString = ((int)(sumFloat * 100)).ToString() + "%";

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
        if (total >= 0.975f && total < 1.025f) {
            return true;
        }

        return false;
    }

    void ResetButtonColor(Button chanceButton) {
        ColorBlock cb = chanceButton.colors;
        cb.normalColor = Color.white;
        cb.highlightedColor = new Color(0.45f, 0.85f, 0.3f, 1);
        cb.selectedColor = new Color(0.45f, 0.85f, 0.3f, 1);
        chanceButton.colors = cb;
    }

    // Sets the DisplayText's message depending on which page of the menu is visible
    void SetGeneralDisplayTextMessage() {
        GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the game mode menu:\nSet how likely and which objects are randomly generated by the game,\nalong with how quickly they move and are spawned.", true);
    }
}