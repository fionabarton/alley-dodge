using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Animator             playerLeftHandAnim;
    public Animator             playerRightHandAnim;

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

    //// For testing!
    //private void Update() {
    //    if (Input.GetKeyDown(KeyCode.Space)) {
    //        Test();
    //    }
    //}
    //void Test() {

    //}
}