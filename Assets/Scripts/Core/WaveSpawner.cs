using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class WaveSpawner : Singleton<WaveSpawner>
{
    private GameObject[] enemies;

    private string[] waves;
    private string[] order;

    public int waveNumber = 0;

    protected override void Awake()
    {
        base.Awake();
        ParseLevel();
    }

    public IEnumerator SpawnWave()
    {
        if (waveNumber >= waves.Length)
            yield break;
        ParseWaveData();
        for (int i = 0; i < order.Length; i++)
        {
            SpawnEnemy(i);
            yield return new WaitForSeconds(0.3f);
        }

        waveNumber++;
    }

    private void ParseLevel()
    {
        // load wave data
        string data = LevelManager.Instance.levelData.waveData.text;
        waves = Regex.Split(data, "\n");

        // load enemies list
        enemies = LevelManager.Instance.levelData.enemies;
    }

    private void ParseWaveData()
    {
        string text = waves[waveNumber];
        order = Regex.Split(text, ",");
    }

    private void SpawnEnemy(int i)
    {
        if (!int.TryParse(order[i], out int enemy))
        {
            return;
        }
        Instantiate(enemies[enemy], transform.position, Quaternion.identity);
    }
}
