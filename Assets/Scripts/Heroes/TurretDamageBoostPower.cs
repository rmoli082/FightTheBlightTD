using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDamageBoostPower : HeroUpgrade
{
    public Hero theHero;

    public float turretBonus;

    private void Awake()
    {
        UpgradeName = "Turret Boost";
        isActivated = false;
        theHero = gameObject.GetComponentInParent(typeof(Hero)) as Hero;
    }

    private void Start()
    {
        GameEvents.WaveStarted += BoostTurrets;
    }

    private void OnDisable()
    {
        GameEvents.WaveStarted -= BoostTurrets;
    }
        

    private void BoostTurrets()
    {
        int xpBoost = 0;

        Collider[] colliders = Physics.OverlapSphere(theHero.transform.position, theHero.range);
        foreach (Collider c in colliders)
        {
            Turret turret = c.GetComponent(typeof(Turret)) as Turret;
            if (turret != null)
            {
                turret.DamageAmount *= turretBonus;
                xpBoost++;
            }
                
        }

        theHero.AdjustXP(xpBoost);
    }
}
