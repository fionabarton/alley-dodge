using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

//
public class PlayerDamage : MonoBehaviour {
    [Header("Set in Inspector")]
    public XRController leftXR;
    public XRController rightXR;

    public GameObject   explosionGO;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "EnemyBullet") {
            // Haptic feedback (amplitude, duration)
            leftXR.SendHapticImpulse(0.25f, 0.5f);
            rightXR.SendHapticImpulse(0.25f, 0.5f);

            // SFX

            // Instantiate explosion
            Instantiate(explosionGO, transform.position, transform.rotation);

            // Destroy hazard
            Destroy(other.gameObject);

            // If shield isn't active...
            if (!GameManager.shield.shieldIsActive) {
                // Freeze all objects
                GameManager.S.spawner.objectsCanMove = false;
                GameManager.S.spawner.canSpawn = false;

                // Display text
                GameManager.S.score.SetDisplayText("GAME OVER!", Color.red, Color.red);

                // Reload scene
                Invoke("LoadScene", 2);
            } else {
                // Deactivate shield
                GameManager.shield.SetActiveShield(false);

                // Display text
                GameManager.S.score.SetDisplayText(GameManager.words.GetRandomInterjection() + "!", Color.red, Color.red);
            }
        }
    }

    void LoadScene() {
        // Reload scene
        SceneManager.LoadScene("7 Alleyway");
    }
}