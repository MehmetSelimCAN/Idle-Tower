using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLauncher : MonoBehaviour
{
    [SerializeField] private GameObject beamPrefab;
    [SerializeField] private float launchInterval = 3f;
    [SerializeField] private float beamLength = 100f;
    [SerializeField] private BeamData beamData;
    [SerializeField] private Transform platform;

    private Transform enemyParent;

    private void Start()
    {
        platform = transform.parent.parent;
        enemyParent = GameObject.Find("EnemyParent").transform;
        StartCoroutine(LaunchBeams());
        launchInterval = beamData.fireRate;
    }

    private IEnumerator LaunchBeams()
    {
        while (true)
        {
            yield return new WaitForSeconds(launchInterval);
            LaunchBeam();
        }
    }

    private void LaunchBeam()
    {
        GameObject beamObject = Instantiate(beamPrefab, transform.position, Quaternion.identity);
        Beam beam = beamObject.GetComponent<Beam>();

        GameObject randomEnemy = GetRandomActiveEnemy();

        if (randomEnemy != null)
        {
            Vector3 targetPosition = randomEnemy.transform.position;
            
            Vector3 lookAtDirection = targetPosition - platform.position;
            lookAtDirection.y = 0f; 
            platform.LookAt(platform.position + lookAtDirection, Vector3.up);
            
            Vector3 startPoint = transform.position;
            Vector3 direction = randomEnemy.transform.position - startPoint;
            Vector3 endPoint = startPoint + direction.normalized * beamLength;
            beam.ActivateBeam(startPoint, endPoint);
            
        }
    }

    private GameObject GetRandomActiveEnemy()
    {
        List<Transform> activeEnemies = new List<Transform>();

        foreach (Transform enemy in enemyParent)
        {
            if (enemy.gameObject.activeSelf)
            {
                activeEnemies.Add(enemy);
            }
        }

        if (activeEnemies.Count > 0)
        {
            Transform randomEnemy = activeEnemies[Random.Range(0, activeEnemies.Count)];
            return randomEnemy.gameObject;
        }

        return null;
    }
}
