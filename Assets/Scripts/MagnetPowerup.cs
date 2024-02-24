using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPowerup : MonoBehaviour
{
    // Add a variable to store the coins sound
    public AudioClip magnetSound;
    private float volume = 1.0f;
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

        GameObject magnetPowerup = GameObject.FindWithTag("MagnetPowerup");
        Destroy(magnetPowerup);
        // If the collider is the magnet powerup
        if(otherObject.tag == "Player")
        {
            GameObject[] magneticCoins = GameObject.FindGameObjectsWithTag("MagneticCoin");

            foreach (GameObject magneticCoin in magneticCoins)
            {
                
                MagneticCoinController magneticCoinController = magneticCoin.GetComponent<MagneticCoinController>();
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