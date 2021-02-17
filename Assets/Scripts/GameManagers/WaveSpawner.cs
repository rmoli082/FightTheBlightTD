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

    public int waveNumber = 0;
    public float spawnPause = 0.45f;

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
        LevelManager.Instance.sceneData.currentWave.text = $"WAVE {waveNumber + 1}";
        for (int i = 0; i < order.Length; i++)
        {
            SpawnEnemy(i);
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

        LevelManager.Instance.levelData.totalWaves = waves.Length;

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
        GameManager.Instance.EnemiesRemaining++;
        Vector3 pos = transform.position;
        pos.y = 0;
        transform.position = pos;
        GameEvents.OnEnemySpawned(e.GetComponent<Enemy>());
        return e;
    }

    private void WaveEndedStuff()
    {
        LevelManager.Instance.sceneData.nextWaveButton.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
        LevelManager.Instance.sceneData.nextWaveButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0, 1f);
    }
}
