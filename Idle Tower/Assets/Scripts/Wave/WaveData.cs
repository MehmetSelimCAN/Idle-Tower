using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WaveData", menuName = "Enemy Waves/WaveData")]
public class WaveData : ScriptableObject
{
    public WaveInfo[] waves;
}


