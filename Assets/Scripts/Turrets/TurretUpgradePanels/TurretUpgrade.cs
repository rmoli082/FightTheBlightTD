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
            upgradePanel.turretToUpgrade.DamageAmount *= 1.2f;
            PurchaseUpgrade(0);
            Analytics.CustomEvent("First Turret Upgrade Bought");
        }
    }

    public void SecondSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.fireRate *= 1.15f;
            PurchaseUpgrade(1);
            Analytics.CustomEvent("Second Turret Upgrade Bought");
        }
    }

    public void ThirdSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            upgradePanel.turretToUpgrade.range *= 1.15f;
            PurchaseUpgrade(2);
            Analytics.CustomEvent("Third Turret Upgrade Bought");
        }
    }

}
