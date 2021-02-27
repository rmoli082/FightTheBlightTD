using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePower : HeroUpgrade
{
    public Hero theHero;

    private void Awake()
    {
        UpgradeName = "Damage Boost";
        isActivated = false;
        theHero = gameObject.GetComponentInParent(typeof(Hero)) as Hero;
    }

    private void Start()
    {
        GameEvents.EnemySpawned += DamageEnemy;
    }

    private void OnDisable()
    {
        GameEvents.EnemySpawned -= DamageEnemy;
    }

    private void DamageEnemy(Enemy e)
    {
        if (isActivated)
        {
            e.Damage(theHero.DamageAmount, theHero.TurretType.ToString());
            theHero.AdjustXP(1);
            return;
        }
        
    }
}
