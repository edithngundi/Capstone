using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Reference to the player object
    public GameObject player;
    // Offset between the camera and the player
    private Vector3 offset;

    void Start()
    {
        // Set offset to the camera position minus player's position
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // Set camera position to player's position plus offset
        transform.position = player.transform.position + offset;
    }
}