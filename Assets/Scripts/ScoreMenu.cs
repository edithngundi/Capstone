using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 

    void OnEnable() {
        ScoreManager scoreManager = ScoreManager.Instance;
        if (scoreManager != null) {
            scoreManager.scoreText = this.scoreText; 
            scoreManager.UpdateScoreText(); 
        } else {
            Debug.LogWarning("ScoreManager is not found in the scene.");
        }
    }
}