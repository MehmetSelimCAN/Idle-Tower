using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeVisualManager : MonoBehaviour
{
    public static UpgradeVisualManager Instance { get; private set; }

    [SerializeField] private UpgradeTypeList turretUpgrades;
    [SerializeField] private UpgradeTypeList aoeUpgrades;
    [SerializeField] private UpgradeTypeList towerUpgrades;

    [SerializeField] private RectTransform turretUpgradesParent;
    [SerializeField] private RectTransform aoeUpgradesParent;
    [SerializeField] private RectTransform towerUpgradesParent;

    [SerializeField] private RectTransform upgradeVisualTemplate;
    [SerializeField] private int upgradesHorizontalCount;
    [SerializeField] private int upgradesOffset;
    private List<UpgradeVisual> upgradeVisuals;

    [SerializeField] private RectTransform requiredResourceVisualTemplate;
    [SerializeField] private int requiredResourceOffset;
    private Dictionary<UpgradeListSO, List<RequiredResourceVisual>> upgradeRequiredResourceDictionary;


    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
        upgradeRequiredResourceDictionary = new Dictionary<UpgradeListSO, List<RequiredResourceVisual>>();
        upgradeVisuals = new List<UpgradeVisual>();
    }

    private void Start()
    {
        Initialize(turretUpgrades, turretUpgradesParent);
        Initialize(aoeUpgrades, aoeUpgradesParent);
        Initialize(towerUpgrades, towerUpgradesParent);
    }

    private void Initialize(UpgradeTypeList upgradeTypeList, RectTransform parent)
    {
        int rowNumber = 0;
        int horizontal = 0;
        for (int i = 0; i < upgradeTypeList.list.Count; i++)
        {
            var upgradeVisual = Instantiate(upgradeVisualTemplate, parent).GetComponent<UpgradeVisual>();
            UpgradeSO upgradeSO = UpgradeManager.Instance.GetCurrentUpgradeSO(upgradeTypeList.list[i]);
            upgradeVisual.SetUpgradeName(upgradeSO.upgradeName);
            upgradeVisual.SetIncrementValue(upgradeSO.incrementValue);
            upgradeVisual.SetUpgradeList(upgradeTypeList.list[i]);

            List<RequiredResourceVisual> requiredResourceVisuals = new List<RequiredResourceVisual>();
            for (int j = 0; j < upgradeSO.requiredResources.Count; j++)
            {
                var requiredResourceVisual = Instantiate(requiredResourceVisualTemplate, upgradeVisual.RequiredResourcesParent).GetComponent<RequiredResourceVisual>();
                RequiredResource requiredResource = upgradeSO.requiredResources[j];
                requiredResourceVisual.SetResourceAmount(requiredResource.requiredValue);
                requiredResourceVisual.SetResourceType(requiredResource.resourceSO.resourceType);
                requiredResourceVisual.SetResourceImage(requiredResource.resourceSO.sprite);
                requiredResourceVisual.UpdateVisual();
                requiredResourceVisual.transform.localPosition += new Vector3(j * requiredResourceOffset, 0, 0);
                requiredResourceVisuals.Add(requiredResourceVisual);
                requiredResourceVisual.gameObject.SetActive(true);
            }

            upgradeRequiredResourceDictionary.Add(upgradeTypeList.list[i], requiredResourceVisuals);

            if (i % 3 == 0)
            {
                rowNumber--;
                horizontal = 0;
            }
            upgradeVisual.transform.localPosition += new Vector3(horizontal * upgradesOffset, rowNumber * 600, 0);
            upgradeVisuals.Add(upgradeVisual);
            upgradeVisual.gameObject.SetActive(true);

            horizontal++;
        }
    }

    public void UpdateUpgradeVisual(UpgradeListSO upgradeListSO)
    {
        UpgradeVisual upgradeVisual = upgradeVisuals.Find(x => x.UpgradeListSO == upgradeListSO);
        UpgradeSO upgradeSO = UpgradeManager.Instance.GetCurrentUpgradeSO(upgradeListSO);
        upgradeVisual.SetUpgradeName(upgradeSO.upgradeName);
        upgradeVisual.SetIncrementValue(upgradeSO.incrementValue);
        upgradeVisual.SetUpgradeList(upgradeListSO);

        SetRequiredResourceVisuals(upgradeListSO);
        UpdateRequiredResourceVisuals();
    }

    public void UpdateRequiredResourceVisuals()
    {
        foreach (var upgradeSO in upgradeRequiredResourceDictionary.Keys)
        {
            List<RequiredResourceVisual> requiredResourceVisuals = upgradeRequiredResourceDictionary[upgradeSO];
            for (int i = 0; i < requiredResourceVisuals.Count; i++)
            {
                requiredResourceVisuals[i].UpdateVisual();
            }
        }
    }

    public void SetRequiredResourceVisuals(UpgradeListSO upgradeListSO)
    {
        List<RequiredResourceVisual> requiredResourceVisuals = upgradeRequiredResourceDictionary[upgradeListSO];
        UpgradeSO upgradeSO = UpgradeManager.Instance.GetCurrentUpgradeSO(upgradeListSO);

        for (int i = 0; i < requiredResourceVisuals.Count; i++)
        {
            requiredResourceVisuals[i].SetVisual(upgradeSO);
        }
    }
}
