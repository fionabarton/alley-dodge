// Please note that I, Fiona Barton, did not write all of this code.
// The more useful/trickier bits were sourced from:
// https://www.youtube.com/watch?v=ii31ObaAaJo
// https://www.youtube.com/watch?v=aSNj2nvSyD4

using System.IO;
using UnityEngine;

// Reads and writes save data to and from a currently unencrypted JSON file
public class SaveManager : MonoBehaviour {
    [Header("Set Dynamically")]
    public CryptographyManager  crypt;
    
    public SaveData             data;

    private string              persistentPath = "";

    private void Awake() {
        // Create save data
        data = new SaveData();

        // Set path
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";

        crypt = GetComponent<CryptographyManager>();
    }
    public void Save() {
        // Cache data displayed in game
        for (int i = 0; i < GameManager.S.highScore.highScores.Length; i++) {
            data.names[i] = GameManager.S.highScore.highScores[i].name;
            data.scores[i] = GameManager.S.highScore.highScores[i].score;
            data.levels[i] = GameManager.S.highScore.highScores[i].level;
            data.objects[i] = GameManager.S.highScore.highScores[i].objects;
            data.runTimes[i] = GameManager.S.highScore.highScores[i].runTime;

            data.dates[i] = GameManager.S.highScore.highScores[i].date;
            data.times[i] = GameManager.S.highScore.highScores[i].time;
            data.alleyCounts[i] = GameManager.S.highScore.highScores[i].alleyCount;
            data.playerHeights[i] = GameManager.S.highScore.highScores[i].playerHeight;
            data.fallBelowFloorCounts[i] = GameManager.S.highScore.highScores[i].fallBelowFloorCount;
            
            data.startingObjectSpeed[i] = GameManager.S.highScore.highScores[i].startingObjectSpeed;
            data.amountToIncreaseObjectSpeed[i] = GameManager.S.highScore.highScores[i].amountToIncreaseObjectSpeed;
            data.startingSpawnSpeed[i] = GameManager.S.highScore.highScores[i].startingSpawnSpeed;
            data.amountToDecreaseSpawnSpeed[i] = GameManager.S.highScore.highScores[i].amountToDecreaseSpawnSpeed;
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

    public void Load() {
        // If save data file exists
        if (File.Exists(persistentPath)) {
            // Read data from file
            using StreamReader reader = new StreamReader(persistentPath);
            string json = reader.ReadToEnd();

            // Decrypt data string
            json = crypt.Decrypt(json);

            // Create SaveData object from its JSON representation
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Display high score data in game UI
            for (int i = 0; i < GameManager.S.highScore.highScores.Length; i++) {
                GameManager.S.highScore.highScores[i].name = data.names[i];
                GameManager.S.highScore.highScores[i].score = data.scores[i];
                GameManager.S.highScore.highScores[i].level = data.levels[i];
                GameManager.S.highScore.highScores[i].objects = data.objects[i];
                GameManager.S.highScore.highScores[i].runTime = data.runTimes[i];

                GameManager.S.highScore.highScores[i].date = data.dates[i];
                GameManager.S.highScore.highScores[i].time = data.times[i];
                GameManager.S.highScore.highScores[i].alleyCount = data.alleyCounts[i];
                GameManager.S.highScore.highScores[i].playerHeight = data.playerHeights[i];
                GameManager.S.highScore.highScores[i].fallBelowFloorCount = data.fallBelowFloorCounts[i];

                GameManager.S.highScore.highScores[i].startingObjectSpeed = data.startingObjectSpeed[i];
                GameManager.S.highScore.highScores[i].amountToIncreaseObjectSpeed = data.amountToIncreaseObjectSpeed[i];
                GameManager.S.highScore.highScores[i].startingSpawnSpeed = data.startingSpawnSpeed[i];
                GameManager.S.highScore.highScores[i].amountToDecreaseSpawnSpeed = data.amountToDecreaseSpawnSpeed[i];
            }
        } else {
            // Display default high score data in game UI
            GameManager.S.highScore.SetHighScoresToDefaultValues();
        }
    }
}

//
public class SaveData {
    public string[] names = new string[100];
    public int[] scores = new int[100];
    public int[] levels = new int[100];
    public int[] objects = new int[100];
    public string[] runTimes = new string[100];

    public string[] dates = new string[100];
    public string[] times = new string[100];
    public int[] alleyCounts = new int[100];
    public float[] playerHeights = new float[100];
    public int[] fallBelowFloorCounts = new int[100];

    public string[] startingObjectSpeed = new string[100];
    public string[] amountToIncreaseObjectSpeed = new string[100];
    public string[] startingSpawnSpeed = new string[100];
    public string[] amountToDecreaseSpawnSpeed = new string[100];
}