using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractAreaVisual : MonoBehaviour
{
    private Canvas canvas;
    [SerializeField] private Image interactTimerVisual;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    public void Hide()
    {
        canvas.enabled = false;
    }

    public void Show()
    {
        canvas.enabled = true;
    }

    public void UpdateTimerVisual(float remainingTime, float requiredTime)
    {
        interactTimerVisual.fillAmount = 1 - (remainingTime / requiredTime);
    }

    public void ResetTimerVisual()
    {
        interactTimerVisual.fillAmount = 0;
    }
}
