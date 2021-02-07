using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HeroManager : Singleton<HeroManager>
{
    public int heroLevel = 1;
    public int heroXP = 0;

    public bool isFirstUpgradeActive = false;
    public bool isSecondUpgradeActive = false;
    public bool isThirdUpgradeActive = false;

    private readonly int levels = 50;
    private readonly int xp_for_first_level = 100;
    private readonly int xp_for_last_level = 100000;
    private readonly string saveTag = "Hero";

    protected override void Awake()
    {
        base.Awake();

        if (SaveLoad.SaveExists(saveTag))
        {
            LoadHero();
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameEvents.SaveInitiated += Save;
    }

    public int GetHeroXP()
    {
        return heroXP;
    }

    public void AddHeroXP(int xpAmount)
    {
        heroXP += xpAmount;
        CalculateLevel();
        UIXpBar.Instance.SetXpValue((float)heroXP / (float)(CalculateXP(heroLevel + 1)));
        Debug.Log($"{heroLevel} : {heroXP}");
        Debug.Log($"{heroXP / (float)(CalculateXP(heroLevel + 1))}");
    }

    public int CalculateLevel()
    {
        if (heroXP >= CalculateXP(heroLevel + 1))
        {
            heroLevel = Mathf.Clamp(heroLevel + 1, 1, 50);
        }

        for (int i = 1; i <= levels; i++ )
        {
            Debug.Log(CalculateXP(i));
        }

        return heroLevel;
    }

    public void ActivateUpgradeSlots()
    {
        if (heroLevel >= 10)
        {
            isFirstUpgradeActive = true;
        }

        if (heroLevel >= 20)
        {
            isSecondUpgradeActive = true;
        }

        if (heroLevel >= 30)
        {
            isThirdUpgradeActive = true;
        }
    }

    private int CalculateXP(int level)
    {
        float B = Mathf.Log(1.0f * xp_for_last_level / xp_for_first_level) / (levels - 1);
        float A = 1.0f * xp_for_first_level / (Mathf.Exp(B) - 1.0f);

        int x = (int)(A * Mathf.Exp(B * level));
        int y = (int)Mathf.Pow(10f, Mathf.Log(x) / Mathf.Log(10) - 2.2f);

        return (int)(x / y) * y;
    }

    private void Save()
    {
        SaveLoad.Save<HeroSave>(new HeroSave(heroLevel, heroXP, isFirstUpgradeActive, isSecondUpgradeActive, isThirdUpgradeActive), saveTag);
    }

    private void LoadHero()
    {
        HeroSave hero = SaveLoad.Load<HeroSave>(saveTag);

        heroLevel = hero.heroLevel;
        heroXP = hero.heroXP;
        isFirstUpgradeActive = hero.firstActive;
        isSecondUpgradeActive = hero.secondActive;
        isThirdUpgradeActive = hero.thirdActive;
    }

    [Serializable]
    protected class HeroSave
    {
        public int heroLevel;
        public int heroXP;
        public bool firstActive;
        public bool secondActive;
        public bool thirdActive;

        public HeroSave() { }
        public HeroSave(int _heroLevel, int _heroXP, bool _firstActive, bool _secondActive, bool _thirdActive)
        {
            heroLevel = _heroLevel;
            heroXP = _heroXP;
            firstActive = _firstActive;
            secondActive = _secondActive;
            thirdActive = _thirdActive;
        }
    }
}
