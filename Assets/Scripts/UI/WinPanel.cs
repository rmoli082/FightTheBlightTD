using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    Scene current;

    private void Awake()
    {
        current = SceneManager.GetActiveScene();
        PlayerPrefs.SetInt($"{current} Completed", 1);
    }
    public void Replay()
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
