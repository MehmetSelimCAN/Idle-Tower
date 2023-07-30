using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public string upgradeName;
    public float incrementValue;
    public List<RequiredResource> requiredResources;
}

[Serializable]
public struct RequiredResource
{
    public ResourceSO resourceSO;
    public int requiredValue;
}
