using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class GameManager : Singleton<GameManager>
{
    private bool isPaused = false;
    private SceneData data;

    public int EnemiesRemaining;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        data = GameObject.FindObjectOfType<SceneData>();
        
    }

    private void Start()
    {
        UpdateGemsDisplay();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PausePlay()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = !isPaused;
        }
        else
        {
            Time.timeScale = 0f;
            isPaused = !isPaused;
        }

    }

    public void UpdateGemsDisplay()
    {
        data.playerGems.text = $"§{ Player.Instance.GetGems()}";
    }

    public void Win()
    {
        PausePlay();
        LevelManager.Instance.sceneData.winGems.text = $"§{LevelManager.Instance.levelData.winGems}";
        LevelManager.Instance.sceneData.winPanel.SetActive(true);
        Player.Instance.AdjustGems(LevelManager.Instance.levelData.winGems);
        Analytics.CustomEvent("LevelWin", 
            new Dictionary<string, object> {
                { "Level", SceneManager.GetActiveScene().name},
                { "Gold", LevelManager.Instance.playerStats.playerGold },
                { "Lives", LevelManager.Instance.playerStats.playerLives }
            });
    }

    public void Lose()
    {
        PausePlay();
        LevelManager.Instance.sceneData.losePanel.SetActive(true);
        Analytics.CustomEvent("LevelLose",
            new Dictionary<string, object> {
                { "Level", SceneManager.GetActiveScene().name},
                { "Wave", WaveSpawner.Instance.waveNumber + 1 },
                { "Gold", LevelManager.Instance.playerStats.playerGold }
            });
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        data = GameObject.FindObjectOfType<SceneData>();
        UpdateGemsDisplay();
        
    }
}
