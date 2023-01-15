using System;
using System.Collections.Generic;
using UnityEngine;

// Stores and displays the top 20 high scores (name, score, level, objects, time)
public class HighScoreManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public List<TMPro.TextMeshProUGUI>  rankText;
    public List<TMPro.TextMeshProUGUI>  nameText;
    public List<TMPro.TextMeshProUGUI>  scoreText;
    public List<TMPro.TextMeshProUGUI>  levelText;
    public List<TMPro.TextMeshProUGUI>  objectsText;
    public List<TMPro.TextMeshProUGUI>  timeText;

    public List<GameObject>             cursorGO;

    [Header("Set Dynamically")]
    public HighScore[]                  highScores;

    // Index at which to store a new HighScore in the 'highScores' array
    public int                          newHighScoreNdx = 0;

    void Start() {
        // Initialize array of high scores
        highScores = new HighScore[20];
        highScores[0] = new HighScore("Fiona", 75, 16, 0, "00:01:52.629");
        highScores[1] = new HighScore("Tom", 45, 10, 0, "00:03:18.875");
        highScores[2] = new HighScore("Chani", 42, 9, 0, "00:01:52.629");
        highScores[3] = new HighScore("Mill", 40, 9, 0, "00:01:52.629");
        highScores[4] = new HighScore("Steve", 35, 8, 0, "00:03:18.875");
        highScores[5] = new HighScore("Mr. Bagli", 34, 8, 0, "00:03:18.875");
        highScores[6] = new HighScore("Popcorn", 33, 8, 0, "00:03:18.875");
        highScores[7] = new HighScore("Little Max", 32, 8, 0, "00:03:18.875");
        highScores[8] = new HighScore("Barley Corn", 31, 8, 0, "00:00:00:000");
        highScores[9] = new HighScore("McGhee", 30, 7, 0, "00:00:00:000");
        highScores[10] = new HighScore("Jj", 10, 3, 0, "00:00:00:000");
        highScores[11] = new HighScore("Ii", 9, 2, 0, "00:00:00:000");
        highScores[12] = new HighScore("Hh", 8, 2, 0, "00:00:00:000");
        highScores[13] = new HighScore("Gg", 7, 2, 0, "00:00:00:000");
        highScores[14] = new HighScore("Ff", 6, 2, 0, "00:00:00:000");
        highScores[15] = new HighScore("Ee", 5, 2, 0, "00:00:00:000");
        highScores[16] = new HighScore("Dd", 4, 1, 0, "00:00:00:000");
        highScores[17] = new HighScore("Cc", 3, 1, 0, "00:00:00:000");
        highScores[18] = new HighScore("Bb", 2, 1, 0, "00:00:00:000");
        highScores[19] = new HighScore("Aa", 1, 1, 0, "00:00:00:000");

        //
        UpdateHighScoreDisplay();

        gameObject.SetActive(false);
    }

    // Checks if the score is a new high score,
    // then returns at what index it belongs in the highScores array
    public bool CheckForNewHighScore(int score) {
        for(int i = 0; i < highScores.Length; i++) {
            if(score > highScores[i].score) {
                // Set newHighScoreNdx
                newHighScoreNdx = i;
                return true;
            }
        }
        return false;
    }

    //
    public void AddNewHighScore(HighScore newScore) {
        // Initialize new array
        HighScore[] tScores = new HighScore[20];

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

        //
        UpdateHighScoreDisplay();

        // Activate cursors 
        GameManager.utilities.SetActiveList(cursorGO, true);

        // Set cursor positions
        GameManager.utilities.PositionCursor(cursorGO[0], nameText[newHighScoreNdx].gameObject, -8.25f, 0, 0);
        GameManager.utilities.PositionCursor(cursorGO[1], timeText[newHighScoreNdx].gameObject, 3.5f, 0, 2);

        // Set new HighScore text color
        SetHighScoreColors(newHighScoreNdx, Color.yellow);

        // Reset score for next game
        GameManager.S.score.ResetScore();
    }

    //
    public void UpdateHighScoreDisplay() {
        //
        for (int i = 0; i < highScores.Length; i++) {
            //
            nameText[i].text = highScores[i].name;
            scoreText[i].text = highScores[i].score.ToString();
            levelText[i].text = highScores[i].level.ToString();
            objectsText[i].text = highScores[i].objects.ToString();
            timeText[i].text = highScores[i].time;

            // Reset text color
            SetHighScoreColors(i, Color.white);
        }
    }

    // Sets the colors of a single HighScore entry's text
    void SetHighScoreColors(int scoreNdx, Color color) {
        rankText[scoreNdx].color = color;
        nameText[scoreNdx].color = color;
        scoreText[scoreNdx].color = color;
        levelText[scoreNdx].color = color;
        objectsText[scoreNdx].color = color;
        timeText[scoreNdx].color = color;
    }

    //
    public void BackToMainMenuButton() {
        // Deactivate high score menu
        GameManager.S.highScoreMenuGO.SetActive(false);

        // Activate keyboard input menu
        GameManager.S.startGameMenuGO.SetActive(true);

        //
        GameManager.S.previouslyHighlightedGO = null;
    }
}

//
public class HighScore {
    public string name;
    public int score;
    public int level;
    public int objects;
    public string time;

    public HighScore(string _name, int _score, int _level, int _objects, string _time) {
        name = _name;
        score = _score;
        level = _level;
        objects = _objects;
        time = _time;
    }
}