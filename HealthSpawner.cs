using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Waves
{
    public string waveName;
    public int NumOfHealths;
    public GameObject[] HealthBars;
    public float spawnIntervals;
}

public class HealthSpawner : MonoBehaviour
{
    public Waves[] waves;
    public Transform[] spawnPoints;

    private Waves currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;
    private bool canSpawn = true;

    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        HaelthSpawn();

    }

    void HaelthSpawn()
    {
        if(canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomHealthBar = currentWave.HealthBars[Random.Range(0, currentWave.HealthBars.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomHealthBar, randomPoint.position, Quaternion.identity);
            currentWave.NumOfHealths--;
            nextSpawnTime = Time.time + currentWave.spawnIntervals;
            if(currentWave.NumOfHealths == 0)
            {
                canSpawn = false;
            }
        }
    }
}
