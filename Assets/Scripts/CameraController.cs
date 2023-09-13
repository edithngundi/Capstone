using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    /// <summary>
    /// This method is called when the game starts before the first frame update
    /// </summary>
    void Start()
    {
        // Set offset to the camera position minus player's position
        offset = transform.position - player.transform.position;
    }

    /// <summary>
    /// This method updates the camera's position after the Physics systems are updated using Update
    /// LateUpdate works like Update but is called after everything else runs
    /// </summary>
    void LateUpdate()
    {
        // The camera is placed into a new position
        // In line with the player position before the frame updates
        // Behaves like the camera were a child of the player object
        transform.position = player.transform.position + offset;
    }
}