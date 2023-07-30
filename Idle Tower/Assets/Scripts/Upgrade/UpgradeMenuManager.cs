using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform turretUpgrades;
    [SerializeField] private RectTransform aoeUpgrades;
    [SerializeField] private RectTransform towerUpgrades;
    [SerializeField] private Button turretMenuButton;
    [SerializeField] private Button aoeMenuButton;
    [SerializeField] private Button towerMenuButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Sprite buttonPressedSprite;
    [SerializeField] private Sprite buttonReleasedSprite;

    [SerializeField] private Canvas gameCanvas;

    private void Awake()
    {
        turretMenuButton.onClick.AddListener(() => OpenMenu(MenuType.TurretUpgradeMenu));
        aoeMenuButton.onClick.AddListener(() => OpenMenu(MenuType.AOEUpgradeMenu));
        towerMenuButton.onClick.AddListener(() => OpenMenu(MenuType.TowerUpgradeMenu));
        closeButton.onClick.AddListener(() => CloseMenu());

        OpenMenu(MenuType.TurretUpgradeMenu);
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
        turretUpgrades.gameObject.SetActive(false);
        aoeUpgrades.gameObject.SetActive(false);
        towerUpgrades.gameObject.SetActive(false);

        turretMenuButton.GetComponent<Image>().sprite = buttonReleasedSprite;
        aoeMenuButton.GetComponent<Image>().sprite = buttonReleasedSprite;
        towerMenuButton.GetComponent<Image>().sprite = buttonReleasedSprite;

        switch (menuType)
        {
            case MenuType.TurretUpgradeMenu:
                turretMenuButton.GetComponent<Image>().sprite = buttonPressedSprite;
                turretUpgrades.gameObject.SetActive(true);
                break;
            case MenuType.AOEUpgradeMenu:
                aoeMenuButton.GetComponent<Image>().sprite = buttonPressedSprite;
                aoeUpgrades.gameObject.SetActive(true);
                break;
            case MenuType.TowerUpgradeMenu:
                towerMenuButton.GetComponent<Image>().sprite = buttonPressedSprite;
                towerUpgrades.gameObject.SetActive(true);
                break;
        }
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}

public enum MenuType
{
    TurretUpgradeMenu,
    AOEUpgradeMenu,
    TowerUpgradeMenu,
}