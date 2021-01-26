using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesPower : MonoBehaviour
{
    public Hero theHero;

    public int livesBonus;

    private void Awake()
    {
        theHero = gameObject.GetComponentInParent(typeof(Hero)) as Hero;
    }

    private void Start()
    {
        GameEvents.WaveEnded += LivesBonus;
    }

    private void LivesBonus()
    {
        LevelManager.Instance.AdjustLives(livesBonus);
    }
}
