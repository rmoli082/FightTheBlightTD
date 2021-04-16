using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    private bool isPaused = false;
    public bool IsPaused { get => isPaused; }

    private bool isFirstRun = true;
    public bool IsFirstRun { get => isFirstRun; }

    [SerializeField]
    private SceneData data;

    public int EnemiesRemaining;

    public DifficultyAdjuster.DifficultyConfig Difficulty { get; set; }


    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
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
        SceneManager.sceneLoaded += OnSceneLoaded;
        // UpdateGemsDisplay();
    }


    public void LoadScene(string sceneName)
    {
        ReferenceManager.Instance.ClearLoadedGameObjects();
        SceneManager.LoadScene(sceneName);
    }

    public void PausePlay()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            data.backgroundMusic.Play();
            data.playPauseButton.SetPauseImage();
            isPaused = !isPaused;
        }
        else
        {
            Time.timeScale = 0f;
            data.backgroundMusic.Pause();
            data.playPauseButton.SetPlayImage();
            data.speedButton.SetFastImage();
            isPaused = !isPaused;
        }

    }

    public void SpeedUp()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 2f;

            data.speedButton.SetNormalImage();
        }
        else if (Time.timeScale == 2f)
        {
            Time.timeScale = 1f;

            data.speedButton.SetFastImage();
        }
    }

    public void UpdateGemsDisplay()
    {
        data.playerGems.text = Player.Instance.GetGems().ToString();

    }

    public void Win()
    {
        PausePlay();
        LevelManager.Instance.sceneData.winGems.text = $"§{LevelManager.Instance.levelData.winGems}";
        LevelManager.Instance.sceneData.gameOverPanel.SetActive(true);
        UpdateGemsDisplay();
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
        LevelManager.Instance.sceneData.gameOverPanel.SetActive(true);
        UpdateGemsDisplay();
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
