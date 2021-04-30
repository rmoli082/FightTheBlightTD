using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveBonusStuff : Singleton<WaveBonusStuff>
{
    public int PerfectStreak { get; set; } = 0;

    private void Start()
    {
        GameEvents.WaveEnded += DoStreakBonus;
        GameEvents.NewGame += Reset;
    }

    private IEnumerator StreakBonusPopup(string message)
    {
        LevelManager.Instance.sceneData.streakPopup.SetActive(true);
        LevelManager.Instance.sceneData.streakPopup.GetComponentInChildren<TextMeshProUGUI>().text = message;
        yield return new WaitForSecondsRealtime(1.5f);
        LevelManager.Instance.sceneData.streakPopup.SetActive(false);
    }

    private void DoStreakBonus()
    {
        if (NewWaveSpawner.Instance.LivesLostThisWave > 0)
        {
            if (PerfectStreak > 1)
            {
                    StartCoroutine(StreakBonusPopup("Perfect Streak Broken!"));
            }
        } 
        else if (NewWaveSpawner.Instance.LivesLostThisWave == 0)
        {
            CalculateStreakBonuses();
        }
    }

    private void CalculateStreakBonuses()
    {
        
        if (PerfectStreak % 5 == 0)
        {
            LevelManager.Instance.AdjustGold(LevelManager.Instance.levelData.waveGoldReward);
            LevelManager.Instance.AdjustLives(5);
            StartCoroutine(StreakBonusPopup($"Perfect Streak {PerfectStreak} Rounds"));

            if (PerfectStreak == 5)
            {
                PlayGames.UnlockAchievement(GPGSIds.achievement_perfect_streak_5);
            }
            else if (PerfectStreak == 10)
            {
                PlayGames.UnlockAchievement(GPGSIds.achievement_perfect_streak_10);
            }
        } 
        else
        {
            StartCoroutine(StreakBonusPopup("Perfect!"));
        }
    }

    private void OnDestroy()
    {
        GameEvents.WaveEnded -= DoStreakBonus;
    }

    private void Reset()
    {
        PerfectStreak = 0;
    }
}
