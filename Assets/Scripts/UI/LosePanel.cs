using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
        currentReplayCost = replayCost + (LevelManager.Instance.playerStats.continues * replayCost);
        LevelManager.Instance.sceneData.loseReplayText.text = $"Continue wave?{System.Environment.NewLine}-§{currentReplayCost}";
    }

    public void Continue()
    {
        if (Player.Instance.GetGems() >= currentReplayCost)
        {
            Player.Instance.AdjustGems(-currentReplayCost);
            LevelManager.Instance.AdjustLives(100);
            LevelManager.Instance.AdjustGold(650);
            LevelManager.Instance.sceneData.losePanel.SetActive(false);
            LevelManager.Instance.playerStats.continues++;
            WaveSpawner.Instance.healthModifier = 1;
            WaveSpawner.Instance.speedModifier = 1;


            LevelManager.Instance.sceneData.speedButton.GetComponentInChildren<TextMeshProUGUI>().text = "Speed Up";

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
        LevelManager.Instance.AdjustLives(100);
        LevelManager.Instance.AdjustGold(650);
        LevelManager.Instance.sceneData.losePanel.SetActive(false);
        LevelManager.Instance.playerStats.continues++;
        WaveSpawner.Instance.healthModifier = 1;
        WaveSpawner.Instance.speedModifier = 1;
        if (WaveSpawner.Instance.waveNumber == WaveSpawner.Instance.GetNumberOfWaves)
        {
            StopCoroutine(WaveSpawner.Instance.SpawnWave());
            WaveSpawner.Instance.ResetWave(WaveSpawner.Instance.GetNumberOfWaves - 1);
        }

        LevelManager.Instance.sceneData.speedButton.GetComponentInChildren<TextMeshProUGUI>().text = "Speed Up";
    }

    public void ReplayStartOver()
    {
        LevelManager.Instance.sceneData.losePanel.SetActive(false);
        GameManager.Instance.LoadScene(current.name);
        GameManager.Instance.PausePlay();
        GameEvents.OnGameOverLose();
    }

    public void Exit()
    {
        LevelManager.Instance.sceneData.losePanel.SetActive(false);
        GameManager.Instance.LoadScene("MainMenu");
        GameManager.Instance.PausePlay();
        GameEvents.OnGameOverLose();
    }
}
