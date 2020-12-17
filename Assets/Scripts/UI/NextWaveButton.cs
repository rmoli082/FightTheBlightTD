using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWaveButton : MonoBehaviour
{
    public void StartWave()
    {
        Debug.Log($"Wave {WaveSpawner.Instance.waveNumber}");
        StartCoroutine(WaveSpawner.Instance.SpawnWave());
    }
}
