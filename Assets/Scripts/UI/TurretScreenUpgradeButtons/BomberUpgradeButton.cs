using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberUpgradeButton : TurretScreenUpgrade
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        if (!TurretStats.Instance.bomberPermanentBought[upgradeNumber] &&
            TurretStats.Instance.GetTurretStats(PlaceableType.bomber.ToString()) >= blueprint.cost)
        {
            this.button.interactable = true;
        }
    }

    public override void BuyUpgrade()
    {
        if (TurretStats.Instance.GetTurretStats(PlaceableType.bomber.ToString()) >= blueprint.cost)
        {
            TurretStats.Instance.bomberPermanentBought[upgradeNumber] = true;
            TurretStats.Instance.AddTurretKills(PlaceableType.bomber.ToString(), -blueprint.cost);
            screenData.turretKills.text = $"{TurretStats.Instance.GetTurretStats(PlaceableType.bomber.ToString())} points";
            GameEvents.OnSaveInitiated();
        }
    }
}
