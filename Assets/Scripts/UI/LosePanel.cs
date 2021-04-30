using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    public int replayCost;
    public GameObject buyGemsPanel;
    public string storeScene;
    private int currentReplayCost;
    Scene current;


    private void Awake()
    {
        current = SceneManager.GetActiveScene();
    }

    private void OnEnable()
    {
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
                NewWaveSpawner.Instance.ResetWave(NewWaveSpawner.Instance.TotalWaves);
            }
        }
        else
        {
            buyGemsPanel.SetActive(true);
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
            NewWaveSpawner.Instance.ResetWave(NewWaveSpawner.Instance.TotalWaves);
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
        GameEvents.OnNewGame();
    }

    public void Exit()
    {
        LevelManager.Instance.sceneData.losePanel.SetActive(false);
        LevelManager.Instance.sceneData.gameOverPanel.SetActive(false);
        GameManager.Instance.LoadScene("MainMenu");
        GameManager.Instance.PausePlay();
        GameEvents.OnGameOverLose();
        GameEvents.OnNewGame();
    }

    public void YesButton()
    {
        buyGemsPanel.SetActive(false);
        SceneManager.LoadScene(storeScene, LoadSceneMode.Additive);
    }

    public void NoButton()
    {
        buyGemsPanel.SetActive(false);
    }
}
