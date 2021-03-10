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

    private float healthModifier = 1f;
    private float speedModifier = 1f;
    private int perfectStreak = 0;
    private float goldAdjustment = 1f;
    public int livesLostThisWave = 0;

    protected override void Awake()
    {
        base.Awake();
        ParseLevel();
    }

    private void Start()
    {
        GameEvents.WaveEnded += WaveEndedStuff;
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
            LevelManager.Instance.sceneData.soundEffects.PlayOneShot(en.bossAlert);
        GameManager.Instance.EnemiesRemaining++;
        Vector3 pos = transform.position;
        pos.y = 0;
        transform.position = pos;
        GameEvents.OnEnemySpawned(en);
        return e;
    }

    private void WaveEndedStuff()
    {
        LevelManager.Instance.sceneData.nextWaveButton.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
        LevelManager.Instance.sceneData.nextWaveButton.GetComponentInChildren<TextMeshProUGUI>().text = "Next Wave";
        LevelManager.Instance.sceneData.nextWaveButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
        SetDifficultyModifiers();
        LevelManager.Instance.AdjustGold(Mathf.CeilToInt(LevelManager.Instance.levelData.waveGoldReward * goldAdjustment));
        livesLostThisWave = 0;
        goldAdjustment = 1f;
    }

    private void SetDifficultyModifiers()
    {
        if (livesLostThisWave == 0)
        {
            healthModifier += 0.15f;
            speedModifier += 0.15f;
            goldAdjustment += 1f;
            perfectStreak++;
            Debug.Log($"No lives lost");
        }
        else if (livesLostThisWave <= 3)
        {
            healthModifier += 0.1f;
            speedModifier += 0.1f;
            perfectStreak = 0;
            Debug.Log($"3 or less lives lost");
        }
        else if (livesLostThisWave <= 6)
        {
            healthModifier += 0.05f;
            perfectStreak = 0;
            Debug.Log($"6 or less lives lost");
        }
        else if (livesLostThisWave <= 9)
        {
            perfectStreak = 0;
            healthModifier -= 0.05f;
            speedModifier -= 0.05f;
            goldAdjustment += 0.25f;
            Debug.Log($"9 or less lives lost");
        }
        else if (livesLostThisWave >= 10)
        {
            perfectStreak = 0;
            healthModifier -= 0.1f;
            speedModifier -= 0.1f;
            goldAdjustment += 0.5f;
            Debug.Log($"10 or more lives lost");
        }

        Debug.Log($"Health modifier: {healthModifier}");
        Debug.Log($"Speed modifier: {speedModifier}");
        if (perfectStreak == 5)
        {
            goldAdjustment += 1.5f;
            LevelManager.Instance.AdjustLives(5);
            StartCoroutine(PopupStreakNotice());
            perfectStreak = 0;
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

    private IEnumerator PopupStreakNotice()
    {
        LevelManager.Instance.sceneData.streakPopup.SetActive(true);
        yield return new WaitForSeconds(2f);
        LevelManager.Instance.sceneData.streakPopup.SetActive(false);
    }
}
