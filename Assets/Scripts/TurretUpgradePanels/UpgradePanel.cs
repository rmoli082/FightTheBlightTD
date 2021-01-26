using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradePanel : MonoBehaviour
{
    public Turret turretToUpgrade;

    public Transform contentListing;

    public void SetTurret(Turret _turret)
    {
        turretToUpgrade = _turret;
    }
}
