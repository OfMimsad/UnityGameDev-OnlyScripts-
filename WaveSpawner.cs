using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Wave
{
    public string waveName;
    public int nuOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spwanInterval;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public Animator anim;
    public Text waveName;

    private Wave currentWave;
    private int currentWaveNumber;

    private bool canSpawn = true;
    private bool canAnimate= false;
    private float nextSpawnTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0 && currentWaveNumber+1 != waves.Length && canAnimate)
        {
            waveName.text = waves[currentWaveNumber + 1].waveName;
            anim.SetBool("WaveComplete", canAnimate);
            
            canAnimate = false;

        }
    }
    
    void SpwanNextWave()
    {
        currentWaveNumber++;
        canSpawn = true;

    }

    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject cloneObject = Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            cloneObject.SetActive(true);
            currentWave.nuOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spwanInterval;
            if (currentWave.nuOfEnemies == 0)
            {
                canSpawn = false;
                canAnimate = true;
            }
        }
    }




}

