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
            NewWaveSpawner.Instance.Modifier = 1;


           LevelManager.Instance.sceneData.speedButton.SetFastImage();

            if (NewWaveSpawner.Instance.CurrentWave == NewWaveSpawner.Instance.TotalWaves)
            {
                NewWaveSpawner.Instance.ResetWave(NewWaveSpawner.Instance.TotalWaves - 1);
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
        NewWaveSpawner.Instance.Modifier = 1;
        if (NewWaveSpawner.Instance.CurrentWave == NewWaveSpawner.Instance.TotalWaves)
        {
            StopCoroutine(NewWaveSpawner.Instance.SpawnWave());
            NewWaveSpawner.Instance.ResetWave(NewWaveSpawner.Instance.TotalWaves - 1);
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
