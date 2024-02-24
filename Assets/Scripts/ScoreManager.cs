using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton pattern
    public TextMeshProUGUI scoreText; // You'll assign this in the menu scene

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    // This method will be called from the ScoresMenu in the menu scene
    public void UpdateScoreText() {
        if (scoreText != null) {
            scoreText.text = PlayerManager.coinsCollected.ToString();
        } else {
            Debug.LogWarning("Score Text is not assigned in the ScoreManager");
        }
    }
}
