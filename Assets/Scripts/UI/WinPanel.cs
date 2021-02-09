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
    }
    public void Replay()
    {
        GameManager.Instance.LoadScene(current.name);
    }

    public void Exit()
    {
        GameManager.Instance.LoadScene("MainMenu");
    }
}
