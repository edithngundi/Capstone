using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundController : MonoBehaviour
{
    // Add a static instance of the MainSoundController
   public static MainSoundController instance = null;

    void Awake()
    {
        // If the instance is null
        if (instance == null)
        {
            // Set the instance to this
            instance = this;
            // Dont destroy the instance when loading a new scene
            DontDestroyOnLoad(transform.gameObject);
        }
        // If the instance is not null and it is not this
        else if (instance != this)
        {
            // Destroy the instance if it is not this
            Destroy(gameObject);
        }
    }
}