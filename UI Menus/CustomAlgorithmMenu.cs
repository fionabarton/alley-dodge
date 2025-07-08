using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

// Allows the user to load and save customized game algorithms (speeds, chances to spawn, & objects to spawn)
public class CustomAlgorithmMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public List<Button> entryButtons;
    public List<TextMeshProUGUI> entryButtonNameTexts;
    public List<TextMeshProUGUI> entryButtonDateTexts;
    public Text selectAnAlgorithmText;

    public Button goBackButton;
    public Button resetButton;
    public CryptographyManager crypt;

    // Preview the of selected custom algorithm
    public List<TextMeshProUGUI> chanceToSpawnTexts;
    public List<Image> objectsToSpawnImages;
    public List<TextMeshProUGUI> speedTexts;

    [Header("Set Dynamically")]
    public CustomAlgorithm[] customAlgorithms;
    public CustomAlgorithmSaveData data;
    private string persistentPath = "";

    private int selectedButtonNdx;

    private void Awake() {
        // Create save data
        data = new CustomAlgorithmSaveData();

        // Set path
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "CustomAlgorithmSaveData.json";
    }

    void Start() {
        // Add listeners
        goBackButton.onClick.AddListener(delegate { GoBackButton(); });

        // Initialize array
        customAlgorithms = new CustomAlgorithm[8];
        SetToDefaultSettings();

        gameObject.SetActive(false);
    }

    public void UpdateGUI() {
        for (int i = 0; i < customAlgorithms.Length; i++) {
            entryButtonNameTexts[i].text = customAlgorithms[i].name;
            entryButtonDateTexts[i].text = customAlgorithms[i].date;
        }
    }

    void GoBackButton() {
        gameObject.SetActive(false);
    }

    public void ActivateMenu(string actionToBePerformed) {
        if (GameManager.S.algorithmMenuCS.CheckAllButtonsForValidValues()) {
            if (actionToBePerformed == "Load") {
                AddLoadAlgorithmListeners();

                // Set title text
                selectAnAlgorithmText.text = "Please select a slot of custom game options to load!";
            } else if (actionToBePerformed == "Save") {
                AddSaveAlgorithmListeners();

                // Set title text
                selectAnAlgorithmText.text = "Please select a slot to save your custom game options!";
            }

            LoadAll();

            UpdateGUI();

            gameObject.SetActive(true);
        } else {
            // Audio: Damage
            GameManager.audioMan.PlayRandomDamageSFX();
        }
    }

    public void AddLoadAlgorithmListeners() {
        RemoveAllListeners();

        for (int i = 0; i < customAlgorithms.Length; i++) {
            int copy = i;
            entryButtons[copy].onClick.AddListener(delegate { AddLoadAlgorithmConfirmationListener(copy); });
        }
    }

    public void AddSaveAlgorithmListeners() {
        RemoveAllListeners();

        for (int i = 0; i < customAlgorithms.Length; i++) {
            int copy = i;
            entryButtons[copy].onClick.AddListener(delegate { AddSaveAlgorithmConfirmationListener(copy); });
        }
    }

    void RemoveAllListeners() {
        // Remove listeners from entry buttons
        for (int i = 0; i < entryButtons.Count; i++) {
            int copy = i;
            entryButtons[copy].onClick.RemoveAllListeners();
        }
    }

    void AddLoadAlgorithmConfirmationListener(int ndx) {
        selectedButtonNdx = ndx;

        // Activate sub menu
        GameManager.S.subMenuCS.AddListeners(LoadAlgorithm, "Would you like to\nload these custom game options?");
    }
    public void LoadAlgorithm(int yesOrNo = -1) {
        // Deactivate sub menu
        GameManager.S.subMenuGO.SetActive(false);

        if (yesOrNo == 0) {
            // Reset all object shadow images
            GameManager.S.algorithmMenuCS.ActivateAllObjectShadowImages();

            GameManager.S.algorithmMenuCS.SetStartingObjectSpeedDropdownValue(customAlgorithms[selectedButtonNdx].startingObjectSpeed);
            GameManager.S.algorithmMenuCS.SetAmountToIncreaseObjectSpeedDropdownValue(customAlgorithms[selectedButtonNdx].amountToIncreaseObjectSpeed);
            GameManager.S.algorithmMenuCS.SetStartingSpawnSpeedDropdownValue(customAlgorithms[selectedButtonNdx].startingSpawnSpeed);
            GameManager.S.algorithmMenuCS.SetAmountToDecreaseSpawnSpeedDropdownValue(customAlgorithms[selectedButtonNdx].amountToDecreaseSpawnSpeed);

            GameManager.S.algorithmMenuCS.SetChanceButtonValue(0, customAlgorithms[selectedButtonNdx].chanceToSpawn0);
            GameManager.S.algorithmMenuCS.SetChanceButtonValue(1, customAlgorithms[selectedButtonNdx].chanceToSpawn1);
            GameManager.S.algorithmMenuCS.SetChanceButtonValue(2, customAlgorithms[selectedButtonNdx].chanceToSpawn2);
            GameManager.S.algorithmMenuCS.SetChanceButtonValue(3, customAlgorithms[selectedButtonNdx].chanceToSpawn3);
            GameManager.S.algorithmMenuCS.SetChanceButtonValue(4, customAlgorithms[selectedButtonNdx].chanceToSpawn4);
            GameManager.S.algorithmMenuCS.SetChanceButtonValue(5, customAlgorithms[selectedButtonNdx].chanceToSpawn5);
            GameManager.S.algorithmMenuCS.SetChanceButtonValue(6, customAlgorithms[selectedButtonNdx].chanceToSpawn6);
            GameManager.S.algorithmMenuCS.SetChanceButtonValue(7, customAlgorithms[selectedButtonNdx].chanceToSpawn7);
            GameManager.S.algorithmMenuCS.SetChanceButtonValue(8, customAlgorithms[selectedButtonNdx].chanceToSpawn8);
            GameManager.S.algorithmMenuCS.SetChanceButtonValue(9, customAlgorithms[selectedButtonNdx].chanceToSpawn9);

            GameManager.S.algorithmMenuCS.SetObjectToSpawnLOADCUSTOM(customAlgorithms[selectedButtonNdx].objectToSpawn0, 0);
            GameManager.S.algorithmMenuCS.SetObjectToSpawnLOADCUSTOM(customAlgorithms[selectedButtonNdx].objectToSpawn1, 1);
            GameManager.S.algorithmMenuCS.SetObjectToSpawnLOADCUSTOM(customAlgorithms[selectedButtonNdx].objectToSpawn2, 2);
            GameManager.S.algorithmMenuCS.SetObjectToSpawnLOADCUSTOM(customAlgorithms[selectedButtonNdx].objectToSpawn3, 3);
            GameManager.S.algorithmMenuCS.SetObjectToSpawnLOADCUSTOM(customAlgorithms[selectedButtonNdx].objectToSpawn4, 4);
            GameManager.S.algorithmMenuCS.SetObjectToSpawnLOADCUSTOM(customAlgorithms[selectedButtonNdx].objectToSpawn5, 5);
            GameManager.S.algorithmMenuCS.SetObjectToSpawnLOADCUSTOM(customAlgorithms[selectedButtonNdx].objectToSpawn6, 6);
            GameManager.S.algorithmMenuCS.SetObjectToSpawnLOADCUSTOM(customAlgorithms[selectedButtonNdx].objectToSpawn7, 7);
            GameManager.S.algorithmMenuCS.SetObjectToSpawnLOADCUSTOM(customAlgorithms[selectedButtonNdx].objectToSpawn8, 8);
            GameManager.S.algorithmMenuCS.SetObjectToSpawnLOADCUSTOM(customAlgorithms[selectedButtonNdx].objectToSpawn9, 9);

            UpdateGUI();

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Selected game options loaded!", true);

            gameObject.SetActive(false);
        }
    }

    void AddSaveAlgorithmConfirmationListener(int ndx) {
        selectedButtonNdx = ndx;

        // Activate keyboard menu
        GameManager.S.keyboardMenuCS.Activate("NameCustomAlgorithmEntry");
    }
    public void SaveAlgorithm(int yesOrNo = -1) {
        // Deactivate sub menu
        GameManager.S.subMenuGO.SetActive(false);

        // Deactivate keyboard input menu
        GameManager.S.keyboardMenuGO.SetActive(false);

        if (yesOrNo == 0) {
            customAlgorithms[selectedButtonNdx].name = GameManager.S.keyboardMenuCS.inputString;
            customAlgorithms[selectedButtonNdx].date = System.DateTime.UtcNow.ToString("dd MMMM, yyyy");

            customAlgorithms[selectedButtonNdx].startingObjectSpeed = PlayerPrefs.GetInt("Speed Dropdown 0");
            customAlgorithms[selectedButtonNdx].amountToIncreaseObjectSpeed = PlayerPrefs.GetInt("Speed Dropdown 1");
            customAlgorithms[selectedButtonNdx].startingSpawnSpeed = PlayerPrefs.GetInt("Speed Dropdown 2");
            customAlgorithms[selectedButtonNdx].amountToDecreaseSpawnSpeed = PlayerPrefs.GetInt("Speed Dropdown 3");

            customAlgorithms[selectedButtonNdx].chanceToSpawn0 = PlayerPrefs.GetInt("Chance Value 0");
            customAlgorithms[selectedButtonNdx].chanceToSpawn1 = PlayerPrefs.GetInt("Chance Value 1");
            customAlgorithms[selectedButtonNdx].chanceToSpawn2 = PlayerPrefs.GetInt("Chance Value 2");
            customAlgorithms[selectedButtonNdx].chanceToSpawn3 = PlayerPrefs.GetInt("Chance Value 3");
            customAlgorithms[selectedButtonNdx].chanceToSpawn4 = PlayerPrefs.GetInt("Chance Value 4");
            customAlgorithms[selectedButtonNdx].chanceToSpawn5 = PlayerPrefs.GetInt("Chance Value 5");
            customAlgorithms[selectedButtonNdx].chanceToSpawn6 = PlayerPrefs.GetInt("Chance Value 6");
            customAlgorithms[selectedButtonNdx].chanceToSpawn7 = PlayerPrefs.GetInt("Chance Value 7");
            customAlgorithms[selectedButtonNdx].chanceToSpawn8 = PlayerPrefs.GetInt("Chance Value 8");
            customAlgorithms[selectedButtonNdx].chanceToSpawn9 = PlayerPrefs.GetInt("Chance Value 9");

            customAlgorithms[selectedButtonNdx].objectToSpawn0 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[0];
            customAlgorithms[selectedButtonNdx].objectToSpawn1 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[1];
            customAlgorithms[selectedButtonNdx].objectToSpawn2 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[2];
            customAlgorithms[selectedButtonNdx].objectToSpawn3 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[3];
            customAlgorithms[selectedButtonNdx].objectToSpawn4 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[4];
            customAlgorithms[selectedButtonNdx].objectToSpawn5 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[5];
            customAlgorithms[selectedButtonNdx].objectToSpawn6 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[6];
            customAlgorithms[selectedButtonNdx].objectToSpawn7 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[7];
            customAlgorithms[selectedButtonNdx].objectToSpawn8 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[8];
            customAlgorithms[selectedButtonNdx].objectToSpawn9 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[9];

            SaveAll();

            UpdateGUI();

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Your custom game options, " + customAlgorithms[selectedButtonNdx].name + ",\n have been saved to slot " + (selectedButtonNdx + 1).ToString() + "!", true);

            gameObject.SetActive(false);
        } else {
            // Display text
            GameManager.S.keyboardMenuCS.messageDisplay.DisplayText("Oh, okay. That's cool.\nSo what's the name?");
        }
    }

    public void SaveAll() {
        // Cache data displayed in game
        for (int i = 0; i < customAlgorithms.Length; i++) {
            data.names[i] = customAlgorithms[i].name;
            data.dates[i] = customAlgorithms[i].date;

            data.startingObjectSpeed[i] = customAlgorithms[i].startingObjectSpeed;
            data.amountToIncreaseObjectSpeed[i] = customAlgorithms[i].amountToIncreaseObjectSpeed;
            data.startingSpawnSpeed[i] = customAlgorithms[i].startingSpawnSpeed;
            data.amountToDecreaseSpawnSpeed[i] = customAlgorithms[i].amountToDecreaseSpawnSpeed;

            data.chanceToSpawn0[i] = customAlgorithms[i].chanceToSpawn0;
            data.chanceToSpawn1[i] = customAlgorithms[i].chanceToSpawn1;
            data.chanceToSpawn2[i] = customAlgorithms[i].chanceToSpawn2;
            data.chanceToSpawn3[i] = customAlgorithms[i].chanceToSpawn3;
            data.chanceToSpawn4[i] = customAlgorithms[i].chanceToSpawn4;
            data.chanceToSpawn5[i] = customAlgorithms[i].chanceToSpawn5;
            data.chanceToSpawn6[i] = customAlgorithms[i].chanceToSpawn6;
            data.chanceToSpawn7[i] = customAlgorithms[i].chanceToSpawn7;
            data.chanceToSpawn8[i] = customAlgorithms[i].chanceToSpawn8;
            data.chanceToSpawn9[i] = customAlgorithms[i].chanceToSpawn9;

            data.objectToSpawn0[i] = customAlgorithms[i].objectToSpawn0;
            data.objectToSpawn1[i] = customAlgorithms[i].objectToSpawn1;
            data.objectToSpawn2[i] = customAlgorithms[i].objectToSpawn2;
            data.objectToSpawn3[i] = customAlgorithms[i].objectToSpawn3;
            data.objectToSpawn4[i] = customAlgorithms[i].objectToSpawn4;
            data.objectToSpawn5[i] = customAlgorithms[i].objectToSpawn5;
            data.objectToSpawn6[i] = customAlgorithms[i].objectToSpawn6;
            data.objectToSpawn7[i] = customAlgorithms[i].objectToSpawn7;
            data.objectToSpawn8[i] = customAlgorithms[i].objectToSpawn8;
            data.objectToSpawn9[i] = customAlgorithms[i].objectToSpawn9;
        }

        // Get file path
        string savePath = persistentPath;

        // Get data in JSON format
        string json = JsonUtility.ToJson(data);

        // Encrypt data
        json = crypt.Encrypt(json);

        // Write data to file
        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
        writer.Flush();
        writer.Close();
    }

    public void LoadAll() {
        // If save data file exists
        if (File.Exists(persistentPath)) {
            // Read data from file
            using StreamReader reader = new StreamReader(persistentPath);
            string json = reader.ReadToEnd();

            // Decrypt data string
            json = crypt.Decrypt(json);

            // Create SaveData object from its JSON representation
            CustomAlgorithmSaveData data = JsonUtility.FromJson<CustomAlgorithmSaveData>(json);

            // Display custom algorithm data in game UI
            for (int i = 0; i < customAlgorithms.Length; i++) {
                customAlgorithms[i].name = data.names[i];
                customAlgorithms[i].date = data.dates[i];

                customAlgorithms[i].startingObjectSpeed = data.startingObjectSpeed[i];
                customAlgorithms[i].amountToIncreaseObjectSpeed = data.amountToIncreaseObjectSpeed[i];
                customAlgorithms[i].startingSpawnSpeed = data.startingSpawnSpeed[i];
                customAlgorithms[i].amountToDecreaseSpawnSpeed = data.amountToDecreaseSpawnSpeed[i];

                customAlgorithms[i].chanceToSpawn0 = data.chanceToSpawn0[i];
                customAlgorithms[i].chanceToSpawn1 = data.chanceToSpawn1[i];
                customAlgorithms[i].chanceToSpawn2 = data.chanceToSpawn2[i];
                customAlgorithms[i].chanceToSpawn3 = data.chanceToSpawn3[i];
                customAlgorithms[i].chanceToSpawn4 = data.chanceToSpawn4[i];
                customAlgorithms[i].chanceToSpawn5 = data.chanceToSpawn5[i];
                customAlgorithms[i].chanceToSpawn6 = data.chanceToSpawn6[i];
                customAlgorithms[i].chanceToSpawn7 = data.chanceToSpawn7[i];
                customAlgorithms[i].chanceToSpawn8 = data.chanceToSpawn8[i];
                customAlgorithms[i].chanceToSpawn9 = data.chanceToSpawn9[i];

                customAlgorithms[i].objectToSpawn0 = data.objectToSpawn0[i];
                customAlgorithms[i].objectToSpawn1 = data.objectToSpawn1[i];
                customAlgorithms[i].objectToSpawn2 = data.objectToSpawn2[i];
                customAlgorithms[i].objectToSpawn3 = data.objectToSpawn3[i];
                customAlgorithms[i].objectToSpawn4 = data.objectToSpawn4[i];
                customAlgorithms[i].objectToSpawn5 = data.objectToSpawn5[i];
                customAlgorithms[i].objectToSpawn6 = data.objectToSpawn6[i];
                customAlgorithms[i].objectToSpawn7 = data.objectToSpawn7[i];
                customAlgorithms[i].objectToSpawn8 = data.objectToSpawn8[i];
                customAlgorithms[i].objectToSpawn9 = data.objectToSpawn9[i];
            }
        } else {
            // Display default custom algorithm presets in game UI
            SetToDefaultSettings();
        }
    }

    // Fun fictional historical dates
    // 2/26/1984: Venom Snake awakens from 9 year coma
    // 11/26/1985: Marty McFly accidentally travels back to 1955
    // 11/29/86: Lan Di murders Iwao Hazuki
    // 1/12/1992: HAL 9000 becomes operational
    // 8/29/1997: Judgement Day: Skynet becomes self-aware and launches nuclear missiles at Russia
    // 12/31/1999: Philip J.Fry accidentally cryogenically freezes himself
    // 1/8/2016: Birth of Roy Batty
    // 7/13/2035: Birth of Jean-Luc Picard
    // 4/5/2063: Zefram Cochrane makes first human warp flight
    public void SetToDefaultSettings() {
        //customAlgorithms[0] = new CustomAlgorithm("Normal Game Mode");
        //customAlgorithms[1] = new CustomAlgorithm("Random Objects", "1 January, 2025", 2, 1, 9, 3,
        //15, 4, 1, 0, 0, 0, 0, 0, 0, 0,
        //46, 47, 48, 50, 50, 50, 50, 50, 50, 50);
        customAlgorithms[0] = new CustomAlgorithm("Zigzag", "29 August, 1997", 4, 1, 14, 2,
        4, 4, 4, 2, 4, 2, 0, 0, 0, 0,
        7, 23, 24, 25, 47, 48, 50, 50, 50, 50);
        customAlgorithms[1] = new CustomAlgorithm("Intersecting Lines", "31 December, 1999", 1, 1, 14, 5,
        5, 5, 5, 4, 1, 0, 0, 0, 0, 0,
        17, 25, 8, 47, 48, 50, 50, 50, 50, 50);
        customAlgorithms[2] = new CustomAlgorithm("Climb & Crouch", "5 April, 2063", 2, 1, 6, 2,
        7, 7, 4, 2, 0, 0, 0, 0, 0, 0,
        20, 21, 47, 48, 50, 50, 50, 50, 50, 50);
        //customAlgorithms[5] = new("In The Middle", "5 April, 2063", 4, 1, 14, 2,
        //4, 4, 4, 4, 4, 0, 0, 0, 0, 0,
        //36, 39, 44, 45, 49, 50, 50, 50, 50, 50); 

        customAlgorithms[3] = new CustomAlgorithm("Slot 4: EMPTY");
        customAlgorithms[4] = new CustomAlgorithm("Slot 5: EMPTY");
        customAlgorithms[5] = new CustomAlgorithm("Slot 6: EMPTY");
        customAlgorithms[6] = new CustomAlgorithm("Slot 7: EMPTY");
        customAlgorithms[7] = new CustomAlgorithm("Slot 8: EMPTY");
    }

    // Displays the properties and values of this entry's custom game algorithm
    // Called on highlight in OnHighlightDisplayCustomAlgorithm.cs
    public void DisplaySelectedCustomAlgorithm(int buttonNdx) {
        chanceToSpawnTexts[0].text = (customAlgorithms[buttonNdx].chanceToSpawn0 * 5f).ToString() + "%";
        chanceToSpawnTexts[1].text = (customAlgorithms[buttonNdx].chanceToSpawn1 * 5f).ToString() + "%";
        chanceToSpawnTexts[2].text = (customAlgorithms[buttonNdx].chanceToSpawn2 * 5f).ToString() + "%";
        chanceToSpawnTexts[3].text = (customAlgorithms[buttonNdx].chanceToSpawn3 * 5f).ToString() + "%";
        chanceToSpawnTexts[4].text = (customAlgorithms[buttonNdx].chanceToSpawn4 * 5f).ToString() + "%";
        chanceToSpawnTexts[5].text = (customAlgorithms[buttonNdx].chanceToSpawn5 * 5f).ToString() + "%";
        chanceToSpawnTexts[6].text = (customAlgorithms[buttonNdx].chanceToSpawn6 * 5f).ToString() + "%";
        chanceToSpawnTexts[7].text = (customAlgorithms[buttonNdx].chanceToSpawn7 * 5f).ToString() + "%";
        chanceToSpawnTexts[8].text = (customAlgorithms[buttonNdx].chanceToSpawn8 * 5f).ToString() + "%";
        chanceToSpawnTexts[9].text = (customAlgorithms[buttonNdx].chanceToSpawn9 * 5f).ToString() + "%";

        objectsToSpawnImages[0].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn0];
        objectsToSpawnImages[1].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn1];
        objectsToSpawnImages[2].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn2];
        objectsToSpawnImages[3].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn3];
        objectsToSpawnImages[4].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn4];
        objectsToSpawnImages[5].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn5];
        objectsToSpawnImages[6].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn6];
        objectsToSpawnImages[7].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn7];
        objectsToSpawnImages[8].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn8];
        objectsToSpawnImages[9].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn9];

        speedTexts[0].text = "Starting object speed (MPH):<color=#D9D9D9> " + GameManager.S.objectSpeedDisplayedValues[customAlgorithms[buttonNdx].startingObjectSpeed].ToString();
        speedTexts[1].text = "Amount to increase per level:<color=#D9D9D9> " + GameManager.S.amountToIncreaseDisplayedValues[customAlgorithms[buttonNdx].amountToIncreaseObjectSpeed].ToString();
        speedTexts[2].text = "Starting spawn speed (OPM):<color=#D9D9D9> " + GameManager.S.spawnSpeedDisplayedValues[customAlgorithms[buttonNdx].startingSpawnSpeed].ToString();
        speedTexts[3].text = "Amount to increase per level:<color=#D9D9D9> " + GameManager.S.amountToDecreaseDisplayedValues[customAlgorithms[buttonNdx].amountToDecreaseSpawnSpeed].ToString();
    }
}

