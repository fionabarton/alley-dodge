using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Provides access to all components
public class GameManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public ConfettiManager      confetti;
    public ScoreManager         score;
    public ObjectSpawner        spawner;

    [Header("Set dynamically")]
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
}