using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; 
    public TextMeshProUGUI scoreText; 
    private int highScore;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadHighScore();
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public void UpdateScoreText() {
        if (scoreText != null) {
            scoreText.text = PlayerManager.coinsCollected.ToString();
        } else {
            Debug.LogWarning("No Score Text assigned");
        }
    }

    // check and update the high score if the current score is greater
    public void CheckHighScore(int currentScore) {
        if (currentScore > highScore) {
            highScore = currentScore;
            SaveHighScore();
            UpdateScoreText();
        }
    }

    // save the high score to PlayerPrefs for persistent storage
    private void SaveHighScore() {
        PlayerPrefs.SetInt("Highscore", highScore);
        PlayerPrefs.Save();
    }

    // load the high score from PlayerPrefs
    private void LoadHighScore() {
        highScore = PlayerPrefs.GetInt("Highscore", 0);
    }
}