public class CustomAlgorithmSaveData {
    public string[] names = new string[8];
    public string[] dates = new string[8];
    public string[] times = new string[8];

    public int[] startingObjectSpeed = new int[8];
    public int[] amountToIncreaseObjectSpeed = new int[8];
    public int[] startingSpawnSpeed = new int[8];
    public int[] amountToDecreaseSpawnSpeed = new int[8];

    public int[] chanceToSpawn0 = new int[8];
    public int[] chanceToSpawn1 = new int[8];
    public int[] chanceToSpawn2 = new int[8];
    public int[] chanceToSpawn3 = new int[8];
    public int[] chanceToSpawn4 = new int[8];
    public int[] chanceToSpawn5 = new int[8];
    public int[] chanceToSpawn6 = new int[8];
    public int[] chanceToSpawn7 = new int[8];
    public int[] chanceToSpawn8 = new int[8];
    public int[] chanceToSpawn9 = new int[8];

    public int[] objectToSpawn0 = new int[8];
    public int[] objectToSpawn1 = new int[8];
    public int[] objectToSpawn2 = new int[8];
    public int[] objectToSpawn3 = new int[8];
    public int[] objectToSpawn4 = new int[8];
    public int[] objectToSpawn5 = new int[8];
    public int[] objectToSpawn6 = new int[8];
    public int[] objectToSpawn7 = new int[8];
    public int[] objectToSpawn8 = new int[8];
    public int[] objectToSpawn9 = new int[8];
}

