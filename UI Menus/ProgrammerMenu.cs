using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Allows users to adjust the random object instantiation algorithm
public class ProgrammerMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public List<TMPro.TMP_Dropdown> chanceDropdowns;
    public List<TMPro.TMP_Dropdown> objectDropdowns;

    public List<TMPro.TMP_Dropdown> speedDropdowns;
    public Button                   defaultSettingsButton;

    public Button                   previousPageButton;
    public Button                   nextPageButton;

    public List<GameObject>         textGO;

    public TMPro.TextMeshProUGUI    pageText;

    // Shake display text animator
    public Animator                 messageDisplayAnim;

    [Header("Set dynamically")]
    //
    public int                      currentPageNdx = 0;

    private void OnEnable() {
        // Display text
        if (Time.time > 0.01f) {
            SetGeneralDisplayTextMessage(currentPageNdx);
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
            SetObjectDropdownValue(0, PlayerPrefs.GetInt("Object Dropdown 0"));
        } else {
            objectDropdowns[0].value = 0; // Horizontal block
        }
        if (PlayerPrefs.HasKey("Object Dropdown 1")) {
            SetObjectDropdownValue(1, PlayerPrefs.GetInt("Object Dropdown 1"));
        } else {
            objectDropdowns[1].value = 1; // Vertical low block
        }
        if (PlayerPrefs.HasKey("Object Dropdown 2")) {
            SetObjectDropdownValue(2, PlayerPrefs.GetInt("Object Dropdown 2"));
        } else {
            objectDropdowns[2].value = 2; // Vertical high block
        }
        if (PlayerPrefs.HasKey("Object Dropdown 3")) {
            SetObjectDropdownValue(3, PlayerPrefs.GetInt("Object Dropdown 3"));
        } else {
            objectDropdowns[3].value = 3; // Quid pickup
        }
        if (PlayerPrefs.HasKey("Object Dropdown 4")) {
            SetObjectDropdownValue(4, PlayerPrefs.GetInt("Object Dropdown 4"));
        } else {
            objectDropdowns[4].value = 4; // Shield pickup
        }

        // Get speedDropdowns PlayerPrefs
        if (PlayerPrefs.HasKey("Speed Dropdown 0")) {
            SetStartingObjectSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 0"));
        } else {
            speedDropdowns[0].value = 4; // 5
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 1")) {
            SetAmountToIncreaseObjectSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 1"));
        } else {
            speedDropdowns[1].value = 1; // 0.1f
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 2")) {
            SetStartingSpawnSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 2"));
        } else {
            speedDropdowns[2].value = 19; // 2.0f
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 3")) {
            SetAmountToDecreaseSpawnSpeedDropdownValue(PlayerPrefs.GetInt("Speed Dropdown 3"));
        } else {
            speedDropdowns[3].value = 1; // 0.1f
        }

        // Add listener to dropdowns
        objectDropdowns[0].onValueChanged.AddListener(delegate { SetObjectDropdownValue(0, objectDropdowns[0].value); });
        objectDropdowns[1].onValueChanged.AddListener(delegate { SetObjectDropdownValue(1, objectDropdowns[1].value); });
        objectDropdowns[2].onValueChanged.AddListener(delegate { SetObjectDropdownValue(2, objectDropdowns[2].value); });
        objectDropdowns[3].onValueChanged.AddListener(delegate { SetObjectDropdownValue(3, objectDropdowns[3].value); });
        objectDropdowns[4].onValueChanged.AddListener(delegate { SetObjectDropdownValue(4, objectDropdowns[4].value); });

        chanceDropdowns[0].onValueChanged.AddListener(delegate { SetChanceDropdownValue(0, chanceDropdowns[0].value); });
        chanceDropdowns[1].onValueChanged.AddListener(delegate { SetChanceDropdownValue(1, chanceDropdowns[1].value); });
        chanceDropdowns[2].onValueChanged.AddListener(delegate { SetChanceDropdownValue(2, chanceDropdowns[2].value); });
        chanceDropdowns[3].onValueChanged.AddListener(delegate { SetChanceDropdownValue(3, chanceDropdowns[3].value); });
        chanceDropdowns[4].onValueChanged.AddListener(delegate { SetChanceDropdownValue(4, chanceDropdowns[4].value); });
        chanceDropdowns[5].onValueChanged.AddListener(delegate { SetChanceDropdownValue(5, chanceDropdowns[5].value); });
        chanceDropdowns[6].onValueChanged.AddListener(delegate { SetChanceDropdownValue(6, chanceDropdowns[6].value); });

        speedDropdowns[0].onValueChanged.AddListener(delegate { SetStartingObjectSpeedDropdownValue(speedDropdowns[0].value); });
        speedDropdowns[1].onValueChanged.AddListener(delegate { SetAmountToIncreaseObjectSpeedDropdownValue(speedDropdowns[1].value); });
        speedDropdowns[2].onValueChanged.AddListener(delegate { SetStartingSpawnSpeedDropdownValue(speedDropdowns[2].value); });
        speedDropdowns[3].onValueChanged.AddListener(delegate { SetAmountToDecreaseSpawnSpeedDropdownValue(speedDropdowns[3].value); });

        // Add listeners to button
        defaultSettingsButton.onClick.AddListener(delegate { AddDefaultSettingsConfirmationListeners(); });
        previousPageButton.onClick.AddListener(delegate { GoToPreviousOrNextPage(-1); });
        nextPageButton.onClick.AddListener(delegate { GoToPreviousOrNextPage(1); });
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    void SetChanceDropdownValue(int ndx, int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.chancesToSpawn[ndx] = valueAsFloat / 20;

        chanceDropdowns[ndx].value = value;

        // If dropdown values total == 100%, reset colors
        if(ndx == 0 || ndx == 1 || ndx == 4) {
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

    void SetObjectDropdownValue(int ndx, int value) {
        GameManager.S.spawner.objectsToSpawn[ndx] = value;

        objectDropdowns[ndx].value = value;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //
    void SetStartingObjectSpeedDropdownValue(int value) {
        GameManager.S.spawner.startingObjectSpeed = value + 1;

        speedDropdowns[0].value = value;
    }

    void SetAmountToIncreaseObjectSpeedDropdownValue(int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.amountToIncreaseObjectSpeed = (valueAsFloat / 10);

        speedDropdowns[1].value = value;
    }

    void SetStartingSpawnSpeedDropdownValue(int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.startingSpawnSpeed = (valueAsFloat / 10) + 0.1f;

        speedDropdowns[2].value = value;
    }

    void SetAmountToDecreaseSpawnSpeedDropdownValue(int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.amountToDecreaseSpawnSpeed = (valueAsFloat / 10);

        speedDropdowns[3].value = value;
    }

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

            SetObjectDropdownValue(0, 0); // Horizontal block
            SetObjectDropdownValue(1, 1); // Vertical low block
            SetObjectDropdownValue(2, 2); // Vertical high block
            SetObjectDropdownValue(3, 3); // Quid pickup
            SetObjectDropdownValue(4, 4); // Shield pickup

            SetStartingObjectSpeedDropdownValue(4); // 5
            SetAmountToIncreaseObjectSpeedDropdownValue(1); // 0.1f
            SetStartingSpawnSpeedDropdownValue(19); // 2.0f
            SetAmountToDecreaseSpawnSpeedDropdownValue(1); // 0.1f

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Options set to their default values!");
        } else {
            // Display text
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the programmer menu!");
        }
    }

    // Displays either the previous or next page of instructions
    public void GoToPreviousOrNextPage(int amountToChange) {
        //
        if (CheckAllDropdownsForValidValues()) {
            //
            currentPageNdx += amountToChange;

            // Reset
            if (currentPageNdx > 1) {
                currentPageNdx = 0;
            } else if (currentPageNdx < 0) {
                currentPageNdx = 1;
            }

            // Deactivate all text pages
            GameManager.utilities.SetActiveList(textGO, false);

            // Activate current page text
            textGO[currentPageNdx].SetActive(true);

            // Set page text
            pageText.text = "Page: " + "<color=white>" + (currentPageNdx + 1).ToString() + "/2" + "</color>";

            // Display text
            SetGeneralDisplayTextMessage(currentPageNdx);
        } else {
            // Audio: Damage
            GameManager.audioMan.PlayRandomDamageSFX();
        }
    }

    //
    public bool CheckAllDropdownsForValidValues() {
        List<bool> isTrue = new List<bool>();

        List<TMPro.TMP_Dropdown> a = new List<TMPro.TMP_Dropdown> () { chanceDropdowns[0], chanceDropdowns[1], chanceDropdowns[4] };
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

    //
    void SavePlayerPrefs() {
        // Chance dropdowns
        for (int i = 0; i < chanceDropdowns.Count; i++) {
            string tString = "Chance Dropdown " + i.ToString();
            PlayerPrefs.SetInt(tString, chanceDropdowns[i].value);
        }

        // Object dropdowns
        for (int i = 0; i < objectDropdowns.Count; i++) {
            string tString = "Object Dropdown " + i.ToString();
            PlayerPrefs.SetInt(tString, objectDropdowns[i].value);
        }

        // Speed dropdowns
        for (int i = 0; i < speedDropdowns.Count; i++) {
            string tString = "Speed Dropdown " + i.ToString();
            PlayerPrefs.SetInt(tString, speedDropdowns[i].value);
        }
    }

    // Sets the DisplayText's message depending on which page of the menu is visible
    void SetGeneralDisplayTextMessage(int currentPageNdx = 0) {
        if (currentPageNdx == 0) {
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the programmer menu:\nView and adjust the flow chart of the game's random\nobject spawner algorithm; basically the likelihood of\nwhat objects will be generated.");
        } else {
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Welcome to the programmer menu:\nAdjust gameplay settings such object speed\nand how often objects are spawned.");
        }
    }
}