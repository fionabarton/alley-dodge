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

	// Stores amount of time game was paused, then adds it to timer when game is unpaused
	private float					timePaused;

	private void FixedUpdate() {
		// Display time
        if (timerIsOn) {
			timeText.text = GetTime(Time.time);
		}
    }

	// Display text message, then...
	public void SetDisplayText(string message, Color textColor, Color frameColor, eVOX vox = eVOX.voxNull, bool invokeFunction = true) {
		displayText.color = textColor;

		displayText.text = message;

		// Set frame color
		displayMessageFrame.color = frameColor;

		// Play VOX audio clip
		if(vox != eVOX.voxNull) {
			GameManager.audioMan.PlayVOXClip(vox);
		}

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

		// Play VOX audio clip
		string voxEnumName = "vox" + amountToNextLevel.ToString() + "ToGo";
		eVOX vox = (eVOX)Enum.Parse(typeof(eVOX), voxEnumName);
		GameManager.audioMan.PlayVOXClip(vox);
	}

	// Amount of monetary pickups collected
	public void AddToScore(int amount = 1) {
		score += amount;

		scoreText.text = "Score: <color=white>" + score;

		amountToNextLevel -= 1;

		// Every 10 points, go to next level
		if (score % 5 == 0) {
			// Go to next level
			InitializeNextLevel();
		} else {
			SetDisplayText(GameManager.words.GetRandomExclamation(true) + "!", Color.yellow, Color.yellow);
		}
	}

	// Amount of total objects spawned
	public void AddToObjectCount(int amount = 1) {
		objectCount += amount;

		objectCountText.text = "Objects: <color=white>" + objectCount;
	}

	// Set up for next level
	void InitializeNextLevel() {
		// Set alley colors
		GameManager.color.GetNewColorPalette();

		// Play confetti particle systems
		GameManager.S.confetti.DropConfetti();

		// Decrease spawn speed (starts at 2.0f)
		GameManager.S.spawner.currentSpawnSpeed = GameManager.S.GetIncreasedSpawnSpeedLevel();

        // Prevent decreasing spawn speed below 0
        if (GameManager.S.spawner.currentSpawnSpeed <= 0.1f) {
			GameManager.S.spawner.currentSpawnSpeed = 0.1f;
        }

		// Increase object speed (starts at 0)
		GameManager.S.spawner.currentObjectSpeed += GameManager.S.spawner.amountToIncreaseObjectSpeed;

		// Increment level
		level += 1;
		levelText.text = "Level: <color=white>" + level;

        // Set smoke particle system starting size based on the number of the level currently being played
        GameManager.S.PlaySmokeParticelSystemAndSetSize();

        // Reset amountToNextLevel 
        amountToNextLevel = 5;

		// Display text
		SetDisplayText("NEXT LEVEL!", GameManager.color.alleyMaterial1.color, GameManager.color.alleyMaterial2.color, eVOX.voxNextLevel);
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

	public void PauseTimer() {
		// Cache timePaused
		timePaused = Time.time;
		timerIsOn = false;
	}

	public void UnpauseTimer() {
		// Add amount of time that's passed since timer was paused 
		startingTime += Time.time - timePaused;
		timerIsOn = true;
	}

	// Resets script properties to their default values
	public void ResetScore() {
		score = 0;
		objectCount = 0;
		startingTime = 0f;
		endingTime = 0f;
		amountToNextLevel = 5;
	}

	// Updates score and level GUI
	public void UpdateGUI() {
		scoreText.text = "Score: <color=white>" + score;
		levelText.text = "Level: <color=white>" + level;
	}

	// Display UI countdown from 3 to 1
	public IEnumerator Countdown(int count = 4) {
		//
		if (count == 3) {
			GameManager.S.fallBelowFloorCount += 1;
		}

		// Set text color
		if (count == 4) {
			displayText.text = "GET\nREADY";
			displayText.color = GameManager.color.alleyMaterial1.color;
			GameManager.audioMan.PlayVOXClip(eVOX.voxGetReady);

			// Activate exit run menu podium
			GameManager.S.podiums.ActivateMenus(true, false);
		} else if(count == 3) {
			displayText.text = "3";
			displayText.color = Color.red;
			GameManager.audioMan.PlayUISFXClip(eSFX.sfxQuid1);
			GameManager.audioMan.PlayVOXClip(eVOX.vox3);
		} else if (count == 2) {
			displayText.text = "2";
			displayText.color = new Color(1.0f, 0.35f, 0.0f);
			GameManager.audioMan.PlayUISFXClip(eSFX.sfxQuid1);
			GameManager.audioMan.PlayVOXClip(eVOX.vox2);
		} else if (count == 1) {
			displayText.text = "1";
			displayText.color = Color.yellow;
			GameManager.audioMan.PlayUISFXClip(eSFX.sfxQuid1);
			GameManager.audioMan.PlayVOXClip(eVOX.vox1);
		} else if (count == 0) {
			displayText.text = "GO!";
			displayText.color = Color.green;
			GameManager.audioMan.PlayUISFXClip(eSFX.sfxQuid5);
			GameManager.audioMan.PlayVOXClip(eVOX.voxGo);
		}

		// If the countdown is done...
		if (count == -1) {
			// Display text: amount to next level
			DisplayAmountToNextLevel();

			// Unfreeze objects and restart spawner
			GameManager.S.spawner.UnpauseSpawner();

			GameManager.S.playerIsInvincible = false;

			// Reset exit run button positions and allow them to be pressed again
			GameManager.S.exitRunButtonLeftCS.ResetButton();
			GameManager.S.exitRunButtonRightCS.ResetButton();

			// Unpause ScoreManager timer
			UnpauseTimer();
		} else {
			// Wait for 1 second and restart this coroutine
			yield return new WaitForSeconds(1);
			StartCoroutine(Countdown(count - 1));
		}
	}
}