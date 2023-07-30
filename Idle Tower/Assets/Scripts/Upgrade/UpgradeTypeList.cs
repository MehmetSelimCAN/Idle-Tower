using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade Type List")]
public class UpgradeTypeList : ScriptableObject
{
    public List<UpgradeListSO> list;
}
