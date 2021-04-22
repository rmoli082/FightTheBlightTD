using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerUpgradeButton : TurretScreenUpgrade
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        if (!TurretStats.Instance.seekerPermanentBought[upgradeNumber] &&
            TurretStats.Instance.GetTurretStats(PlaceableType.seeker.ToString()) >= blueprint.cost)
        {
            this.button.interactable = true;
        }
    }

    public override void BuyUpgrade()
    {
        if (TurretStats.Instance.GetTurretStats(PlaceableType.seeker.ToString()) >= blueprint.cost)
        {
            TurretStats.Instance.seekerPermanentBought[upgradeNumber] = true;
            TurretStats.Instance.AddTurretKills(PlaceableType.seeker.ToString(), -blueprint.cost);
            screenData.turretKills.text = $"{TurretStats.Instance.GetTurretStats(PlaceableType.seeker.ToString())} points";
            GameEvents.OnSaveInitiated();
        }
    }
}
