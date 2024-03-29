using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
     // Defines the game over panel
    public GameObject gameOverPanel;

    // Add a flag to check if GameOver was called
    public bool isGameOverCalled = false;
    // Add a flag to check if RestartGame was called
    public bool isGameRestartCalled = false;

    public void RestartGame()
    {
        // Set the flag to true
        isGameRestartCalled = true;
        // Reload the current scene
        SceneManager.LoadScene("Level");
    }

    public void GoToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // Add a timer to delay the game over condition
    public float timer = 3f;
    // Add a flag to check if the game over condition was called
    public void GameOver()
    {
        // Set the flag to true
        isGameOverCalled = true;
        // Start the timer
        timer -= 0.1f;
        
        // Check if the timer has reached zero
        if (timer <= 0)
        {
            // Set the game over condition to true
            PlayerManager.gameOver = true;
            // Stop the game
            Time.timeScale = 0;
            // Show the game over panel
            gameOverPanel.SetActive(true);
        }
    }
}