using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeButton : TurretScreenUpgrade
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        if (!TurretStats.Instance.turretPermanentBought[upgradeNumber] && 
            TurretStats.Instance.GetTurretStats(PlaceableType.turret.ToString()) >= blueprint.cost)
        {
            this.button.interactable = true;
        }
    }

    public override void BuyUpgrade()
    {
        if (TurretStats.Instance.GetTurretStats(PlaceableType.turret.ToString()) >= blueprint.cost)
        {
            TurretStats.Instance.turretPermanentBought[upgradeNumber] = true;
            TurretStats.Instance.AddTurretKills(PlaceableType.turret.ToString(), -blueprint.cost);
            screenData.turretKills.text = TurretStats.Instance.GetTurretStats(PlaceableType.turret.ToString()).ToString();
            this.button.interactable = false;
            GameEvents.OnSaveInitiated();
        }
    }

}
