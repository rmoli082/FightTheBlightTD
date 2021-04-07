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
        PlayerPrefs.SetInt($"{current.name} Complete", 1);
    }

    private void OnEnable()
    {
        GameManager.Instance.UpdateGemsDisplay();
        PlayGames.UnlockAchievement(GPGSIds.achievement_beat_1_level);
        PlayGames.IncrementAchievement(GPGSIds.achievement_beat_5_levels, 1);
        PlayGames.IncrementAchievement(GPGSIds.achievement_beat_10_levels, 1);

        if (LevelManager.Instance.playerStats.continues == 0)
        {
            PlayGames.UnlockAchievement(GPGSIds.achievement_no_continues);
        }
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
