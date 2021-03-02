using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class SeekerUpgrade : TurretUpgradePanels
{
    public void FirstSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.projectileForce *= 1.1f;
            upgradePanel.turretToUpgrade.slotLevel[0]++;
            upgradePanel.turretToUpgrade.SellCost = upgradePanel.turretToUpgrade.SellCost + (upgradeCost / 2);
            LevelManager.Instance.AdjustGold(-upgradeCost);
            LevelManager.Instance.sceneData.turretUpgradePanel.SetActive(false);
            Analytics.CustomEvent("First Seeker Upgrade Bought");
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
            Analytics.CustomEvent("Second Seeker Upgrade Bought");
        }
    }

    public void ThirdSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.DamageAmount *= 1.5f;
            upgradePanel.turretToUpgrade.slotLevel[2]++;
            upgradePanel.turretToUpgrade.SellCost = upgradePanel.turretToUpgrade.SellCost + (upgradeCost / 2);
            LevelManager.Instance.AdjustGold(-upgradeCost);
            LevelManager.Instance.sceneData.turretUpgradePanel.SetActive(false);
            Analytics.CustomEvent("Third Seeker Upgrade Bought");
        }
    }
}
