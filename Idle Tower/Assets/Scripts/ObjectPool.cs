using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set;}
    [Serializable]
    public struct Pool
    {
        public GameObject parentObject;
        public GameObject objectPrefab;
        public Queue<GameObject> pooledObjects;
        public int poolSize;
    }
    
    [SerializeField] private  Pool[] pools = null;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
           
        }
        
        for (int i = 0; i < pools.Length ; i++)
        {
            pools[i].pooledObjects = new Queue<GameObject>();

            for (int j = 0; j < pools[i].poolSize; j++)
            {
                GameObject obj = Instantiate(pools[i].objectPrefab, pools[i].parentObject.transform, true);
                obj.SetActive(false);
                pools[i].pooledObjects.Enqueue(obj);
            }
        }
    }

    public GameObject GetPooledObject(int objectType)
    {
        if (objectType >= pools.Length)
        {
            return null;
        }
        
        GameObject obj =  pools[objectType].pooledObjects.Dequeue();
        
        obj.SetActive(true);
        return obj;
    }
    
    public void ReturnObjectToPool(int objectType, GameObject obj)
    {
        obj.SetActive(false);
        pools[objectType].pooledObjects.Enqueue(obj);
    }
    
}
