using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// OnDestroy, instantiates a barrage of exploding red cubes 
public class OnDestroyInstantiateExplodingCubes : MonoBehaviour {
    [Header("Set dynamically")]
    public int          cubeNdx = 0;

    public void InstantiateCubes() {
        switch (cubeNdx) {
            case 0:
                Instantiate(GameManager.S.spawner.horizontalDestruction, transform.position, transform.rotation);
                break;
            case 1:
                Instantiate(GameManager.S.spawner.verticalHighDestruction, transform.position, transform.rotation);
                break;
            case 2:
                Instantiate(GameManager.S.spawner.verticalLowDestruction, transform.position, transform.rotation);
                break;
        }
    }
}