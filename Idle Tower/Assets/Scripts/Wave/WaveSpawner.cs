using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public WaveData waveData; 
    private int currentWaveIndex = 0;
    Vector3 targetTransform = Vector3.zero;
    [SerializeField] private float maxSpawnRadius = 15f;
    [SerializeField] private float minSpawnRadius = 10f;

   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnWaves();
        }
    }

    private void SpawnWaves()
    {
        WaveInfo waveInfo = waveData.waves[currentWaveIndex];
        for (int i = 0; i < waveInfo.enemies.Length; i++)
        {
            for (int j = 0; j < waveInfo.enemies[i].enemyCount; j++)
            {
                GameObject enemy = ObjectPool.Instance.GetPooledObject(waveInfo.enemies[i].enemyType);
                var spawnPosition = GetRandomSpawnPosition(targetTransform, maxSpawnRadius, minSpawnRadius);
                enemy.transform.position = new Vector3(spawnPosition.x, enemy.transform.position.y, spawnPosition.z);
            }
        }
        if (currentWaveIndex < waveData.waves.Length - 1)
        {
            currentWaveIndex++;
        }
    }

    private Vector3 GetRandomSpawnPosition(Vector3 targetTransform, float maxRadius, float minRadius)
    {
        // Calculate a random angle in radians
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);

        // Calculate a random distance between minRadius and maxRadius
        float randomDistance = Random.Range(minRadius, maxRadius);

        // Calculate the random spawn position offset
        Vector3 spawnOffset = new Vector3(Mathf.Cos(randomAngle) * randomDistance, 0f, Mathf.Sin(randomAngle) * randomDistance);

        // Calculate the final spawn position
        Vector3 spawnPosition = targetTransform + spawnOffset;

        return spawnPosition;
    }
}
