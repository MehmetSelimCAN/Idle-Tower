using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade List")]
public class UpgradeListSO : ScriptableObject
{
    public UpgradeType upgradeType;
    public List<UpgradeSO> list;
}
