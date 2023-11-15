using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticCoinController : MonoBehaviour
{
    // Add a variable to store the coin sound
    public AudioClip coinSound;
    private float volume = 1.0f;
    // Update is called once per frame
    // Player's position
    public Transform player;

    void Update()
    {
        // Rotate the coin along the x-axis
        transform.Rotate(100 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        // Play the coin sound
        AudioSource.PlayClipAtPoint(coinSound, transform.position, volume);
        // If the collider is the player
        if(otherCollider.tag == "Player")
        {
            // Increase the number of coins collected
            PlayerManager.coinsCollected += 1;

            // Destroy the coin
            //Destroy(gameObject);
            Debug.Log("Coin Destroyed");
            
        }
    }
}
