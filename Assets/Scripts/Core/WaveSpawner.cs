using System;
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
        for (int i = 0; i < order.Length; i++)
        {
            SpawnEnemy(i);
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void ParseLevel()
    {
        string data = levelData.text;
        waves = Regex.Split(data, "\n|\r|\r\n");
    }

    private void ParseWaveData()
    {
        string text = waves[waveNumber];
        order = Regex.Split(text, ",");
    }

    private void SpawnEnemy(int i)
    {
        int enemy = Convert.ToInt32(order[i]);
        if (enemy == -1)
        {
            return;
        }
        Instantiate(enemies[enemy], transform.position, Quaternion.identity);
    }
}
