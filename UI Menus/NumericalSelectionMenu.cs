using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//
public class NumericalSelectionMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public List<Button> amountButtons;

    public Button       goBackButton;

    public List<int>    valueCount;

    public List<int>    levelValues;
    public List<int>    alleyAmountValues;
    public List<int>    objectSpeedValues;
    public List<float>  amountToIncreaseValues;
    public List<float>  spawnSpeedValues;
    public List<float>  amountToDecreaseValues;
    public List<int>    chanceValues;

    // level, alleyAmount, moveSpeed, amountToIncrease, spawnSpeed, amountToDecrease, chance (0-6)
    public List<Button> propertyButtons;

    //
    //public List<Button> chanceButtons;

    void Start() {
        // Add listener
        goBackButton.onClick.AddListener(delegate { GoBackButton(); });

        Invoke("OnStart", 0.1f);
    }

    void OnStart() {
        // Set property button text
        if (PlayerPrefs.HasKey("Level Select")) {
            propertyButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Level "+ PlayerPrefs.GetInt("Level Select").ToString();
        } else {
            propertyButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Level 1";
        }

        if (PlayerPrefs.HasKey("Alley Amount")) {
            int amount = PlayerPrefs.GetInt("Alley Amount");
            switch (amount) {
                case 0:
                    propertyButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "3";
                    break;
                case 1:
                    propertyButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "5";
                    break;
                case 2:
                    propertyButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "7";
                    break;
            }
        } else {
            propertyButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "3";   
        }

        if (PlayerPrefs.HasKey("Speed Dropdown 0")) {
            propertyButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = (PlayerPrefs.GetInt("Speed Dropdown 0") + 1f).ToString();
        } else {
            propertyButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = "10";
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 1")) {
            propertyButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = (PlayerPrefs.GetInt("Speed Dropdown 1") / 10f).ToString();
        } else {
            propertyButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = "0.2";
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 2")) {
            propertyButtons[4].GetComponentInChildren<TextMeshProUGUI>().text = ((PlayerPrefs.GetInt("Speed Dropdown 2") / 10f) + 0.1f).ToString();
        } else {
            propertyButtons[4].GetComponentInChildren<TextMeshProUGUI>().text = "2.0";
        }
        if (PlayerPrefs.HasKey("Speed Dropdown 3")) {
            propertyButtons[5].GetComponentInChildren<TextMeshProUGUI>().text = (PlayerPrefs.GetInt("Speed Dropdown 3") / 10f).ToString();
        } else {
            propertyButtons[5].GetComponentInChildren<TextMeshProUGUI>().text = "0.1";
        }

        // Get top level chanceDropdowns PlayerPrefs
        if (PlayerPrefs.HasKey("Chance Value 0")) {
            propertyButtons[6].GetComponentInChildren<TextMeshProUGUI>().text = (PlayerPrefs.GetInt("Chance Value 0") * 5f).ToString() + "%";
        } else {
            propertyButtons[6].GetComponentInChildren<TextMeshProUGUI>().text = "30%";
        }
        if (PlayerPrefs.HasKey("Chance Value 1")) {
            propertyButtons[7].GetComponentInChildren<TextMeshProUGUI>().text = (PlayerPrefs.GetInt("Chance Value 1") * 5f).ToString() + "%";
        } else {
            propertyButtons[7].GetComponentInChildren<TextMeshProUGUI>().text = "35%";
        }
        if (PlayerPrefs.HasKey("Chance Value 2")) {
            propertyButtons[8].GetComponentInChildren<TextMeshProUGUI>().text = (PlayerPrefs.GetInt("Chance Value 2") * 5f).ToString() + "%";
        } else {
            propertyButtons[8].GetComponentInChildren<TextMeshProUGUI>().text = "35%";
        }

        // Get mid level chanceDropdowns PlayerPrefs
        if (PlayerPrefs.HasKey("Chance Value 3")) {
            propertyButtons[9].GetComponentInChildren<TextMeshProUGUI>().text = (PlayerPrefs.GetInt("Chance Value 3") * 5f).ToString() + "%";
        } else {
            propertyButtons[9].GetComponentInChildren<TextMeshProUGUI>().text = "50%";
        }
        if (PlayerPrefs.HasKey("Chance Value 4")) {
            propertyButtons[10].GetComponentInChildren<TextMeshProUGUI>().text = (PlayerPrefs.GetInt("Chance Value 4") * 5f).ToString() + "%";
        } else {
            propertyButtons[10].GetComponentInChildren<TextMeshProUGUI>().text = "50%";
        }
        if (PlayerPrefs.HasKey("Chance Value 5")) {
            propertyButtons[11].GetComponentInChildren<TextMeshProUGUI>().text = (PlayerPrefs.GetInt("Chance Value 5") * 5f).ToString() + "%";
        } else {
            propertyButtons[11].GetComponentInChildren<TextMeshProUGUI>().text = "75%";
        }
        if (PlayerPrefs.HasKey("Chance Value 6")) {
            propertyButtons[12].GetComponentInChildren<TextMeshProUGUI>().text = (PlayerPrefs.GetInt("Chance Value 6") * 5f).ToString() + "%";
        } else {
            propertyButtons[12].GetComponentInChildren<TextMeshProUGUI>().text = "25%";
        }

        gameObject.SetActive(false);
    }

    void GoBackButton() {
        gameObject.SetActive(false);
    }

    void SetButtonText(int ndx, string text) {
        //
        propertyButtons[ndx].GetComponentInChildren<TextMeshProUGUI>().text = text;

        //
        gameObject.SetActive(false);
    }

    public void ActivateMenu(int ndx) {
        // Activate this game object
        gameObject.SetActive(true);

        // Deactivate & remove all listeners from amountButtons
        for (int i = 0; i < amountButtons.Count; i++) {
            amountButtons[i].gameObject.SetActive(false);
            amountButtons[i].onClick.RemoveAllListeners();
        }

        // Activate selected amountButtons
        for (int i = 0; i < valueCount[ndx]; i++) {
            amountButtons[i].gameObject.SetActive(true);
        }

        // Set button texts
        switch (ndx) {
            case 0:
                for (int i = 0; i < levelValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = levelValues[i].ToString();

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.mainMenuCS.SetLevel(levelValues[copy]); });
                    amountButtons[copy].onClick.AddListener(delegate { SetButtonText(0, levelValues[copy].ToString()); });
                }
                break;
            case 1:
                for (int i = 0; i < alleyAmountValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = alleyAmountValues[i].ToString();

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.mainMenuCS.SetAlleyAmount(copy); });
                    amountButtons[copy].onClick.AddListener(delegate { SetButtonText(1, alleyAmountValues[copy].ToString()); });
                }
                break;
            case 2:
                for (int i = 0; i < objectSpeedValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = objectSpeedValues[i].ToString();

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.speedMenuCS.SetStartingObjectSpeedDropdownValue(copy); });
                    amountButtons[copy].onClick.AddListener(delegate { SetButtonText(2, objectSpeedValues[copy].ToString()); });
                }
                break;
            case 3:
                for (int i = 0; i < amountToIncreaseValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = amountToIncreaseValues[i].ToString();

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.speedMenuCS.SetAmountToIncreaseObjectSpeedDropdownValue(copy); });
                    amountButtons[copy].onClick.AddListener(delegate { SetButtonText(3, amountToIncreaseValues[copy].ToString()); });
                }
                break;
            case 4:
                for (int i = 0; i < spawnSpeedValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = spawnSpeedValues[i].ToString();

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.speedMenuCS.SetStartingSpawnSpeedDropdownValue(copy); });
                    amountButtons[copy].onClick.AddListener(delegate { SetButtonText(4, spawnSpeedValues[copy].ToString()); });
                }
                break;
            case 5:
                for (int i = 0; i < amountToDecreaseValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = amountToDecreaseValues[i].ToString();

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.speedMenuCS.SetAmountToDecreaseSpawnSpeedDropdownValue(copy); });
                    amountButtons[copy].onClick.AddListener(delegate { SetButtonText(5, amountToDecreaseValues[copy].ToString()); });
                }
                break;
            case 6:
                for (int i = 0; i < chanceValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = chanceValues[i].ToString() + "%";

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetChanceButtonValue(0, copy); });
                    amountButtons[copy].onClick.AddListener(delegate { SetButtonText(6, chanceValues[copy].ToString() + "%"); });
                }
                break;
            case 7:
                for (int i = 0; i < chanceValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = chanceValues[i].ToString() + "%";

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetChanceButtonValue(1, copy); });
                    amountButtons[copy].onClick.AddListener(delegate { SetButtonText(7, chanceValues[copy].ToString() + "%"); });
                }
                break;
            case 8:
                for (int i = 0; i < chanceValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = chanceValues[i].ToString() + "%";

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetChanceButtonValue(2, copy); });
                    amountButtons[copy].onClick.AddListener(delegate { SetButtonText(8, chanceValues[copy].ToString() + "%"); });
                }
                break;
            case 9:
                for (int i = 0; i < chanceValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = chanceValues[i].ToString() + "%";

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetChanceButtonValue(3, copy); });
                    amountButtons[copy].onClick.AddListener(delegate { SetButtonText(9, chanceValues[copy].ToString() + "%"); });
                }
                break;
            case 10:
                for (int i = 0; i < chanceValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = chanceValues[i].ToString() + "%";

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetChanceButtonValue(4, copy); });
                    amountButtons[copy].onClick.AddListener(delegate { SetButtonText(10, chanceValues[copy].ToString() + "%"); });
                }
                break;
            case 11:
                for (int i = 0; i < chanceValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = chanceValues[i].ToString() + "%";

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetChanceButtonValue(5, copy); });
                    amountButtons[copy].onClick.AddListener(delegate { SetButtonText(11, chanceValues[copy].ToString() + "%"); });
                }
                break;
            case 12:
                for (int i = 0; i < chanceValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = chanceValues[i].ToString() + "%";

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetChanceButtonValue(6, copy); });
                    amountButtons[copy].onClick.AddListener(delegate { SetButtonText(12, chanceValues[copy].ToString() + "%"); });
                }
                break;
        }
    }
}

//Main Menu
    //Level select (1-10) (1, 2, 3, etc.) (Count: 10)
    //Alley amount(3,5,7) (Count: 3)

//Algorithm Menu
    //Chance dropdowns (0%-100%) (0%, 5%, 10%, etc.) (Count: 21)

//Speed Menu
    //Object speed (1-20) (1, 2, 3, etc.) (Count: 20)
    //Amount to increase (0-2.0) (0, 0.1, 0.2, etc.) (Count: 21)
    //Spawn speed(0.1-2.0) (0.1, 0.2, 0.3, etc.) (Count: 20)
    //Amount to decrease (0-2.0) (0, 0.1, 0.2, etc.) (Count: 21)