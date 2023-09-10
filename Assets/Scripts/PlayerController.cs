using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     // Controls the character's movement
    private CharacterController characterController;

    // Defines the character's direction
    private Vector3 movementDirection;

    // Defines the character's forward racing speed
    public float racingSpeed;

    // Defines the position of the track: 0 Left, 1 Middle, 2 Right
    private int positionOnTrack = 1;

    // Defines the distance between these positions
    public float distanceBetween = 5;

    /// <summary>
    /// This method is called when the game starts before the first frame update
    /// </summary>
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sets player's speed
        movementDirection.z = racingSpeed;

        // Movement to the right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            positionOnTrack++;
            if (positionOnTrack == 3)
                positionOnTrack = 2;
        }
        // Movement to the left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            positionOnTrack--;
            if (positionOnTrack == -1)
                positionOnTrack = 0;
        }

        // Calculate future positions
        Vector3 futurePosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        // Move to the right
        if (positionOnTrack == 0)
        {
            futurePosition += Vector3.left * distanceBetween;
        }
        // Move to the left
        if (positionOnTrack == 2)
        {
            futurePosition += Vector3.right * distanceBetween;
        }
        // Smoothen the transition in movement
        transform.position = Vector3.Lerp(transform.position, futurePosition, 80*Time.fixedDeltaTime);
    }

    /// <summary>
    /// This method is responsible for moving the player. 
    /// Preferred to Update because it runs at a fixed rate/per delta time while Update runs per frame
    /// </summary>
    private void FixedUpdate()
    {
        characterController.Move(movementDirection * Time.fixedDeltaTime);
    }
}
