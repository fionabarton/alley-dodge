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

	// Amount of time until next object is spawned
	public float			timeDuration = 2.0f;

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

	private void Start() {
		timeDone = timeDuration + Time.time;
	}

    private void FixedUpdate() {
		if (canSpawn) {
			if (timeDone <= Time.time) {
				InstantiateRandomObject();
				timeDone = timeDuration + Time.time;
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

        float randomVal = Random.value;

        //if (randomVal <= 0.5f) {
        //	InstantiateHorizontalBlock();
        //} else {
        //	InstantiateQuidPickup();
        //}

        if (randomVal <= 0.33f) {
            // Instantiate horizontal block
            InstantiateHorizontalBlock();
        } else if (randomVal > 0.33f && randomVal <= 0.66f) {
            // Instantiate vertical block
            if (Random.value > 0.5) {
                // Low block
                InstantiateVerticalLowBlock();
            } else {
                // High block
                InstantiateVerticalHighBlock();
            }
        } else {
            // Spawn pickups
            if (Random.value > 0.25) {
                InstantiateQuidPickup();
            } else {
                InstantiateShieldPickup();
            }
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