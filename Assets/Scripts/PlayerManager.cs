using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    // Defines the game stop condition
    public static bool gameOver;

    // Defines the game over panel
    public GameObject gameOverPanel;

    // Defines the game start condition
    public static bool isGameStarted;

    // Defines the games start info
    public GameObject startingText;

    // Defines the number of coins collected
    public static int coinsCollected;

    // Defines the coins collected text
    //public Text coinsText;
    public TextMeshProUGUI coinsCollectedText;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        gameOver = false;
        isGameStarted = false;
        coinsCollected = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update the coins text with the coins collected
        coinsCollectedText.text = "Coins:" + coinsCollected;        
    }
}
