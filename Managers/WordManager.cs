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
        exclamations = new List<string>() { "Oh yeah", "Heck yeah", "Hoorah", "Whoopee", "Yahoo", "Wahoo", "Hot diggity dog",
            "Huzzah", "Yippee", "Woo hoo", "Whoop dee doo", "Hooray",  "Gee whiz", "Right on", "Far out",
            "Groovy", "Awesome", "Excellent", "Cool", "Incredible", "Unreal", "Fabulous", "Terrific", "Yay",
            "Fantastic", "Great", "Gnarly", "Sweet", "Nice", "Splendid", "Wicked", "Wow", "Dude", "Cool beans",
            "Booyah", "Cowabunga", "Tubular" };

        interjections = new List<string>() { "Rats", "Yuck", "Dang", "Darn", "Blast", "Oh bother", "Doggone it", "Darnation",
            "Gosh darn it", "Gosh darn it to heck", "Oh fiddlesticks", "Cripes", "Confound it", "Shucks", "Shoot",
            "Blooming heck", "Flipping heck", "Blinking heck", "Dash it", "Strike me pink", "My goodness", "For Pete's sake",
            "For Heaven's sake", "Frick", "Good gosh", "Bloody heck", "Dagnabbit", "Oh poo","Blimey", "Great Scott",
            "Goodness me", "For crying out loud", "Good gracious", "Good golly", "Dang it", "Darn it" };

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
    public string GetRandomExclamation() {
        // If empty, repopulate list of remaining indexes
        if (remainingExclamationNdxs.Count <= 0) {
            PopulateRemainingExclamationNdxs();
        }

        // Get random remaining exclamation index
        int remainingExclamationNdx = Random.Range(0, remainingExclamationNdxs.Count);

        // Get exclamation index
        int exclamationNdx = remainingExclamationNdxs[remainingExclamationNdx];

        // Remove remaining exclamation from list
        remainingExclamationNdxs.RemoveAt(remainingExclamationNdx);

        // Return exclamation
        return exclamations[exclamationNdx].ToUpper();
    }

    // Returns a random, NEGATIVE word or phrase
    public string GetRandomInterjection() {
        // If empty, repopulate list of remaining indexes
        if (remainingInterjectionNdxs.Count <= 0) {
            PopulateRemainingInterjectionNdxs();
        }

        // Get random remaining interjection index
        int remainingInterjectionNdx = Random.Range(0, remainingInterjectionNdxs.Count);

        // Get interjection index
        int interjectionNdx = remainingInterjectionNdxs[remainingInterjectionNdx];

        // Remove remaining interjection from list
        remainingInterjectionNdxs.RemoveAt(remainingInterjectionNdx);

        // Return interjection
        return interjections[interjectionNdx].ToUpper();
    }
}