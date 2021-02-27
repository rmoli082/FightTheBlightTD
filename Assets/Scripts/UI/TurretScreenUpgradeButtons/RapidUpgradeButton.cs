using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidUpgradeButton : TurretScreenUpgrade
{
    protected override void Awake()
    {
        base.Awake();
    }
    public override void BuyUpgrade()
    {
        if (TurretStats.Instance.GetTurretStats(PlaceableType.rapid.ToString()) >= blueprint.cost)
        {
            TurretStats.Instance.rapidPermanentBought[upgradeNumber] = true;
            TurretStats.Instance.AddTurretKills(PlaceableType.rapid.ToString(), -blueprint.cost);
            screenData.turretKills.text = TurretStats.Instance.GetTurretStats(PlaceableType.turret.ToString()).ToString();
            GameEvents.OnSaveInitiated();
        }

    }

}
