// Please note that I, Fiona Barton, did not write all of this code.
// The more useful/trickier bits were sourced from:
// https://www.youtube.com/watch?v=ii31ObaAaJo

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Reads and writes save data to and from a currently unencrypted JSON file
public class SaveManager : MonoBehaviour {
    public SaveData data;

    private string file = "gdfyrt5y5yddddg.txt";

    private void Awake() {
        data = new SaveData();
    }

    //////////////////////////////////////////////////////////////////////////////////////
    //
    public void SaveData() {
        // Cache data displayed in game
        for (int i = 0; i < GameManager.S.highScore.highScores.Length; i++) {
            data.names[i] = GameManager.S.highScore.highScores[i].name;
            data.scores[i] = GameManager.S.highScore.highScores[i].score;
            data.levels[i] = GameManager.S.highScore.highScores[i].level;
            data.objects[i] = GameManager.S.highScore.highScores[i].objects;
            data.times[i] = GameManager.S.highScore.highScores[i].time;

            data.dateTimes[i] = GameManager.S.highScore.highScores[i].dateTime;
            data.alleyCounts[i] = GameManager.S.highScore.highScores[i].alleyCount;
            data.playerHeights[i] = GameManager.S.highScore.highScores[i].playerHeight;
            data.fallBelowFloorCounts[i] = GameManager.S.highScore.highScores[i].fallBelowFloorCount;
        }

        // Save cached data to file
        string json = JsonUtility.ToJson(data);
        WriteToFile(file, json);
    }

    //
    public void LoadData() {
        // Load data from file
        data = new SaveData();
        string json = ReadFromFile(file);
        JsonUtility.FromJsonOverwrite(json, data);

        // If data file exists,
        // Display data loaded from file
        if(json != "") {
            for (int i = 0; i < GameManager.S.highScore.highScores.Length; i++) {
                GameManager.S.highScore.highScores[i].name = data.names[i];
                GameManager.S.highScore.highScores[i].score = data.scores[i];
                GameManager.S.highScore.highScores[i].level = data.levels[i];
                GameManager.S.highScore.highScores[i].objects = data.objects[i];
                GameManager.S.highScore.highScores[i].time = data.times[i];

                GameManager.S.highScore.highScores[i].dateTime = data.dateTimes[i];
                GameManager.S.highScore.highScores[i].alleyCount = data.alleyCounts[i];
                GameManager.S.highScore.highScores[i].playerHeight = data.playerHeights[i];
                GameManager.S.highScore.highScores[i].fallBelowFloorCount = data.fallBelowFloorCounts[i];
            }
        } else {
            //
            GameManager.S.highScore.SetHighScoresToDefaultValues();
        }
    }

    //
    public void WriteToFile(string fileName, string json) {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using(StreamWriter writer = new StreamWriter(fileStream)) {
            writer.Write(json);
            writer.Flush();
            writer.Close();
        }
    }

    //
    public string ReadFromFile(string fileName) {
        string path = GetFilePath(fileName);
        if (File.Exists(path)) {
            using(StreamReader reader = new StreamReader(path)) {
                string json = reader.ReadToEnd();
                return json;
            }
        } else {
            Debug.LogWarning("File not found!");
        }
        return "";
    }

    // Returns the saved data file's path
    private string GetFilePath(string fileName) {
        //Debug.Log(Application.persistentDataPath + "/" + fileName);
        return Application.persistentDataPath + "/" + fileName;
    }
}

//
public class SaveData {
    public string[] names = new string[100];
    public int[] scores = new int[100];
    public int[] levels = new int[100];
    public int[] objects = new int[100];
    public string[] times = new string[100];

    public string[] dateTimes = new string[100];
    public int[] alleyCounts = new int[100];
    public float[] playerHeights = new float[100];
    public int[] fallBelowFloorCounts = new int[100];
}