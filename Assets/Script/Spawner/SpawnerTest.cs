using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTest : MonoBehaviour
{
    public GameObject objectPrefab; // Prefab of the object to spawn
    public float spawnInterval = 1f; // Interval between object spawns
    public int objectsPerWave = 10; // Number of objects to spawn per wave
    public int totalWaves = 3; // Total number of waves in the game
    public float waveDelay = 3f; // Delay between waves

    private int currentWave = 0; // Current wave number
    private int spawnedObjects = 0; // Number of objects spawned in the current wave
    private int activeObjects = 0; // Number of currently active objects
    private bool isWaveInProgress = false; // Flag to track if a wave is in progress

    [SerializeField] private GameObject RoundPanel;

    private void Start()
    {
        StartNewWave();
        RoundPanel.SetActive(false);
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
                RoundPanel.SetActive(true);
                Invoke("StartNewWave", waveDelay);
            }
        }
    }

    private void StartNewWave()
    {
        Debug.Log("Start Wave" + currentWave);
        RoundPanel.SetActive(false);

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
        GameObject newObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);
        newObject.SetActive(true);

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
