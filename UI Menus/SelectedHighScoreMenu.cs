using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
public class SelectedHighScoreMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject                   scoreboardMenuGO;
    public GameObject                   selectedHighScoreMenuGO;

    // Buttons
    public List<Button>                 entryButtons;
    public Button                       backToScoreboardButton;

    // Property values text
    public TMPro.TextMeshProUGUI        rankValue;
    public TMPro.TextMeshProUGUI        nameValue;
    public TMPro.TextMeshProUGUI        scoreValue;
    public TMPro.TextMeshProUGUI        levelValue;
    public TMPro.TextMeshProUGUI        objectsValue;
    public TMPro.TextMeshProUGUI        runTimeValue;
    public TMPro.TextMeshProUGUI        dateValue;
    public TMPro.TextMeshProUGUI        timeValue;
    public TMPro.TextMeshProUGUI        alleyAmountValue;
    public TMPro.TextMeshProUGUI        playerHeightValue;
    public TMPro.TextMeshProUGUI        fallBelowFloorCountValue;
    public TMPro.TextMeshProUGUI        damageCountValue;
    public TMPro.TextMeshProUGUI        pauseCountValue;
    public TMPro.TextMeshProUGUI        startingObjectSpeedValue;
    public TMPro.TextMeshProUGUI        amountToIncreaseObjectSpeedValue;
    public TMPro.TextMeshProUGUI        startingSpawnSpeedValue;
    public TMPro.TextMeshProUGUI        amountToDecreaseSpawnSpeedValue;
    public List<TMPro.TextMeshProUGUI>  chanceToSpawnValues;
    public List<TMPro.TextMeshProUGUI>  objectToSpawnValues;

    //[Header("Set Dynamically")]

    private void Start() {
        // Add listeners to buttons
        entryButtons[0].onClick.AddListener(delegate { DisplaySelectedHighScoreEntryData(0); });
        entryButtons[1].onClick.AddListener(delegate { DisplaySelectedHighScoreEntryData(1); });
        entryButtons[2].onClick.AddListener(delegate { DisplaySelectedHighScoreEntryData(2); });
        entryButtons[3].onClick.AddListener(delegate { DisplaySelectedHighScoreEntryData(3); });
        entryButtons[4].onClick.AddListener(delegate { DisplaySelectedHighScoreEntryData(4); });
        entryButtons[5].onClick.AddListener(delegate { DisplaySelectedHighScoreEntryData(5); });
        entryButtons[6].onClick.AddListener(delegate { DisplaySelectedHighScoreEntryData(6); });
        entryButtons[7].onClick.AddListener(delegate { DisplaySelectedHighScoreEntryData(7); });
        entryButtons[8].onClick.AddListener(delegate { DisplaySelectedHighScoreEntryData(8); });
        entryButtons[9].onClick.AddListener(delegate { DisplaySelectedHighScoreEntryData(9); });
        backToScoreboardButton.onClick.AddListener(delegate { GoBackToScoreboard(); });

        selectedHighScoreMenuGO.SetActive(false);
    }

    //
    void DisplaySelectedHighScoreEntryData(int buttonNdx) {
        int ndx = buttonNdx + (GameManager.S.highScore.currentPageNdx * 10);

        // Deactivate ScoreboardMenu gameObject
        scoreboardMenuGO.SetActive(false);

        // Activate SelectedHighScoreMenu gameObject
        selectedHighScoreMenuGO.SetActive(true);

        // Copy data from HighScores to menu text
        rankValue.text = (ndx + 1).ToString();
        nameValue.text = GameManager.S.highScore.highScores[ndx].name;
        scoreValue.text = GameManager.S.highScore.highScores[ndx].score.ToString();
        levelValue.text = GameManager.S.highScore.highScores[ndx].level.ToString();
        objectsValue.text = GameManager.S.highScore.highScores[ndx].objects.ToString();
        runTimeValue.text = GameManager.S.highScore.highScores[ndx].runTime;
        dateValue.text = GameManager.S.highScore.highScores[ndx].date;
        timeValue.text = GameManager.S.highScore.highScores[ndx].time;
        alleyAmountValue.text = GameManager.S.highScore.highScores[ndx].alleyCount.ToString();
        playerHeightValue.text = GameManager.S.highScore.highScores[ndx].playerHeight;
        fallBelowFloorCountValue.text = GameManager.S.highScore.highScores[ndx].fallBelowFloorCount.ToString();
        damageCountValue.text = GameManager.S.highScore.highScores[ndx].damageCount.ToString();
        pauseCountValue.text = GameManager.S.highScore.highScores[ndx].pauseCount.ToString();
        startingObjectSpeedValue.text = GameManager.S.highScore.highScores[ndx].startingObjectSpeed;
        amountToIncreaseObjectSpeedValue.text = GameManager.S.highScore.highScores[ndx].amountToIncreaseObjectSpeed;
        startingSpawnSpeedValue.text = GameManager.S.highScore.highScores[ndx].startingSpawnSpeed;
        amountToDecreaseSpawnSpeedValue.text = GameManager.S.highScore.highScores[ndx].amountToDecreaseSpawnSpeed;
        chanceToSpawnValues[0].text = GameManager.S.highScore.highScores[ndx].chanceToSpawn0;
        chanceToSpawnValues[1].text = GameManager.S.highScore.highScores[ndx].chanceToSpawn1;
        chanceToSpawnValues[2].text = GameManager.S.highScore.highScores[ndx].chanceToSpawn2;
        chanceToSpawnValues[3].text = GameManager.S.highScore.highScores[ndx].chanceToSpawn3;
        chanceToSpawnValues[4].text = GameManager.S.highScore.highScores[ndx].chanceToSpawn4;
        chanceToSpawnValues[5].text = GameManager.S.highScore.highScores[ndx].chanceToSpawn5;
        chanceToSpawnValues[6].text = GameManager.S.highScore.highScores[ndx].chanceToSpawn6;
        objectToSpawnValues[0].text = GameManager.S.highScore.highScores[ndx].objectToSpawn0;
        objectToSpawnValues[1].text = GameManager.S.highScore.highScores[ndx].objectToSpawn1;
        objectToSpawnValues[2].text = GameManager.S.highScore.highScores[ndx].objectToSpawn2;
        objectToSpawnValues[3].text = GameManager.S.highScore.highScores[ndx].objectToSpawn3;
        objectToSpawnValues[4].text = GameManager.S.highScore.highScores[ndx].objectToSpawn4;
    }

    //
    void GoBackToScoreboard() {
        // Activate ScoreboardMenu gameObject
        scoreboardMenuGO.SetActive(true);

        // Deactivate SelectedHighScoreMenu gameObject
        selectedHighScoreMenuGO.SetActive(false);

        // Update high score display
        GameManager.S.highScore.UpdateHighScoreDisplay(GameManager.S.highScore.currentPageNdx);
    }
}