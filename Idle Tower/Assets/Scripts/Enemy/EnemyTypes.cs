using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Type", menuName = "Enemy")]
public class EnemyTypes : ScriptableObject
{
   public string enemyName;
   public float health;
   public float damage;
   public float speed;
   
}
