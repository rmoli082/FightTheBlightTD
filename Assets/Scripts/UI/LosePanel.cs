using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    public int replayCost;
    Scene current;


    private void Awake()
    {
        current = SceneManager.GetActiveScene();
    }

    public void Replay()
    {
        Player.Instance.AdjustGems(-replayCost);
        GameManager.Instance.LoadScene(current.name);
        GameManager.Instance.PausePlay();
    }

    public void Exit()
    {
        GameManager.Instance.LoadScene("MainMenu");
        GameManager.Instance.PausePlay();
    }
}
