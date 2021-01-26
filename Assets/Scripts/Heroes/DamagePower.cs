using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePower : Singleton<DamagePower>
{
    public Hero theHero;

    protected override void Awake()
    {
        base.Awake();
        theHero = gameObject.GetComponentInParent(typeof(Hero)) as Hero;
    }

    private void Start()
    {
        GameEvents.EnemySpawned += DamageEnemy;
    }

    private void DamageEnemy(Enemy e)
    {
        e.Damage(theHero.DamageAmount);
        theHero.AdjustXP(1);
        return;
    }
}
