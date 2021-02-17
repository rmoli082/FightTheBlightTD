using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    public int replayCost;
    private int currentReplayCost;
    Scene current;


    private void Awake()
    {
        current = SceneManager.GetActiveScene();
        currentReplayCost = replayCost + (LevelManager.Instance.playerStats.continues * replayCost);
        LevelManager.Instance.sceneData.loseReplayText.text = $"Continue?{System.Environment.NewLine}-§{currentReplayCost}";
    }

    public void Replay()
    {
        if (Player.Instance.GetGems() >= currentReplayCost)
        {
            Player.Instance.AdjustGems(-currentReplayCost);
            GameManager.Instance.PausePlay();
            LevelManager.Instance.sceneData.losePanel.SetActive(false);
        }
        else
        {
            LevelManager.Instance.sceneData.loseButton.interactable = false;
        }
        
    }

    public void ReplayStartOver()
    {
        GameManager.Instance.LoadScene(current.name);
        GameManager.Instance.PausePlay();
    }

    public void Exit()
    {
        GameManager.Instance.LoadScene("MainMenu");
        GameManager.Instance.PausePlay();
    }
}
