using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDamageBoostPower : HeroUpgrade
{
    public Hero theHero;

    public float turretBonus;

    private static TurretDamageBoostPower _instance;
    public static TurretDamageBoostPower Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TurretDamageBoostPower>();

                if (_instance == null)
                {
                    GameObject container = new GameObject();
                    _instance = container.AddComponent<TurretDamageBoostPower>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as TurretDamageBoostPower;
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
        GameEvents.WaveStarted += BoostTurrets;
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