//
public class CustomAlgorithm {
    public string name;
    public string date;

    public int startingObjectSpeed;
    public int amountToIncreaseObjectSpeed;
    public int startingSpawnSpeed;
    public int amountToDecreaseSpawnSpeed;

    public int chanceToSpawn0;
    public int chanceToSpawn1;
    public int chanceToSpawn2;
    public int chanceToSpawn3;
    public int chanceToSpawn4;
    public int chanceToSpawn5;
    public int chanceToSpawn6;
    public int chanceToSpawn7;
    public int chanceToSpawn8;
    public int chanceToSpawn9;

    public int objectToSpawn0;
    public int objectToSpawn1;
    public int objectToSpawn2;
    public int objectToSpawn3;
    public int objectToSpawn4;
    public int objectToSpawn5;
    public int objectToSpawn6;
    public int objectToSpawn7;
    public int objectToSpawn8;
    public int objectToSpawn9;

    //customAlgorithms[0] = new CustomAlgorithm("Normal Game Mode");
    //customAlgorithms[1] = new CustomAlgorithm(
    //"Random Objects", "1 January, 2025",
    //2, 1, 9, 3,
    //15, 4, 1, 0, 0, 0, 0, 0, 0, 0,
    //46, 47, 48, 50, 50, 50, 50, 50, 50, 50);

