using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeArea : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Upgrade menu opened!");
    }

    public int GetInteractCount()
    {
        return 1;
    }

    public bool IsPermanent()
    {
        return true;
    }
}
