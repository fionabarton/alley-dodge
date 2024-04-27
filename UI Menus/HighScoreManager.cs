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

    public List<string>                 names = new List<string>();
    void Start() {
        // Populate list of names for high score entries
        names = new List<string>() { "Bernise", "Mortimer", "Ruth", "Herbert", "Ingrid", "Murray", "Thelma", "Martha", "Humphrey", "Norman",
            "Mavis", "Wilbert", "Doris", "Orville", "Maude", "Harold", "Melvin", "Ethel", "Dilmore", "Gertrude",
            "Harriet", "Otis", "Trudy", "Alfred", "Hilda", "Rutherford", "Gladys", "Sherman", "Mabel", "Deforest",
            "Opal", "Edmund", "Bernadette", "Peyton", "Etta", "Silas", "Louise", "Felix", "Ophelia", "Horace",
            "Marjorie", "Reginald", "Helga", "Mortimer", "Ursula", "Cassius", "Millie", "Homer", "Calliope", "Milo",
            "Caldonia", "Edgar", "Blanche", "Rufus", "Betty", "Llewellyn", "Ramona", "Langston", "Henrietta", "Gilbert",
            "Bertie", "Holden", "Maude", "Ignatius", "Drusilla", "Thaddeus", "Wilma", "Percy", "Fanny", "Atlas",
            "Shirley", "Neville", "Liza", "Brooks", "Posey", "Hugo", "Sibyl", "Archibald", "Glenda", "Stanley",
            "Elaine", "Atticus", "Daphne", "Francis", "Susannah", "Rawlins", "Claribel", "Omar", "Astrid", "Waldo",
            "Lucille", "Cornelius", "Darcy", "Gil", "Sandra", "Quincy", "Lucretia", "Chester", "Margaret", "Ansel",
            "Agatha", "Jasper", "Hester", "Amos", "Hazel", "Rollo", "Agnes", "Demetrius", "Pollyanna", "Tobias" };

        // Initialize array of high scores
        highScores = new HighScore[100];

        //
        SetHighScoresToDefaultValues();

        gameObject.SetActive(false);
    }

    private void OnEnable() {
        // Display text
        if (Time.time > 0.01f) {
            delayedTextDisplay.DisplayText("Welcome to the high score menu:\nView the high score leaderboard\nand detailed information for each entry.", true);
        }
    }

    // Deletes all saved high scores and resets them to default values 
    public void SetHighScoresToDefaultValues(bool saveData = false) {
        // Populate list of high score entries
        int highestScore = 75;
        int highestObjects = 200;
        int highestTime = 360;
        for (int i = 0; i < highScores.Length; i++) {
            highScores[i] = new HighScore(names[i], highestScore, (int)((highestScore * 0.2f) + 1) , highestObjects, GameManager.S.score.GetTime(highestTime));

            // Decrement topScore
            if(highestScore > 1) {
                highestScore -= 1;
            }

            // Decrement topObjects
            if (highestObjects > 0) {
                highestObjects -= 2;
            }

            // Decrement topTime
            if (highestTime > 0) {
                highestTime -= 3;
            }
        }

        // Save data
        if (saveData) {
            GameManager.save.Save();

            UpdateHighScoreDisplay(currentPageNdx);
        }
    }

    public void ResetNewHighScoreNdx() {
        // Reset new high score text color
        for (int i = 0; i < 10; i++) {
            SetHighScoreColors(i, "RainbowTextWhite");
        }

        // Deactivate new high score cursors 
        GameManager.utilities.SetActiveList(cursorGO, false);

        // Reset new high score index to default value
        newHighScoreNdx = -1;
        newHighScoreListNdx = -1;
        newHighScorePageNdx = -1;
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
        //titleText.text = "Top 100 High Scores " + "(" + (startingNdx + 1).ToString() + "-" + (startingNdx + 10).ToString() + ")";

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
        pageText.text = "Page: " + "<color=#D9D9D9>" + (pageNdx + 1).ToString() + "/10" + "</color>";
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
    public string chanceToSpawn7;
    public string chanceToSpawn8;
    public string chanceToSpawn9;

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

    public HighScore(string _name = "", int _score = 0, int _level = 1, int _objects = 0, string _runTime = "00:00:00:000",
        string _date = "29 August, 1997", string _time = "12:00", int _alleyCount = 3, string _playerHeight = "1.68 m / 5 ft 6.14 in", int _fallBelowFloorCount = 0, int _damageCount = 0, int _pauseCount = 0,
        string _startingObjectSpeed = "6", string _amountToIncreaseObjectSpeed = "1", string _startingSpawnSpeed = "20", string _amountToDecreaseSpawnSpeed = "3",
        string _chanceToSpawn0 = "20%", string _chanceToSpawn1 = "20%", string _chanceToSpawn2 = "20%", string _chanceToSpawn3 = "30%", string _chanceToSpawn4 = "10%", string _chanceToSpawn5 = "0%", string _chanceToSpawn6 = "0%", string _chanceToSpawn7 = "0%", string _chanceToSpawn8 = "0%", string _chanceToSpawn9 = "0%",
        int _objectToSpawn0 = 7, int _objectToSpawn1 = 0, int _objectToSpawn2 = 20, int _objectToSpawn3 = 47, int _objectToSpawn4 = 48, int _objectToSpawn5 = 50, int _objectToSpawn6 = 50, int _objectToSpawn7 = 50, int _objectToSpawn8 = 50, int _objectToSpawn9 = 50) {
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