    public CustomAlgorithm(string _name = "", string _date = "",
        int _startingObjectSpeed = 2, int _amountToIncreaseObjectSpeed = 1, int _startingSpawnSpeed = 9, int _amountToDecreaseSpawnSpeed = 3,
        int _chanceToSpawn0 = 4, int _chanceToSpawn1 = 4, int _chanceToSpawn2 = 4, int _chanceToSpawn3 = 6, int _chanceToSpawn4 = 2, int _chanceToSpawn5 = 0, int _chanceToSpawn6 = 0, int _chanceToSpawn7 = 0, int _chanceToSpawn8 = 0, int _chanceToSpawn9 = 0,
        int _objectToSpawn0 = 7, int _objectToSpawn1 = 0, int _objectToSpawn2 = 20, int _objectToSpawn3 = 47, int _objectToSpawn4 = 48, int _objectToSpawn5 = 50, int _objectToSpawn6 = 50, int _objectToSpawn7 = 50, int _objectToSpawn8 = 50, int _objectToSpawn9 = 50) {
        name = _name;
        date = _date;

        startingObjectSpeed = _startingObjectSpeed;
        amountToIncreaseObjectSpeed = _amountToIncreaseObjectSpeed;
        startingSpawnSpeed = _startingSpawnSpeed;
        amountToDecreaseSpawnSpeed = _amountToDecreaseSpawnSpeed;

        chanceToSpawn0 = _chanceToSpawn0;
        chanceToSpawn1 = _chanceToSpawn1;
        chanceToSpawn2 = _chanceToSpawn2;
        chanceToSpawn3 = _chanceToSpawn3;
        chanceToSpawn4 = _chanceToSpawn4;
        chanceToSpawn5 = _chanceToSpawn5;
        chanceToSpawn6 = _chanceToSpawn6;
        chanceToSpawn7 = _chanceToSpawn7;
        chanceToSpawn8 = _chanceToSpawn8;
        chanceToSpawn9 = _chanceToSpawn9;

        objectToSpawn0 = _objectToSpawn0;
        objectToSpawn1 = _objectToSpawn1;
        objectToSpawn2 = _objectToSpawn2;
        objectToSpawn3 = _objectToSpawn3;
        objectToSpawn4 = _objectToSpawn4;
        objectToSpawn5 = _objectToSpawn5;
        objectToSpawn6 = _objectToSpawn6;
        objectToSpawn7 = _objectToSpawn7;
        objectToSpawn8 = _objectToSpawn8;
        objectToSpawn9 = _objectToSpawn9;
    }
}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using System.IO;

