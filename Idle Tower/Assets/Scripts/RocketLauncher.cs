using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private float launchInterval = 3f;
    [SerializeField] private RocketData rocketData;
    [SerializeField] private GameObject rocketLauncherVisual;

    private Transform enemyParent;

    private void Start()
    {
        enemyParent = GameObject.Find("EnemyParent").transform;
        launchInterval = rocketData.fireRate;
        StartCoroutine(LaunchRockets());
    }

    private IEnumerator LaunchRockets()
    {
        while (true)
        {
            yield return new WaitForSeconds(launchInterval);
            
            if (HasActiveEnemies())
            {
                LaunchRocket();
            }
        }
    }

    private bool HasActiveEnemies()
    {
        foreach (Transform enemy in enemyParent)
        {
            if (enemy.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void LaunchRocket()
    {
        GameObject rocketObject = Instantiate(rocketPrefab, transform.position, transform.rotation);
        Rocket rocket = rocketObject.GetComponent<Rocket>();

        GameObject randomEnemy = GetRandomActiveEnemy();

        if (randomEnemy != null)
        {
            rocket.SetTarget(randomEnemy);
            Vector3 targetDirection = randomEnemy.transform.position - transform.position;
            targetDirection.y = 0f;
            
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection.normalized);
            
            Vector3 eulerRotation = targetRotation.eulerAngles;
            eulerRotation.x = -90f; 
            eulerRotation.z = 90f;  
            targetRotation = Quaternion.Euler(eulerRotation);
            rocketLauncherVisual.transform.rotation = targetRotation;
            
        }
    }



    private GameObject GetRandomActiveEnemy()
    {
        foreach (Transform enemy in enemyParent)
        {
            if (enemy.gameObject.activeSelf)
            {
                return enemy.gameObject;
            }
        }
        return null;
    }
}
