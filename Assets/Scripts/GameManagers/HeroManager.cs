using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HeroManager : Singleton<HeroManager>
{
    public Hero theHero;
    private int heroLevel = 0;
    [SerializeField]
    private int heroXP = 0;

    public bool isFirstUpgradeActive = false;
    public bool isSecondUpgradeActive = false;
    public bool isThirdUpgradeActive = false;

    public bool isSpawned = false;

    public HeroUpgrade[] availableBoosts;

    public HeroUpgrade[] activeUpgrades = new HeroUpgrade[3];

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
        GameEvents.GameOverWin += WinAwards;
        GameEvents.GameOverLose += LoseAwards;
    }

    public int GetHeroXP()
    {
        return heroXP;
    }

    public int GetHeroLevel()
    {
        return heroLevel;
    }

    public Hero GetHero()
    {
        return theHero;
    }

    public void SetHero(Hero hero)
    {
        theHero = hero;
    }

    public void AddHeroXP(int xpAmount)
    {
        heroXP += xpAmount;
        CheckForLevelUp();
        GameEvents.OnSaveInitiated();
    }

    public void ActivateUpgradeSlots()
    {
        CheckForLevelUp();
        if (heroLevel >= 5)
        {
            isFirstUpgradeActive = true;
        }

        if (heroLevel >= 10)
        {
            isSecondUpgradeActive = true;
        }

        if (heroLevel >= 20)
        {
            isThirdUpgradeActive = true;
        }
    }

    private int XpForLevel(int level)
    {
        return 25 * level * (1 + level);
    }

    public void CheckForLevelUp()
    {
        if (heroXP >= XpForLevel(heroLevel + 1))
        {
            heroLevel++;
        }
    }

    private void Save()
    {
        string[] upgrades = new string[3];
        for (int i = 0; i < 3; i++)
        {
            if (activeUpgrades[i] != null)
                upgrades[i] = activeUpgrades[i].UpgradeName;
        }
        SaveLoad.Save<HeroSave>(new HeroSave(heroLevel, heroXP, isFirstUpgradeActive, isSecondUpgradeActive, isThirdUpgradeActive, upgrades), saveTag);
    }

    private void LoadHero()
    {
        HeroSave hero = SaveLoad.Load<HeroSave>(saveTag);

        heroLevel = hero.heroLevel;
        heroXP = hero.heroXP;
        isFirstUpgradeActive = hero.firstActive;
        isSecondUpgradeActive = hero.secondActive;
        isThirdUpgradeActive = hero.thirdActive;

        for (int i = 0; i <3; i++)
        {
            if (hero.upgradeList[i] != null)
            {
                foreach (HeroUpgrade upgrade in availableBoosts)
                {
                    if (hero.upgradeList[i].Equals(upgrade.UpgradeName))
                    {
                        activeUpgrades[i] = upgrade;
                    }
                }
            }
        }
    }

    private void WinAwards()
    {
        if (isSpawned)
            AddHeroXP(10);
    }

    private void LoseAwards()
    {
        if (isSpawned)
            AddHeroXP(5);
    }

    [Serializable]
    protected class HeroSave
    {
        public int heroLevel;
        public int heroXP;
        public bool firstActive;
        public bool secondActive;
        public bool thirdActive;
        public string[] upgradeList;

        public HeroSave() { }
        public HeroSave(int _heroLevel, int _heroXP, bool _firstActive, bool _secondActive, bool _thirdActive, string[] _upgrades)
        {
            heroLevel = _heroLevel;
            heroXP = _heroXP;
            firstActive = _firstActive;
            secondActive = _secondActive;
            thirdActive = _thirdActive;
            upgradeList = _upgrades;
        }
    }
}
