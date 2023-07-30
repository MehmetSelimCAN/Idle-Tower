using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform gunUpgrades;
    [SerializeField] private RectTransform aoeUpgrades;
    [SerializeField] private RectTransform generalUpgrades;
    [SerializeField] private Button gunMenuButton;
    [SerializeField] private Button aoeMenuButton;
    [SerializeField] private Button generalMenuButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Sprite buttonPressedSprite;
    [SerializeField] private Sprite buttonReleasedSprite;

    [SerializeField] private Canvas gameCanvas;

    private void Awake()
    {
        gunMenuButton.onClick.AddListener(() => OpenMenu(MenuType.GunUpgradeMenu));
        aoeMenuButton.onClick.AddListener(() => OpenMenu(MenuType.AOEUpgradeMenu));
        generalMenuButton.onClick.AddListener(() => OpenMenu(MenuType.GeneralUpgradeMenu));
        closeButton.onClick.AddListener(() => CloseMenu());

        OpenMenu(MenuType.GunUpgradeMenu);
    }

    private void OnEnable()
    {
        gameCanvas.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        gameCanvas.gameObject.SetActive(true);
    }

    private void OpenMenu(MenuType menuType)
    {
        gunUpgrades.gameObject.SetActive(false);
        aoeUpgrades.gameObject.SetActive(false);
        generalUpgrades.gameObject.SetActive(false);

        gunMenuButton.GetComponent<Image>().sprite = buttonReleasedSprite;
        aoeMenuButton.GetComponent<Image>().sprite = buttonReleasedSprite;
        generalMenuButton.GetComponent<Image>().sprite = buttonReleasedSprite;

        switch (menuType)
        {
            case MenuType.GunUpgradeMenu:
                gunMenuButton.GetComponent<Image>().sprite = buttonPressedSprite;
                gunUpgrades.gameObject.SetActive(true);
                break;
            case MenuType.AOEUpgradeMenu:
                aoeMenuButton.GetComponent<Image>().sprite = buttonPressedSprite;
                aoeUpgrades.gameObject.SetActive(true);
                break;
            case MenuType.GeneralUpgradeMenu:
                generalMenuButton.GetComponent<Image>().sprite = buttonPressedSprite;
                generalUpgrades.gameObject.SetActive(true);
                break;
        }
    }

    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}

public enum MenuType
{
    GunUpgradeMenu,
    AOEUpgradeMenu,
    GeneralUpgradeMenu,
}