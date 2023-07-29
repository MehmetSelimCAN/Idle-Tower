using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    [SerializeField] private ResourceListSO resources;

    private Dictionary<ResourceType, int> m_Resources;

    private void Awake()
    {
        m_Resources = new Dictionary<ResourceType, int>();
        for (int i = 0; i < resources.list.Count; i++)
        {
            AddResourceType(resources.list[i].resourceType, 0);
        }

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddResourceType(ResourceType resourceType, int value)
    {
        if (!m_Resources.TryAdd(resourceType, value))
        {
            Debug.Log(resourceType + " is already in dictionary");
        }
    }

    public int GetResourceValue(ResourceType resourceType)
    {
        return m_Resources[resourceType];
    }

    public bool CanSpendResource(ResourceType resourceType, int spendValue)
    {
        return m_Resources[resourceType] >= spendValue;
    }

    public void AddResource(ResourceType resourceType, int value)
    {
        m_Resources[resourceType] += value;
        Debug.Log(resourceType + " Resource gathered!" + " Total: " + m_Resources[resourceType]);
        UpgradeVisualManager.Instance.UpdateRequiredResourceVisuals();
        UpgradeMenuResourcesUI.Instance.UpdateResourceTexts();
        GameResourcesUI.Instance.UpdateResourceTexts();
    }

    public void SpendResource(ResourceType resourceType, int value)
    {
        m_Resources[resourceType] -= value;
        UpgradeVisualManager.Instance.UpdateRequiredResourceVisuals();
        UpgradeMenuResourcesUI.Instance.UpdateResourceTexts();
        GameResourcesUI.Instance.UpdateResourceTexts();
    }
}