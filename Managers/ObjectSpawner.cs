using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instantiates a stream of randomly selected hazards/pickups at a constant rate.
public class ObjectSpawner : MonoBehaviour {
	[Header("Set in Inspector")]
	public GameObject		shieldPickup;
	public GameObject		quidPickup;

	public List<GameObject> objects;

	public GameObject		horizontalDestruction;
	public GameObject		verticalHighDestruction;
	public GameObject		verticalLowDestruction;

	// Amount of time until next object is spawned
	public float			startingSpawnSpeed = 2.0f;
	public float			currentSpawnSpeed = 2.0f;

	// Speed at which objects travel down the alley
	public float			startingObjectSpeed = 5;
	public float			currentObjectSpeed = 5;

	public float			cachedObjectSpeed = 5;
	public float			cachedSpawnSpeed = 2.0f;

	// Amount to increase per level
	public float			amountToDecreaseSpawnSpeed = 0.1f;
	public float			amountToIncreaseObjectSpeed = 0.2f;

	// Controls whether ALL objects (hazards, pickups, etc.) can move
	public bool				objectsCanMove = true;

	// Controls whether script can spawn objects
	public bool				canSpawn = true;

	[Header("Set Dynamically")]
	public float			timeDone;

	// Dictates the range objects are spawned on the x-axis,
	// their values are affected by the value of alleyCount
	public int				minXPos;
	public int				maxXPos;

	// Adjusted based on player height
	public float			pickupMaxYPos = 5.5f;

	//
	public List<int>		objectsToSpawn;
	public List<float>		chancesToSpawn;

	// Stores amount of time game was paused, then adds it to timer when game is unpaused
	private float			timePaused;

	// On game start, ensure this is set to -1
	public int				previousObjectNdx = -1;

	private void Start() {
		timeDone = currentSpawnSpeed + Time.time; 
	}

    private void FixedUpdate() {
		if (canSpawn) {
			if (timeDone <= Time.time) {
				InstantiateObject();
				timeDone = currentSpawnSpeed + Time.time;
			}
		}
	}

	// Freeze objects and stop spawner
	public void PauseSpawner() {
		// Cache timePaused
		timePaused = Time.time;

		// Freeze objects and stop spawner
		GameManager.S.spawner.canSpawn = false;
		GameManager.S.spawner.objectsCanMove = false;
	}

	/// Unfreeze objects and restart spawner
	public void UnpauseSpawner() {
		// Add amount of time that's passed since timer was paused 
		timeDone += Time.time - timePaused;

		// Unfreeze objects and restart spawner
		GameManager.S.spawner.canSpawn = true;
		GameManager.S.spawner.objectsCanMove = true;
	}

	// Set the scale of all cube-shaped obstacles, but NOT the items (yellow quid, blue shield)
	public void SetObstacleScale(float xScale, float yScale) {
		for(int i = 0; i < objects.Count - 2; i++) {
			if (xScale == -1) {
				// Set xScale to its current value
				xScale = objects[i].transform.localScale.x;
			}

			// Set the object's scale
			GameManager.utilities.SetScale(objects[i], xScale, yScale, 1);
		}
    }

	////////////////////////////////////////////////////////////////////////////////////////
	// Object instantiation functions
	////////////////////////////////////////////////////////////////////////////////////////
	
	// Instantiate a random hazard or pickup
	void InstantiateObject() {
        // Get random value
        float randomVal = Random.value;

        // Convert 'chancesToSpawn' list to work for this implementation
        List<float> chanceValues = new List<float> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		float total = 0;
		for(int i = 0; i < chancesToSpawn.Count; i++) {
			// Increment total amount
			total += chancesToSpawn[i];

			// Assign total amount to this element
			chanceValues[i] = total;
        }

		// Randomly instantiate 1 of the 10 possible objects to spawn
		if (randomVal >= 0 && randomVal <= chanceValues[0]) {
			InstantiateObject(objectsToSpawn[0]);
		} else if (randomVal > chanceValues[0] && randomVal <= chanceValues[1]) {
			InstantiateObject(objectsToSpawn[1]);
		} else if (randomVal > chanceValues[1] && randomVal <= chanceValues[2]) {
			InstantiateObject(objectsToSpawn[2]);
		} else if (randomVal > chanceValues[2] && randomVal <= chanceValues[3]) {
			InstantiateObject(objectsToSpawn[3]);
		} else if (randomVal > chanceValues[3] && randomVal <= chanceValues[4]) {
			InstantiateObject(objectsToSpawn[4]);
		} else if (randomVal > chanceValues[4] && randomVal <= chanceValues[5]) {
			InstantiateObject(objectsToSpawn[5]);
		} else if (randomVal > chanceValues[5] && randomVal <= chanceValues[6]) {
			InstantiateObject(objectsToSpawn[6]);
		} else if (randomVal > chanceValues[6] && randomVal <= chanceValues[7]) {
			InstantiateObject(objectsToSpawn[7]);
		} else if (randomVal > chanceValues[7] && randomVal <= chanceValues[8]) {
			InstantiateObject(objectsToSpawn[8]);
		} else if (randomVal > chanceValues[8] && randomVal <= chanceValues[9]) {
			InstantiateObject(objectsToSpawn[9]);
		}
	}

