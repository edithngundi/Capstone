using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTileManager : MonoBehaviour
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
    // Defines the list of spawned tiles
    private List<GameObject> spawns = new List<GameObject>();
    // Defines a buffer distance to prevent the player from falling off at the start 
    private int buffer = 35;

    void Start()
    {
        // Generate the road tiles
        for(int tile = 0; tile < numberOfRoadTiles; tile++)
        {
            // Instantiate RoadTile1 at start
            if (tile == 0)
                RoadTileSpawner(0);
            // Choose at random
            else
                RoadTileSpawner(Random.Range(0, roadTilePefabs.Length));
        }
    }

    void Update()
    {
        // Checks if the player has moved far enough to warrant spawning new road tiles
        if (playerTransform.position.z - buffer > spawnPosition - (numberOfRoadTiles * roadTileLength))
        {
            // Choose at random
            RoadTileSpawner(Random.Range(0, roadTilePefabs.Length));
            // Delete stale tiles
            DeleteRoadTile();
        }       
    }

    public void RoadTileSpawner(int roadTileIndex)
    {
        // Spawn the road tiles
        GameObject spawn = Instantiate(roadTilePefabs[roadTileIndex], transform.forward * spawnPosition, transform.rotation);
        // Add them to the list of spawned tiles
        spawns.Add(spawn);
        // Ensure the next tile is spawned near the previous one
        spawnPosition += roadTileLength;
    }

    private void DeleteRoadTile()
    {
        // Destroy and delete the tile at the first index of spawns
        Destroy(spawns[0]);
        spawns.RemoveAt(0);
    }
}
