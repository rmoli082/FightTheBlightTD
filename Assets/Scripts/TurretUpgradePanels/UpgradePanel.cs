using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    public Turret turretToUpgrade;

    public void SetTurret(Turret _turret)
    {
        turretToUpgrade = _turret;
    }
}
