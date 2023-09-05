using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
public class SelectedHighScoreMenu : MonoBehaviour {
    [Header("Set in Inspector")]
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
    public List<Image>                  objectToSpawnImages;

    private void Start() {
        // Add listeners to buttons
        for (int i = 0; i < entryButtons.Count; i++) {
            int copy = i;
            entryButtons[copy].onClick.AddListener(delegate { DisplaySelectedHighScoreEntryData(copy); });
        }
        backToScoreboardButton.onClick.AddListener(delegate { GoBackToScoreboard(); });

        GameManager.S.selectedHighScoreMenuGO.SetActive(false);
    }

    //
    public void DisplaySelectedHighScoreEntryData(int buttonNdx, bool getByHighScoresNdx = false) {
        int ndx = buttonNdx;
        if (!getByHighScoresNdx) {
            ndx += (GameManager.S.highScore.currentPageNdx * 10);
        } else {
            GameManager.S.highScore.currentPageNdx = buttonNdx / 10;
        }
        
        // Deactivate ScoreboardMenu gameObject
        GameManager.S.scoreboardMenuGO.SetActive(false);

        // Activate SelectedHighScoreMenu gameObject
        GameManager.S.selectedHighScoreMenuGO.SetActive(true);

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
        objectToSpawnImages[0].sprite = GameManager.S.algorithmMenuCS.objectSprites[GameManager.S.highScore.highScores[ndx].objectToSpawn0];
        objectToSpawnImages[1].sprite = GameManager.S.algorithmMenuCS.objectSprites[GameManager.S.highScore.highScores[ndx].objectToSpawn1];
        objectToSpawnImages[2].sprite = GameManager.S.algorithmMenuCS.objectSprites[GameManager.S.highScore.highScores[ndx].objectToSpawn2];
        objectToSpawnImages[3].sprite = GameManager.S.algorithmMenuCS.objectSprites[GameManager.S.highScore.highScores[ndx].objectToSpawn3];
        objectToSpawnImages[4].sprite = GameManager.S.algorithmMenuCS.objectSprites[GameManager.S.highScore.highScores[ndx].objectToSpawn4];
    }

    //
    void GoBackToScoreboard() {
        // Activate ScoreboardMenu gameObject
        GameManager.S.scoreboardMenuGO.SetActive(true);

        // Deactivate SelectedHighScoreMenu gameObject
        GameManager.S.selectedHighScoreMenuGO.SetActive(false);

        // Update high score display
        GameManager.S.highScore.UpdateHighScoreDisplay(GameManager.S.highScore.currentPageNdx);
    }
}