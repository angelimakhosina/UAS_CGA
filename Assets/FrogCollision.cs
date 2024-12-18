using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For reloading the scene or transitioning to a game over screen

public class FrogCollision : MonoBehaviour
{
    // Store the initial position of the frog
    private Vector3 startPosition;

    // Life points for the frog
    public int lifePoints = 3;

    // UI to display life points (Optional, you can use a UI Text to show lives on screen)
    public UnityEngine.UI.Text lifeText;

    void Start()
    {
        // Store the initial position at the start of the game
        startPosition = transform.position;

        // Debug log to confirm the starting position
        Debug.Log("Starting position: " + startPosition);

        // Debug log to confirm initial life points
        Debug.Log("Initial Lives: " + lifePoints);

        // Update life points UI (if you have one)
        UpdateLifeText();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding with the frog is a car
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("The frog was hit by a car!");

            // Decrease the life points
            lifePoints--;

            // Debug log to show the current life points after hit
            Debug.Log("Lives after collision: " + lifePoints);

            // Update the life points UI (if you have one)
            UpdateLifeText();

            // Check if the frog has no more lives
            if (lifePoints <= 0)
            {
                // Trigger game over logic
                GameOver();
            }
            else
            {
                // Respawn the frog at the initial position if lives are still remaining
                transform.position = startPosition;

                // Debug log to confirm the respawn position
                Debug.Log("Frog respawned to position: " + transform.position);
            }
        }
    }

    // Function to update the UI with the current life points
    void UpdateLifeText()
    {
        if (lifeText != null)
        {
            lifeText.text = "Lives: " + lifePoints;
        }

        // Debug log to show the current life points whenever updated in the UI
        Debug.Log("Lives updated in UI: " + lifePoints);
    }

    // Game over logic (e.g., reload the scene or show a game over screen)
    void GameOver()
    {
        // Log game over
        Debug.Log("Game Over!");

        // Optional: Restart the level or load a game over scene
        // Example: Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
