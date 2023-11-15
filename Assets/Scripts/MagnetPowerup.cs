using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPowerup : MonoBehaviour
{
    // Add a variable to store the magnet sound
    public AudioClip magnetSound;
    private float volume = 1.0f;
    public float rotationSpeed = 2.0f;
    // Player's position
    public Transform player;
    public bool isActive = false;

    void MagnetIsOn()
    {
        isActive = true;
    }

    void MagnetIsOff()
    {
        isActive = false;
    }

    void Update()
    {
        //transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(rotationSpeed, rotationSpeed, rotationSpeed) * Time.deltaTime);   
    }

    private void OnTriggerEnter(Collider otherObject)
    {
        // Play the coin sound
        AudioSource.PlayClipAtPoint(magnetSound, transform.position, volume);
        // If the collider is the magnet powerup
        if(otherObject.tag == "Player")
        {
            isActive = true;
            
            Vector3 playerPosition = new Vector3(PlayerController.instance.playerPosX, PlayerController.instance.playerPosY, PlayerController.instance.playerPosZ);
            GameObject magneticCoin = GameObject.FindWithTag("MagneticCoin");
            if (isActive == true)
            {
                StartCoroutine(MoveCoinToPlayer(magneticCoin, playerPosition));
            } 
        } 
    }
    IEnumerator MoveCoinToPlayer(GameObject magneticCoin, Vector3 playerPosition)
    {
        float speed = 3f;
        while (Vector3.Distance(magneticCoin.transform.position, playerPosition) > 0.1f)
        {
            magneticCoin.transform.position = Vector3.Lerp(magneticCoin.transform.position, playerPosition, speed*Time.deltaTime);
            yield return null;
        }
        GameObject magnetPowerup = GameObject.FindWithTag("MagnetPowerup");
        // Destroy the powerup
        Destroy(magnetPowerup);
    }   
}
