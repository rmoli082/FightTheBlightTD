using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesPower : HeroUpgrade
{
    public Hero theHero;

    public int livesBonus;


    private void Awake()
    {
        UpgradeName = "Lives Boost";
        isActivated = false;
        theHero = gameObject.GetComponentInParent(typeof(Hero)) as Hero;
    }

    private void Start()
    {
        GameEvents.WaveEnded += LivesBonus;
    }

    private void OnDisable()
    {
        GameEvents.WaveEnded -= LivesBonus;
    }

    private void LivesBonus()
    {
        LevelManager.Instance.AdjustLives(livesBonus);
        theHero.AdjustXP(livesBonus);
    }
}
