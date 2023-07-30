using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyTypes enemyData;
    private Rigidbody rb;
    private GameObject tower;
    private Vector3 targetDir;

    private float health;
    private float damage;
    private float speed;
    private string typeName;

    private bool shouldStop;
    private float initialYPosition;

    private float damageRate = 1f;
    
    private void Start()
    {
        InitializeEnemy();
        FindTower();
        initialYPosition = transform.position.y;
    }

    private void FixedUpdate()
    {
        MoveTowardsTower();
    }
    
    private void InitializeEnemy()
    {
        health = enemyData.health;
        damage = enemyData.damage;
        speed = enemyData.speed;
        typeName = enemyData.enemyName;
        rb = GetComponent<Rigidbody>();
    }

    private void MoveTowardsTower()
    {
        if (!shouldStop)
        {
            Vector3 targetDirWithoutY = new Vector3(targetDir.x, 0f, targetDir.z);
            
            Vector3 newPosition = transform.position;
            newPosition.y = initialYPosition;
            
            rb.MovePosition(newPosition + targetDirWithoutY.normalized * speed * Time.fixedDeltaTime);
        }

    }

    private void FindTower()
    {
        tower = GameObject.FindWithTag("Tower");
        var towerPos = tower.transform.position;
        targetDir = towerPos - transform.position;
        
        Vector3 targetDirWithoutY = new Vector3(targetDir.x, 0f, targetDir.z);
        
        Quaternion newRotation = Quaternion.LookRotation(targetDirWithoutY);
        transform.rotation = newRotation;
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            ObjectPool.Instance.ReturnObjectToPool(1, gameObject);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            shouldStop = true;
            other.gameObject.GetComponent<Tower>().TakeDamage(damage);
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            StartCoroutine(DamageOverTime());
        }
    }

    private void OnCollisionExit(Collision other)
    {
        StopCoroutine(DamageOverTime());
    }

    private IEnumerator DamageOverTime()
    {
        while (shouldStop)
        {
            // Wait for the specified interval before applying damage
            yield return new WaitForSeconds(damageRate);
            tower.GetComponent<Tower>().TakeDamage(damage);
        }
    }
}
