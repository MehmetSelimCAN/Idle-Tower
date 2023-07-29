using UnityEngine;

[CreateAssetMenu(fileName = "WaveInfo", menuName = "Enemy Waves/WaveInfo", order = 1)]
public class WaveInfo : ScriptableObject
{
    public EnemyData[] enemies;
}

[System.Serializable]
public class EnemyData
{
    public int enemyType;
    public int enemyCount;
}