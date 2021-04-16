using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyFactory : MonoBehaviour
{
    [System.Serializable]
    public class EnemyConfig
    {
        public GameObject prefab;
        [FloatRangeSlider(1f, 100f)]
        public FloatRange health = new FloatRange(1f, 4f);
        [FloatRangeSlider(5f, 15f)]
        public FloatRange speed = new FloatRange(5f, 10f);
    }

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    EnemyConfig easy, mediumLow, mediumHard, hard;

    EnemyConfig GetConfig(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.EASY: 
                return easy;
            case EnemyType.MEDIUM_LOW: 
                return mediumLow;
            case EnemyType.MEDIUM_HARD:
                return mediumHard;
            case EnemyType.HARD: 
                return hard;
        }

        return null;
    }

    public Enemy Get(EnemyType type = EnemyType.EASY)
    {
        EnemyConfig config = GetConfig(type);
        GameObject obj = Instantiate(config.prefab, spawnPoint.position, Quaternion.identity);
        Enemy instance = obj.GetComponent<Enemy>();

        instance.Initialize(config.speed.RandomValueInRange, config.health.RandomValueInRange);

        return instance;
    }
}
