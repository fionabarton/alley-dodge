using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sets the playfield to two complementary colors;
// changes which set of colors for each level.
public class ColorManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public Material alleyMaterial1;
    public Material alleyMaterial2;

    [Header("Set dynamically")]
    // Current color palette index (0 = red & cyan)
    public int      colorNdx = 0;

    List<Color> colors = new List<Color>() {
        new Color(0.9f, 0.0f, 0.05f), // red
        new Color(1.0f, 0.35f, 0.0f), // orange
        new Color(1.0f, 0.8f, 0.0f), // yellow
        new Color(0.5f, 1.0f, 0.0f), // chartreuse green
        new Color(0.0f, 0.85f, 0.0f), // green
        new Color(0.0f, 0.85f, 0.45f), // spring green
        Color.cyan, // cyan
        new Color(0.0f, 0.5f, 1.0f), // azure
        Color.blue, // blue 
        new Color(0.5f, 0.0f, 1.0f), // violet
        Color.magenta, // magenta
        new Color(1.0f, 0.0f, 0.5f) // rose
    };

    public void Start() {
        alleyMaterial1.color = colors[0];
        alleyMaterial2.color = colors[6];
    }

    // Resets script properties to their default values
    public void ResetPalette() {
        colorNdx = 0;
        alleyMaterial1.color = colors[0];
        alleyMaterial2.color = colors[6];
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
        if (colorNdx < colors.Count - 1) {
            colorNdx += 1;
        } else {
            colorNdx = 0;
        }

        // Set material 1 color
        alleyMaterial1.color = colors[colorNdx];

        // Set material 2 color
        if (colorNdx < 6) {
            alleyMaterial2.color = colors[colorNdx + 6];
        } else {
            alleyMaterial2.color = colors[colorNdx - 6];
        }
    }
}