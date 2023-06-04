using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    ObjectPoolScript[] objectPools;
    public GameObject[] positions;

    public float spawnTime = 3;
    float timeSinceLastSpawn = 0;

    public float waveDelay;
    float CurrentDelay = 0;

    public int SpawnLimit;
    public int Index;

    public bool canSpawn = true;

    private int activeObjects = 0; // Number of currently active objects


    // Use this for initialization
    void Start()
    {
        Index = SpawnLimit;
        objectPools = GetComponentsInChildren<ObjectPoolScript>();
        //RoundClear.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Spawning();

        string[] tags = { "Type1", "Type2", "Type3" };
        List<GameObject> objectsWithTags = new List<GameObject>();

        foreach (string tag in tags)
        {
            GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(tag);
            objectsWithTags.AddRange(foundObjects);
        }

        GameObject[] totalObjects = objectsWithTags.ToArray();
        activeObjects = totalObjects.Length;

        if (activeObjects == 0 && !canSpawn)
        {
            WaveComplete();
            Debug.Log("Wave Complete");
        }
    }

    void WaveComplete()
    {
        OnDelayWave();
        //RoundClear.alpha = 1;
    }

    void OnDelayWave()
    {
        CurrentDelay += Time.deltaTime;
        if (CurrentDelay > waveDelay)
        {
            SpawnNextWave();
            CurrentDelay = 0;
        }
    }

    void SpawnNextWave()
    {
        //RoundClear.alpha = 0;
        //currentWaveNumber++;
        canSpawn = true;
        Index = SpawnLimit;
    }

    public void Spawning()
    {
        timeSinceLastSpawn += Time.deltaTime;
        
        if (canSpawn)
        {
            if (timeSinceLastSpawn > spawnTime && objectPools != null)
            {
                Index--;

                int randomPool = Random.Range(0, objectPools.Length);
                int randomPosition = Random.Range(0, positions.Length);
                try
                {
                    GameObject obj = objectPools[randomPool].GetPooledObject();

                    if (obj == null) return;

                    obj.transform.position = positions[randomPosition].transform.position;
                    obj.SetActive(true);
                }
                catch
                {
                }

                if (Index == 0)
                {
                    canSpawn = false;
                }

                timeSinceLastSpawn = 0;
            }
        }
    }
}
