using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

[Serializable]
public class Hero : Placeable
{
    [Header("Hero Info")]
    public float range = 12f;
    public GameObject mainPower;
    public int matchLevel = 1;
    public int levelXP = 0;
    public GameObject hitEffect;
    public GameObject levelEffect;
    GameObject textyCanvas;
    bool rotated = false;
    bool doUpgrade = false;
    ParticleSystem particles;
    [SerializeField]
    Transform partToRotate;

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
        textyCanvas = LevelManager.Instance.sceneData.powerActivatedPopup;
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
                PopupLevelupText("Power Activated!!");
                GameObject effect = Instantiate(levelEffect, transform.position, Quaternion.identity);
                Destroy(effect, 5);
            }
        }

        if (matchLevel >= 8 && HeroManager.Instance.isSecondUpgradeActive)
        {
            if (!slotSpawned[1])
            {
                HeroUpgrade k = Instantiate(heroUpgrade[1], this.transform);
                k.isActivated = true;
                slotSpawned[1] = true;
                PopupLevelupText("Power Activated!!");
                GameObject effect = Instantiate(levelEffect, transform.position, Quaternion.identity);
                Destroy(effect, 5);
            }
        }

        if (matchLevel >= 13 && HeroManager.Instance.isThirdUpgradeActive)
        {
            if (!slotSpawned[2])
            {
                HeroUpgrade k = Instantiate(heroUpgrade[2], this.transform);
                k.isActivated = true;
                slotSpawned[2] = true;
                PopupLevelupText("Power Activated!!");
                GameObject effect = Instantiate(levelEffect, transform.position, Quaternion.identity);
                Destroy(effect, 5);
            }
        }

        if (matchLevel % 4 != 0)
        {
            doUpgrade = true;
        }

        if (matchLevel % 4 == 0 && doUpgrade == true)
        {
            DamageAmount++;
            doUpgrade = false;
            PopupLevelupText("Damage Increased!!");
            GameObject effect = Instantiate(levelEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5);
        }
    }

    private IEnumerator LookAtStart()
    {
        Transform target = GameObject.FindGameObjectWithTag("Start").transform;
        Vector3 dir = target.position - partToRotate.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.LerpUnclamped(partToRotate.localRotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
        partToRotate.localRotation = Quaternion.Euler(0f, rotation.y, 0f);
        yield return new WaitForSeconds(1.5f);
        rotated = true;
    }

    private void PopupLevelupText(string message)
    {
        textyCanvas.GetComponentInChildren<TextMeshProUGUI>().text = message;
        textyCanvas.SetActive(true);
        StartCoroutine(FadeCanvas());
    }

    private IEnumerator FadeCanvas()
    {
        yield return new WaitForSeconds(1.5f);
        textyCanvas.SetActive(false);
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
