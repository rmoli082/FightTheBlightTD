using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnerUpgradeButton : TurretScreenUpgrade
{
    protected override void Awake()
    {
        base.Awake();
    }
    public override void BuyUpgrade()
    {
        if (TurretStats.Instance.GetTurretStats(PlaceableType.stunner.ToString()) >= blueprint.cost)
        {
            TurretStats.Instance.stunnerPermanentBought[upgradeNumber] = true;
            TurretStats.Instance.AddTurretKills(PlaceableType.stunner.ToString(), -blueprint.cost);
            screenData.turretKills.text = TurretStats.Instance.GetTurretStats(PlaceableType.turret.ToString()).ToString();
            GameEvents.OnSaveInitiated();
        }
    }
}
