using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundController : MonoBehaviour
{
    public Events events;
    void Awake()
    {
        if (events.isGameRestartCalled == true)
        {
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
