using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // Set offset to the camera position minus player's position
        offset = transform.position - player.transform.position;
    }

    // LateUpdate works like Update but is called after everything else runs
    // The camera's position will be updated after the Physics systems are updated using Update
    void LateUpdate()
    {
        // The camera is placed into a new position
        // In line with the player position before the frame updates
        // Behaves like the camera were a child of the player object
        transform.position = player.transform.position + offset;
    }
}