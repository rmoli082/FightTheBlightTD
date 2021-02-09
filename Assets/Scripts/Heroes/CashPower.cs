using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashPower : HeroUpgrade
{
    public Hero theHero;

    public int endBonusGold;
    public int killBonusGold;

    private static CashPower _instance;
    public static CashPower Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CashPower>();

                if (_instance == null)
                {
                    GameObject container = new GameObject();
                    _instance = container.AddComponent<CashPower>();
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as CashPower;
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
        GameEvents.WaveEnded += EndBonus;
        GameEvents.EnemyKilled += KillBonus;
    }

    private void EndBonus()
    {
        LevelManager.Instance.AdjustGold(endBonusGold);
    }

    private void KillBonus()
    {
        LevelManager.Instance.AdjustGold(killBonusGold);
        theHero.AdjustXP(1);
    }
}
