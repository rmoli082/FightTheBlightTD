using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private bool isPaused = false;
    public bool IsPaused { get => isPaused; }

    private bool isFirstRun = true;
    public bool IsFirstRun { get => isFirstRun; }

    [SerializeField]
    private SceneData data;

    public int EnemiesRemaining;

    public AsyncOperation asyncScene;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        data = GameObject.FindObjectOfType<SceneData>();

        if (PlayerPrefs.HasKey("FirstRunComplete"))
        {
            isFirstRun = false;
        }
        else
        {
            isFirstRun = true;
        }
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

    public void LoadSceneAsync(string sceneName)
    {
        asyncScene = SceneManager.LoadSceneAsync(sceneName);
        asyncScene.allowSceneActivation = false;
    }

    public void PausePlay()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            data.backgroundMusic.Play();
            isPaused = !isPaused;
        }
        else
        {
            Time.timeScale = 0f;
            data.backgroundMusic.Pause();
            isPaused = !isPaused;
        }

        data.playPauseButton.GetComponent<PausePlayButton>().UpdateButtonText();
    }

    public void SpeedUp()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 2f;
            data.speedButton.GetComponentInChildren<TextMeshProUGUI>().text = "Slow down";
        }
        else if (Time.timeScale == 2f)
        {
            Time.timeScale = 1f;
            data.speedButton.GetComponentInChildren<TextMeshProUGUI>().text = "Speed up";
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
        GameEvents.OnGameOverWin();
    }

    public void Lose()
    {
        PausePlay();
        LevelManager.Instance.sceneData.losePanel.SetActive(true);
        Analytics.CustomEvent("LevelLose",
            new Dictionary<string, object> {
                { "Level", SceneManager.GetActiveScene().name},
                { $"{SceneManager.GetActiveScene().name} wave", WaveSpawner.Instance.waveNumber + 1 },
                { "Gold", LevelManager.Instance.playerStats.playerGold }
            });
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        data = GameObject.FindObjectOfType<SceneData>();
        UpdateGemsDisplay();
        if (PlayerPrefs.HasKey("FirstRunComplete"))
        {
            isFirstRun = false;
        }
        else
        {
            isFirstRun = true;
        }
    }
}
