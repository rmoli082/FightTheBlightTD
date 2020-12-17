using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class WaveSpawner : Singleton<WaveSpawner>
{
    public TextAsset levelData;
    public GameObject[] enemies;

    string[] waves;
    string[] order;

    public int waveNumber = 0;

    protected override void Awake()
    {
        base.Awake();
        ParseLevel();
    }

    public IEnumerator SpawnWave()
    {
        ParseWaveData();
        if (waveNumber >= waves.Length)
        {
            yield break;
        }
        for (int i = 0; i < order.Length; i++)
        {
            SpawnEnemy(i);
            yield return new WaitForSeconds(0.3f);
        }

        waveNumber++;
    }

    private void ParseLevel()
    {
        string data = levelData.text;
        waves = Regex.Split(data, "\n|\r|\r\n");
    }

    private void ParseWaveData()
    {
        if (waveNumber >= waves.Length)
            return;
        string text = waves[waveNumber];
        order = Regex.Split(text, ",");
    }

    private void SpawnEnemy(int i)
    {
        int enemy;
        if (!int.TryParse(order[i], out enemy))
        {
            return;
        }
        Instantiate(enemies[enemy], transform.position, Quaternion.identity);
    }
}
