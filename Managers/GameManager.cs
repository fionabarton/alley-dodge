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
    public List<XRDirectClimbInteractor> climbInteractors;

    // Objects to be activated/deactivated
    public List<GameObject>     xrRayInteractorsGO;

    // Menu game objects
    public GameObject           keyboardMenuGO;
    public GameObject           highScoreMenuGO;
    public GameObject           mainMenuGO;
    public GameObject           optionsMenuGO;

    // Menu scripts
    public KeyboardInputMenu    keyboardMenuCS;
    public MainMenu             mainMenuCS;
    public OptionsMenu          optionsMenuCS;

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

    // For testing!
    //private void Update() {
    //    if (Input.GetKeyDown(KeyCode.Space)) {
    //
    //    }
    //}
}