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

    public CanvasGroup RoundClear;

    // Use this for initialization
    void Start()
    {
        Index = SpawnLimit;
        objectPools = GetComponentsInChildren<ObjectPoolScript>();
        RoundClear.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Spawning();
        GameObject[] totalEnemy = GameObject.FindGameObjectsWithTag("Enemy");

        if (totalEnemy.Length == 0 && !canSpawn)
        {
            WaveComplete();
            Debug.Log("Wave Complete");
        }
    }

    void WaveComplete()
    {
        OnDelayWave();
        RoundClear.alpha = 1;
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
        RoundClear.alpha = 0;
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
