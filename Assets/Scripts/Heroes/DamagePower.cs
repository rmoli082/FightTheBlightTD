using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePower : MonoBehaviour
{
    public Hero theHero;

    private void Awake()
    {
        theHero = gameObject.GetComponentInParent(typeof(Hero)) as Hero;
    }

    private void Start()
    {
        GameEvents.EnemySpawned += DamageEnemy;
    }

    private void DamageEnemy(Enemy e)
    {
        e.Damage(theHero.DamageAmount);
        return;
    }
}
