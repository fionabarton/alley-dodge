using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

// Allows the user to load and save customized game algorithms (speeds, chances to spawn, & objects to spawn)
public class CustomAlgorithmMenu : MonoBehaviour {
    /*
     * - Name algorithm to save
     * - Confirmation sub-menu
     * - Save data
     * - On hover display algorithm preview
    */

    [Header("Set in Inspector")]
    public List<Button>             entryButtons;
    public List<TextMeshProUGUI>    entryButtonNameTexts;
    public List<TextMeshProUGUI>    entryButtonDateTexts;

    public Button                   goBackButton;
    public CryptographyManager      crypt;

    [Header("Set Dynamically")]
    public CustomAlgorithm[]        customAlgorithms;
    public CustomAlgorithmSaveData  data;
    private string                  persistentPath = "";

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

    void UpdateGUI() {
        for(int i = 0; i < customAlgorithms.Length; i++) {
            entryButtonNameTexts[i].text = customAlgorithms[i].name;
            entryButtonDateTexts[i].text = customAlgorithms[i].date;
        }
    }

    void GoBackButton() {
        gameObject.SetActive(false);
    }

    public void ActivateMenu(string actionToBePerformed) {
        if (actionToBePerformed == "Load") {
            AddLoadAlgorithmListeners();
        } else if (actionToBePerformed == "Save") {
            AddSaveAlgorithmListeners();
        }

        LoadAll();

        UpdateGUI();

        gameObject.SetActive(true);
    }

    public void AddLoadAlgorithmListeners() {
        RemoveAllListeners();

        for (int i = 0; i < customAlgorithms.Length; i++) {
            int copy = i;
            entryButtons[copy].onClick.AddListener(delegate { LoadAlgorithm(copy); });
        }
    }

    public void AddSaveAlgorithmListeners() {
        RemoveAllListeners();

        for (int i = 0; i < customAlgorithms.Length; i++) {
            int copy = i;
            entryButtons[copy].onClick.AddListener(delegate { SaveAlgorithm(copy); });
        }
    }

    void RemoveAllListeners() {
        // Remove listeners from entry buttons
        for (int i = 0; i < entryButtons.Count; i++) {
            int copy = i;
            entryButtons[copy].onClick.RemoveAllListeners();
        }
    }

