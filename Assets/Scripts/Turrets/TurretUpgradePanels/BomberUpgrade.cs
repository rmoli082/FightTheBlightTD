using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class BomberUpgrade : TurretUpgradePanels
{
    public void FirstSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.DamageAmount *= 1.1f;
            upgradePanel.turretToUpgrade.slotLevel[0]++;
            upgradePanel.turretToUpgrade.SellCost = upgradePanel.turretToUpgrade.SellCost + (upgradeCost / 2);
            LevelManager.Instance.AdjustGold(-upgradeCost);
            LevelManager.Instance.sceneData.turretUpgradePanel.SetActive(false);
            upgradePanel.turretToUpgrade.Glow(false);
            Analytics.CustomEvent("First Bomber Upgrade Bought");
        }
        
    }

    public void SecondSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.explodeRange *= 1.15f;
            upgradePanel.turretToUpgrade.slotLevel[1]++;
            upgradePanel.turretToUpgrade.SellCost = upgradePanel.turretToUpgrade.SellCost + (upgradeCost / 2);
            LevelManager.Instance.AdjustGold(-upgradeCost);
            LevelManager.Instance.sceneData.turretUpgradePanel.SetActive(false);
            upgradePanel.turretToUpgrade.Glow(false);
            Analytics.CustomEvent("Second Bomber Upgrade Bought");
        }
        
    }

    public void ThirdSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.range *= 1.1f;
            upgradePanel.turretToUpgrade.slotLevel[2]++;
            upgradePanel.turretToUpgrade.SellCost = upgradePanel.turretToUpgrade.SellCost + (upgradeCost / 2);
            LevelManager.Instance.AdjustGold(-upgradeCost);
            LevelManager.Instance.sceneData.turretUpgradePanel.SetActive(false);
            upgradePanel.turretToUpgrade.Glow(false);
            Analytics.CustomEvent("Third Bomber Upgrade Bought");
        }
    }
}
