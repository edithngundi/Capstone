using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
    #if UNITY_WEBGL && !UNITY_EDITOR
        Application.ExternalEval("window.close()");
    #else
        Application.Quit();
    #endif
    }
}
