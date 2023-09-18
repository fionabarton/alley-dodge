using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Displays a delayed text message,
// each word of which is incrementally displayed every 0.05 seconds
public class DelayedTextDisplay : MonoBehaviour {
	[Header("Set in Inspector")]
	public Text			message; // named "Text" in the Hierarchy/Inspector
	public GameObject	cursorGO;

	[Header("Set Dynamically")]
	public bool			dialogueFinished = true;

	public void DisplayText(string text, bool waitForTextToFinish = false) {
        if (waitForTextToFinish) {
			GameManager.S.waitForDialogueToFinish = waitForTextToFinish;
		}

		gameObject.SetActive(true);

		StopAllCoroutines();

		StopCoroutine("DisplayTextCo");
		StartCoroutine(DisplayTextCo(text));
	}
	IEnumerator DisplayTextCo(string text) {
		// Deactivate Cursor
		cursorGO.SetActive(false);

		// Reset Text Strings
		string dialogueSentences = null;

		dialogueFinished = false;

		// Split text argument w/ blank space
		string[] dialogueWords = text.Split(' ');
		// Display text one word at a time
		for (int i = 0; i < dialogueWords.Length; i++) {
			dialogueSentences += dialogueWords[i] + " ";
			message.text = dialogueSentences;
			yield return new WaitForSeconds(0.05f);
		}

		// Activate cursor
		cursorGO.SetActive(true);

		// Dialogue Finished
		dialogueFinished = true;

		GameManager.S.waitForDialogueToFinish = false;
	}
}