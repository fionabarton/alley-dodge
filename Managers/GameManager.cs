using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Provides access to all components
public class GameManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public ConfettiManager      confetti;
    public HighScoreManager     highScore;
    public ScoreManager         score;
    public ObjectSpawner        spawner;
    public DamageColliderFollow damageColl;
    public ClimbProvider        climb;
    public DisplayPodiumManager podiums;
    public List<XRDirectClimbInteractor> climbInteractors;

    // Objects to be activated/deactivated
    public List<GameObject>     xrRayInteractorsGO;

    // Menu game objects
    public GameObject           keyboardMenuGO;
    public GameObject           highScoreMenuGO;
    public GameObject           mainMenuGO;
    public GameObject           moreMenuGO;
    public GameObject           subMenuGO;
    public GameObject           scoreboardMenuGO;
    public GameObject           selectedHighScoreMenuGO;

    // Menu scripts
    public KeyboardInputMenu    keyboardMenuCS;
    public MainMenu             mainMenuCS;
    public MoreMenu             moreMenuCS;
    public AlgorithmMenu        algorithmMenuCS;
    public SubMenu              subMenuCS;
    public ExitRunButton        exitRunButtonLeftCS;
    public ExitRunButton        exitRunButtonRightCS;
    public CustomAlgorithmMenu  customAlgorithmMenuCS;
    public SelectedHighScoreMenu selectedHighScoreMenuCS;

    // Audio
    public AudioSource          playerAudioSource;
    public AudioSource          UIAudioSource;

    // Animators
    public Animator             playerDamageColldierAnim;
    public Animator             playerLeftHandAnim1;
    public Animator             playerLeftHandAnim2;
    public Animator             playerRightHandAnim1;
    public Animator             playerRightHandAnim2;

    // Claws
    public Transform            playerLeftHandTrans1;
    public Transform            playerLeftHandTrans2;
    public Transform            playerRightHandTrans1;
    public Transform            playerRightHandTrans2;

    // Hand controllers for vibration
    public XRController         leftXR;
    public XRController         rightXR;

    // Smoke particle system (on new level increases its starting size by 1)
    public ParticleSystem       smokePS;

    // Actual "under-the-hood" speed values 
    public List<float>          objectSpeedValues;
    public List<float>          amountToIncreaseValues;
    public List<float>          spawnSpeedValues;
    public List<float>          amountToDecreaseValues;

    // Displayed "digestable-to-a-layperson" speed values
    public List<float>          objectSpeedDisplayedValues;
    public List<float>          amountToIncreaseDisplayedValues;
    public List<float>          spawnSpeedDisplayedValues;
    public List<float>          amountToDecreaseDisplayedValues;

    [Header("Set dynamically")]
    public GameObject           previouslyHighlightedGO;

    //
    public int                  fallBelowFloorCount;
    public int                  damageCount;
    public int                  pauseCount;

    public bool                 playerIsInvincible;
    public bool                 waitForDialogueToFinish;

    public static AlleyManager  alley;
    public static ColorManager  color;
    public static ShieldManager shield;
    public static Utilities     utilities;
    public static WordManager   words;
    public static AudioManager  audioMan;
    public static SaveManager   save;

    public ParticleSystem.MainModule smokePSmain;

    private static GameManager _S;
    public static GameManager S { get { return _S; } set { _S = value; } }

    void Awake() {
        S = this;
    }

    private void Start() {
        // Get components
        alley = GetComponent<AlleyManager>();
        color = GetComponent<ColorManager>();
        shield = GetComponent<ShieldManager>();
        utilities = GetComponent<Utilities>();
        words = GetComponent<WordManager>();
        audioMan = GetComponent<AudioManager>();
        save = GetComponent<SaveManager>();
        smokePSmain = smokePS.main;
    }

    public void EnableClimbInteractors(bool isActive){
        climbInteractors[0].enabled = isActive;
        climbInteractors[1].enabled = isActive;
    }

    // Find and destroy all hazard and pickup game objects
    public void DestroyAllObject() {
        // Find all hazards and pickups
        GameObject[] hazards = GameObject.FindGameObjectsWithTag("Hazard");
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");

        // Destroy all hazards and pickups
        for (int i = 0; i < hazards.Length; i++) {
            Destroy(hazards[i]);

            // Instantiate exploding cubes
            OnDestroyInstantiateExplodingCubes cubes = hazards[i].gameObject.GetComponent<OnDestroyInstantiateExplodingCubes>();
            if (cubes) {
                cubes.InstantiateCubes();
            }
        }
        for (int i = 0; i < pickups.Length; i++) {
            Destroy(pickups[i]);
        }
    }

    // Play smoke particle system and set its size based on the number of the level currently being played
    public void PlaySmokeParticelSystemAndSetSize() {
        // For levels 5 and higher...
        if (score.level >= 5) {
            // Set size based on level number
            smokePSmain.startSize = score.level * 0.3f;

            // If not playing, start playing
            if (!smokePS.isPlaying) {
                smokePS.Play();
            }
        }
    }

    public float GetIncreasedSpawnSpeedLevel() {
        // Convert both ‘current spawn speed’ AND ‘amount to increase’ from seconds to OPM
        float currentSpawnSpeedOPM = 60 / spawner.currentSpawnSpeed;
        float amountToIncreaseOPM = spawner.amountToDecreaseSpawnSpeed;

        // Add them together
        float sum = currentSpawnSpeedOPM + amountToIncreaseOPM;

        // Convert the resulting sum from OPM to seconds
        sum = 60 / sum;

        // Return sum
        return sum;
    }

    //// For testing!
    //private void Update() {
    //    if (Input.GetKeyDown(KeyCode.Space)) {
    //        Test();
    //    }
    //}
    //void Test() {

    //}
}