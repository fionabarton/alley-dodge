using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stores and returns commonly used words and phrases
public class WordManager : MonoBehaviour {
    [Header("Set Dynamically")]
    public List<string> exclamations = new List<string>();
    public List<string> interjections = new List<string>();

    // Stores the indexes of remaining exclamations/interjections that have yet to be displayed
    public List<int>    remainingExclamationNdxs = new List<int>();
    public List<int>    remainingInterjectionNdxs = new List<int>();

    void Start() {
        exclamations = new List<string>() { "Awesome", "Booyah", "Cool", "Cool beans", "Cowabunga", "Dude", "Excellent",
            "Fabulous", "Fantastic", "Far out", "Gee whiz", "Great", "Groovy", "Heck yeah", "Hoorah", "Hooray",
            "Hot diggity dog", "Huzzah", "Incredible", "Nice", "Oh yeah", "Right on", "Splendid", "Sweet", "Terrific",
            "Unreal", "Wahoo", "Whoop dee doo", "Whoopee", "Wicked", "Yahoo", "Yay", "Yippee" };
        // "Gnarly", "Tubular", "Woo hoo", "Wow"

        interjections = new List<string>() { 
            "Blimey", "Blinking heck", "Bloody heck", "Blooming heck", "Dagnabbit", "Dang", "Dang it", "Darn",
            "Darnation", "Dash it", "Doggone it", "Flipping heck", "For crying out loud", "For Heaven's sake",
            "For Pete's sake", "Good gosh", "Goodness me", "Gosh darn it", "Gosh darn it to heck", "Great Scott",
            "Oh fiddlesticks", "Rats", "Shoot" };
        // "Blast", "Confound it", "Cripes", "Darn it", "Frick", "Good golly", "Good gracious", "My goodness",
        // "Oh bother", "Oh poo", "Shucks", "Strike me pink", "Yuck"  

        // Populate lists of remaining indexes
        PopulateRemainingExclamationNdxs();
        PopulateRemainingInterjectionNdxs();
    }

    // Populate lists of indexes of remaining exclamations/interjections that have yet to be displayed
    void PopulateRemainingExclamationNdxs() {
        for (int i = 0; i < exclamations.Count; i++) {
            remainingExclamationNdxs.Add(i);
        }
    }
    void PopulateRemainingInterjectionNdxs() {
        for (int i = 0; i < interjections.Count; i++) {
            remainingInterjectionNdxs.Add(i);
        }
    }

    // Returns a random, POSITIVE word or phrase
    public string GetRandomExclamation(bool playVOX = false) {
        // If empty, repopulate list of remaining indexes
        if (remainingExclamationNdxs.Count <= 0) {
            PopulateRemainingExclamationNdxs();
        }

        // Get random remaining exclamation index
        int remainingExclamationNdx = Random.Range(0, remainingExclamationNdxs.Count);

        // Get exclamation index
        int exclamationNdx = remainingExclamationNdxs[remainingExclamationNdx];

        // Play VOX audio clip
        if (playVOX) {
            GameManager.audioMan.PlayVOXExclamationClip(exclamationNdx);
        }

        // Remove remaining exclamation from list
        remainingExclamationNdxs.RemoveAt(remainingExclamationNdx);

        // Return exclamation
        return exclamations[exclamationNdx].ToUpper();
    }

    // Returns a random, NEGATIVE word or phrase
    public string GetRandomInterjection(bool playVOX = false) {
        // If empty, repopulate list of remaining indexes
        if (remainingInterjectionNdxs.Count <= 0) {
            PopulateRemainingInterjectionNdxs();
        }

        // Get random remaining interjection index
        int remainingInterjectionNdx = Random.Range(0, remainingInterjectionNdxs.Count);

        // Get interjection index
        int interjectionNdx = remainingInterjectionNdxs[remainingInterjectionNdx];

        // Play VOX audio clip
        if (playVOX) {
            GameManager.audioMan.PlayVOXInterjectionClip(interjectionNdx);
        }

        // Remove remaining interjection from list
        remainingInterjectionNdxs.RemoveAt(remainingInterjectionNdx);

        // Return interjection
        return interjections[interjectionNdx].ToUpper();
    }
}