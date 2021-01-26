using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Placeable
{
    [Header("Hero Info")]
    public float range = 12f;
    public GameObject mainPower;
    public int matchLevel = 1;
    public int levelXP = 0;

    [Header("Upgrade Slots")]
    public Upgrade[] heroUpgrades = new Upgrade[3];
    public bool slotOneEnabled = false;
    public bool slotTwoEnabled = false;
    public bool slotThreeEnabled = false;
    private bool slotOneSpawned = false;
    private bool slotTwoSpawned = false;
    private bool slotThreeSpawned = false;

    [Header("Upgrade Panels")]
    public GameObject upgradePanel; 

    private void Awake()
    {
        Instantiate(mainPower, this.transform);
    }

    private void Update()
    {
        if (matchLevel >= 3 && HeroManager.Instance.isFirstUpgradeActive)
        {
            slotOneEnabled = true;
            if (!slotOneSpawned)
            {
                Instantiate(heroUpgrades[0], this.transform);
                slotOneSpawned = true;
            }
        }

        if (matchLevel >= 6 && HeroManager.Instance.isSecondUpgradeActive)
        {
            slotTwoEnabled = true;
            if (!slotTwoSpawned)
            {
                Instantiate(heroUpgrades[1], this.transform);
                slotTwoSpawned = true;
            }
        }

        if (matchLevel >= 9 && HeroManager.Instance.isThirdUpgradeActive)
        {
            slotThreeEnabled = true;
            if (!slotThreeSpawned)
            {
                Instantiate(heroUpgrades[2], this.transform);
                slotThreeSpawned = true;
            }    
        }
    }

    public void AdjustXP(int amount)
    {
        levelXP += amount;
        matchLevel = Mathf.Clamp(CalculateLevel(), 1, 20);
    }

    private int CalculateLevel()
    {
        return (int)(50 + (Mathf.Log(levelXP * 100) / 50));
    }

    
}
