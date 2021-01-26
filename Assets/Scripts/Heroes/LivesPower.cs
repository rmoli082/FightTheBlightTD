using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesPower : Singleton<LivesPower>
{
    public Hero theHero;

    public int livesBonus;

    protected override void Awake()
    {
        base.Awake();
        theHero = gameObject.GetComponentInParent(typeof(Hero)) as Hero;
    }

    private void Start()
    {
        GameEvents.WaveEnded += LivesBonus;
    }

    private void LivesBonus()
    {
        LevelManager.Instance.AdjustLives(livesBonus);
        theHero.AdjustXP(livesBonus);
    }
}
