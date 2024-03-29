using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    // List of the different types of tiles
    public GameObject[] tilePrefabs;
    // Defines the z-position where the tiles are spawned
    public float spawnPosition = 0;
    // Defines the length of the road tile
    public float tileLength = 30;
    // Defines the number of tile prefabs
    public int numberOfTiles = 6;
    // Defines the player's position
    public Transform playerTransform;
    // Defines the list of spawned tiles
    private List<GameObject> spawns = new List<GameObject>();
    // Defines a buffer distance to prevent the player from falling off at the start 
    private int buffer = 35;
    // Defines the previous tile index
    private int previousTileIndex; 
    // Defines the current tile index
    private int tileIndex;

    void Start()
    {
        // Generate the tiles
        for(int tile = 0; tile < numberOfTiles; tile++)
        {
            // Instantiate Tile1 at start
            if (tile == 0)
                TileSpawner(0);
            else
                // Choose at random
                TileSpawner(Random.Range(0, tilePrefabs.Length));
        }
    }

    void Update()
    {
        // Checks if the player has moved far enough to warrant spawning new tiles
        if (playerTransform.position.z - buffer > spawnPosition - (numberOfTiles * tileLength))
        {
            TileSpawner(Random.Range(0, tilePrefabs.Length));
            // Delete stale tiles
            DeleteTile();
        }       
    }

    public void TileSpawner(int tileIndex)
    {
        // Spawn the road tiles
        GameObject spawn = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPosition, transform.rotation);
        // Add them to the list of spawned tiles
        spawns.Add(spawn);
        // Ensure the next tile is spawned near the previous one
        spawnPosition += tileLength;
    }

    private void DeleteTile()
    {
        // Destroy and delete the tile at the first index of spawns
        Destroy(spawns[0]);
        spawns.RemoveAt(0);
    }
}