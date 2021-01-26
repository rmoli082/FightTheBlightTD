using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashPower : MonoBehaviour
{
    public Hero theHero;

    public int endBonusGold;
    public int killBonusGold;

    private void Awake()
    {
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
        Debug.Log("Kill Bonus");
        LevelManager.Instance.AdjustGold(killBonusGold);
    }
}