	// Returns true if any of the objectsToSpawn slots are set to 100%
	bool checkIfMultipleObjectsCanBeSpawned() {
		for(int i = 0; i < chancesToSpawn.Count; i++) {
			if(chancesToSpawn[i] >= 1.0f) {
				return false;
			}
        }
		return true;
    }

	//
	bool checkIfSingleObjectToSpawn() {
		int previousNdx = -1;
		
		for (int i = 0; i < objectsToSpawn.Count; i++) {
			if (objectsToSpawn[i] != previousNdx) {
				//
				previousNdx = objectsToSpawn[i];
				return false;
			}
		}
		return true;
	}

	public bool checkIfObjectToSpawnHasDuplicates(int ndx, int buttonNdx) {
		bool tBool = false;
		
		for (int i = 0; i < objectsToSpawn.Count; i++) {
			// Skip over selected object to spawn slot
			if (i != buttonNdx) {
				if (objectsToSpawn[i] == ndx) {
					tBool = true;
				}
			}
		}

		return tBool;
	}

	void InstantiateObject(int objectNdx) {
        // If more than one object can be spawned...
        if (checkIfMultipleObjectsCanBeSpawned() && checkIfSingleObjectToSpawn()) {
			// ...and if the previously spawned object is not the same as the current one...
			if(objectNdx != previousObjectNdx) {
				// Instantiate object
				if (objectNdx == 3) {
					InstantiateRandomHorizontalBlock();
				} else if (objectNdx == 7) {
					InstantiateRandomVerticalBlock();
				} else if (objectNdx == 47) {
					InstantiateQuidPickup();
				} else if (objectNdx == 48) {
					InstantiateShieldPickup();
				} else if (objectNdx == 46) {
					InstantiateRandomObstacle();
				} else if (objectNdx == 49) {
					InstantiateRandomItem();
				} else if (objectNdx == 50) {
					// Nothing!
                } else {
					Instantiate(objects[objectNdx], new Vector3(0, 0, 40), transform.rotation);
				}

				// Cache previous object index
				previousObjectNdx = objectNdx;

				// Add to object count
				GameManager.S.score.AddToObjectCount();
			} else {
				// If the previously spawned object is the same as the current one, try again
				InstantiateObject();
			}
        } else {
			// Instantiate object
			if (objectNdx == 3) {
				InstantiateRandomHorizontalBlock();
			} else if (objectNdx == 7) {
				InstantiateRandomVerticalBlock();
			} else if (objectNdx == 47) {
				InstantiateQuidPickup();
			} else if (objectNdx == 48) {
				InstantiateShieldPickup();
			} else if (objectNdx == 46) {
				InstantiateRandomObstacle();
			} else if (objectNdx == 49) {
				InstantiateRandomItem();
			} else if (objectNdx == 50) {
                // Nothing!
            } else {
				Instantiate(objects[objectNdx], new Vector3(0, 0, 40), transform.rotation);
			}

			// Cache previous object index
			previousObjectNdx = objectNdx;

			// Add to object count
			GameManager.S.score.AddToObjectCount();
		}
	}

	void InstantiateRandomObstacle() {
		// Get random index
		int ndx = Random.Range(0, 43);

		Instantiate(objects[ndx], new Vector3(0, 0, 40), transform.rotation);
	}

	void InstantiateRandomItem() {
		if(Random.value > 0.5f) {
			InstantiateQuidPickup();
		} else {
			InstantiateShieldPickup();
		}
	}

	void InstantiateRandomHorizontalBlock() {
		// Get random index
		int ndx = Random.Range(0, 3);

		Instantiate(objects[ndx], new Vector3(0, 0, 40), transform.rotation);
	}

	void InstantiateRandomVerticalBlock() {
		// Get random index
		int ndx = Random.Range(4, 7);

		Instantiate(objects[ndx], new Vector3(0, 0, 40), transform.rotation);
	}

	void InstantiateQuidPickup() {
		// Get random position 
		int xPos = Random.Range(minXPos, maxXPos);
		float yPos = Random.Range(0.5f, pickupMaxYPos);

		Instantiate(quidPickup, new Vector3(xPos, yPos, 40), transform.rotation);
	}

	void InstantiateShieldPickup() {
		// Get random position 
		int xPos = Random.Range(minXPos, maxXPos);
		float yPos = Random.Range(0.5f, pickupMaxYPos);

		Instantiate(shieldPickup, new Vector3(xPos, yPos, 40), transform.rotation);
	}

	public void SetEndingMoveSpeedForHighScoreEntry() {
		float endingMoveSpeedKPH = currentObjectSpeed * 3.6f;
		cachedObjectSpeed = endingMoveSpeedKPH / 1.609f;

		// Round up to nearest int
		cachedObjectSpeed = (int)System.Math.Round(cachedObjectSpeed, 0); 
	}

	public void SetEndingSpawnSpeedForHighScoreEntry() {
		cachedSpawnSpeed = 60 / currentSpawnSpeed;
	}
}