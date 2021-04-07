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
            PurchaseUpgrade(0);
            Analytics.CustomEvent("First Bomber Upgrade Bought");
        }
        
    }

    public void SecondSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.explodeRange *= 1.15f;
            PurchaseUpgrade(1);
            Analytics.CustomEvent("Second Bomber Upgrade Bought");
        }
        
    }

    public void ThirdSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.range *= 1.1f;
            PurchaseUpgrade(2);
            Analytics.CustomEvent("Third Bomber Upgrade Bought");
        }
    }
}
