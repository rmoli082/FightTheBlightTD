using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerUpgradeButton : TurretScreenUpgrade
{
    protected override void Awake()
    {
        base.Awake();
    }
    public override void BuyUpgrade()
    {
        if (TurretStats.Instance.GetTurretStats(PlaceableType.seeker.ToString()) >= blueprint.cost)
        {
            TurretStats.Instance.seekerPermanentBought[upgradeNumber] = true;
            TurretStats.Instance.AddTurretKills(PlaceableType.seeker.ToString(), -blueprint.cost);
            screenData.turretKills.text = TurretStats.Instance.GetTurretStats(PlaceableType.turret.ToString()).ToString();
            GameEvents.OnSaveInitiated();
        }
    }
}
