using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class tilemanager: MonoBehaviour
{
    public GameObject[] tilePrefabs; 
    public Transform player; 
    public float spawnZ = 0; 
    public float tileLength = 10; 
    public int tilesOnScreen = 5; 

    private List<GameObject> activeTiles = new List<GameObject>(); 

    void Start()
    {
      
        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        
        if (player.position.z - 35 > spawnZ - tilesOnScreen * tileLength)
        {
            SpawnTile();
            DeleteTile();
        }
    }

    void SpawnTile()
    {
        
        GameObject tile = Instantiate(tilePrefabs[Random.Range(0, tilePrefabs.Length)], transform.forward * spawnZ, transform.rotation);
        activeTiles.Add(tile);
        
        spawnZ += tileLength;
    }

    void DeleteTile()
    {
    
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
