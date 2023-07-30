using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private ResourceListSO resources;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        int resourceAreaCount = Random.Range(2, 5);
        for (int i = 0; i < resourceAreaCount; i++)
        {
            SpawnResourceArea();
        }
    }

    private void SpawnResourceArea()
    {
        int randomResourceIndex = Random.Range(0, resources.list.Count);
        ResourceSO resourceSO = resources.list[randomResourceIndex];
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(resourceSO.prefab, spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
    }
}
