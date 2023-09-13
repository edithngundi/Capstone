using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTileManager : MonoBehaviour
{
    // List of the different types of road tiles
    public GameObject[] roadTilePefabs;

    // Defines the z-position where the road tiles are spawned
    public float spawnPosition = 0;

    // Defines the length of the road tile
    public float roadTileLength = 30;

    // Defines the number of road tile prefabs
    public int numberOfRoadTiles = 6;

    // Defines the player's position
    public Transform playerTransform;

    /// <summary>
    /// This method is called when the game starts before the first frame update
    /// </summary>
    void Start()
    {
        // Generate the road tiles
        for(int tile = 0; tile < numberOfRoadTiles; tile++)
        {
            if (tile == 0)
                TileSpawner(0);
            else
                TileSpawner(Random.Range(0, roadTilePefabs.Length));
        }
    }

     /// <summary>
    /// This method is called once per frame
    /// </summary>
    void Update()
    {
        //
        if (playerTransform.position.z > spawnPosition - (numberOfRoadTiles * roadTileLength))
        {
            TileSpawner(Random.Range(0, roadTilePefabs.Length));
        }
        
    }

    /// <summary>
    /// This method instantiates new road tiles
    /// </summary>
    public void TileSpawner(int roadTileIndex)
    {
        // Spawn the road tiles
        Instantiate(roadTilePefabs[roadTileIndex], transform.forward * spawnPosition, transform.rotation);
        // Ensure the next tile is spawned near the previous one
        spawnPosition += roadTileLength;
    }
}
