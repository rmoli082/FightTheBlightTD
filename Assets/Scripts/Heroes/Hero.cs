using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Placeable
{
    [Header("Hero Info")]
    public float range = 12f;

    [Header("Upgrade Slots")]
    public Upgrade[] heroUpgrades = new Upgrade[3];
    public int slotOneLevel = 0;
    public int slotTwoLevel = 0;
    public int slotThreeLevel = 0;

    [Header("Active Upgrades")]
    public GameObject[] heroPowerups = new GameObject[3];

    [Header("Upgrade Panels")]
    public GameObject upgradePanel;

    private void Awake()
    {
        foreach (GameObject power in heroPowerups)
        {
            if (power != null)
                Instantiate(power, this.transform);
        }
    }
}
