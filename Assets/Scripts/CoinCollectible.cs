using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the coin along the x-axis
        transform.Rotate(100 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the collider is the player
        if(other.tag == "Player")
        {
            // Increase the number of coins collected
            PlayerManager.coinsCollected += 1;

            // In the meantime -- use this to report the number of coins collected
            Debug.Log("Coins:" + PlayerManager.coinsCollected);

            // Destroy the coin
            Destroy(gameObject);
        }
    }
}
