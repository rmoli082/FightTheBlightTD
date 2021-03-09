using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Analytics;

public class TurretUpgrade : TurretUpgradePanels
{

    public void FirstSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.DamageAmount *= 1.4f;
            upgradePanel.turretToUpgrade.slotLevel[0]++;
            upgradePanel.turretToUpgrade.SellCost = upgradePanel.turretToUpgrade.SellCost + (upgradeCost / 2);
            LevelManager.Instance.AdjustGold(-upgradeCost);
            LevelManager.Instance.sceneData.turretUpgradePanel.SetActive(false);
            Analytics.CustomEvent("First Turret Upgrade Bought");
        }
    }

    public void SecondSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.fireRate *= 1.25f;
            upgradePanel.turretToUpgrade.slotLevel[1]++;
            upgradePanel.turretToUpgrade.SellCost = upgradePanel.turretToUpgrade.SellCost + (upgradeCost / 2);
            LevelManager.Instance.AdjustGold(-upgradeCost);
            LevelManager.Instance.sceneData.turretUpgradePanel.SetActive(false);
            Analytics.CustomEvent("Second Turret Upgrade Bought");
        }
    }

    public void ThirdSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.range *= 1.2f;
            upgradePanel.turretToUpgrade.slotLevel[2]++;
            upgradePanel.turretToUpgrade.SellCost = upgradePanel.turretToUpgrade.SellCost + (upgradeCost / 2);
            LevelManager.Instance.AdjustGold(-upgradeCost);
            LevelManager.Instance.sceneData.turretUpgradePanel.SetActive(false);
            Analytics.CustomEvent("Third Turret Upgrade Bought");
        }
    }
}
