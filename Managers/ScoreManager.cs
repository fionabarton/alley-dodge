using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Tracks and displays the user's score, current level, amount of total objects spawned, and
// amount of quid needed to progress to the next level.
public class ScoreManager : MonoBehaviour {
	[Header("Set in Inspector")]
	public TMPro.TextMeshProUGUI	displayText;

	public TMPro.TextMeshProUGUI	scoreText;
	public TMPro.TextMeshProUGUI	objectCountText;
	public TMPro.TextMeshProUGUI	levelText;
	public TMPro.TextMeshProUGUI	timeText;

	public Image					displayMessageFrame;

	[Header("Set Dynamically")]
	public int						score = 0;
	public int						level = 1;
	public int						objectCount = 0;
	public float					startingTime = 0f;
	public float					endingTime = 0f;

	// Amount of collectibles needed to proceed to next level
	public int						amountToNextLevel = 5;

	// Dictates whether timerText is updated or not
	public bool						timerIsOn;

    private void FixedUpdate() {
		// Display time
        if (timerIsOn) {
			timeText.text = "Time:\n" + GetTime(Time.time);
		}
    }

    // Display text message, then...
    public void SetDisplayText(string message, Color textColor, Color frameColor, bool invokeFunction = true) {
		displayText.color = textColor;

		displayText.text = message;

		// Set frame color
		displayMessageFrame.color = frameColor;

		CancelInvoke();

		if (invokeFunction) {
			Invoke("DisplayAmountToNextLevel", 2);
		}
    }

	// Display amount of collectibles needed to proceed to next level
	void DisplayAmountToNextLevel() {
		displayText.color = GameManager.color.alleyMaterial1.color;
		displayMessageFrame.color = GameManager.color.alleyMaterial2.color;

		displayText.text = amountToNextLevel + "\nTO GO!";
	}

    // Amount of monetary pickups collected
    public void AddToScore(int amount = 1) {
		score += amount;

		scoreText.text = "Score: " + score;

		amountToNextLevel -= 1;

		// Every 10 points, go to next level
		if (score % 5 == 0) {
			// Go to next level
			InitializeNextLevel();
		} else {
			SetDisplayText(GameManager.words.GetRandomExclamation() + "!", Color.yellow, Color.yellow);
		}
	}

	// Amount of total objects spawned
	public void AddToObjectCount(int amount = 1) {
		objectCount += amount;

		objectCountText.text = "Objects: " + objectCount;
	}

	// Set up for next level
	void InitializeNextLevel() {
		// Set alley colors
		GameManager.color.GetNewColorPalette();

		// Play confetti particle systems
		GameManager.S.confetti.DropConfetti();

		// Increase speed (starts at 2.0f)
		GameManager.S.spawner.timeDuration -= 0.1f;

		// Increment level
		level += 1;
		levelText.text = "Level: " + level;

		// Reset amountToNextLevel 
		amountToNextLevel = 5;

		// Display text
		SetDisplayText("NEXT LEVEL!", GameManager.color.alleyMaterial1.color, GameManager.color.alleyMaterial2.color);
	}

	// Get and return total time in '00:00:00:000' format
	public string GetTime(float _endingTime) {
		// Get time in seconds
		float time = _endingTime - startingTime;

		// Convert time to '00:00:00:000' format
		TimeSpan t = TimeSpan.FromSeconds(time);
		string timeString = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}",
		t.Hours,
		t.Minutes,
		t.Seconds,
		t.Milliseconds);

		// Return string
		return timeString;
	}

	// Resets script properties to their default values
	public void ResetScore() {
		score = 0;
		//level = GameManager.S.me
		objectCount = 0;
		startingTime = 0f;
		endingTime = 0f;
		amountToNextLevel = 5;
	}

	// Updates score and level GUI
	public void UpdateGUI() {
		scoreText.text = "Score: " + score;
		levelText.text = "Level: " + level;
	}
}