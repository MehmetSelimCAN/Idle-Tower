using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }
    private Dictionary<UpgradeListSO, int> currentUpgrades;
    [SerializeField] private UpgradeListSO turretDamageUpgradeList;
    [SerializeField] private UpgradeListSO turretFireRateUpgradeList;
    [SerializeField] private UpgradeListSO rocketDamageUpgradeList;
    [SerializeField] private UpgradeListSO rocketFireRateUpgradeList;
    [SerializeField] private UpgradeListSO beamDamageUpgradeList;
    [SerializeField] private UpgradeListSO beamFireRateUpgradeList;
    [SerializeField] private UpgradeListSO towerHealthUpgradeList;


    [SerializeField] private BulletDataSO initialBulletData;
    [SerializeField] private TowerData initialTowerData;
    [SerializeField] private BeamData initialBeamData;
    [SerializeField] private RocketData initialRocketData;


    [SerializeField] private BulletDataSO bulletData;
    [SerializeField] private TowerData towerData;
    [SerializeField] private BeamData beamData;
    [SerializeField] private RocketData rocketData;


    private void Awake()
    {
        Instance = this;
        currentUpgrades = new Dictionary<UpgradeListSO, int>
        {
            { turretDamageUpgradeList, 0 },
            { turretFireRateUpgradeList, 0 },
            { rocketDamageUpgradeList, 0 },
            { rocketFireRateUpgradeList, 0 },
            { beamDamageUpgradeList, 0 },
            { beamFireRateUpgradeList, 0 },
            { towerHealthUpgradeList, 0 },
        };

        bulletData.damage = initialBulletData.damage;
        bulletData.fireRate = initialBulletData.fireRate;

        towerData.health = initialTowerData.health;

        beamData.damage = initialBeamData.damage;
        beamData.fireRate = initialBeamData.fireRate;

        rocketData.damage = initialRocketData.damage;
        rocketData.fireRate = initialRocketData.fireRate;
    }

    private int GetCurrentUpgradeIndex(UpgradeListSO upgradeListSO)
    {
        return currentUpgrades[upgradeListSO];
    }

    public UpgradeSO GetCurrentUpgradeSO(UpgradeListSO upgradeListSO)
    {
        int currentUpgradeIndex = GetCurrentUpgradeIndex(upgradeListSO);
        return upgradeListSO.list[currentUpgradeIndex];
    }

    public void Upgrade(UpgradeListSO upgradeListSO)
    {
        currentUpgrades[upgradeListSO]++;

        UpgradeSO currentUpgradeSO = GetCurrentUpgradeSO(upgradeListSO);

        switch (upgradeListSO.upgradeType)
        {
            case UpgradeType.TurretDamage:
                bulletData.damage += currentUpgradeSO.incrementValue;
                break;
            case UpgradeType.TurretFireRate:
                bulletData.fireRate += currentUpgradeSO.incrementValue;
                break;
            case UpgradeType.RocketDamage:
                rocketData.damage += currentUpgradeSO.incrementValue;
                break;
            case UpgradeType.RocketFireRate:
                rocketData.fireRate += currentUpgradeSO.incrementValue;
                break;
            case UpgradeType.BeamDamage:
                beamData.damage += currentUpgradeSO.incrementValue;
                break;
            case UpgradeType.BeamFireRate:
                beamData.fireRate += currentUpgradeSO.incrementValue;
                break;
            case UpgradeType.TowerHealth:
                towerData.health += (int)currentUpgradeSO.incrementValue;
                break;
        }
    }
}
