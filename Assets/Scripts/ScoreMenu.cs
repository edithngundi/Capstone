using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreMenu : MonoBehaviour
{
    // Reference to the score text
    public TextMeshProUGUI scoreText; 
    void OnEnable() {
        // Get the ScoreManager instance
        ScoreManager scoreManager = ScoreManager.Instance;
        // If the ScoreManager instance is not null
        if (scoreManager != null) {
            // Set the score text to the scoreText
            scoreManager.scoreText = this.scoreText; 
            // Update the score text
            scoreManager.UpdateScoreText(); 
        } else {
            Debug.LogWarning("No ScoreManager object");
        }
    }
}