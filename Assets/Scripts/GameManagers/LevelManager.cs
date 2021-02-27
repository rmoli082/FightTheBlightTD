using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public LevelData levelData;
    public SceneData sceneData;
    public PlayerStats playerStats;

    public int totalWaves;

    protected override void Awake()
    {
        base.Awake();
        Decorate();
        SetUpShop();
        UpdateStats();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Start()
    {
        GameEvents.WaveEnded += CheckForWin;
    }

    public int GetLives()
    {
        return playerStats.playerLives;
    }

    public void AdjustLives(int lives)
    {
        playerStats.playerLives = Mathf.Clamp(playerStats.playerLives + lives, 0, 100);
        UpdateLives();
        if (playerStats.playerLives == 0)
        {
            GameManager.Instance.Lose();
        }
    }

    public int GetGold()
    {
        return playerStats.playerGold;
    }

    public void AdjustGold(int goldAmount)
    {
        playerStats.playerGold += goldAmount;
        UpdateGold();
        return;
    }

    private void SetUpShop()
    {
        foreach (GameObject button in levelData.shopButtons)
        {
            Instantiate(button, sceneData.shopListing);
        }
    }

    private void Decorate()
    {
        sceneData.plane.GetComponent<Renderer>().material = levelData.planeMaterial;
        foreach (Transform child in sceneData.nodes.transform)
        {
            child.GetComponent<Renderer>().material = levelData.nodeMaterial;
        }
        sceneData.backgroundMusic.clip = levelData.backgroundMusic;
        sceneData.backgroundMusic.Play();
    }

    private void UpdateStats()
    {
        UpdateLives();
        UpdateGold();
    }

    public void UpdateLives()
    {
        sceneData.playerLives.text = playerStats.playerLives.ToString() + " LIVES";
    }

    private void UpdateGold()
    {
        sceneData.playerGold.text = playerStats.playerGold.ToString() + " GOLD";
    }

    private void CheckForWin()
    {
        if (WaveSpawner.Instance.waveNumber >= totalWaves)
        {
            GameManager.Instance.Win();
        }
    }
}
