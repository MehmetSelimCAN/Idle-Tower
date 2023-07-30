using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
   private float health;
   [SerializeField] private TowerData towerData;

   private void Start()
   {
      health = towerData.health;
   }

   public void TakeDamage(float damage)
   {
      health -= damage;
      if (health <= 0)
      {
         Debug.Log("I am dead");
      }
   }
}
