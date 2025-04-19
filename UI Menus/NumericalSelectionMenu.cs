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

    public List<int>    chanceValues;

    // level, alleyAmount, moveSpeed, amountToIncrease, spawnSpeed, amountToDecrease, chance (0-6)
    public List<Button> propertyButtons;

    public Text         headerText;

    void Start() {
        // Add listener
        goBackButton.onClick.AddListener(delegate { Deactivate(); });

        Invoke("OnStart", 0.1f);
    }

    void OnStart() {
        // Set property button text
        if (PlayerPrefs.HasKey("Level Select")) {
            propertyButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Level Select").ToString();
        } else {
            propertyButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "1";
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

        gameObject.SetActive(false);
    }

    void Deactivate() {
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
                // Set header text
                headerText.text = "Please select an amount!";

                for (int i = 0; i < levelValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = levelValues[i].ToString();

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.mainMenuCS.SetLevel(levelValues[copy]); });
                    amountButtons[copy].onClick.AddListener(delegate { SetButtonText(0, levelValues[copy].ToString()); });
                }
                break;
            case 1:
                // Set header text
                headerText.text = "Please select an amount!";

                for (int i = 0; i < alleyAmountValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = alleyAmountValues[i].ToString();

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.mainMenuCS.SetAlleyAmount(copy); });
                    amountButtons[copy].onClick.AddListener(delegate { SetButtonText(1, alleyAmountValues[copy].ToString()); });
                }
                break;
            case 2:
                // Set header text
                headerText.text = "Please select an amount!\n<color=#CC991A>(Measured in miles per hour)</color>";

                for (int i = 0; i < GameManager.S.objectSpeedValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = GameManager.S.objectSpeedDisplayedValues[i].ToString();

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetStartingObjectSpeedDropdownValue(copy); });
                    amountButtons[copy].onClick.AddListener(delegate { Deactivate(); });
                }
                break;
            case 3:
                // Set header text
                headerText.text = "Please select an amount!\n<color=#CC991A>(Measured in miles per hour)</color>";

                for (int i = 0; i < GameManager.S.amountToIncreaseValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = GameManager.S.amountToIncreaseDisplayedValues[i].ToString();

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetAmountToIncreaseObjectSpeedDropdownValue(copy); });
                    amountButtons[copy].onClick.AddListener(delegate { Deactivate(); });
                }
                break;
            case 4:
                // Set header text
                headerText.text = "Please select an amount!\n<color=#CC991A>(Measured in objects generated per minute)</color>";

                for (int i = 0; i < GameManager.S.spawnSpeedValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = GameManager.S.spawnSpeedDisplayedValues[i].ToString();

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetStartingSpawnSpeedDropdownValue(copy); });
                    amountButtons[copy].onClick.AddListener(delegate { Deactivate(); });
                }
                break;
            case 5:
                // Set header text
                headerText.text = "Please select an amount!\n<color=#CC991A>(Measured in objects generated per minute)</color>";

                for (int i = 0; i < GameManager.S.amountToDecreaseValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = GameManager.S.amountToDecreaseDisplayedValues[i].ToString();

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetAmountToDecreaseSpawnSpeedDropdownValue(copy); });
                    amountButtons[copy].onClick.AddListener(delegate { Deactivate(); });
                }
                break;
            case 6:
                // Set header text
                headerText.text = "Please select an amount!";

                for (int i = 0; i < chanceValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = chanceValues[i].ToString() + "%";

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetChanceButtonValue(0, copy); });
                    amountButtons[copy].onClick.AddListener(delegate { Deactivate(); });
                }
                break;
            case 7:
                // Set header text
                headerText.text = "Please select an amount!";

                for (int i = 0; i < chanceValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = chanceValues[i].ToString() + "%";

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetChanceButtonValue(1, copy); });
                    amountButtons[copy].onClick.AddListener(delegate { Deactivate(); });
                }
                break;
            case 8:
                // Set header text
                headerText.text = "Please select an amount!";

                for (int i = 0; i < chanceValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = chanceValues[i].ToString() + "%";

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetChanceButtonValue(2, copy); });
                    amountButtons[copy].onClick.AddListener(delegate { Deactivate(); });
                }
                break;
            case 9:
                // Set header text
                headerText.text = "Please select an amount!";

                for (int i = 0; i < chanceValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = chanceValues[i].ToString() + "%";

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetChanceButtonValue(3, copy); });
                    amountButtons[copy].onClick.AddListener(delegate { Deactivate(); });
                }
                break;
            case 10:
                // Set header text
                headerText.text = "Please select an amount!";

                for (int i = 0; i < chanceValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = chanceValues[i].ToString() + "%";

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetChanceButtonValue(4, copy); });
                    amountButtons[copy].onClick.AddListener(delegate { Deactivate(); });
                }
                break;
            case 11:
                // Set header text
                headerText.text = "Please select an amount!";

                for (int i = 0; i < chanceValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = chanceValues[i].ToString() + "%";

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetChanceButtonValue(5, copy); });
                    amountButtons[copy].onClick.AddListener(delegate { Deactivate(); });
                }
                break;
            case 12:
                // Set header text
                headerText.text = "Please select an amount!";

                for (int i = 0; i < chanceValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = chanceValues[i].ToString() + "%";

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetChanceButtonValue(6, copy); });
                    amountButtons[copy].onClick.AddListener(delegate { Deactivate(); });
                }
                break;
            case 13:
                // Set header text
                headerText.text = "Please select an amount!";

                for (int i = 0; i < chanceValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = chanceValues[i].ToString() + "%";

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetChanceButtonValue(7, copy); });
                    amountButtons[copy].onClick.AddListener(delegate { Deactivate(); });
                }
                break;
            case 14:
                // Set header text
                headerText.text = "Please select an amount!";

                for (int i = 0; i < chanceValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = chanceValues[i].ToString() + "%";

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetChanceButtonValue(8, copy); });
                    amountButtons[copy].onClick.AddListener(delegate { Deactivate(); });
                }
                break;
            case 15:
                // Set header text
                headerText.text = "Please select an amount!";

                for (int i = 0; i < chanceValues.Count; i++) {
                    amountButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = chanceValues[i].ToString() + "%";

                    // Add listeners
                    int copy = i;
                    amountButtons[copy].onClick.AddListener(delegate { GameManager.S.algorithmMenuCS.SetChanceButtonValue(9, copy); });
                    amountButtons[copy].onClick.AddListener(delegate { Deactivate(); });
                }
                break;
        }
    }
}