using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeVisualManager : MonoBehaviour
{
    public static UpgradeVisualManager Instance { get; private set; }

    [SerializeField] private UpgradeListDenemeSO gunUpgrades;

    [SerializeField] private RectTransform upgradeVisualTemplate;
    [SerializeField] private int upgradesHorizontalCount;
    [SerializeField] private int upgradesOffset;
    private List<UpgradeVisual> upgradeVisuals;

    [SerializeField] private RectTransform requiredResourceVisualTemplate;
    [SerializeField] private int requiredResourceOffset;
    private Dictionary<UpgradeListSO, List<RequiredResourceVisual>> upgradeRequiredResourceDictionary;

    [SerializeField] private RectTransform gunUpgradesParent;
    [SerializeField] private RectTransform aoeUpgradesParent;
    [SerializeField] private RectTransform generalUpgradesParent;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
        upgradeRequiredResourceDictionary = new Dictionary<UpgradeListSO, List<RequiredResourceVisual>>();
        upgradeVisuals = new List<UpgradeVisual>();
    }

    private void Start()
    {
        int rowNumber = 0;
        int horizontal = 0;
        for (int i = 0; i < gunUpgrades.list.Count; i++)
        {
            var upgradeVisual = Instantiate(upgradeVisualTemplate, gunUpgradesParent).GetComponent<UpgradeVisual>();
            int currentUpgradeIndex = UpgradeManager.Instance.GetCurrentUpgradeIndex(gunUpgrades.list[i]);
            UpgradeSO upgradeSO = gunUpgrades.list[i].list[currentUpgradeIndex];
            upgradeVisual.SetUpgradeName(upgradeSO.upgradeName);
            upgradeVisual.SetIncrementValue(upgradeSO.incrementValue);
            upgradeVisual.SetUpgradeList(gunUpgrades.list[i]);

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

            upgradeRequiredResourceDictionary.Add(gunUpgrades.list[i], requiredResourceVisuals);

            if (i % 3 == 0)
            {
                rowNumber--;
                horizontal = 0;
            }
            upgradeVisual.transform.localPosition += new Vector3(horizontal * upgradesOffset, rowNumber * 400, 0);
            upgradeVisuals.Add(upgradeVisual);
            upgradeVisual.gameObject.SetActive(true);

            horizontal++;
        }
    }

    public void UpdateUpgradeVisual(UpgradeListSO upgradeListSO)
    {
        UpgradeVisual upgradeVisual = upgradeVisuals.Find(x => x.UpgradeListSO == upgradeListSO);
        int currentUpgradeIndex = UpgradeManager.Instance.GetCurrentUpgradeIndex(upgradeListSO);
        UpgradeSO upgradeSO = upgradeListSO.list[currentUpgradeIndex];
        upgradeVisual.SetUpgradeName(upgradeSO.upgradeName);
        upgradeVisual.SetIncrementValue(upgradeSO.incrementValue);
        upgradeVisual.SetUpgradeList(upgradeListSO);

        SetRequiredResourceVisuals(upgradeListSO);
        UpgradeVisualManager.Instance.UpdateRequiredResourceVisuals();
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
        int currentUpgradeIndex = UpgradeManager.Instance.GetCurrentUpgradeIndex(upgradeListSO);
        UpgradeSO upgradeSO = upgradeListSO.list[currentUpgradeIndex];

        for (int i = 0; i < requiredResourceVisuals.Count; i++)
        {
            requiredResourceVisuals[i].SetVisual(upgradeSO);
        }
    }
}
