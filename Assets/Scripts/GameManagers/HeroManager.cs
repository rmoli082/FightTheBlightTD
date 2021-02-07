using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HeroManager : Singleton<HeroManager>
{
    private int heroLevel = 0;
    private int heroXP = 0;
    private int xpCollected = 0;

    public bool isFirstUpgradeActive = false;
    public bool isSecondUpgradeActive = false;
    public bool isThirdUpgradeActive = false;

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

    public int GetHeroLevel()
    {
        return heroLevel;
    }

    public void AddHeroXP(int xpAmount)
    {
        heroXP += xpAmount;
        xpCollected += xpAmount;
        CheckForLevelUp();
        UIXpBar.Instance.SetXpValue((float)xpCollected / ((float)XpForLevel(heroLevel + 1) - XpForLevel(heroLevel)));
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

    private int XpForLevel(int level)
    {
        return 25 * level * (1 + level);
    }

    private void CheckForLevelUp()
    {
        if (heroXP >= XpForLevel(heroLevel + 1))
        {
            heroLevel++;
            xpCollected -= XpForLevel(heroLevel);
        }
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
