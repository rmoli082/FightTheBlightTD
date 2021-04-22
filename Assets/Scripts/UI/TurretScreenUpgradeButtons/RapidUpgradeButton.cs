using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidUpgradeButton : TurretScreenUpgrade
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Update()
    {
        if (!TurretStats.Instance.rapidPermanentBought[upgradeNumber] &&
            TurretStats.Instance.GetTurretStats(PlaceableType.rapid.ToString()) >= blueprint.cost)
        {
            this.button.interactable = true;
        }
    }

    public override void BuyUpgrade()
    {
        if (TurretStats.Instance.GetTurretStats(PlaceableType.rapid.ToString()) >= blueprint.cost)
        {
            TurretStats.Instance.rapidPermanentBought[upgradeNumber] = true;
            TurretStats.Instance.AddTurretKills(PlaceableType.rapid.ToString(), -blueprint.cost);
            screenData.turretKills.text = $"{TurretStats.Instance.GetTurretStats(PlaceableType.rapid.ToString())} points";
            GameEvents.OnSaveInitiated();
        }

    }

}
