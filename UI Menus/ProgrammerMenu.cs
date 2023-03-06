using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows users to adjust the random object instantiation algorithm
public class ProgrammerMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public List<TMPro.TMP_Dropdown> chanceDropdowns;
    public List<TMPro.TMP_Dropdown> spawnDropdowns;

    void Start() {
        // Set dropdowns to default values
        chanceDropdowns[0].value = 6; // 30%
        chanceDropdowns[1].value = 7; // 35%
        chanceDropdowns[4].value = 7; // 35%

        chanceDropdowns[2].value = 10; // 50%
        chanceDropdowns[3].value = 10; // 50%

        chanceDropdowns[5].value = 15; // 75%
        chanceDropdowns[6].value = 5; // 25%

        spawnDropdowns[0].value = 0; // Horizontal block
        spawnDropdowns[1].value = 1; // Vertical low block
        spawnDropdowns[2].value = 2; // Vertical high block
        spawnDropdowns[3].value = 3; // Quid pickup
        spawnDropdowns[4].value = 4; // Shield pickup

        // Add listener to dropdowns
        spawnDropdowns[0].onValueChanged.AddListener(delegate { SetSpawnDropdownValue(0, spawnDropdowns[0].value); });
        spawnDropdowns[1].onValueChanged.AddListener(delegate { SetSpawnDropdownValue(1, spawnDropdowns[1].value); });
        spawnDropdowns[2].onValueChanged.AddListener(delegate { SetSpawnDropdownValue(2, spawnDropdowns[2].value); });
        spawnDropdowns[3].onValueChanged.AddListener(delegate { SetSpawnDropdownValue(3, spawnDropdowns[3].value); });
        spawnDropdowns[4].onValueChanged.AddListener(delegate { SetSpawnDropdownValue(4, spawnDropdowns[4].value); });

        chanceDropdowns[0].onValueChanged.AddListener(delegate { SetChanceDropdownValue(0, chanceDropdowns[0].value); });
        chanceDropdowns[1].onValueChanged.AddListener(delegate { SetChanceDropdownValue(1, chanceDropdowns[1].value); });
        chanceDropdowns[2].onValueChanged.AddListener(delegate { SetChanceDropdownValue(2, chanceDropdowns[2].value); });
        chanceDropdowns[3].onValueChanged.AddListener(delegate { SetChanceDropdownValue(3, chanceDropdowns[3].value); });
        chanceDropdowns[4].onValueChanged.AddListener(delegate { SetChanceDropdownValue(4, chanceDropdowns[4].value); });
        chanceDropdowns[5].onValueChanged.AddListener(delegate { SetChanceDropdownValue(5, chanceDropdowns[5].value); });
        chanceDropdowns[6].onValueChanged.AddListener(delegate { SetChanceDropdownValue(6, chanceDropdowns[6].value); });

        //// GetPlayerPrefs
        //if (PlayerPrefs.HasKey("Level Select")) {
        //    levelSelectDropdown.value = PlayerPrefs.GetInt("Level Select");
        //} else {
        //    levelSelectDropdown.value = 0;
        //}
    }

    void SetChanceDropdownValue(int ndx, int value) {
        float valueAsFloat = value;
        GameManager.S.spawner.chancesToSpawn[ndx] = valueAsFloat / 20;

        chanceDropdowns[ndx].value = value;
    }

    void SetSpawnDropdownValue(int ndx, int value) {
        GameManager.S.spawner.objectsToSpawn[ndx] = value;

        spawnDropdowns[ndx].value = value;
    }
}