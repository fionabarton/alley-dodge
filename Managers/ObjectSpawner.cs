using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instantiates a stream of randomly selected hazards/pickups at a constant rate.
public class ObjectSpawner : MonoBehaviour {
	[Header("Set in Inspector")]
	public GameObject		shieldPickup;
	public GameObject		quidPickup;

	public List<GameObject> objects;
	public GameObject		testBlenderCube;

	public GameObject		horizontalDestruction;
	public GameObject		verticalHighDestruction;
	public GameObject		verticalLowDestruction;

	// Amount of time until next object is spawned
	public float			startingSpawnSpeed = 2.0f;
	public float			currentSpawnSpeed = 2.0f;

	// Speed at which objects travel down the alley
	public float			startingObjectSpeed = 10;
	public float			currentObjectSpeed = 10;

	// Amount to increase per level
	public float			amountToDecreaseSpawnSpeed = 0.1f;
	public float			amountToIncreaseObjectSpeed = 0;

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
        // Add to object count
        GameManager.S.score.AddToObjectCount();

        //if (Random.value <= 0.75f) {
        //    Instantiate(testBlenderCube, new Vector3(0, 0, 40), transform.rotation);
        //} else {
        //    if (Random.value <= 0.75f) {
        //        InstantiateQuidPickup();
        //    } else {
        //        InstantiateShieldPickup();
        //    }
        //}

        // Get random value
        float randomVal = Random.value;

        if (randomVal <= chancesToSpawn[0]) { // 0.3f (30%)
            // Instantiate horizontal block
            InstantiateObject(objectsToSpawn[0]);
        } else if (randomVal <= (chancesToSpawn[0] + chancesToSpawn[1])) { // 0.65f (65%)
            // Get random value
            randomVal = Random.value;

            // Instantiate vertical block
            if (randomVal > chancesToSpawn[2]) { // 0.5f (50%)
                // Low block
                InstantiateObject(objectsToSpawn[1]);
            } else if (randomVal <= (1 - chancesToSpawn[3])) { // 0.5f (50%)
                // High block
                InstantiateObject(objectsToSpawn[2]);
            }
        } else {
            // Get random value
            randomVal = Random.value;

            // Spawn pickups
            if (randomVal < chancesToSpawn[5]) { // 0.75f (75%)
                InstantiateObject(objectsToSpawn[3]);
            } else if (randomVal >= (1 - chancesToSpawn[6])) { // 0.25f (25%)
                InstantiateObject(objectsToSpawn[4]);
            }
        }
    }

	void InstantiateObject(int ndx) {
		if (ndx == 2) {
			InstantiateHorizontalBlock();
		} else if (ndx == 3) {
			InstantiateHorizontalBlock();
		} else if (ndx == 4) {
			InstantiateHorizontalBlock();
		}else if (ndx == 43) {
			InstantiateQuidPickup();
		} else if (ndx == 44) {
			InstantiateShieldPickup();
		} else if (ndx == 45) {
			InstantiateRandomObstacle();
		} else if (ndx == 46) {
			InstantiateRandomItem();
		} else {
			Instantiate(objects[ndx], new Vector3(0, 0, 40), transform.rotation);
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

	void InstantiateHorizontalBlock() {
		// Get random index
		int ndx = Random.Range(2, 5);

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
}