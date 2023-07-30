using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI upgradeNameText;
    [SerializeField] private TextMeshProUGUI upgradeCurrentValue;
    [SerializeField] private TextMeshProUGUI upgradeIncrementValue;
    public RectTransform RequiredResourcesParent;

    [SerializeField] private Button upgradeButton;
    private UpgradeListSO _upgradeListSO;
    public UpgradeListSO UpgradeListSO { get { return _upgradeListSO; } }

    private void Start()
    {
        upgradeButton.onClick.AddListener(() => Upgrade());
    }

    public void SetUpgradeName(string upgradeName)
    {
        upgradeNameText.SetText(upgradeName);
    }

    public void SetCurrentValue(float value)
    {
        upgradeCurrentValue.SetText(value.ToString());
    }

    public void SetIncrementValue(float value)
    {
        upgradeIncrementValue.SetText("+" + value.ToString());
    }

    public void SetUpgradeList(UpgradeListSO upgradeListSO)
    {
        _upgradeListSO = upgradeListSO;
    }

    private void Upgrade()
    {
        if (CanUpgrade())
        {
            SpendResources();

            UpgradeManager.Instance.Upgrade(_upgradeListSO);
            UpdateVisual();
        }
        else
        {
            Debug.Log("You can't upgrade.");
        }
    }

    public void UpdateVisual()
    {
        UpgradeVisualManager.Instance.UpdateUpgradeVisual(_upgradeListSO);
    }

    public bool CanUpgrade()
    {
        UpgradeSO currentUpgradeSO = UpgradeManager.Instance.GetCurrentUpgradeSO(_upgradeListSO);

        foreach (var requiredResource in currentUpgradeSO.requiredResources)
        {
            int currentResourceAmount = ResourceManager.Instance.GetResourceValue(requiredResource.resourceSO.resourceType);
            int requiredResourceAmount = requiredResource.requiredValue;

            if (currentResourceAmount < requiredResourceAmount)
            {
                return false;
            }
        }

        return true;
    }

    private void SpendResources()
    {
        UpgradeSO currentUpgradeSO = UpgradeManager.Instance.GetCurrentUpgradeSO(_upgradeListSO);

        foreach (var requiredResource in currentUpgradeSO.requiredResources)
        {
            ResourceManager.Instance.SpendResource(requiredResource.resourceSO.resourceType, requiredResource.requiredValue);
        }

        UpgradeMenuResourcesUI.Instance.UpdateResourceTexts();
        GameResourcesUI.Instance.UpdateResourceTexts();
    }
}
