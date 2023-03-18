using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Instantiates a stream of randomly selected hazards/pickups at a constant rate.
public class ObjectSpawner : MonoBehaviour {
	[Header("Set in Inspector")]
	public GameObject		horizontalBlock;
	public GameObject		verticalLowBlock;
	public GameObject		verticalHighBlock;
	public GameObject		shieldPickup;
	public GameObject		quidPickup;

	public GameObject		horizontalDestruction;
	public GameObject		verticalHighDestruction;
	public GameObject		verticalLowDestruction;

	// Amount of time until next object is spawned
	public float			startingSpawnSpeed = 2.0f;
	public float			currentSpawnSpeed = 2.0f;

	// Speed at which objects travel down the alley
	public float			startingObjectSpeed = 5;
	public float			currentObjectSpeed = 5;

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
	public float			horizontalYPos = 2.5f;
	public float			verticalLowYPos = 0.375f;
	public float			verticalHighYPos = 3.25f;

	//
	public List<int>		objectsToSpawn;
	public List<float>		chancesToSpawn;

	private void Start() {
		timeDone = currentSpawnSpeed + Time.time;
	}

    private void FixedUpdate() {
		if (canSpawn) {
			if (timeDone <= Time.time) {
				InstantiateRandomObject();
				timeDone = currentSpawnSpeed + Time.time;
			}
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////
	// Object instantiation functions
	////////////////////////////////////////////////////////////////////////////////////////

	// Instantiate a random hazard or pickup
	void InstantiateRandomObject() {
        // Add to object count
        GameManager.S.score.AddToObjectCount();
		
		// Get random value
		float randomVal = Random.value;

        //if (randomVal <= 0.5f) {
        //	InstantiateHorizontalBlock();
        //} else {
        //	InstantiateQuidPickup();
        //}

        if (randomVal <= chancesToSpawn[0]) { // 0.3f (30%)
			// Instantiate horizontal block
			InstantiateObject(objectsToSpawn[0]);
		} else if (randomVal <= (chancesToSpawn[0] + chancesToSpawn[1])) { // 0.65f (65%)
			// Get random value
			randomVal = Random.value;

			// Instantiate vertical block
			if (randomVal < chancesToSpawn[2]) { // 0.5f (50%)
				// Low block
				InstantiateObject(objectsToSpawn[1]);
			} else if (randomVal >= (1 - chancesToSpawn[3])) { // 0.5f (50%)
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
		if(ndx == 0) {
			InstantiateHorizontalBlock();
		} else if (ndx == 1) {
			InstantiateVerticalLowBlock();
		} else if (ndx == 2) {
			InstantiateVerticalHighBlock();
		} else if (ndx == 3) {
			InstantiateQuidPickup();
		} else if (ndx == 4) {
			InstantiateShieldPickup();
		}
	}

	void InstantiateHorizontalBlock() {
		// Get random position on x-axis
		int xPos = Random.Range(minXPos, maxXPos);
		
		Instantiate(horizontalBlock, new Vector3(xPos, horizontalYPos, 40), transform.rotation);
	}

	void InstantiateVerticalLowBlock() {
		Instantiate(verticalLowBlock, new Vector3(0, verticalLowYPos, 40), transform.rotation);
	}

	void InstantiateVerticalHighBlock() {
		Instantiate(verticalHighBlock, new Vector3(0, verticalHighYPos, 40), transform.rotation);
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