using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPowerup : MonoBehaviour
{
    // Add a variable to store the coins sound
    public AudioClip magnetSound;
    // Add a variable to store the volume of the sound
    private float volume = 1.0f;
    // Add a variable to store the rotation speed of the magnet powerup
    public float rotationSpeed = 2.0f;

    void Update()
    {
        //transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(rotationSpeed, rotationSpeed, rotationSpeed) * Time.deltaTime);   
    }

    private void OnTriggerEnter(Collider otherObject)
    {
        // Play the coins sound
        AudioSource.PlayClipAtPoint(magnetSound, transform.position, volume);
        // Destroy the magnet powerup
        GameObject magnetPowerup = GameObject.FindWithTag("MagnetPowerup");
        Destroy(magnetPowerup);
        // If the collider is the magnet powerup
        if(otherObject.tag == "Player")
        {
            // Find all the magnetic coins in the scene
            GameObject[] magneticCoins = GameObject.FindGameObjectsWithTag("MagneticCoin");
            // Loop through each magnetic coin
            foreach (GameObject magneticCoin in magneticCoins)
            {
                // Get the MagneticCoinController component
                MagneticCoinController magneticCoinController = magneticCoin.GetComponent<MagneticCoinController>();
                // Check if the MagneticCoinController is not null
                if (magneticCoinController != null)
                {
                    // Set the coin to active
                    magneticCoinController.isActive = true;
                    // Set the coin's speed
                    magneticCoinController.SetSpeed(PlayerController.instance.racingSpeed);             
                }
            }
        }
    }
}