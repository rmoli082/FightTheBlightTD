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
            upgradePanel.turretToUpgrade.projectileForce *= 1.3f;
            PurchaseUpgrade(0);
            Analytics.CustomEvent("First Seeker Upgrade Bought");
        }
    }

    public void SecondSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.fireRate *= 1.4f;
            PurchaseUpgrade(1);
            Analytics.CustomEvent("Second Seeker Upgrade Bought");
        }
    }

    public void ThirdSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.DamageAmount *= 1.4f;
            PurchaseUpgrade(2);
            Analytics.CustomEvent("Third Seeker Upgrade Bought");
        }
    }
}
