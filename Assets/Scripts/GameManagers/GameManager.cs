using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private bool isPaused = false;
    private SceneData data;

    protected override void Awake()
    {
        base.Awake();
        data = GameObject.FindObjectOfType<SceneData>();
        UpdateGemsDisplay();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
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
        data.playerGems.text = $"�{ Player.Instance.GetGems()}";
        Debug.Log($"updating gems");
    }

    public void Win()
    {
        PausePlay();
        LevelManager.Instance.sceneData.winGems.text = $"�{LevelManager.Instance.levelData.winGems}";
        LevelManager.Instance.sceneData.winPanel.SetActive(true);
        Player.Instance.AdjustGems(LevelManager.Instance.levelData.winGems);
    }

    public void Lose()
    {
        PausePlay();
        LevelManager.Instance.sceneData.winPanel.SetActive(true);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        data = GameObject.FindObjectOfType<SceneData>();
        UpdateGemsDisplay();
    }
}
