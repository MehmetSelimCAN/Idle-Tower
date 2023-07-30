using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeSO : ScriptableObject
{
    public string upgradeName;
    public UpgradeType upgradeType;
    public float incrementValue;
    public List<RequiredResource> requiredResources;
}

[Serializable]
public struct RequiredResource
{
    public ResourceSO resourceSO;
    public int requiredValue;
}
