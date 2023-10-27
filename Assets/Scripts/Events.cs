using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
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
}
