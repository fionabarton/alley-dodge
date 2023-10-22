using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Stores and displays the top 100 high scores (name, score, level, objects, time)
public class HighScoreManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public TMPro.TextMeshProUGUI        titleText;
    public List<TMPro.TextMeshProUGUI>  rankText;
    public List<TMPro.TextMeshProUGUI>  nameText;
    public List<TMPro.TextMeshProUGUI>  scoreText;
    public List<TMPro.TextMeshProUGUI>  levelText;
    public List<TMPro.TextMeshProUGUI>  objectsText;
    public List<TMPro.TextMeshProUGUI>  runTimeText;
    public TMPro.TextMeshProUGUI        pageText;

    public Button                       resetButton;

    // Needed to highlight new high score text with cycling rainbow color animation clip
    public List<Animator>               rankAnim;
    public List<Animator>               nameAnim;
    public List<Animator>               scoreAnim;
    public List<Animator>               levelAnim;
    public List<Animator>               objectsAnim;
    public List<Animator>               runTimeAnim;

    public List<GameObject>             cursorGO;

    // Delayed text display
    public DelayedTextDisplay           delayedTextDisplay;

    [Header("Set Dynamically")]
    public HighScore[]                  highScores;

    // Index at which to store a new HighScore in the 'highScores' array
    public int                          newHighScoreNdx = -1;

    // Index at which the new HighScore is displayed in the list of 10 displayed scores
    public int                          newHighScoreListNdx = -1;

    //
    public int                          newHighScorePageNdx = -1;

    //
    public int                          currentPageNdx = 0;

    void Start() {
        // Initialize array of high scores
        highScores = new HighScore[100];

        //
        SetHighScoresToDefaultValues();

        gameObject.SetActive(false);
    }

    private void OnEnable() {
        // Display text
        if (Time.time > 0.01f) {
            delayedTextDisplay.DisplayText("Welcome to the high score menu:\nView the top 100 high score leaderboard and\ndetailed information for each entry.", true);
        }
    }

    // Deletes all saved high scores and resets them to default values 
    public void SetHighScoresToDefaultValues(bool saveData = false) {
        // Populate elements 0 to 19
        highScores[0] = new HighScore("Bernise", 63, 13, 300, "00:06:58.629");
        highScores[1] = new HighScore("Mortimer", 57, 12, 192, "00:04:51.582");
        highScores[2] = new HighScore("Ruth", 55, 12, 184, "00:04:38.194");
        highScores[3] = new HighScore("Herbert", 53, 11, 188, "00:04:40.273");
        highScores[4] = new HighScore("Ingrid", 47, 10, 184, "00:04:52.178");
        highScores[5] = new HighScore("Murray", 45, 10, 175, "00:04:31.531");
        highScores[6] = new HighScore("Thelma", 41, 9, 181, "00:05:03.040");
        highScores[7] = new HighScore("Martha", 38, 8, 174, "00:04:52.516");
        highScores[8] = new HighScore("Humphrey", 36, 8, 166, "00:04:39.254");
        highScores[9] = new HighScore("Norman", 34, 7, 114, "00:03:10.624");
        highScores[10] = new HighScore("Mavis", 31, 7, 143, "00:02:53.387");
        highScores[11] = new HighScore("Wilbert", 29, 6, 166, "00:04:39.254");
        highScores[12] = new HighScore("Doris", 24, 5, 152, "00:04:13.472");
        highScores[13] = new HighScore("Orville", 21, 5, 137, "00:03:58.519");
        highScores[14] = new HighScore("Maude", 15, 4, 54, "00:01:40.375");
        highScores[15] = new HighScore("Harold", 14, 3, 41, "00:01:16.891");
        highScores[16] = new HighScore("Melvin", 7, 2, 40, "00:01:18:163");
        highScores[17] = new HighScore("Ethel", 6, 2, 34, "00:00:48:502");
        highScores[18] = new HighScore("Dilmore", 4, 1, 21, "00:00:20:665");
        highScores[19] = new HighScore("Gertrude", 2, 1, 26, "00:00:38:014");


        // Populate elements 20 to 99
        for (int i = 20; i < highScores.Length; i++) {
            highScores[i] = new HighScore("Slot " + (i + 1).ToString() + ": EMPTY");
        }

        // Save data
        if (saveData) {
            GameManager.save.Save();

            UpdateHighScoreDisplay(currentPageNdx);
        }
    }

    // Checks if the score is a new high score,
    // then returns at what index it belongs in the highScores array
    public bool CheckForNewHighScore(int score) {
        // Load data
        GameManager.save.Load();

        for (int i = 0; i < highScores.Length; i++) {
            if(score > highScores[i].score) {
                // Set newHighScoreNdx
                newHighScoreNdx = i;

                // Set currentPageNdx
                if (newHighScoreNdx <= 9) {
                    currentPageNdx = 0;
                } else if (newHighScoreNdx >= 89) {
                    currentPageNdx = 9;
                } else {
                    int tInt = newHighScoreNdx / 10;
                    currentPageNdx = tInt % 10;
                }

                // 
                newHighScorePageNdx = currentPageNdx;

                // Set newHighScoreListNdx
                newHighScoreListNdx = newHighScoreNdx % 10;

                return true;
            }
        }
        return false;
    }

    //
    public void AddNewHighScore(HighScore newScore) {
        // Initialize new array
        HighScore[] tScores = new HighScore[100];

        // Set higher scores that will not change position
        for(int i = 0; i < newHighScoreNdx; i++) {
            tScores[i] = highScores[i];
        }

        // Set new score
        tScores[newHighScoreNdx] = newScore;

        // Set lower scores that will shift position to the right by 1
        for (int i = newHighScoreNdx + 1; i < highScores.Length; i++) {
            tScores[i] = highScores[i - 1];
        }

        // 
        highScores = tScores;

        // Save data
        GameManager.save.Save();

        //
        UpdateHighScoreDisplay(currentPageNdx);

        GameManager.S.selectedHighScoreMenuCS.DisplaySelectedHighScoreEntryData(newHighScoreNdx, true);

        // Reset score for next game
        GameManager.S.score.ResetScore();
    }

    //
    public void UpdateHighScoreDisplay(int pageNdx = 0, bool isCalledInStart = true) {
        // Get index of first score to be displayed on this page
        int startingNdx = pageNdx * 10;

        // Set title text
        titleText.text = "Top 100 High Scores " + "(" + (startingNdx + 1).ToString() + "-" + (startingNdx + 10).ToString() + ")";

        // Loop over the 10 UI text objects
        for (int i = 0; i < 10; i++) {
            // Get rank suffix (1st, 2nd, & 3rd)
            string rankSuffix;
            if (startingNdx + i == 0) {
                rankSuffix = "st";
            } else if (startingNdx + i == 1) {
                rankSuffix = "nd";
            } else if (startingNdx + i == 2) {
                rankSuffix = "rd";
            } else {
                rankSuffix = "th";
            }

            // Set lists of text
            rankText[i].text = (startingNdx + i + 1).ToString() + rankSuffix;
            nameText[i].text = highScores[startingNdx + i].name;
            scoreText[i].text = highScores[startingNdx + i].score.ToString();
            levelText[i].text = highScores[startingNdx + i].level.ToString();
            objectsText[i].text = highScores[startingNdx + i].objects.ToString();
            runTimeText[i].text = highScores[startingNdx + i].runTime;

            // Reset text color
            SetHighScoreColors(i, "RainbowTextWhite");
        }

        // 
        if (isCalledInStart) {
            //
            if (currentPageNdx == newHighScorePageNdx) {
                // Activate cursors 
                GameManager.utilities.SetActiveList(cursorGO, true);

                // Set cursor positions
                GameManager.utilities.PositionCursor(cursorGO[0], nameText[newHighScoreListNdx].gameObject, -430f, 65, 0);
                GameManager.utilities.PositionCursor(cursorGO[1], runTimeText[newHighScoreListNdx].gameObject, 246f, 65, 2);

                // Set new HighScore text color to rainbow cycle
                SetHighScoreColors(newHighScoreListNdx, "RainbowTextCycle");
            } else {
                // Deactivate cursors 
                GameManager.utilities.SetActiveList(cursorGO, false);
            }
        }

        // Set page text
        pageText.text = "Page: " + "<color=white>" + (pageNdx + 1).ToString() + "/10" + "</color>";
    }

    // Sets the colors of a single HighScore entry's text
    public void SetHighScoreColors(int scoreNdx, string clipName) {
        rankAnim[scoreNdx].CrossFade(clipName, 0);
        nameAnim[scoreNdx].CrossFade(clipName, 0);
        scoreAnim[scoreNdx].CrossFade(clipName, 0);
        levelAnim[scoreNdx].CrossFade(clipName, 0);
        objectsAnim[scoreNdx].CrossFade(clipName, 0);
        runTimeAnim[scoreNdx].CrossFade(clipName, 0);
    }

    //
    public void BackToMainMenuButton() {
        // Deactivate high score menu
        GameManager.S.highScoreMenuGO.SetActive(false);

        // Activate Main Menu
        GameManager.S.mainMenuGO.SetActive(true);

        // Stop particle systems from looping
        GameManager.S.confetti.IsLooping(false);

        //
        GameManager.S.previouslyHighlightedGO = null;

        // Reset player body & hand color to default
        GameManager.S.playerDamageColldierAnim.CrossFade("PlayerDamageColliderWhite", 0);
        GameManager.S.playerLeftHandAnim1.CrossFade("DefaultColor", 0);
        GameManager.S.playerLeftHandAnim2.CrossFade("DefaultColor", 0);
        GameManager.S.playerRightHandAnim1.CrossFade("DefaultColor", 0);
        GameManager.S.playerRightHandAnim2.CrossFade("DefaultColor", 0);
    }

    // Displays either the previous or next 10 entries of high scores
    public void GoToPreviousOrNextPage(int amountToChange) {
        //
        currentPageNdx += amountToChange;

        // Reset
        if(currentPageNdx > 9) {
            currentPageNdx = 0;
        }else if (currentPageNdx < 0) {
            currentPageNdx = 9;
        }

        UpdateHighScoreDisplay(currentPageNdx);
    }
}

