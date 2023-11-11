using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

// Allows the user to load and save customized game algorithms (speeds, chances to spawn, & objects to spawn)
public class CustomAlgorithmMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public List<Button>             entryButtons;
    public List<TextMeshProUGUI>    entryButtonNameTexts;
    public List<TextMeshProUGUI>    entryButtonDateTexts;
    public Text                     selectAnAlgorithmText;

    public Button                   goBackButton;
    public Button                   resetButton;
    public CryptographyManager      crypt;

    // Preview the of selected custom algorithm
    public List<TextMeshProUGUI>    chanceToSpawnTexts;
    public List<Image>              objectsToSpawnImages;
    public List<TextMeshProUGUI>    speedTexts;

    [Header("Set Dynamically")]
    public CustomAlgorithm[]        customAlgorithms;
    public CustomAlgorithmSaveData  data;
    private string                  persistentPath = "";

    private int                     selectedButtonNdx;

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
        for(int i = 0; i < customAlgorithms.Length; i++) {
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
                selectAnAlgorithmText.text = "Please select a custom algorithm to load!";
            } else if (actionToBePerformed == "Save") {
                AddSaveAlgorithmListeners();

                // Set title text
                selectAnAlgorithmText.text = "Please select a slot to save your custom algorithm!";
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
        GameManager.S.subMenuCS.AddListeners(LoadAlgorithm, "Are you sure that you would like to\nload this custom algorithm?");
    }
    public void LoadAlgorithm(int yesOrNo = -1) {
        // Deactivate sub menu
        GameManager.S.subMenuGO.SetActive(false);

        if (yesOrNo == 0) {
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

            GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[selectedButtonNdx].objectToSpawn0, 0);
            GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[selectedButtonNdx].objectToSpawn1, 1);
            GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[selectedButtonNdx].objectToSpawn2, 2);
            GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[selectedButtonNdx].objectToSpawn3, 3);
            GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[selectedButtonNdx].objectToSpawn4, 4);

            UpdateGUI();

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Selected custom algorithm loaded!", true);

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

            customAlgorithms[selectedButtonNdx].objectToSpawn0 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[0];
            customAlgorithms[selectedButtonNdx].objectToSpawn1 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[1];
            customAlgorithms[selectedButtonNdx].objectToSpawn2 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[2];
            customAlgorithms[selectedButtonNdx].objectToSpawn3 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[3];
            customAlgorithms[selectedButtonNdx].objectToSpawn4 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[4];

            SaveAll();

            UpdateGUI();

            // Delayed text display
            GameManager.S.moreMenuCS.delayedTextDisplay.DisplayText("Your custom algorithm, " + customAlgorithms[selectedButtonNdx].name + ",\n saved to slot " + (selectedButtonNdx+1).ToString() + "!", true);

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

            data.objectToSpawn0[i] = customAlgorithms[i].objectToSpawn0;
            data.objectToSpawn1[i] = customAlgorithms[i].objectToSpawn1;
            data.objectToSpawn2[i] = customAlgorithms[i].objectToSpawn2;
            data.objectToSpawn3[i] = customAlgorithms[i].objectToSpawn3;
            data.objectToSpawn4[i] = customAlgorithms[i].objectToSpawn4;
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

                customAlgorithms[i].objectToSpawn0 = data.objectToSpawn0[i];
                customAlgorithms[i].objectToSpawn1 = data.objectToSpawn1[i];
                customAlgorithms[i].objectToSpawn2 = data.objectToSpawn2[i];
                customAlgorithms[i].objectToSpawn3 = data.objectToSpawn3[i];
                customAlgorithms[i].objectToSpawn4 = data.objectToSpawn4[i];
            }
        } else {
            // Display default custom algorithm presets in game UI
            SetToDefaultSettings();
        }
    }

    public void SetToDefaultSettings() {
        customAlgorithms[0] = new CustomAlgorithm("Random Objects", "26 February, 1984", 4, 2, 10, 1,
        6, 7, 7, 10, 10, 15, 5,
        46, 46, 46, 47, 48);
        customAlgorithms[1] = new CustomAlgorithm("No Climbing", "29 November, 1986", 4, 1, 10, 2,
        6, 7, 7, 10, 10, 15, 5,
        7, 23, 24, 25, 49);
        customAlgorithms[2] = new CustomAlgorithm("Slow Burn", "31 December, 1999", 2, 2, 5, 1,
        6, 7, 7, 10, 10, 15, 5,
        17, 25, 8, 47, 48);
        customAlgorithms[3] = new CustomAlgorithm("Up & Down", "5 April, 2063", 4, 2, 10, 1,
        4, 8, 8, 10, 10, 10, 10,
        20, 21, 37, 38, 49);
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

        objectsToSpawnImages[0].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn0];
        objectsToSpawnImages[1].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn1];
        objectsToSpawnImages[2].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn2];
        objectsToSpawnImages[3].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn3];
        objectsToSpawnImages[4].sprite = GameManager.S.algorithmMenuCS.objectSprites[customAlgorithms[buttonNdx].objectToSpawn4];

        speedTexts[0].text = "<color=red>Starting object speed:</color> " + (customAlgorithms[buttonNdx].startingObjectSpeed + 1).ToString();
        speedTexts[1].text = "<color=#FFC800>Amount to increase per level:</color> " + ((float)customAlgorithms[buttonNdx].amountToIncreaseObjectSpeed / 10).ToString();
        speedTexts[2].text = "<color=red>Starting spawn speed:</color> " + (((float)customAlgorithms[buttonNdx].startingSpawnSpeed / 10) + 1.0f).ToString();
        speedTexts[3].text = "<color=#FFC800>Amount to decrease per level:</color> " + ((float)customAlgorithms[buttonNdx].amountToDecreaseSpawnSpeed / 10).ToString();
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

    public int[] objectToSpawn0 = new int[8];
    public int[] objectToSpawn1 = new int[8];
    public int[] objectToSpawn2 = new int[8];
    public int[] objectToSpawn3 = new int[8];
    public int[] objectToSpawn4 = new int[8];
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

    public int objectToSpawn0;
    public int objectToSpawn1;
    public int objectToSpawn2;
    public int objectToSpawn3;
    public int objectToSpawn4;

    public CustomAlgorithm(string _name = "", string _date = "29 August, 1997",
        int _startingObjectSpeed = 4, int _amountToIncreaseObjectSpeed = 2, int _startingSpawnSpeed = 19, int _amountToDecreaseSpawnSpeed = 1,
        int _chanceToSpawn0 = 6, int _chanceToSpawn1 = 7, int _chanceToSpawn2 = 7, int _chanceToSpawn3 = 10, int _chanceToSpawn4 = 10, int _chanceToSpawn5 = 15, int _chanceToSpawn6 = 5,
        int _objectToSpawn0 = 7, int _objectToSpawn1 = 0, int _objectToSpawn2 = 20, int _objectToSpawn3 = 47, int _objectToSpawn4 = 48) {
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

        objectToSpawn0 = _objectToSpawn0;
        objectToSpawn1 = _objectToSpawn1;
        objectToSpawn2 = _objectToSpawn2;
        objectToSpawn3 = _objectToSpawn3;
        objectToSpawn4 = _objectToSpawn4;
    }
}