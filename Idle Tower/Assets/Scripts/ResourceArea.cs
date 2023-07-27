using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceArea : MonoBehaviour, IInteractable
{
    private int resourceCount = 3;
    [SerializeField] private Transform[] resources;
    [SerializeField] private ResourceSO resourceSO;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        resourceCount = Random.Range(1, 3);
        for (int i = 0; i < resourceCount; i++)
        {
            resources[i].gameObject.SetActive(true);
        }
    }

    public void Interact()
    {
        resources[resourceCount - 1].gameObject.SetActive(false);
        resourceCount--;
        ResourceManager.Instance.AddResource(resourceSO.resourceType, 1);
    }

    public int GetInteractCount() => resourceCount;

    public bool IsPermanent() => false;
}
