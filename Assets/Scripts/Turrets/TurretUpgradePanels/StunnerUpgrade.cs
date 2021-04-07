using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class StunnerUpgrade : TurretUpgradePanels
{
    public void FirstSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.stunPower *= 1.15f;
            PurchaseUpgrade(0);
            Analytics.CustomEvent("First Stunner Upgrade Bought");
        }
    }

    public void SecondSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.stunTime *= 1.1f;
            PurchaseUpgrade(1);
            Analytics.CustomEvent("Second Stunner Upgrade Bought");
        }
    }

    public void ThirdSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.range *= 1.15f;
            PurchaseUpgrade(2);
            Analytics.CustomEvent("Third Stunner Upgrade Bought");
        }
    }
}
