using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sets the playfield to two complementary colors;
// changes which set of colors for each level.
public class ColorManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public Material alleyMaterial1;
    public Material alleyMaterial2;

    public Material playerMaterial1;
    public Material playerMaterial2;

    [Header("Set dynamically")]
    // Current color palette index (0 = red & cyan)
    public int      colorNdx = 0;

    List<Color> alleyColors = new List<Color>() {
        new Color(0.9f, 0, 0.05f, 1.0f), // red
        new Color(1.0f, 0.35f, 0, 1.0f), // orange
        new Color(1.0f, 0.8f, 0, 1.0f), // yellow
        new Color(0.5f, 1.0f, 0, 1.0f), // chartreuse green
        new Color(0, 0.85f, 0, 1.0f), // green
        new Color(0, 0.85f, 0.45f, 1.0f), // spring green
        new Color(0, 1, 1, 1.0f), // cyan 
        new Color(0, 0.5f, 1.0f, 1.0f), // azure
        new Color(0, 0, 1, 1.0f), // blue 
        new Color(0.5f, 0, 1.0f, 1.0f), // violet
        new Color(1, 0, 1, 1.0f), // magenta 
        new Color(1.0f, 0, 0.5f, 1.0f) // rose
    };

    List<Color> playerColors = new List<Color>() {
        new Color(0.9f, 0, 0.05f, 0.5f), // red
        new Color(1.0f, 0.35f, 0, 0.5f), // orange
        new Color(1.0f, 0.8f, 0, 0.5f), // yellow
        new Color(0.5f, 1.0f, 0, 0.5f), // chartreuse green
        new Color(0, 0.85f, 0, 0.5f), // green
        new Color(0, 0.85f, 0.45f, 0.5f), // spring green
        new Color(0, 1, 1, 0.5f), // cyan 
        new Color(0, 0.5f, 1.0f, 0.5f), // azure
        new Color(0, 0, 1, 0.5f), // blue 
        new Color(0.5f, 0, 1.0f, 0.5f), // violet
        new Color(1, 0, 1, 0.5f), // magenta 
        new Color(1.0f, 0, 0.5f, 0.5f) // rose
    };

    public void Start() {
        alleyMaterial1.color = alleyColors[0];
        alleyMaterial2.color = alleyColors[6];

        playerMaterial1.color = playerColors[0];
        playerMaterial2.color = playerColors[6];
    }

    // Resets script properties to their default values
    public void ResetPalette() {
        colorNdx = 0;
        alleyMaterial1.color = alleyColors[0];
        alleyMaterial2.color = alleyColors[6];

        playerMaterial1.color = playerColors[0];
        playerMaterial2.color = playerColors[6];
    }

    // Sets UI display text colors to alley colors
    public void SetDisplayTextPalette() {
        // Get alley material colors w/ 0.5f opacity
        Color c1 = new Color(alleyMaterial1.color.r, alleyMaterial1.color.g, alleyMaterial1.color.b, 0.5f);
        Color c2 = new Color(alleyMaterial2.color.r, alleyMaterial2.color.g, alleyMaterial2.color.b, 0.5f);

        GameManager.S.score.displayText.color = c1;
        GameManager.S.score.displayMessageFrame.color = c2;
    }

    // Sets the two alley materials to a new set of two complementary colors 
    public void GetNewColorPalette() {
        // Increment colorNdx
        if (colorNdx < alleyColors.Count - 1) {
            colorNdx += 1;
        } else {
            colorNdx = 0;
        }

        // Set material 1 color
        alleyMaterial1.color = alleyColors[colorNdx];
        playerMaterial1.color = playerColors[colorNdx];

        // Set material 2 color
        if (colorNdx < 6) {
            alleyMaterial2.color = alleyColors[colorNdx + 6];
            playerMaterial2.color = playerColors[colorNdx + 6];
        } else {
            alleyMaterial2.color = alleyColors[colorNdx - 6];
            playerMaterial2.color = playerColors[colorNdx - 6];
        }
    }
}