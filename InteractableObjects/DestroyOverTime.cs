using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroys this object within a specified amount of seconds
public class DestroyOverTime : MonoBehaviour {
	[Header("Set in Inspector")]
	public bool		destroyRatherThanSetActive;
	public float	timeDuration = 1f;

	[Header("Set Dynamically")]
	private float	timePaused;
	private float	timeDone;

	private bool	timeIsPaused;

	void OnEnable() {
		timeDone = timeDuration + Time.time;

		StartCoroutine("FixedUpdateCoroutine");
	}

	public IEnumerator FixedUpdateCoroutine() {
        if (!timeIsPaused) {
			if (timeDone <= Time.time) {
				if (destroyRatherThanSetActive) {
					Destroy(gameObject);
				} else {
					gameObject.SetActive(false);
				}
			}
		} 

		yield return new WaitForFixedUpdate();
		StartCoroutine("FixedUpdateCoroutine");
	}

	//
	public void PauseTimer() {
		// Cache timePaused
		timePaused = Time.time;

		// Pause timer
		timeIsPaused = true;
	}

	//
	public void UnpauseTimer() {
		// Add amount of time that's passed since timer was paused 
		timeDone += (Time.time - timePaused);

		// Unpause timer 
		timeIsPaused = false;
	}
}