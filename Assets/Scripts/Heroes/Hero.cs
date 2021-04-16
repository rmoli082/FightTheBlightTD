using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Hero : Placeable
{
    [Header("Hero Info")]
    public float range = 12f;
    public GameObject mainPower;
    public int matchLevel = 1;
    public int levelXP = 0;
    public GameObject hitEffect;
    bool rotated = false;
    ParticleSystem particles;

    [Header("Upgrade Slots")]
    public HeroUpgrade[] heroUpgrade = new HeroUpgrade[3];

    private bool[] slotSpawned = { false, false, false };

    [Header("Upgrade Panels")]
    public GameObject upgradePanel; 

    private void Awake()
    {
        GameObject mainPowerObj = Instantiate(mainPower, this.transform);
        mainPowerObj.GetComponent<HeroUpgrade>().isActivated = true;
        HeroManager.Instance.isSpawned = true;
        particles = gameObject.GetComponent<ParticleSystem>();
        particles.Pause();
    }

    private void Start()
    {
        GameEvents.WaveStarted += WaveStarted;
        GameEvents.WaveEnded += WaveEnded;
    }

    private void Update()
    {
        if (!rotated)
        {
            StartCoroutine(LookAtStart());
        }
    }

    private void OnDisable()
    {
        HeroManager.Instance.isSpawned = false;
        GameEvents.WaveStarted -= WaveStarted;
        GameEvents.WaveEnded -= WaveEnded;
    }

    public void AdjustXP(int amount)
    {
        levelXP += amount;
        matchLevel = Mathf.Clamp(CalculateLevel(), 1, 20);
        CalculateUpgrades();
    }

    private int CalculateLevel()
    {
        return Mathf.FloorToInt((30 + (Mathf.Sqrt(325 + 100 * levelXP))) / 60);
    }

    private void CalculateUpgrades()
    {
        for (int i = 0; i < 3; i++)
        {
            if (HeroManager.Instance.activeUpgrades[i] != null)
            {
                heroUpgrade[i] = HeroManager.Instance.activeUpgrades[i];
                heroUpgrade[i].isActivated = false;
            }
        }

        if (matchLevel >= 3 && HeroManager.Instance.isFirstUpgradeActive)
        {
            if (!slotSpawned[0])
            {
                HeroUpgrade k = Instantiate(heroUpgrade[0], this.transform);
                k.isActivated = true;
                slotSpawned[0] = true;
            }
        }

        if (matchLevel >= 8 && HeroManager.Instance.isSecondUpgradeActive)
        {
            if (!slotSpawned[1])
            {
                HeroUpgrade k = Instantiate(heroUpgrade[1], this.transform);
                k.isActivated = true;
                slotSpawned[1] = true;
            }
        }

        if (matchLevel >= 13 && HeroManager.Instance.isThirdUpgradeActive)
        {
            if (!slotSpawned[2])
            {
                HeroUpgrade k = Instantiate(heroUpgrade[2], this.transform);
                k.isActivated = true;
                slotSpawned[2] = true;
            }
        }

        DamageAmount += (matchLevel / 4);
    }

    private IEnumerator LookAtStart()
    {
        Transform target = GameObject.FindGameObjectWithTag("Start").transform;
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.LerpUnclamped(transform.localRotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
        transform.localRotation = Quaternion.Euler(0f, rotation.y, 0f);
        yield return new WaitForSeconds(1.5f);
        rotated = true;
    }

    private void WaveStarted()
    {
        particles.Play();
    }

    private void WaveEnded()
    {
        particles.Stop();
    }
    
}
