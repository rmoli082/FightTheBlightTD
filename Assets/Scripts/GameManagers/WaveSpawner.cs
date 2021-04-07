using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : Singleton<WaveSpawner>
{
    private GameObject[] enemies;

    private string[] waves;
    private string[] order;

    public int waveNumber = 1;
    public float spawnPause = 0.45f;

    public float healthModifier = 1f;
    public float speedModifier = 1f;
    private int perfectStreak = 0;
    private float goldAdjustment = 1f;
    public int livesLostThisWave = 0;

    public int GetNumberOfWaves { get => waves.Length;  }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GameEvents.WaveEnded += WaveEndedStuff;
        ParseLevel();
    }

    public IEnumerator SpawnWave()
    {
        if (waveNumber >= waves.Length)
            yield break;
        ParseWaveData();
        GameEvents.OnWaveStarted();
        LevelManager.Instance.sceneData.currentWave.text = $"WAVE {waveNumber}";
        for (int i = 0; i < order.Length; i++)
        {
            GameObject enemy = SpawnEnemy(i);
            if (!enemy.GetComponent<Enemy>().isBoss || !enemy.GetComponent<Enemy>().isMiniBoss)
                ApplyModifiers(enemy);
            yield return new WaitForSeconds(spawnPause);
        }

        yield return new WaitUntil(() => GameManager.Instance.EnemiesRemaining <= 0);
        waveNumber++;
        GameEvents.OnWaveEnded();
        GameEvents.OnSaveInitiated();
    }

    private void ParseLevel()
    {
        // load wave data
        string data = LevelManager.Instance.levelData.waveData.text;
        waves = Regex.Split(data, "\n");

        LevelManager.Instance.totalWaves = waves.Length;

        // load enemies list
        enemies = LevelManager.Instance.levelData.enemies;
    }

    private void ParseWaveData()
    {
        string text = waves[waveNumber];
        order = Regex.Split(text, ",");
        GameManager.Instance.EnemiesRemaining = 0;
    }

    private GameObject SpawnEnemy(int i)
    {
        if (!int.TryParse(order[i], out int enemy))
        {
            return null;
        }
        GameObject e = Instantiate(enemies[enemy], transform.position, Quaternion.identity);
        Enemy en = e.GetComponent<Enemy>();
        if (en.bossAlert)
            LevelManager.Instance.sceneData.soundEffectsPlayer.PlayBossAudio(en.bossAlert);
        GameManager.Instance.EnemiesRemaining++;
        Vector3 pos = transform.position;
        pos.y = 0;
        transform.position = pos;
        GameEvents.OnEnemySpawned(en);
        return e;
    }

    private void WaveEndedStuff()
    {
        LevelManager.Instance.sceneData.nextWaveButton.SetColor (new Color(255, 255, 255, 1f));
        SetDifficultyModifiers();
        LevelManager.Instance.AdjustGold(Mathf.CeilToInt(LevelManager.Instance.levelData.waveGoldReward * goldAdjustment));
        livesLostThisWave = 0;
        goldAdjustment = 1f;
    }

    private void SetDifficultyModifiers()
    {
        if (livesLostThisWave > 0)
        {
            if (perfectStreak > 1)
            {
                if (waveNumber != waves.Length)
                    StartCoroutine(PopupStreakNotice("Perfect Streak Broken!"));
            }
        }

        if (livesLostThisWave == 0)
        {
            healthModifier += 0.15f;
            speedModifier += 0.1f;
            goldAdjustment += 1f;
            perfectStreak++;
            if (waveNumber != waves.Length - 1)
                StartCoroutine(PopupStreakNotice("Perfect"));
        }
        else if (livesLostThisWave <= 3)
        {
            healthModifier += 0.1f;
            speedModifier += 0.05f;
            perfectStreak = 0;
        }
        else if (livesLostThisWave <= 6)
        {
            healthModifier += 0.05f;
            perfectStreak = 0;
            goldAdjustment += 0.1f;
        }
        else if (livesLostThisWave <= 9)
        {
            perfectStreak = 0;
            healthModifier -= 0.05f;
            speedModifier -= 0.05f;
            goldAdjustment += 0.25f;
        }
        else if (livesLostThisWave >= 20)
        {
            perfectStreak = 0;
            healthModifier -= 0.2f;
            speedModifier -= 0.2f;
            goldAdjustment += 1f;
        }
        else if (livesLostThisWave >= 10)
        {
            perfectStreak = 0;
            healthModifier -= 0.1f;
            speedModifier -= 0.1f;
            goldAdjustment += 0.5f;
        }

        if (perfectStreak > 0 && perfectStreak % 5 == 0)
        {
            goldAdjustment += 1.5f;
            LevelManager.Instance.AdjustLives(5);
            if (waveNumber != waves.Length)
            {
                StartCoroutine(PopupStreakNotice($"Perfect Streak {perfectStreak} Rounds"));
            }

            if (perfectStreak == 5)
            {
                PlayGames.UnlockAchievement(GPGSIds.achievement_perfect_streak_5);
            }
            else if (perfectStreak == 10)
            {
                PlayGames.UnlockAchievement(GPGSIds.achievement_perfect_streak_10);
            }
        }
    }

    private void ApplyModifiers(GameObject enemy)
    {
        ApplyHealthModifier(enemy);
        ApplySpeedModifier(enemy);
    }

    private void ApplyHealthModifier(GameObject enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e.isBoss || e.isMiniBoss)
            return;
        e.health *= healthModifier;
    }

    private void ApplySpeedModifier(GameObject enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e.isBoss || e.isMiniBoss)
            return;
        enemy.GetComponent<EnemyController>().speed *= speedModifier;
    }

    private IEnumerator PopupStreakNotice(string message)
    {
        LevelManager.Instance.sceneData.streakPopup.SetActive(true);
        LevelManager.Instance.sceneData.streakPopup.GetComponentInChildren<TextMeshProUGUI>().text = message;
        yield return new WaitForSeconds(3f);
        LevelManager.Instance.sceneData.streakPopup.SetActive(false);
    }

    public void ResetWave(int waveNumber)
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy e in enemies)
        {
            Destroy(e);
        }
        this.waveNumber = waveNumber;
        LevelManager.Instance.sceneData.nextWaveButton.SetColor(new Color(255, 255, 255, 1f));
    }

    private void OnDestroy()
    {
        GameEvents.WaveEnded -= WaveEndedStuff;
    }
}
