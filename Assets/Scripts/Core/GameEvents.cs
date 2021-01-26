using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static System.Action SaveInitiated;
    public static System.Action LoadInitiated;
    public static System.Action<Enemy> EnemySpawned;
    public static System.Action EnemyKilled;
    public static System.Action WaveStarted;
    public static System.Action WaveEnded;

    public static void OnSaveInitiated()
    {
        SaveInitiated?.Invoke();
    }

    public static void OnLoadInitiated()
    {
        LoadInitiated?.Invoke();
    }

    public static void OnEnemySpawned(Enemy e)
    {
        EnemySpawned?.Invoke(e);
    }

    public static void OnEnemyKilled()
    {
        EnemyKilled?.Invoke();
    }

    public static void OnWaveStarted()
    {
        WaveStarted?.Invoke();
    }

    public static void OnWaveEnded()
    {
        WaveEnded?.Invoke();
    }
}
