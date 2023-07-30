using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerArea : MonoBehaviour, IInteractable
{
    public void Interact()
    {
       SceneManager.Instance.LoadTowerDefenseScene();
    }

    public int GetInteractCount() => 1;

    public bool IsPermanent() => true;
}
