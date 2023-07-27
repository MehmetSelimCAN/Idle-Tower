using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeSO : ScriptableObject
{
    public UpgradeType upgradeType;
    public float incrementValue;
    public List<RequiredResource> requiredResources;
}

[Serializable]
public struct RequiredResource
{
    public ResourceType resourceType;
    public int requiredValue;
}
