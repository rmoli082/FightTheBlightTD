using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashPower : Singleton<CashPower>
{
    public Hero theHero;

    public int endBonusGold;
    public int killBonusGold;

    protected override void Awake()
    {
        base.Awake();
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
