using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerModule : MonoBehaviour
{
    //public GameObject objectPrefab; // Prefab of the object to spawn
    public float spawnInterval = 1f; // Interval between object spawns
    public int objectsPerWave = 10; // Number of objects to spawn per wave
    public int totalWaves = 3; // Total number of waves in the game
    public float waveDelay = 3f; // Delay between waves

    private int currentWave = 0; // Current wave number
    private int spawnedObjects = 0; // Number of objects spawned in the current wave
    private int activeObjects = 0; // Number of currently active objects
    private bool isWaveInProgress = false; // Flag to track if a wave is in progress

    ObjectPoolScript[] objectPools;
    public GameObject[] positions;


    private void Start()
    {
        StartNewWave();
        objectPools = GetComponentsInChildren<ObjectPoolScript>();
    }

    private void Update()
    {
        GameObject[] totalEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        activeObjects = totalEnemy.Length;

        // Check if all objects in the current wave are inactive
        if (isWaveInProgress && activeObjects == 0)
        {
            isWaveInProgress = false;
            // Check if all waves have been completed
            if (currentWave >= totalWaves)
            {
                // Restart the game
                RestartGame();
            }
            else
            {
                // Start a new wave after a delay
                Invoke("StartNewWave", waveDelay);
            }
        }
    }

    private void StartNewWave()
    {
        Debug.Log("Start Wave" + currentWave);
        currentWave++;
        spawnedObjects = 0;
        activeObjects = 0;
        isWaveInProgress = true;
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    private void SpawnObject()
    {
        if (spawnedObjects >= objectsPerWave)
        {
            CancelInvoke("SpawnObject");
            return;
        }

        // Spawn an object
        int randomPool = Random.Range(0, objectPools.Length);
        int randomPosition = Random.Range(0, positions.Length);

        GameObject obj = objectPools[randomPool].GetPooledObject();

        if (obj == null) return;

        obj.transform.position = positions[randomPosition].transform.position;
        obj.SetActive(true);

        spawnedObjects++;
        activeObjects++;
    }

    private void RestartGame()
    {
        Debug.Log("Restart Wave");

        // Reset game state
        currentWave = 0;
        spawnedObjects = 0;
        activeObjects = 0;

        // Start the first wave after a delay
        Invoke("StartNewWave", waveDelay);
    }
}
