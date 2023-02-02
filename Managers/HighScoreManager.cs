using System;
using System.Collections.Generic;
using UnityEngine;

// Stores and displays the top 100 high scores (name, score, level, objects, time)
public class HighScoreManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public TMPro.TextMeshProUGUI        titleText;
    public List<TMPro.TextMeshProUGUI>  rankText;
    public List<TMPro.TextMeshProUGUI>  nameText;
    public List<TMPro.TextMeshProUGUI>  scoreText;
    public List<TMPro.TextMeshProUGUI>  levelText;
    public List<TMPro.TextMeshProUGUI>  objectsText;
    public List<TMPro.TextMeshProUGUI>  timeText;
    public TMPro.TextMeshProUGUI        pageText;

    // Needed to highlight new high score text with cycling rainbow color animation clip
    public List<Animator>               rankAnim;
    public List<Animator>               nameAnim;
    public List<Animator>               scoreAnim;
    public List<Animator>               levelAnim;
    public List<Animator>               objectsAnim;
    public List<Animator>               timeAnim;

    public List<GameObject>             cursorGO;

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
    public void SetHighScoresToDefaultValues() {
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

        highScores[20] = new HighScore("20", 1, 1, 0, "00:00:00:000");
        highScores[21] = new HighScore("21", 1, 1, 0, "00:00:00:000");
        highScores[22] = new HighScore("22", 1, 1, 0, "00:00:00:000");
        highScores[23] = new HighScore("23", 1, 1, 0, "00:00:00:000");
        highScores[24] = new HighScore("24", 1, 1, 0, "00:00:00:000");
        highScores[25] = new HighScore("25", 1, 1, 0, "00:00:00:000");
        highScores[26] = new HighScore("26", 1, 1, 0, "00:00:00:000");
        highScores[27] = new HighScore("27", 1, 1, 0, "00:00:00:000");
        highScores[28] = new HighScore("28", 1, 1, 0, "00:00:00:000");
        highScores[29] = new HighScore("29", 1, 1, 0, "00:00:00:000");
        highScores[30] = new HighScore("30", 1, 1, 0, "00:00:00:000");
        highScores[31] = new HighScore("31", 1, 1, 0, "00:00:00:000");
        highScores[32] = new HighScore("32", 1, 1, 0, "00:00:00:000");
        highScores[33] = new HighScore("33", 1, 1, 0, "00:00:00:000");
        highScores[34] = new HighScore("34", 1, 1, 0, "00:00:00:000");
        highScores[35] = new HighScore("35", 1, 1, 0, "00:00:00:000");
        highScores[36] = new HighScore("36", 1, 1, 0, "00:00:00:000");
        highScores[37] = new HighScore("37", 1, 1, 0, "00:00:00:000");
        highScores[38] = new HighScore("38", 1, 1, 0, "00:00:00:000");
        highScores[39] = new HighScore("39", 1, 1, 0, "00:00:00:000");

        highScores[40] = new HighScore("40", 1, 1, 0, "00:00:00:000");
        highScores[41] = new HighScore("41", 1, 1, 0, "00:00:00:000");
        highScores[42] = new HighScore("42", 1, 1, 0, "00:00:00:000");
        highScores[43] = new HighScore("43", 1, 1, 0, "00:00:00:000");
        highScores[44] = new HighScore("44", 1, 1, 0, "00:00:00:000");
        highScores[45] = new HighScore("45", 1, 1, 0, "00:00:00:000");
        highScores[46] = new HighScore("46", 1, 1, 0, "00:00:00:000");
        highScores[47] = new HighScore("47", 1, 1, 0, "00:00:00:000");
        highScores[48] = new HighScore("48", 1, 1, 0, "00:00:00:000");
        highScores[49] = new HighScore("49", 1, 1, 0, "00:00:00:000");
        highScores[50] = new HighScore("50", 1, 1, 0, "00:00:00:000");
        highScores[51] = new HighScore("51", 1, 1, 0, "00:00:00:000");
        highScores[52] = new HighScore("52", 1, 1, 0, "00:00:00:000");
        highScores[53] = new HighScore("53", 1, 1, 0, "00:00:00:000");
        highScores[54] = new HighScore("54", 1, 1, 0, "00:00:00:000");
        highScores[55] = new HighScore("55", 1, 1, 0, "00:00:00:000");
        highScores[56] = new HighScore("56", 1, 1, 0, "00:00:00:000");
        highScores[57] = new HighScore("57", 1, 1, 0, "00:00:00:000");
        highScores[58] = new HighScore("58", 1, 1, 0, "00:00:00:000");
        highScores[59] = new HighScore("59", 1, 1, 0, "00:00:00:000");

        highScores[60] = new HighScore("60", 1, 1, 0, "00:00:00:000");
        highScores[61] = new HighScore("61", 1, 1, 0, "00:00:00:000");
        highScores[62] = new HighScore("62", 1, 1, 0, "00:00:00:000");
        highScores[63] = new HighScore("63", 1, 1, 0, "00:00:00:000");
        highScores[64] = new HighScore("64", 1, 1, 0, "00:00:00:000");
        highScores[65] = new HighScore("65", 1, 1, 0, "00:00:00:000");
        highScores[66] = new HighScore("66", 1, 1, 0, "00:00:00:000");
        highScores[67] = new HighScore("67", 1, 1, 0, "00:00:00:000");
        highScores[68] = new HighScore("68", 1, 1, 0, "00:00:00:000");
        highScores[69] = new HighScore("69", 1, 1, 0, "00:00:00:000");
        highScores[70] = new HighScore("70", 1, 1, 0, "00:00:00:000");
        highScores[71] = new HighScore("71", 1, 1, 0, "00:00:00:000");
        highScores[72] = new HighScore("72", 1, 1, 0, "00:00:00:000");
        highScores[73] = new HighScore("73", 1, 1, 0, "00:00:00:000");
        highScores[74] = new HighScore("74", 1, 1, 0, "00:00:00:000");
        highScores[75] = new HighScore("75", 1, 1, 0, "00:00:00:000");
        highScores[76] = new HighScore("76", 1, 1, 0, "00:00:00:000");
        highScores[77] = new HighScore("77", 1, 1, 0, "00:00:00:000");
        highScores[78] = new HighScore("78", 1, 1, 0, "00:00:00:000");
        highScores[79] = new HighScore("79", 1, 1, 0, "00:00:00:000");

        highScores[80] = new HighScore("80", 1, 1, 0, "00:00:00:000");
        highScores[81] = new HighScore("81", 1, 1, 0, "00:00:00:000");
        highScores[82] = new HighScore("82", 1, 1, 0, "00:00:00:000");
        highScores[83] = new HighScore("83", 1, 1, 0, "00:00:00:000");
        highScores[84] = new HighScore("84", 1, 1, 0, "00:00:00:000");
        highScores[85] = new HighScore("85", 1, 1, 0, "00:00:00:000");
        highScores[86] = new HighScore("86", 1, 1, 0, "00:00:00:000");
        highScores[87] = new HighScore("87", 1, 1, 0, "00:00:00:000");
        highScores[88] = new HighScore("88", 1, 1, 0, "00:00:00:000");
        highScores[89] = new HighScore("89", 1, 1, 0, "00:00:00:000");
        highScores[90] = new HighScore("90", 1, 1, 0, "00:00:00:000");
        highScores[91] = new HighScore("91", 1, 1, 0, "00:00:00:000");
        highScores[92] = new HighScore("92", 1, 1, 0, "00:00:00:000");
        highScores[93] = new HighScore("93", 1, 1, 0, "00:00:00:000");
        highScores[94] = new HighScore("94", 1, 1, 0, "00:00:00:000");
        highScores[95] = new HighScore("95", 1, 1, 0, "00:00:00:000");
        highScores[96] = new HighScore("96", 1, 1, 0, "00:00:00:000");
        highScores[97] = new HighScore("97", 1, 1, 0, "00:00:00:000");
        highScores[98] = new HighScore("98", 1, 1, 0, "00:00:00:000");
        highScores[99] = new HighScore("99", 1, 1, 0, "00:00:00:000");
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
            timeText[i].text = highScores[startingNdx + i].time;

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
                GameManager.utilities.PositionCursor(cursorGO[0], nameText[newHighScoreListNdx].gameObject, -8.25f, 0, 0);
                GameManager.utilities.PositionCursor(cursorGO[1], timeText[newHighScoreListNdx].gameObject, 3.5f, 0, 2);

                // Set new HighScore text color
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
        timeAnim[scoreNdx].CrossFade(clipName, 0);
    }

    //
    public void BackToMainMenuButton() {
        // Deactivate high score menu
        GameManager.S.highScoreMenuGO.SetActive(false);

        //
        GameManager.S.mainMenuGO.SetActive(true);

        //
        GameManager.S.previouslyHighlightedGO = null;
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
    public string   name;
    public int      score;
    public int      level;
    public int      objects;
    public string   time;

    // Metadata not visible on scoreboard
    public string   dateTime;
    public int      alleyCount;
    public float    playerHeight;
    public int      fallBelowFloorCount;

    public HighScore(string _name, int _score, int _level, int _objects, string _time,
        string _dateTime = "", int _alleyCount = 3, float _playerHeight = 168, int _fallBelowFloorCount = 0) {
        name = _name;
        score = _score;
        level = _level;
        objects = _objects;
        time = _time;

        // Metadata not visible on scoreboard
        dateTime = _dateTime;
        alleyCount = _alleyCount;
        playerHeight = _playerHeight;
        fallBelowFloorCount = _fallBelowFloorCount;
    }
}