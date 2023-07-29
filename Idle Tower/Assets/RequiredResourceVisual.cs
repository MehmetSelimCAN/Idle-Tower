using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequiredResourceVisual : MonoBehaviour
{
    [SerializeField] private Image resourceImage;
    [SerializeField] private TextMeshProUGUI resourceNameText;
    [SerializeField] private TextMeshProUGUI resourceAmountText;
    private ResourceType _resourceType;
    private int resourceAmount;

    private Color availableColor = new Color(1, 1, 1, 1);
    private Color notAvailableColor = new Color(1, 1, 1, 0.35f);

    public void SetResourceAmount(int value)
    {
        resourceAmountText.SetText(value.ToString());
        resourceAmount = value;
    }

    public void SetResourceType(ResourceType resourceType)
    {
        _resourceType = resourceType;
    }

    public void SetResourceImage(Sprite resourceSprite)
    {
        resourceImage.sprite = resourceSprite;
    }

    public void SetVisual(UpgradeSO upgradeSO)
    {
        RequiredResource requiredResource = upgradeSO.requiredResources.Find(x => x.resourceSO.resourceType == _resourceType);
        SetResourceAmount(requiredResource.requiredValue);
    }

    public void UpdateVisual()
    {
        if (ResourceManager.Instance.GetResourceValue(_resourceType) >= resourceAmount)
        {
            resourceImage.color = availableColor;
        }
        else
        {
            resourceImage.color = notAvailableColor;
        }
    }

    public ResourceType GetResourceType()
    {
        return _resourceType;
    }
}
