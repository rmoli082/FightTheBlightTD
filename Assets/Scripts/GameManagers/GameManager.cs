using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private bool isPaused = false;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
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

}