//
public class HighScore {
    public string name;
    public int score;
    public int level;
    public int objects;
    public string runTime;

    // Metadata not visible on scoreboard
    public string date;
    public string time;
    public int alleyCount;
    public string playerHeight;
    public int fallBelowFloorCount;
    public int damageCount;
    public int pauseCount;

    public string startingObjectSpeed;
    public string amountToIncreaseObjectSpeed;
    public string startingSpawnSpeed;
    public string amountToDecreaseSpawnSpeed;

    public string chanceToSpawn0;
    public string chanceToSpawn1;
    public string chanceToSpawn2;
    public string chanceToSpawn3;
    public string chanceToSpawn4;
    public string chanceToSpawn5;
    public string chanceToSpawn6;

    public int objectToSpawn0;
    public int objectToSpawn1;
    public int objectToSpawn2;
    public int objectToSpawn3;
    public int objectToSpawn4;

    public HighScore(string _name = "", int _score = 0, int _level = 1, int _objects = 0, string _runTime = "00:00:00:000",
        string _date = "29 August, 1997", string _time = "12:00", int _alleyCount = 3, string _playerHeight = "1.68 m / 5 ft 6.14 in", int _fallBelowFloorCount = 0, int _damageCount = 0, int _pauseCount = 0,
        string _startingObjectSpeed = "10", string _amountToIncreaseObjectSpeed = "0.1", string _startingSpawnSpeed = "2", string _amountToDecreaseSpawnSpeed = "0.1",
        string _chanceToSpawn0 = "30%", string _chanceToSpawn1 = "35%", string _chanceToSpawn2 = "35%", string _chanceToSpawn3 = "50%", string _chanceToSpawn4 = "50%", string _chanceToSpawn5 = "75%", string _chanceToSpawn6 = "25%",
        int _objectToSpawn0 = 3, int _objectToSpawn1 = 0, int _objectToSpawn2 = 17, int _objectToSpawn3 = 43, int _objectToSpawn4 = 44) {
        name = _name;
        score = _score;
        level = _level;
        objects = _objects;
        runTime = _runTime;

        // Metadata not visible on scoreboard
        date = _date;
        time = _time + " UTC";
        alleyCount = _alleyCount;
        playerHeight = _playerHeight;
        fallBelowFloorCount = _fallBelowFloorCount;
        damageCount = _damageCount;
        pauseCount = _pauseCount;

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