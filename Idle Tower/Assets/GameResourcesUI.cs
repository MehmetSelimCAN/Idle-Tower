using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameResourcesUI : MonoBehaviour
{
    public static GameResourcesUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI ironResourceAmountText;
    [SerializeField] private TextMeshProUGUI goldResourceAmountText;
    [SerializeField] private TextMeshProUGUI diamondResourceAmountText;
    [SerializeField] private TextMeshProUGUI pointText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateResourceTexts()
    {
        ironResourceAmountText.SetText(ResourceManager.Instance.GetResourceValue(ResourceType.Iron).ToString());
        goldResourceAmountText.SetText(ResourceManager.Instance.GetResourceValue(ResourceType.Gold).ToString());
        diamondResourceAmountText.SetText(ResourceManager.Instance.GetResourceValue(ResourceType.Diamond).ToString());
    }

    public void UpdatePointText()
    {
        pointText.SetText(ResourceManager.Instance.GetResourceValue(ResourceType.Point).ToString());
    }
}
