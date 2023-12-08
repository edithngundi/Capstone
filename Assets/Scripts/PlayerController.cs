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
    // Defines the character's maximum speed
    private float maximumSpeed = 30;

    // Defines the position of the track: 0 Left, 1 Middle, 2 Right
    private int positionOnTrack = 1;

    // Defines the distance between these positions
    public float distanceBetween = 5;

    // Defines the upward jump force
    public float upwardJumpForce;

    // Defines the gravity on the player
    public float gravity = -20;

    // Defines the box colliders for the capsule
    private BoxCollider boxColliderX;
    private BoxCollider boxColliderY;

    // Add a variable to store the jump sound
    public AudioClip jumpSound;
    // Add a variable to store the land sound
    public AudioClip landSound;
    // Add a variable to store the swerve sound
    public AudioClip swerveSound;
    private float volume = 1.0f;

    // Player's position
    public Transform player;

    // Variables to activate the magnetic powerup
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;

    // Crouch variables
    public float crouchSpeed = 0.1f;
    private float crouchYScale = 0.5f;
    private float startYScale;
    private bool isCrouching = false;

    public static PlayerController instance;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    /// <summary>
    /// This method is called when the game starts before the first frame update
    /// </summary>
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // Save the normal y-scale of the player
        startYScale = transform.localScale.y;
    }

    /// <summary>
    /// This method is called once per frame
    /// </summary>
    void Update()
    {
        // Get player's position
        playerPosX = player.position.x;
        playerPosY = player.position.y;
        playerPosZ = player.position.z;

        // If the game is not started, do not move the player
        if (!PlayerManager.isGameStarted)
            return;

        // Increasing player's speed
        if (racingSpeed < maximumSpeed)
            racingSpeed += Time.deltaTime * 0.2f;
        // Sets player's speed
        movementDirection.z = racingSpeed;

        // Prevent mid-air jumps
        if(characterController.isGrounded)
        {
            // No gravity when grounded
            movementDirection.y = -1;
            // Movement up
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                // Play the jump sound
                AudioSource.PlayClipAtPoint(jumpSound, transform.position, volume);
                CharacterJump();
            }
            // Crouch movement
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                isCrouching = true;
                StartCoroutine(Crouch());
            }
            if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
            {
                isCrouching = false;
                StartCoroutine(StandUp());
            }
        }
        else
        {
            // Movement down
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                // Play the land sound
                AudioSource.PlayClipAtPoint(landSound, transform.position, volume);
                CharacterFall();
            }
            else
            {
            // Player is affected by gravity
            movementDirection.y += gravity * Time.deltaTime;
            }
        
        }

        // Movement to the right
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            // Play the swerve sound
            AudioSource.PlayClipAtPoint(swerveSound, transform.position, volume);
            positionOnTrack++;
            if (positionOnTrack == 3)
                positionOnTrack = 2;
        }
        // Movement to the left
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            // Play the swerve sound
            AudioSource.PlayClipAtPoint(swerveSound, transform.position, volume);
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
        if (transform.position == futurePosition)
            return;

        Vector3 difference = futurePosition - transform.position;
        Vector3 direction = difference.normalized * 25 * Time.deltaTime;

        if (direction.sqrMagnitude < difference.sqrMagnitude)
            characterController.Move(direction);
        else
            characterController.Move(difference);
    }

    /// <summary>
    /// This method is responsible for moving the player. 
    /// Preferred to Update because it runs at a fixed rate/per delta time while Update runs per frame
    /// </summary>
    private void FixedUpdate()
    {
        // If the game is not started, do not move the player
        if (!PlayerManager.isGameStarted)
            return;
        
        // Move the player
        characterController.Move(movementDirection * Time.fixedDeltaTime);
    }

    IEnumerator Crouch()
    {
        while (transform.localScale.y > crouchYScale)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - crouchSpeed, transform.localScale.z);
            yield return null;
        }
    }

    IEnumerator StandUp()
    {
        while (transform.localScale.y < startYScale)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + crouchSpeed, transform.localScale.z);
            yield return null;
        }
    }

    /// <summary>
    /// This method makes the player jump
    /// </summary>
    private void CharacterJump()
    {
        if (!isCrouching)
        {
            movementDirection.y  = upwardJumpForce;
            characterController.Move(movementDirection * Time.fixedDeltaTime);
        }
    }

    private void CharacterFall()
    {
        movementDirection.y  = -upwardJumpForce;
        characterController.Move(movementDirection * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // If the player hits an obstacle
        if(hit.transform.tag == "Obstacle" || hit.transform.tag == "Moving Obstacle")
        {
            // Set the game over condition to true
            PlayerManager.gameOver = true;
        }
    }
}