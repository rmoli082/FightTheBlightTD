using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    public int replayCost;
    private int currentReplayCost;
    Scene current;


    private void Awake()
    {
        current = SceneManager.GetActiveScene();
    }

    private void OnEnable()
    {
        GameManager.Instance.UpdateGemsDisplay();
        currentReplayCost = replayCost + (LevelManager.Instance.playerStats.continues * replayCost);
        LevelManager.Instance.sceneData.loseReplayText.text = $"-{currentReplayCost}";
    }

    public void Continue()
    {
        if (Player.Instance.GetGems() >= currentReplayCost)
        {
            Player.Instance.AdjustGems(-currentReplayCost);
            LevelManager.Instance.AdjustLives(100);
            LevelManager.Instance.AdjustGold(650);
            LevelManager.Instance.sceneData.losePanel.SetActive(false);
            LevelManager.Instance.sceneData.gameOverPanel.SetActive(false);
            LevelManager.Instance.playerStats.continues++;
            WaveSpawner.Instance.healthModifier = 1;
            WaveSpawner.Instance.speedModifier = 1;


           LevelManager.Instance.sceneData.speedButton.SetFastImage();

            if (WaveSpawner.Instance.waveNumber == WaveSpawner.Instance.GetNumberOfWaves)
            {
                WaveSpawner.Instance.ResetWave(WaveSpawner.Instance.GetNumberOfWaves - 1);
            }
        }
        else
        {
            LevelManager.Instance.sceneData.loseButton.interactable = false;
        }
        
    }

    public void ContinueAdWatch()
    {
        Time.timeScale = 0f;
        LevelManager.Instance.AdjustLives(100);
        LevelManager.Instance.AdjustGold(650);
        LevelManager.Instance.sceneData.losePanel.SetActive(false);
        LevelManager.Instance.sceneData.gameOverPanel.SetActive(false);
        LevelManager.Instance.playerStats.continues++;
        WaveSpawner.Instance.healthModifier = 1;
        WaveSpawner.Instance.speedModifier = 1;
        if (WaveSpawner.Instance.waveNumber == WaveSpawner.Instance.GetNumberOfWaves)
        {
            StopCoroutine(WaveSpawner.Instance.SpawnWave());
            WaveSpawner.Instance.ResetWave(WaveSpawner.Instance.GetNumberOfWaves - 1);
            LevelManager.Instance.bossIsDead = false;
        }

        LevelManager.Instance.sceneData.speedButton.SetFastImage();
    }

    public void ReplayStartOver()
    {
        LevelManager.Instance.sceneData.losePanel.SetActive(false);
        LevelManager.Instance.sceneData.gameOverPanel.SetActive(false);
        GameManager.Instance.LoadScene(current.name);
        GameManager.Instance.PausePlay();
        GameEvents.OnGameOverLose();
    }

    public void Exit()
    {
        LevelManager.Instance.sceneData.losePanel.SetActive(false);
        LevelManager.Instance.sceneData.gameOverPanel.SetActive(false);
        GameManager.Instance.LoadScene("MainMenu");
        GameManager.Instance.PausePlay();
        GameEvents.OnGameOverLose();
    }
}
