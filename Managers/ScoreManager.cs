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

	public Image					displayMessageFrame;

	[Header("Set Dynamically")]
	public int						score = 0;
	public int						level = 1;
	public int						objectCount = 0;

	// Amount of collectibles needed to proceed to next level
	public int						amountToNextLevel = 5;

    private void Start() {
		// Display text
		//SetDisplayText("Let's go!", GameManager.color.alleyMaterial1.color, GameManager.color.alleyMaterial2.color);
		Invoke("StartDelay", 0.1f);
	}

	void StartDelay() {
		// Display text
		SetDisplayText("Let's go!", GameManager.color.alleyMaterial1.color, GameManager.color.alleyMaterial2.color);
	}

	// Display text message, then...
    public void SetDisplayText(string message, Color textColor, Color frameColor) {
		displayText.color = textColor;

		displayText.text = message;

		// Set frame color
		displayMessageFrame.color = frameColor;

		CancelInvoke();
		Invoke("DisplayAmountToNextLevel", 2);
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

		scoreText.text = "£" + score;

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

		// Increase speed (starts at 1.5f)
		GameManager.S.spawner.timeDuration -= 0.1f;

		// Increment level
		level += 1;
		levelText.text = "Level: " + level;

		// Reset amountToNextLevel 
		amountToNextLevel = 5;

		// Display text
		SetDisplayText("NEXT LEVEL!", GameManager.color.alleyMaterial1.color, GameManager.color.alleyMaterial2.color);
	}
}