    public void LoadAlgorithm(int ndx) {
        GameManager.S.speedMenuCS.SetStartingObjectSpeedDropdownValue(customAlgorithms[ndx].startingObjectSpeed);
        GameManager.S.speedMenuCS.SetAmountToIncreaseObjectSpeedDropdownValue(customAlgorithms[ndx].amountToIncreaseObjectSpeed);
        GameManager.S.speedMenuCS.SetStartingSpawnSpeedDropdownValue(customAlgorithms[ndx].startingSpawnSpeed);
        GameManager.S.speedMenuCS.SetAmountToDecreaseSpawnSpeedDropdownValue(customAlgorithms[ndx].amountToDecreaseSpawnSpeed);

        GameManager.S.algorithmMenuCS.SetChanceButtonValue(0, customAlgorithms[ndx].chanceToSpawn0);
        GameManager.S.algorithmMenuCS.SetChanceButtonValue(1, customAlgorithms[ndx].chanceToSpawn1);
        GameManager.S.algorithmMenuCS.SetChanceButtonValue(2, customAlgorithms[ndx].chanceToSpawn2);
        GameManager.S.algorithmMenuCS.SetChanceButtonValue(3, customAlgorithms[ndx].chanceToSpawn3);
        GameManager.S.algorithmMenuCS.SetChanceButtonValue(4, customAlgorithms[ndx].chanceToSpawn4);
        GameManager.S.algorithmMenuCS.SetChanceButtonValue(5, customAlgorithms[ndx].chanceToSpawn5);
        GameManager.S.algorithmMenuCS.SetChanceButtonValue(6, customAlgorithms[ndx].chanceToSpawn6);

        GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[ndx].objectToSpawn0, 0);
        GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[ndx].objectToSpawn1, 1);
        GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[ndx].objectToSpawn2, 2);
        GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[ndx].objectToSpawn3, 3);
        GameManager.S.algorithmMenuCS.SetObjectToSpawn(customAlgorithms[ndx].objectToSpawn4, 4);

        UpdateGUI();

        gameObject.SetActive(false);
    }

    public void SaveAlgorithm(int ndx) {
        customAlgorithms[ndx].name = "Preset" + ndx.ToString();
        customAlgorithms[ndx].date = System.DateTime.UtcNow.ToString("dd MMMM, yyyy");

        customAlgorithms[ndx].startingObjectSpeed = PlayerPrefs.GetInt("Speed Dropdown 0");
        customAlgorithms[ndx].amountToIncreaseObjectSpeed = PlayerPrefs.GetInt("Speed Dropdown 1");
        customAlgorithms[ndx].startingSpawnSpeed = PlayerPrefs.GetInt("Speed Dropdown 2");
        customAlgorithms[ndx].amountToDecreaseSpawnSpeed = PlayerPrefs.GetInt("Speed Dropdown 3");

        customAlgorithms[ndx].chanceToSpawn0 = PlayerPrefs.GetInt("Chance Value 0");
        customAlgorithms[ndx].chanceToSpawn1 = PlayerPrefs.GetInt("Chance Value 1");
        customAlgorithms[ndx].chanceToSpawn2 = PlayerPrefs.GetInt("Chance Value 2");
        customAlgorithms[ndx].chanceToSpawn3 = PlayerPrefs.GetInt("Chance Value 3");
        customAlgorithms[ndx].chanceToSpawn4 = PlayerPrefs.GetInt("Chance Value 4");
        customAlgorithms[ndx].chanceToSpawn5 = PlayerPrefs.GetInt("Chance Value 5");
        customAlgorithms[ndx].chanceToSpawn6 = PlayerPrefs.GetInt("Chance Value 6");

        customAlgorithms[ndx].objectToSpawn0 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[0];
        customAlgorithms[ndx].objectToSpawn1 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[1];
        customAlgorithms[ndx].objectToSpawn2 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[2];
        customAlgorithms[ndx].objectToSpawn3 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[3];
        customAlgorithms[ndx].objectToSpawn4 = GameManager.S.algorithmMenuCS.objectButtonSpriteNdx[4];

        SaveAll();

        UpdateGUI();

        gameObject.SetActive(false);
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
        customAlgorithms[0] = new CustomAlgorithm("Preset 1", "99 September, 9999");
        customAlgorithms[1] = new CustomAlgorithm("Preset 2");
        customAlgorithms[2] = new CustomAlgorithm("Preset 3");
        customAlgorithms[3] = new CustomAlgorithm("Preset 4");
        customAlgorithms[4] = new CustomAlgorithm("Preset 5");
        customAlgorithms[5] = new CustomAlgorithm("Preset 6");
        customAlgorithms[6] = new CustomAlgorithm("Preset 7");
        customAlgorithms[7] = new CustomAlgorithm("Preset 8");
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
        int _startingObjectSpeed = 9, int _amountToIncreaseObjectSpeed = 2, int _startingSpawnSpeed = 19, int _amountToDecreaseSpawnSpeed = 1,
        int _chanceToSpawn0 = 6, int _chanceToSpawn1 = 7, int _chanceToSpawn2 = 7, int _chanceToSpawn3 = 10, int _chanceToSpawn4 = 10, int _chanceToSpawn5 = 15, int _chanceToSpawn6 = 5,
        int _objectToSpawn0 = 3, int _objectToSpawn1 = 0, int _objectToSpawn2 = 17, int _objectToSpawn3 = 43, int _objectToSpawn4 = 44) {
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