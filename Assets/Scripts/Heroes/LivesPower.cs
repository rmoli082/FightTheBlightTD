using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesPower : HeroUpgrade
{
    public Hero theHero;

    public int livesBonus;

    private static LivesPower _instance;
    public static LivesPower Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LivesPower>();

                if (_instance == null)
                {
                    GameObject container = new GameObject();
                    _instance = container.AddComponent<LivesPower>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as LivesPower;
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
        GameEvents.WaveEnded += LivesBonus;
    }

    private void LivesBonus()
    {
        LevelManager.Instance.AdjustLives(livesBonus);
        theHero.AdjustXP(livesBonus);
    }
}
