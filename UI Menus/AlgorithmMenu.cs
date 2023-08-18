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

    public Button                   goBackButton;

    public Button                   defaultSettingsButton;

    // Shake display text animator
    public Animator                 messageDisplayAnim;

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

        // Get objectDropdowns PlayerPrefs
        if (PlayerPrefs.HasKey("Object Dropdown 0")) {
            SetObjectButtonValue(0, PlayerPrefs.GetInt("Object Dropdown 0"));

            //objectButtonSpriteNdx[0] = PlayerPrefs.GetInt("Object Button Sprite Ndx 0");
            //objectButtons[0].GetComponent<Image>().sprite = objectSprites[PlayerPrefs.GetInt("Object Button Sprite Ndx 0")];
            SetObjectButtonSprite(0, PlayerPrefs.GetInt("Object Button Sprite Ndx 0"));
        } else {
            //objectDropdowns[0].value = 2; // Horizontal block
            GameManager.S.spawner.objectsToSpawn[0] = 3;

            //objectButtonSpriteNdx[0] = 3;
            //objectButtons[0].GetComponent<Image>().sprite = objectSprites[3];
            SetObjectButtonSprite(0, 3);
        }
        if (PlayerPrefs.HasKey("Object Dropdown 1")) {
            SetObjectButtonValue(1, PlayerPrefs.GetInt("Object Dropdown 1"));

            //objectButtonSpriteNdx[1] = PlayerPrefs.GetInt("Object Button Sprite Ndx 1");
            //objectButtons[1].GetComponent<Image>().sprite = objectSprites[PlayerPrefs.GetInt("Object Button Sprite Ndx 1")];
            SetObjectButtonSprite(1, PlayerPrefs.GetInt("Object Button Sprite Ndx 1"));
        } else {
            //objectDropdowns[1].value = 0; // Vertical low block
            GameManager.S.spawner.objectsToSpawn[1] = 0;

            //objectButtonSpriteNdx[1] = 0;
            //objectButtons[1].GetComponent<Image>().sprite = objectSprites[0];
            SetObjectButtonSprite(1, 0);
        }
        if (PlayerPrefs.HasKey("Object Dropdown 2")) {
            SetObjectButtonValue(2, PlayerPrefs.GetInt("Object Dropdown 2"));

            //objectButtonSpriteNdx[2] = PlayerPrefs.GetInt("Object Button Sprite Ndx 2");
            //objectButtons[2].GetComponent<Image>().sprite = objectSprites[PlayerPrefs.GetInt("Object Button Sprite Ndx 2")];
            SetObjectButtonSprite(2, PlayerPrefs.GetInt("Object Button Sprite Ndx 2"));
        } else {
            //objectDropdowns[2].value = 17; // Vertical high block
            GameManager.S.spawner.objectsToSpawn[2] = 17;

            //objectButtonSpriteNdx[2] = 17;
            //objectButtons[2].GetComponent<Image>().sprite = objectSprites[17];
            SetObjectButtonSprite(2, 17);
        }
        if (PlayerPrefs.HasKey("Object Dropdown 3")) {
            SetObjectButtonValue(3, PlayerPrefs.GetInt("Object Dropdown 3"));

            //objectButtonSpriteNdx[3] = PlayerPrefs.GetInt("Object Button Sprite Ndx 3");
            //objectButtons[3].GetComponent<Image>().sprite = objectSprites[PlayerPrefs.GetInt("Object Button Sprite Ndx 3")];
            SetObjectButtonSprite(3, PlayerPrefs.GetInt("Object Button Sprite Ndx 3"));
        } else {
            //objectDropdowns[3].value = 43; // Quid pickup
            GameManager.S.spawner.objectsToSpawn[3] = 43;

            //objectButtonSpriteNdx[3] = 43;
            //objectButtons[3].GetComponent<Image>().sprite = objectSprites[43];
            SetObjectButtonSprite(3, 43);
        }
        if (PlayerPrefs.HasKey("Object Dropdown 4")) {
            SetObjectButtonValue(4, PlayerPrefs.GetInt("Object Dropdown 4"));

            //objectButtonSpriteNdx[4] = PlayerPrefs.GetInt("Object Button Sprite Ndx 4");
            //objectButtons[4].GetComponent<Image>().sprite = objectSprites[PlayerPrefs.GetInt("Object Button Sprite Ndx 4")];
            SetObjectButtonSprite(4, PlayerPrefs.GetInt("Object Button Sprite Ndx 4"));
        } else {
            //objectDropdowns[4].value = 44; // Shield pickup
            GameManager.S.spawner.objectsToSpawn[4] = 44;

            //objectButtonSpriteNdx[4] = 44;
            //objectButtons[4].GetComponent<Image>().sprite = objectSprites[44];
            SetObjectButtonSprite(4, 44);
        }

        // Add listener to dropdowns
        chanceDropdowns[0].onValueChanged.AddListener(delegate { SetChanceDropdownValue(0, chanceDropdowns[0].value); });
        chanceDropdowns[1].onValueChanged.AddListener(delegate { SetChanceDropdownValue(1, chanceDropdowns[1].value); });
        chanceDropdowns[2].onValueChanged.AddListener(delegate { SetChanceDropdownValue(2, chanceDropdowns[2].value); });
        chanceDropdowns[3].onValueChanged.AddListener(delegate { SetChanceDropdownValue(3, chanceDropdowns[3].value); });
        chanceDropdowns[4].onValueChanged.AddListener(delegate { SetChanceDropdownValue(4, chanceDropdowns[4].value); });
        chanceDropdowns[5].onValueChanged.AddListener(delegate { SetChanceDropdownValue(5, chanceDropdowns[5].value); });
        chanceDropdowns[6].onValueChanged.AddListener(delegate { SetChanceDropdownValue(6, chanceDropdowns[6].value); });

        // Add listeners to buttons
        objectButtons[0].onClick.AddListener(delegate { OpenObjectSelectionSubMenu(0); });
        objectButtons[1].onClick.AddListener(delegate { OpenObjectSelectionSubMenu(1); });
        objectButtons[2].onClick.AddListener(delegate { OpenObjectSelectionSubMenu(2); });
        objectButtons[3].onClick.AddListener(delegate { OpenObjectSelectionSubMenu(3); });
        objectButtons[4].onClick.AddListener(delegate { OpenObjectSelectionSubMenu(4); });
        defaultSettingsButton.onClick.AddListener(delegate { AddDefaultSettingsConfirmationListeners(); });
        goBackButton.onClick.AddListener(delegate { CloseObjectSelectionSubMenu(); });

        // Add listeners to object selection sub menu buttons
        objectSubMenuButtons[0].onClick.AddListener(delegate { SetObjectToSpawn(0); });
        objectSubMenuButtons[1].onClick.AddListener(delegate { SetObjectToSpawn(1); });
        objectSubMenuButtons[2].onClick.AddListener(delegate { SetObjectToSpawn(2); });
        objectSubMenuButtons[3].onClick.AddListener(delegate { SetObjectToSpawn(3); });
        objectSubMenuButtons[4].onClick.AddListener(delegate { SetObjectToSpawn(4); });
        objectSubMenuButtons[5].onClick.AddListener(delegate { SetObjectToSpawn(5); });
        objectSubMenuButtons[6].onClick.AddListener(delegate { SetObjectToSpawn(6); });
        objectSubMenuButtons[7].onClick.AddListener(delegate { SetObjectToSpawn(7); });
        objectSubMenuButtons[8].onClick.AddListener(delegate { SetObjectToSpawn(8); });
        objectSubMenuButtons[9].onClick.AddListener(delegate { SetObjectToSpawn(9); });
        objectSubMenuButtons[10].onClick.AddListener(delegate { SetObjectToSpawn(10); });
        objectSubMenuButtons[11].onClick.AddListener(delegate { SetObjectToSpawn(11); });
        objectSubMenuButtons[12].onClick.AddListener(delegate { SetObjectToSpawn(12); });
        objectSubMenuButtons[13].onClick.AddListener(delegate { SetObjectToSpawn(13); });
        objectSubMenuButtons[14].onClick.AddListener(delegate { SetObjectToSpawn(14); });
        objectSubMenuButtons[15].onClick.AddListener(delegate { SetObjectToSpawn(15); });
        objectSubMenuButtons[16].onClick.AddListener(delegate { SetObjectToSpawn(16); });
        objectSubMenuButtons[17].onClick.AddListener(delegate { SetObjectToSpawn(17); });
        objectSubMenuButtons[18].onClick.AddListener(delegate { SetObjectToSpawn(18); });
        objectSubMenuButtons[19].onClick.AddListener(delegate { SetObjectToSpawn(19); });
        objectSubMenuButtons[20].onClick.AddListener(delegate { SetObjectToSpawn(20); });
        objectSubMenuButtons[21].onClick.AddListener(delegate { SetObjectToSpawn(21); });
        objectSubMenuButtons[22].onClick.AddListener(delegate { SetObjectToSpawn(22); });
        objectSubMenuButtons[23].onClick.AddListener(delegate { SetObjectToSpawn(23); });
        objectSubMenuButtons[24].onClick.AddListener(delegate { SetObjectToSpawn(24); });
        objectSubMenuButtons[25].onClick.AddListener(delegate { SetObjectToSpawn(25); });
        objectSubMenuButtons[26].onClick.AddListener(delegate { SetObjectToSpawn(26); });
        objectSubMenuButtons[27].onClick.AddListener(delegate { SetObjectToSpawn(27); });
        objectSubMenuButtons[28].onClick.AddListener(delegate { SetObjectToSpawn(28); });
        objectSubMenuButtons[29].onClick.AddListener(delegate { SetObjectToSpawn(29); });
        objectSubMenuButtons[30].onClick.AddListener(delegate { SetObjectToSpawn(30); });
        objectSubMenuButtons[31].onClick.AddListener(delegate { SetObjectToSpawn(31); });
        objectSubMenuButtons[32].onClick.AddListener(delegate { SetObjectToSpawn(32); });
        objectSubMenuButtons[33].onClick.AddListener(delegate { SetObjectToSpawn(33); });
        objectSubMenuButtons[34].onClick.AddListener(delegate { SetObjectToSpawn(34); });
        objectSubMenuButtons[35].onClick.AddListener(delegate { SetObjectToSpawn(35); });
        objectSubMenuButtons[36].onClick.AddListener(delegate { SetObjectToSpawn(36); });
        objectSubMenuButtons[37].onClick.AddListener(delegate { SetObjectToSpawn(37); });
        objectSubMenuButtons[38].onClick.AddListener(delegate { SetObjectToSpawn(38); });
        objectSubMenuButtons[39].onClick.AddListener(delegate { SetObjectToSpawn(39); });
        objectSubMenuButtons[40].onClick.AddListener(delegate { SetObjectToSpawn(40); });
        objectSubMenuButtons[41].onClick.AddListener(delegate { SetObjectToSpawn(41); });
        objectSubMenuButtons[42].onClick.AddListener(delegate { SetObjectToSpawn(42); });
        objectSubMenuButtons[43].onClick.AddListener(delegate { SetObjectToSpawn(43); });
        objectSubMenuButtons[44].onClick.AddListener(delegate { SetObjectToSpawn(44); });
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

    void SetObjectButtonValue(int ndx, int value) {
        GameManager.S.spawner.objectsToSpawn[ndx] = value;

        //objectDropdowns[ndx].value = value;
    }



    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /*
    1) On obstacle button press, cache button index (0 to 4)
    2) Open ObstacleMenu where the user can select an obstacle
    3) On button press,
    */

    public int selectedObjectButtonNdx = 0;

    void OpenObjectSelectionSubMenu(int ndx) {
        // Cache button index
        selectedObjectButtonNdx = ndx;

        // Open object (obstacles & items) sub menu
        objectSelectionSubMenu.SetActive(true);
    }

    void SetObjectToSpawn(int value) {
        // Set object to spawn
        GameManager.S.spawner.objectsToSpawn[selectedObjectButtonNdx] = value;

        // Set button image
        objectButtonSpriteNdx[selectedObjectButtonNdx] = value;
        objectButtons[selectedObjectButtonNdx].GetComponent<Image>().sprite = objectSprites[value];

        // Save   !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!fhehae
        string tString = "Object Button Sprite Ndx " + selectedObjectButtonNdx.ToString();
        PlayerPrefs.SetInt(tString, objectButtonSpriteNdx[selectedObjectButtonNdx]);

        //string uString = "Object Button Sprite Ndx " + selectedObjectButtonNdx.ToString();
        //PlayerPrefs.SetInt(uString, objectButtonSpriteNdx[selectedObjectButtonNdx]);

        string uString = "Object Dropdown " + selectedObjectButtonNdx.ToString();
        PlayerPrefs.SetInt(uString, GameManager.S.spawner.objectsToSpawn[selectedObjectButtonNdx]);

        // Close object (obstacles & items) sub menu
        CloseObjectSelectionSubMenu();
    }

    void CloseObjectSelectionSubMenu() {
        objectSelectionSubMenu.SetActive(false);
    }

    void SetObjectButtonSprite(int buttonNdx, int spriteNdx) {
        objectButtonSpriteNdx[buttonNdx] = spriteNdx;

        objectButtons[buttonNdx].GetComponent<Image>().sprite = objectSprites[spriteNdx];
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

            SetObjectButtonValue(0, 2); // Horizontal block
            SetObjectButtonValue(1, 0); // Vertical low block
            SetObjectButtonValue(2, 17); // Vertical high block
            SetObjectButtonValue(3, 43); // Quid pickup
            SetObjectButtonValue(4, 44); // Shield pickup

            //objectButtonSpriteNdx[0] = 3;
            //objectButtonSpriteNdx[1] = 0;
            //objectButtonSpriteNdx[2] = 17;
            //objectButtonSpriteNdx[3] = 43;
            //objectButtonSpriteNdx[4] = 44;
            //objectButtons[0].GetComponent<Image>().sprite = objectSprites[3];
            //objectButtons[1].GetComponent<Image>().sprite = objectSprites[0];
            //objectButtons[2].GetComponent<Image>().sprite = objectSprites[17];
            //objectButtons[3].GetComponent<Image>().sprite = objectSprites[43];
            //objectButtons[4].GetComponent<Image>().sprite = objectSprites[44];
            SetObjectButtonSprite(0, 3);
            SetObjectButtonSprite(1, 0);
            SetObjectButtonSprite(2, 17);
            SetObjectButtonSprite(3, 43);
            SetObjectButtonSprite(4, 33);

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

        // Object dropdowns
        //for (int i = 0; i < objectDropdowns.Count; i++) {
        //    string tString = "Object Dropdown " + i.ToString();
        //    PlayerPrefs.SetInt(tString, objectDropdowns[i].value);
        //}

        //
        //for (int i = 0; i < GameManager.S.spawner.objectsToSpawn.Count; i++) {
        //    string tString = "Object Dropdown " + i.ToString();
        //    PlayerPrefs.SetInt(tString, GameManager.S.spawner.objectsToSpawn[i]); 
        //}
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