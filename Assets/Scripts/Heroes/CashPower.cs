using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashPower : HeroUpgrade
{
    public Hero theHero;

    public int endBonusGold;
    public int killBonusGold;

    private void Awake()
    {
        UpgradeName = "Gold Boost";
        isActivated = false;
        theHero = gameObject.GetComponentInParent(typeof(Hero)) as Hero;
    }

    private void Start()
    {
        GameEvents.WaveEnded += EndBonus;
        GameEvents.EnemyKilled += KillBonus;
    }

    private void OnDisable()
    {
        GameEvents.WaveEnded -= EndBonus;
        GameEvents.EnemyKilled -= KillBonus;
    }

    private void EndBonus()
    {
        if (isActivated)
            LevelManager.Instance.AdjustGold(endBonusGold);
    }

    private void KillBonus()
    {
        if (isActivated)
        {
            Debug.Log("payout");
            Debug.Log($"{isActivated}");
            LevelManager.Instance.AdjustGold(killBonusGold);
            theHero.AdjustXP(1);
        }
        
    }
}
