using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }
    private Dictionary<UpgradeListSO, int> currentUpgrades;
    [SerializeField] private UpgradeListSO gunAttackUpgradeList;
    [SerializeField] private UpgradeListSO gunAttackSpeedUpgradeList;


    private void Awake()
    {
        Instance = this;
        currentUpgrades = new Dictionary<UpgradeListSO, int>
        {
            { gunAttackUpgradeList, 0 },
            { gunAttackSpeedUpgradeList, 0 },
        };
    }

    public int GetCurrentUpgradeIndex(UpgradeListSO upgradeListSO)
    {
        return currentUpgrades[upgradeListSO];
    }

    public void Upgrade(UpgradeListSO upgradeListSO)
    {
        //TODO: Upgrade'e göre stat deðiþimi yapýlacak?
        currentUpgrades[upgradeListSO]++;
    }
}
