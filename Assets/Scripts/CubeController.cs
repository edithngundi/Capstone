using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    // Starting speed of the cube obstraction
    private float speed = 10f;
    // Slider distance that the cube traverses from origin to left and right
    private int distance = 10;
    // Check to determine if the cube is moving right or left
    private bool movingRight = true;
    // Starting position of the cube
    private Vector3 startPosition;

    private void Start()
    {
        // Set the starting position of the cube
        startPosition = transform.position;
    }

    private void Update()
    {
        // Check if the cube is moving right and has reached the end of the slider
        if (movingRight && transform.position.x >= startPosition.x + distance)
        {
            // Change the direction of the cube to left
            movingRight = false;
        }
        // Check if the cube is moving left and has reached the end of the slider
        else if (!movingRight && transform.position.x <= startPosition.x - distance)
        {
            // Change the direction of the cube to right
            movingRight = true;
        }

        float direction;
        if (movingRight)
        {
            // Set the direction of the cube to right
            direction = 1f;
        }
        else
        {
            // Set the direction of the cube to left
            direction = -1f;
        }

        // Update the position based on the movement of the cube
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0f, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision was with the projectile object
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // Destroy the Cube object
            Destroy(gameObject);
        }
    }
}
