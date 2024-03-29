using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Create a static instance of the ScoreManager
    public static ScoreManager Instance; 
    // Create a reference to the score text
    public TextMeshProUGUI scoreText; 
    // Create a variable to store the high score
    private int highScore;

    void Awake() {
        // If the instance is null
        if (Instance == null) {
            // Set the instance to this
            Instance = this;
            // Don't destroy the instance when loading a new scene
            DontDestroyOnLoad(gameObject);
            // Load the high score
            LoadHighScore();
        } else if (Instance != this) {
            // Destroy the instance if it is not this
            Destroy(gameObject);
        }
    }

    public void UpdateScoreText() {
        // If the score text is not null
        if (scoreText != null) {
            // Set the score text to the number of coins collected
            scoreText.text = PlayerManager.coinsCollected.ToString();
        } else {
            Debug.LogWarning("No Score Text assigned");
        }
    }

    // Check and update the high score if the current score is greater
    public void CheckHighScore(int currentScore) {
        if (currentScore > highScore) {
            // Set the high score to the current score
            highScore = currentScore;
            // Save the high score
            SaveHighScore();
            // Update the score text
            UpdateScoreText();
        }
    }

    // Save the high score to PlayerPrefs for persistent storage
    private void SaveHighScore() {
        // Save the high score
        PlayerPrefs.SetInt("Highscore", highScore);
        // Save the PlayerPrefs
        PlayerPrefs.Save();
    }

    // Load the high score from PlayerPrefs
    private void LoadHighScore() {
        // Load the high score
        highScore = PlayerPrefs.GetInt("Highscore", 0);
    }
}