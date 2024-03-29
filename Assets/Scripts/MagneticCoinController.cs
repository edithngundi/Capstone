using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticCoinController : MonoBehaviour
{
    // Check to determine if the coin is active
    public bool isActive = false;
    // Set the speed of the coin
    private float racingSpeed;
    // Reference to the player object
    private GameObject player;
    // Set the range of the magnet
    private int magnetRange = 30;
    //  Set the speed of the coin
    public void SetSpeed(float newSpeed)
    {
        // Set the speed of the coin
        racingSpeed = newSpeed;
    }

    void Start()
    {
        // Set the speed of the coin
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // Rotate the coin along the x-axis
        transform.Rotate(100 * Time.deltaTime, 0, 0);
    }

    void FixedUpdate()
    {
        // Check if the coin is active
        if (isActive)
        {
            // Set the position of the player
            Vector3 playerPosition = new Vector3(PlayerController.instance.playerPosX, PlayerController.instance.playerPosY, PlayerController.instance.playerPosZ);
            // Calculate the distance between the coin and the player
            float distance = Vector3.Distance(transform.position, playerPosition);
            // Check if the distance is less than the magnet range
            if (distance <= magnetRange)
            {
                // Move the coin towards the player
                transform.position = Vector3.Lerp(transform.position, playerPosition, racingSpeed * Time.deltaTime);
            }
            else
            {
                // Set the coin to inactive
                isActive = false;
            }          
        }
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        // If the collider is the player
        if(otherCollider.tag == "Player")
        {
            // Disable the coin's collider
            GetComponent<Collider>().enabled = false;

            // Increase the number of coins collected
            PlayerManager.coinsCollected += 1;

            // Destroy the coin
            Destroy(gameObject);
            Debug.Log("Coin Destroyed");           
        }
    }
}