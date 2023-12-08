using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticCoinController : MonoBehaviour
{
    // Add a variable to store the coin sound
    public AudioClip coinSound;
    private float volume = 1.0f;

    public bool isActive = false;

    private float racingSpeed;

    private GameObject player;

    //private float speed = 10.0f;

    private int magnetRange = 30;

    public void SetSpeed(float newSpeed)
    {
        racingSpeed = newSpeed;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // Rotate the coin along the x-axis
        transform.Rotate(100 * Time.deltaTime, 0, 0);
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            Vector3 playerPosition = new Vector3(PlayerController.instance.playerPosX, PlayerController.instance.playerPosY, PlayerController.instance.playerPosZ);
            float distance = Vector3.Distance(transform.position, playerPosition);
            if (distance <= magnetRange)
            {
                transform.position = Vector3.Lerp(transform.position, playerPosition, racingSpeed * Time.deltaTime);
            }
            else
            {
                isActive = false;
            }          
        }
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        // Play the coin sound
        AudioSource.PlayClipAtPoint(coinSound, transform.position, volume);
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