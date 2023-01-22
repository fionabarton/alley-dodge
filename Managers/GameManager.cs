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

    // Objects to be activated/deactivated
    public List<GameObject>     xrRayInteractorsGO;

    // Menus
    public GameObject           keyboardMenuGO;
    public GameObject           highScoreMenuGO;
    public GameObject           startGameMenuGO;

    public MenuManager          startGameMenuCS;

    [Header("Set dynamically")]
    public GameObject           previouslyHighlightedGO;

    //
    public int                  fallBelowFloorCount;

    public static AlleyManager  alley;
    public static ColorManager  color;
    public static ShieldManager shield;
    public static Utilities     utilities;
    public static WordManager   words;

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
    }

    // For testing!
    //private void Update() {
    //    if (Input.GetKeyDown(KeyCode.Space)) {
    //        Debug.Log("name: " + highScore.highScores[highScore.newHighScoreNdx].name);
    //        Debug.Log("score: " + highScore.highScores[highScore.newHighScoreNdx].score);
    //        Debug.Log("level: " + highScore.highScores[highScore.newHighScoreNdx].level);
    //        Debug.Log("objects: " + highScore.highScores[highScore.newHighScoreNdx].objects);
    //        Debug.Log("time: " + highScore.highScores[highScore.newHighScoreNdx].time);
    //        Debug.Log("dateTime: " + highScore.highScores[highScore.newHighScoreNdx].dateTime);
    //        Debug.Log("alleyCount: " + highScore.highScores[highScore.newHighScoreNdx].alleyCount);
    //        Debug.Log("playerHeight: " + highScore.highScores[highScore.newHighScoreNdx].playerHeight);
    //        Debug.Log("fallBelowFloorCount: " + highScore.highScores[highScore.newHighScoreNdx].fallBelowFloorCount);
    //    }
    //}
}