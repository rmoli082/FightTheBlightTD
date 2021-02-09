using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePower : HeroUpgrade
{
    public Hero theHero;

    private static DamagePower _instance;
    public static DamagePower Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DamagePower>();

                if (_instance == null)
                {
                    GameObject container = new GameObject();
                    _instance = container.AddComponent<DamagePower>();
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as DamagePower;
        }
        else if (_instance != this)
        {
            Destroy(this);
            return;
        }
    
    theHero = gameObject.GetComponentInParent(typeof(Hero)) as Hero;
    }

    private void Start()
    {
        GameEvents.EnemySpawned += DamageEnemy;
    }

    private void DamageEnemy(Enemy e)
    {
        e.Damage(theHero.DamageAmount, theHero.TurretType.ToString());
        theHero.AdjustXP(1);
        return;
    }
}