//// Allows the user to load and save customized game algorithms (speeds, chances to spawn, & objects to spawn)
//public class CustomAlgorithmMenu : MonoBehaviour {
//    [Header("Set in Inspector")]
//    public List<Button>             entryButtons;
//    public List<TextMeshProUGUI>    entryButtonNameTexts;
//    public List<TextMeshProUGUI>    entryButtonDateTexts;
//    public Text                     selectAnAlgorithmText;

//    public Button                   goBackButton;
//    public Button                   resetButton;
//    public CryptographyManager      crypt;

//    // Preview the of selected custom algorithm
//    public List<TextMeshProUGUI>    chanceToSpawnTexts;
//    public List<Image>              objectsToSpawnImages;
//    public List<TextMeshProUGUI>    speedTexts;

//    [Header("Set Dynamically")]
//    public CustomAlgorithm[]        customAlgorithms;
//    public CustomAlgorithmSaveData  data;
//    private string                  persistentPath = "";

//    private int                     selectedButtonNdx;

//    private void Awake() {
//        // Create save data
//        data = new CustomAlgorithmSaveData();

//        // Set path
//        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "CustomAlgorithmSaveData.json";
//    }

//    void Start() {
//        // Add listeners
//        goBackButton.onClick.AddListener(delegate { GoBackButton(); });

//        // Initialize array
//        customAlgorithms = new CustomAlgorithm[8];
//        SetToDefaultSettings();

//        gameObject.SetActive(false);
//    }

//    public void UpdateGUI() {
//        for(int i = 0; i < customAlgorithms.Length; i++) {
//            entryButtonNameTexts[i].text = customAlgorithms[i].name;
//            entryButtonDateTexts[i].text = customAlgorithms[i].date;
//        }
//    }

//    void GoBackButton() {
//        gameObject.SetActive(false);
//    }

//    public void ActivateMenu(string actionToBePerformed) {
//        if (GameManager.S.algorithmMenuCS.CheckAllButtonsForValidValues()) {
//            if (actionToBePerformed == "Load") {
//                AddLoadAlgorithmListeners();

//                // Set title text
//                selectAnAlgorithmText.text = "Please select a custom game mode to load!";
//            } else if (actionToBePerformed == "Save") {
//                AddSaveAlgorithmListeners();

//                // Set title text
//                selectAnAlgorithmText.text = "Please select a slot to save your custom game mode!";
//            }

//            LoadAll();

//            UpdateGUI();

//            gameObject.SetActive(true);
//        } else {
//            // Audio: Damage
//            GameManager.audioMan.PlayRandomDamageSFX();
//        }
//    }

//    public void AddLoadAlgorithmListeners() {
//        RemoveAllListeners();

//        for (int i = 0; i < customAlgorithms.Length; i++) {
//            int copy = i;
//            entryButtons[copy].onClick.AddListener(delegate { AddLoadAlgorithmConfirmationListener(copy); });
//        }
//    }

//    public void AddSaveAlgorithmListeners() {
//        RemoveAllListeners();

//        for (int i = 0; i < customAlgorithms.Length; i++) {
//            int copy = i;
//            entryButtons[copy].onClick.AddListener(delegate { AddSaveAlgorithmConfirmationListener(copy); });
//        }
//    }

//    void RemoveAllListeners() {
//        // Remove listeners from entry buttons
//        for (int i = 0; i < entryButtons.Count; i++) {
//            int copy = i;
//            entryButtons[copy].onClick.RemoveAllListeners();
//        }
//    }

//    void AddLoadAlgorithmConfirmationListener(int ndx) {
//        selectedButtonNdx = ndx;

//        // Activate sub menu
//        GameManager.S.subMenuCS.AddListeners(LoadAlgorithm, "Are you sure that you would like to\nload this custom game mode?");
//    }
//    public void LoadAlgorithm(int yesOrNo = -1) {
//        // Deactivate sub menu
//        GameManager.S.subMenuGO.SetActive(false);

//        if (yesOrNo == 0) {
//            GameManager.S.algorithmMenuCS.SetStartingObjectSpeedDropdownValue(customAlgorithms[selectedButtonNdx].startingObjectSpeed);
//            GameManager.S.algorithmMenuCS.SetAmountToIncreaseObjectSpeedDropdownValue(customAlgorithms[selectedButtonNdx].amountToIncreaseObjectSpeed);
//            GameManager.S.algorithmMenuCS.SetStartingSpawnSpeedDropdownValue(customAlgorithms[selectedButtonNdx].startingSpawnSpeed);
//            GameManager.S.algorithmMenuCS.SetAmountToDecreaseSpawnSpeedDropdownValue(customAlgorithms[selectedButtonNdx].amountToDecreaseSpawnSpeed);

//            GameManager.S.algorithmMenuCS.SetChanceButtonValue(0, customAlgorithms[selectedButtonNdx].chanceToSpawn0);
//            GameManager.S.algorithmMenuCS.SetChanceButtonValue(1, customAlgorithms[selectedButtonNdx].chanceToSpawn1);
//            GameManager.S.algorithmMenuCS.SetChanceButtonValue(2, customAlgorithms[selectedButtonNdx].chanceToSpawn2);
//            GameManager.S.algorithmMenuCS.SetChanceButtonValue(3, customAlgorithms[selectedButtonNdx].chanceToSpawn3);
//            GameManager.S.algorithmMenuCS.SetChanceButtonValue(4, customAlgorithms[selectedButtonNdx].chanceToSpawn4);
//            GameManager.S.algorithmMenuCS.SetChanceButtonValue(5, customAlgorithms[selectedButtonNdx].chanceToSpawn5);
//            GameManager.S.algorithmMenuCS.SetChanceButtonValue(6, customAlgorithms[selectedButtonNdx].chanceToSpawn6);
//            GameManager.S.algorithmMenuCS.SetChanceButtonValue(7, customAlgorithms[selectedButtonNdx].chanceToSpawn7);
//            GameManager.S.algorithmMenuCS.SetChanceButtonValue(8, customAlgorithms[selectedButtonNdx].chanceToSpawn8);
//            GameManager.S.algorithmMenuCS.SetChanceButtonValue(9, customAlgorithms[selectedButtonNdx].chanceToSpawn9);

//            GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[selectedButtonNdx].objectToSpawn0, 0);
//            GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[selectedButtonNdx].objectToSpawn1, 1);
//            GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[selectedButtonNdx].objectToSpawn2, 2);
//            GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[selectedButtonNdx].objectToSpawn3, 3);
//            GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[selectedButtonNdx].objectToSpawn4, 4);
//            GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[selectedButtonNdx].objectToSpawn5, 5);
//            GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[selectedButtonNdx].objectToSpawn6, 6);
//            GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[selectedButtonNdx].objectToSpawn7, 7);
//            GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[selectedButtonNdx].objectToSpawn8, 8);
//            GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[selectedButtonNdx].objectToSpawn9, 9);

