using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceArea : MonoBehaviour, IInteractable
{
    private int resourceCount = 3;

    public void Interact()
    {
        Debug.Log("Resource gathered!");
    }

    public int GetInteractCount()
    {
        return resourceCount;
    }

    public bool IsPermanent()
    {
        return false;
    }
}
