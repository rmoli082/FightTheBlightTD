using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class RapidUpgrade : TurretUpgradePanels
{
    public void FirstSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.fireRate *= 1.2f;
            PurchaseUpgrade(0);
            Analytics.CustomEvent("First Rapid Upgrade Bought");
        }
    }

    public void SecondSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.DamageAmount *= 1.25f;
            PurchaseUpgrade(1);
            Analytics.CustomEvent("Second Rapid Upgrade Bought");
        }
    }

    public void ThirdSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.projectileForce *= 1.15f;
            PurchaseUpgrade(2);
            Analytics.CustomEvent("Third Rapid Upgrade Bought");
        }
    }
}