//            UpdateGUI();

//            // Delayed text display
//            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Selected game mode loaded!", true);

//            gameObject.SetActive(false);
//        } 
//    }

//    void AddSaveAlgorithmConfirmationListener(int ndx) {
//        selectedButtonNdx = ndx;

//        // Activate keyboard menu
//        GameManager.S.keyboardMenuCS.Activate("NameCustomAlgorithmEntry");
//    }
//    public void SaveAlgorithm(int yesOrNo = -1) {
//        // Deactivate sub menu
//        GameManager.S.subMenuGO.SetActive(false);

//        // Deactivate keyboard input menu
//        GameManager.S.keyboardMenuGO.SetActive(false);

//        if (yesOrNo == 0) {
//            customAlgorithms[selectedButtonNdx].name = GameManager.S.keyboardMenuCS.inputString;
//            customAlgorithms[selectedButtonNdx].date = System.DateTime.UtcNow.ToString("dd MMMM, yyyy");

//            customAlgorithms[selectedButtonNdx].startingObjectSpeed = PlayerPrefs.GetInt("Speed Dropdown 0");
//            customAlgorithms[selectedButtonNdx].amountToIncreaseObjectSpeed = PlayerPrefs.GetInt("Speed Dropdown 1");
//            customAlgorithms[selectedButtonNdx].startingSpawnSpeed = PlayerPrefs.GetInt("Speed Dropdown 2");
//            customAlgorithms[selectedButtonNdx].amountToDecreaseSpawnSpeed = PlayerPrefs.GetInt("Speed Dropdown 3");

//            customAlgorithms[selectedButtonNdx].chanceToSpawn0 = PlayerPrefs.GetInt("Chance Value 0");
//            customAlgorithms[selectedButtonNdx].chanceToSpawn1 = PlayerPrefs.GetInt("Chance Value 1");
//            customAlgorithms[selectedButtonNdx].chanceToSpawn2 = PlayerPrefs.GetInt("Chance Value 2");
//            customAlgorithms[selectedButtonNdx].chanceToSpawn3 = PlayerPrefs.GetInt("Chance Value 3");
//            customAlgorithms[selectedButtonNdx].chanceToSpawn4 = PlayerPrefs.GetInt("Chance Value 4");
//            customAlgorithms[selectedButtonNdx].chanceToSpawn5 = PlayerPrefs.GetInt("Chance Value 5");
//            customAlgorithms[selectedButtonNdx].chanceToSpawn6 = PlayerPrefs.GetInt("Chance Value 6");
//            customAlgorithms[selectedButtonNdx].chanceToSpawn7 = PlayerPrefs.GetInt("Chance Value 7");
//            customAlgorithms[selectedButtonNdx].chanceToSpawn8 = PlayerPrefs.GetInt("Chance Value 8");
//            customAlgorithms[selectedButtonNdx].chanceToSpawn9 = PlayerPrefs.GetInt("Chance Value 9");

//            customAlgorithms[selectedButtonNdx].objectToSpawn0 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[0];
//            customAlgorithms[selectedButtonNdx].objectToSpawn1 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[1];
//            customAlgorithms[selectedButtonNdx].objectToSpawn2 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[2];
//            customAlgorithms[selectedButtonNdx].objectToSpawn3 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[3];
//            customAlgorithms[selectedButtonNdx].objectToSpawn4 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[4];
//            customAlgorithms[selectedButtonNdx].objectToSpawn5 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[5];
//            customAlgorithms[selectedButtonNdx].objectToSpawn6 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[6];
//            customAlgorithms[selectedButtonNdx].objectToSpawn7 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[7];
//            customAlgorithms[selectedButtonNdx].objectToSpawn8 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[8];
//            customAlgorithms[selectedButtonNdx].objectToSpawn9 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[9];

//            SaveAll();

//            UpdateGUI();

//            // Delayed text display
//            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Your custom game mode, " + customAlgorithms[selectedButtonNdx].name + ",\n saved to slot " + (selectedButtonNdx+1).ToString() + "!", true);

//            gameObject.SetActive(false);
//        } else {
//            // Display text
//            GameManager.S.keyboardMenuCS.messageDisplay.DisplayText("Oh, okay. That's cool.\nSo what's the name?");
//        }
//    }

//    public void SaveAll() {
//        // Cache data displayed in game
//        for (int i = 0; i < customAlgorithms.Length; i++) {
//            data.names[i] = customAlgorithms[i].name;
//            data.dates[i] = customAlgorithms[i].date;

//            data.startingObjectSpeed[i] = customAlgorithms[i].startingObjectSpeed;
//            data.amountToIncreaseObjectSpeed[i] = customAlgorithms[i].amountToIncreaseObjectSpeed;
//            data.startingSpawnSpeed[i] = customAlgorithms[i].startingSpawnSpeed;
//            data.amountToDecreaseSpawnSpeed[i] = customAlgorithms[i].amountToDecreaseSpawnSpeed;

//            data.chanceToSpawn0[i] = customAlgorithms[i].chanceToSpawn0;
//            data.chanceToSpawn1[i] = customAlgorithms[i].chanceToSpawn1;
//            data.chanceToSpawn2[i] = customAlgorithms[i].chanceToSpawn2;
//            data.chanceToSpawn3[i] = customAlgorithms[i].chanceToSpawn3;
//            data.chanceToSpawn4[i] = customAlgorithms[i].chanceToSpawn4;
//            data.chanceToSpawn5[i] = customAlgorithms[i].chanceToSpawn5;
//            data.chanceToSpawn6[i] = customAlgorithms[i].chanceToSpawn6;
//            data.chanceToSpawn7[i] = customAlgorithms[i].chanceToSpawn7;
//            data.chanceToSpawn8[i] = customAlgorithms[i].chanceToSpawn8;
//            data.chanceToSpawn9[i] = customAlgorithms[i].chanceToSpawn9;

//            data.objectToSpawn0[i] = customAlgorithms[i].objectToSpawn0;
//            data.objectToSpawn1[i] = customAlgorithms[i].objectToSpawn1;
//            data.objectToSpawn2[i] = customAlgorithms[i].objectToSpawn2;
//            data.objectToSpawn3[i] = customAlgorithms[i].objectToSpawn3;
//            data.objectToSpawn4[i] = customAlgorithms[i].objectToSpawn4;
//            data.objectToSpawn5[i] = customAlgorithms[i].objectToSpawn5;
//            data.objectToSpawn6[i] = customAlgorithms[i].objectToSpawn6;
//            data.objectToSpawn7[i] = customAlgorithms[i].objectToSpawn7;
//            data.objectToSpawn8[i] = customAlgorithms[i].objectToSpawn8;
//            data.objectToSpawn9[i] = customAlgorithms[i].objectToSpawn9;
//        }

//        // Get file path
//        string savePath = persistentPath;

