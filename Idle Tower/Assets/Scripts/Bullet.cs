using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float lifeTime = 3f;
    private float timeAlive = 0f;
    public float speed = 200f;

    [SerializeField] private BulletDataSO bulletData;
    private void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifeTime)
        {
            ObjectPool.Instance.ReturnObjectToPool(0,gameObject);
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Debug.Log("I hit an enemy!");
            other.gameObject.GetComponent<Enemy>().TakeDamage(bulletData.damage);
        }
        ObjectPool.Instance.ReturnObjectToPool(0, gameObject);
        
    }
}
