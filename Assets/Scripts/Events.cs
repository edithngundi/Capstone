using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
     // Defines the game over panel
    public GameObject gameOverPanel;

    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }

    public float timer = 3f;
    public void GameOver()
    {

        // Start the timer
        timer -= 0.1f;
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
