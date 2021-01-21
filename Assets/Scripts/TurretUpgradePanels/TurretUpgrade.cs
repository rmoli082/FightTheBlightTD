using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{

    public Turret selectedTurret;

   public void FirstSlot()
    {
        selectedTurret.DamageAmount *= 1.4f;
    }

    public void SecondSlot()
    {
        selectedTurret.fireRate *= 1.5f;
    }

    public void ThirdSlot()
    {
        selectedTurret.range *= 1.5f;
    }
}