//        // Get data in JSON format
//        string json = JsonUtility.ToJson(data);

//        // Encrypt data
//        json = crypt.Encrypt(json);

//        // Write data to file
//        using StreamWriter writer = new StreamWriter(savePath);
//        writer.Write(json);
//        writer.Flush();
//        writer.Close();
//    }

//    public void LoadAll() {
//        // If save data file exists
//        if (File.Exists(persistentPath)) {
//            // Read data from file
//            using StreamReader reader = new StreamReader(persistentPath);
//            string json = reader.ReadToEnd();

//            // Decrypt data string
//            json = crypt.Decrypt(json);

//            // Create SaveData object from its JSON representation
//            CustomAlgorithmSaveData data = JsonUtility.FromJson<CustomAlgorithmSaveData>(json);

//            // Display custom algorithm data in game UI
//            for (int i = 0; i < customAlgorithms.Length; i++) {
//                customAlgorithms[i].name = data.names[i];
//                customAlgorithms[i].date = data.dates[i];

//                customAlgorithms[i].startingObjectSpeed = data.startingObjectSpeed[i];
//                customAlgorithms[i].amountToIncreaseObjectSpeed = data.amountToIncreaseObjectSpeed[i];
//                customAlgorithms[i].startingSpawnSpeed = data.startingSpawnSpeed[i];
//                customAlgorithms[i].amountToDecreaseSpawnSpeed = data.amountToDecreaseSpawnSpeed[i];

//                customAlgorithms[i].chanceToSpawn0 = data.chanceToSpawn0[i];
//                customAlgorithms[i].chanceToSpawn1 = data.chanceToSpawn1[i];
//                customAlgorithms[i].chanceToSpawn2 = data.chanceToSpawn2[i];
//                customAlgorithms[i].chanceToSpawn3 = data.chanceToSpawn3[i];
//                customAlgorithms[i].chanceToSpawn4 = data.chanceToSpawn4[i];
//                customAlgorithms[i].chanceToSpawn5 = data.chanceToSpawn5[i];
//                customAlgorithms[i].chanceToSpawn6 = data.chanceToSpawn6[i];
//                customAlgorithms[i].chanceToSpawn7 = data.chanceToSpawn7[i];
//                customAlgorithms[i].chanceToSpawn8 = data.chanceToSpawn8[i];
//                customAlgorithms[i].chanceToSpawn9 = data.chanceToSpawn9[i];

//                customAlgorithms[i].objectToSpawn0 = data.objectToSpawn0[i];
//                customAlgorithms[i].objectToSpawn1 = data.objectToSpawn1[i];
//                customAlgorithms[i].objectToSpawn2 = data.objectToSpawn2[i];
//                customAlgorithms[i].objectToSpawn3 = data.objectToSpawn3[i];
//                customAlgorithms[i].objectToSpawn4 = data.objectToSpawn4[i];
//                customAlgorithms[i].objectToSpawn5 = data.objectToSpawn5[i];
//                customAlgorithms[i].objectToSpawn6 = data.objectToSpawn6[i];
//                customAlgorithms[i].objectToSpawn7 = data.objectToSpawn7[i];
//                customAlgorithms[i].objectToSpawn8 = data.objectToSpawn8[i];
//                customAlgorithms[i].objectToSpawn9 = data.objectToSpawn9[i];
//            }
//        } else {
//            // Display default custom algorithm presets in game UI
//            SetToDefaultSettings();
//        }
//    }

//    // Fun fictional historical dates
//    // 2/26/1984: Venom Snake awakens from 9 year coma
//    // 11/26/1985: Marty McFly accidentally travels back to 1955
//    // 11/29/86: Lan Di murders Iwao Hazuki
//    // 1/12/1992: HAL 9000 becomes operational
//    // 8/29/1997: Judgement Day: Skynet becomes self-aware and launches nuclear missiles at Russia
//    // 12/31/1999: Philip J.Fry accidentally cryogenically freezes himself
//    // 1/8/2016: Birth of Roy Batty
//    // 7/13/2035: Birth of Jean-Luc Picard
//    // 4/5/2063: Zefram Cochrane makes first human warp flight
//    public void SetToDefaultSettings() {
//        customAlgorithms[0] = new CustomAlgorithm("Normal Game Mode");
//        customAlgorithms[1] = new CustomAlgorithm("Random Objects", "26 February, 1984", 4, 1, 14, 2,
//        15, 4, 1, 0, 0, 0, 0, 0, 0, 0,
//        46, 47, 48, 50, 50, 50, 50, 50, 50, 50);
//        customAlgorithms[2] = new CustomAlgorithm("No Climbing", "29 November, 1986", 4, 1, 14, 2,
//        4, 1, 1, 2, 2, 2, 1, 1, 2, 4,
//        7, 23, 24, 25, 30, 31, 42, 43, 45, 49);
//        customAlgorithms[3] = new CustomAlgorithm("Slow Burn", "31 December, 1999", 1, 1, 14, 5,
//        5, 5, 5, 4, 1, 0, 0, 0, 0, 0,
//        17, 25, 8, 47, 48, 50, 50, 50, 50, 50);
//        customAlgorithms[4] = new CustomAlgorithm("Up & Down", "8 January, 2016", 2, 1, 6, 2,
//        7, 7, 4, 2, 0, 0, 0, 0, 0, 0,
//        20, 21, 47, 48, 50, 50, 50, 50, 50, 50);
//        customAlgorithms[5] = new("In The Middle", "5 April, 2063", 4, 1, 14, 2,
//        4, 4, 4, 4, 4, 0, 0, 0, 0, 0,
//        36, 39, 44, 45, 49, 50, 50, 50, 50, 50); 
//        customAlgorithms[6] = new CustomAlgorithm("Slot 7: EMPTY");
//        customAlgorithms[7] = new CustomAlgorithm("Slot 8: EMPTY");
//    }

//    // Displays the properties and values of this entry's custom game algorithm
//    // Called on highlight in OnHighlightDisplayCustomAlgorithm.cs
//    public void DisplaySelectedCustomAlgorithm(int buttonNdx) {
//        chanceToSpawnTexts[0].text = (customAlgorithms[buttonNdx].chanceToSpawn0 * 5f).ToString() + "%";
//        chanceToSpawnTexts[1].text = (customAlgorithms[buttonNdx].chanceToSpawn1 * 5f).ToString() + "%";
//        chanceToSpawnTexts[2].text = (customAlgorithms[buttonNdx].chanceToSpawn2 * 5f).ToString() + "%";
//        chanceToSpawnTexts[3].text = (customAlgorithms[buttonNdx].chanceToSpawn3 * 5f).ToString() + "%";
//        chanceToSpawnTexts[4].text = (customAlgorithms[buttonNdx].chanceToSpawn4 * 5f).ToString() + "%";
//        chanceToSpawnTexts[5].text = (customAlgorithms[buttonNdx].chanceToSpawn5 * 5f).ToString() + "%";
//        chanceToSpawnTexts[6].text = (customAlgorithms[buttonNdx].chanceToSpawn6 * 5f).ToString() + "%";
//        chanceToSpawnTexts[7].text = (customAlgorithms[buttonNdx].chanceToSpawn7 * 5f).ToString() + "%";
//        chanceToSpawnTexts[8].text = (customAlgorithms[buttonNdx].chanceToSpawn8 * 5f).ToString() + "%";
//        chanceToSpawnTexts[9].text = (customAlgorithms[buttonNdx].chanceToSpawn9 * 5f).ToString() + "%";

