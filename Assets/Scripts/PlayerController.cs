using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     // Controls the character's movement
    private CharacterController characterController;

    // Defines the character's direction
    private Vector3 movementDirection;

    // Defines the forward racing speed
    public float racingSpeed;

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
