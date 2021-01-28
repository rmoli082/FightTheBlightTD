using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWaveButton : MonoBehaviour
{
    public void StartWave()
    {
        StartCoroutine(WaveSpawner.Instance.SpawnWave());
    }
}