//        objectsToSpawnImages[0].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn0];
//        objectsToSpawnImages[1].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn1];
//        objectsToSpawnImages[2].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn2];
//        objectsToSpawnImages[3].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn3];
//        objectsToSpawnImages[4].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn4];
//        objectsToSpawnImages[5].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn5];
//        objectsToSpawnImages[6].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn6];
//        objectsToSpawnImages[7].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn7];
//        objectsToSpawnImages[8].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn8];
//        objectsToSpawnImages[9].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn9];

//        speedTexts[0].text = "Starting object speed (MPH):<color=#D9D9D9> " + GameManager.S.objectSpeedDisplayedValues[customAlgorithms[buttonNdx].startingObjectSpeed].ToString();
//        speedTexts[1].text = "Amount to increase per level:<color=#D9D9D9> " + GameManager.S.amountToIncreaseDisplayedValues[customAlgorithms[buttonNdx].amountToIncreaseObjectSpeed].ToString();
//        speedTexts[2].text = "Starting spawn speed (OPM):<color=#D9D9D9> " + GameManager.S.spawnSpeedDisplayedValues[customAlgorithms[buttonNdx].startingSpawnSpeed].ToString();
//        speedTexts[3].text = "Amount to increase per level:<color=#D9D9D9> " + GameManager.S.amountToDecreaseDisplayedValues[customAlgorithms[buttonNdx].amountToDecreaseSpawnSpeed].ToString();
//    }
//}

//public class CustomAlgorithmSaveData {
//    public string[] names = new string[8];
//    public string[] dates = new string[8];
//    public string[] times = new string[8];

//    public int[] startingObjectSpeed = new int[8];
//    public int[] amountToIncreaseObjectSpeed = new int[8];
//    public int[] startingSpawnSpeed = new int[8];
//    public int[] amountToDecreaseSpawnSpeed = new int[8];

//    public int[] chanceToSpawn0 = new int[8];
//    public int[] chanceToSpawn1 = new int[8];
//    public int[] chanceToSpawn2 = new int[8];
//    public int[] chanceToSpawn3 = new int[8];
//    public int[] chanceToSpawn4 = new int[8];
//    public int[] chanceToSpawn5 = new int[8];
//    public int[] chanceToSpawn6 = new int[8];
//    public int[] chanceToSpawn7 = new int[8];
//    public int[] chanceToSpawn8 = new int[8];
//    public int[] chanceToSpawn9 = new int[8];

//    public int[] objectToSpawn0 = new int[8];
//    public int[] objectToSpawn1 = new int[8];
//    public int[] objectToSpawn2 = new int[8];
//    public int[] objectToSpawn3 = new int[8];
//    public int[] objectToSpawn4 = new int[8];
//    public int[] objectToSpawn5 = new int[8];
//    public int[] objectToSpawn6 = new int[8];
//    public int[] objectToSpawn7 = new int[8];
//    public int[] objectToSpawn8 = new int[8];
//    public int[] objectToSpawn9 = new int[8];
//}

////
//public class CustomAlgorithm {
//    public string name;
//    public string date;

//    public int startingObjectSpeed;
//    public int amountToIncreaseObjectSpeed;
//    public int startingSpawnSpeed;
//    public int amountToDecreaseSpawnSpeed;

//    public int chanceToSpawn0;
//    public int chanceToSpawn1;
//    public int chanceToSpawn2;
//    public int chanceToSpawn3;
//    public int chanceToSpawn4;
//    public int chanceToSpawn5;
//    public int chanceToSpawn6;
//    public int chanceToSpawn7;
//    public int chanceToSpawn8;
//    public int chanceToSpawn9;

//    public int objectToSpawn0;
//    public int objectToSpawn1;
//    public int objectToSpawn2;
//    public int objectToSpawn3;
//    public int objectToSpawn4;
//    public int objectToSpawn5;
//    public int objectToSpawn6;
//    public int objectToSpawn7;
//    public int objectToSpawn8;
//    public int objectToSpawn9;

//    public CustomAlgorithm(string _name = "", string _date = "29 August, 1997",
//        int _startingObjectSpeed = 2, int _amountToIncreaseObjectSpeed = 1, int _startingSpawnSpeed = 9, int _amountToDecreaseSpawnSpeed = 3,
//        int _chanceToSpawn0 = 4, int _chanceToSpawn1 = 4, int _chanceToSpawn2 = 4, int _chanceToSpawn3 = 6, int _chanceToSpawn4 = 2, int _chanceToSpawn5 = 0, int _chanceToSpawn6 = 0, int _chanceToSpawn7 = 0, int _chanceToSpawn8 = 0, int _chanceToSpawn9 = 0,
//        int _objectToSpawn0 = 7, int _objectToSpawn1 = 0, int _objectToSpawn2 = 20, int _objectToSpawn3 = 47, int _objectToSpawn4 = 48, int _objectToSpawn5 = 50, int _objectToSpawn6 = 50, int _objectToSpawn7 = 50, int _objectToSpawn8 = 50, int _objectToSpawn9 = 50) {
//        name = _name;
//        date = _date;

//        startingObjectSpeed = _startingObjectSpeed;
//        amountToIncreaseObjectSpeed = _amountToIncreaseObjectSpeed;
//        startingSpawnSpeed = _startingSpawnSpeed;
//        amountToDecreaseSpawnSpeed = _amountToDecreaseSpawnSpeed;

//        chanceToSpawn0 = _chanceToSpawn0;
//        chanceToSpawn1 = _chanceToSpawn1;
//        chanceToSpawn2 = _chanceToSpawn2;
//        chanceToSpawn3 = _chanceToSpawn3;
//        chanceToSpawn4 = _chanceToSpawn4;
//        chanceToSpawn5 = _chanceToSpawn5;
//        chanceToSpawn6 = _chanceToSpawn6;
//        chanceToSpawn7 = _chanceToSpawn7;
//        chanceToSpawn8 = _chanceToSpawn8;
//        chanceToSpawn9 = _chanceToSpawn9;

//        objectToSpawn0 = _objectToSpawn0;
//        objectToSpawn1 = _objectToSpawn1;
//        objectToSpawn2 = _objectToSpawn2;
//        objectToSpawn3 = _objectToSpawn3;
//        objectToSpawn4 = _objectToSpawn4;
//        objectToSpawn5 = _objectToSpawn5;
//        objectToSpawn6 = _objectToSpawn6;
//        objectToSpawn7 = _objectToSpawn7;
//        objectToSpawn8 = _objectToSpawn8;
//        objectToSpawn9 = _objectToSpawn9;
//    }
//}