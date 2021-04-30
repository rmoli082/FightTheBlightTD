using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NewWaveSpawner : Singleton<NewWaveSpawner>
{
    public int CurrentWave { get; set; } = 1;
    public int LivesLostThisWave { get; set; } = 0;
    public float Modifier { get; set; }  = 1;
    public int TotalWaves { get => config.TotalWaves; }

    public EnemyFactory[] factory;
    public EnemyFactory bossFactory;
    public GameObject mainBossPrefab;

    int enemiesThisWave;
    [SerializeField]
    float spawnPause = 0.45f;
    [SerializeField]
    DifficultyAdjuster.DifficultyConfig config;
    
    System.Random randomFactoryGenerator;
    System.Random randomEnemyGenerator;
    System.Random randomDifficultyGenerator;
    [SerializeField]
    int seed = 19831980;
    [SerializeField]
    int minibossSpawnPercent;

    private void Start()
    {
        GameEvents.WaveEnded += WaveEnded;
        GameEvents.NewGame += Reset;
        ParseLevelData();
        System.Random seeder = new System.Random(seed);
        randomFactoryGenerator = new System.Random(seeder.Next());
        randomEnemyGenerator = new System.Random(seeder.Next());
        randomDifficultyGenerator = new System.Random(seeder.Next());

        if (config == null)
        {
            config = new DifficultyAdjuster.DifficultyConfig();
            config.TotalWaves = 25;
            config.healthModifier = 1f;
            config.speedModifier = 1f;
            config.factoryModifier = 0.3f;
            config.spawnModifier = 0.48f;
        }
    }

    public IEnumerator SpawnWave()
    {
        if (CurrentWave > config.TotalWaves)
        {
            yield break;
        }
        
        if (CurrentWave == config.TotalWaves)
        {
            GameEvents.OnWaveStarted();
            LevelManager.Instance.sceneData.currentWave.text = $"BOSS FIGHT";
            Enemy boss = Instantiate(mainBossPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
            if (boss.bossAlert != null)
                LevelManager.Instance.sceneData.soundEffectsPlayer.PlayBossAudio(boss.bossAlert);
            GameManager.Instance.EnemiesRemaining++;
        }
        else
        {
            ParseWaveData();

            GameEvents.OnWaveStarted();
            LevelManager.Instance.sceneData.currentWave.text = $"WAVE {CurrentWave}";

            for (int i = 0; i < enemiesThisWave; i++)
            {
                Enemy enemy;
                if (CurrentWave % 5 == 0 && randomEnemyGenerator.Next(0, 101) <= minibossSpawnPercent)
                {
                    enemy = SelectMiniBoss();
                    if (enemy.bossAlert != null)
                        LevelManager.Instance.sceneData.soundEffectsPlayer.PlayBossAudio(enemy.bossAlert);
                }
                else
                {
                    enemy = SelectEnemy();
                }

                ApplyDifficultyModifier(enemy);
                if (!enemy.isMiniBoss)
                    ApplySkillLevelModifier(enemy, Modifier);
                GameEvents.OnEnemySpawned(enemy);
                GameManager.Instance.EnemiesRemaining++;
                yield return new WaitForSeconds(spawnPause);
            }
        
        }

        yield return new WaitUntil(() => GameManager.Instance.EnemiesRemaining <= 0);
        CurrentWave = CurrentWave + 1;
        SetModifier();
        GameEvents.OnWaveEnded();
        GameEvents.OnSaveInitiated();
    }

    private void ParseLevelData()
    {
        config = GameManager.Instance.Difficulty;
    }

    private void ParseWaveData()
    {
        // Set EnemiesThisWave
        enemiesThisWave = 10 + (int)(Mathf.Pow(CurrentWave, 2) / 2) ;
        if (CurrentWave >= 17)
        {
            enemiesThisWave = 10 + (int)(Mathf.Pow(CurrentWave, 2) / 4); 
        }

        LivesLostThisWave = 0;
    }

    private Enemy SelectEnemy()
    {
        int factoryNumber = Mathf.Clamp((int)(CurrentWave / (config.TotalWaves * config.factoryModifier) * 3), 0, factory.Length);

        // Select a random factory
        int randFactory = randomFactoryGenerator.Next(0, factoryNumber);

        // Select a random difficulty
        int difficulty = GenerateDifficulty(randFactory);
        EnemyType type = GenerateType(difficulty);
        return factory[randFactory].Get(type);
        
    }

    private Enemy SelectMiniBoss()
    {
        int bossType;
        if (CurrentWave <= config.TotalWaves - 5)
        {
            bossType = (CurrentWave / 5) - 1;
        }
        else
        {
            bossType = randomEnemyGenerator.Next(0, 4);
        }

        return bossFactory.Get(GenerateType(bossType));
    }

    public void ResetWave(int waveNumber)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            Destroy(e);
        }
        this.CurrentWave = waveNumber;
        LevelManager.Instance.bossIsDead = false;
        LevelManager.Instance.sceneData.nextWaveButton.SetColor(new Color(255, 255, 255, 1f));
    }

    private void ApplyDifficultyModifier(Enemy enemy)
    {
        enemy.speed *= config.speedModifier;
        enemy.health *= config.healthModifier;
    }

    private void ApplySkillLevelModifier(Enemy enemy, float modifier)
    {
        enemy.speed *= modifier;
        if (enemy.speed > 20)
        {
            enemy.speed = 20;
        }
        enemy.health *= modifier;
    }

    private void SetModifier()
    {
        if (LivesLostThisWave == 0)
        {
            Modifier += 0.07f;
            WaveBonusStuff.Instance.PerfectStreak++;
        }
        else if (LivesLostThisWave <= 3)
        {
            Modifier += 0.05f;
            WaveBonusStuff.Instance.PerfectStreak = 0;
        }
        else if (LivesLostThisWave <= 6)
        {
            Modifier += 0.03f;
            WaveBonusStuff.Instance.PerfectStreak = 0;
        }
        else if (LivesLostThisWave <= 9)
        {
            Modifier -= 0.05f;
            WaveBonusStuff.Instance.PerfectStreak = 0;
        }
        else if (LivesLostThisWave >= 20)
        {
            Modifier -= 0.2f;
            WaveBonusStuff.Instance.PerfectStreak = 0;
        }
        else if (LivesLostThisWave >= 10)
        {
            Modifier -= 0.1f;
            WaveBonusStuff.Instance.PerfectStreak = 0;
        }

    }

    private void WaveEnded()
    {
        LevelManager.Instance.sceneData.nextWaveButton.SetColor(new Color(255, 255, 255, 1f));
        LevelManager.Instance.AdjustGold(Mathf.CeilToInt(LevelManager.Instance.levelData.waveGoldReward * (Modifier)));
    }

    private void Reset()
    {
        CurrentWave = 1;
        LivesLostThisWave = 0;
        Modifier = 1;
    }

    private int GenerateDifficulty(int factoryNumber)
    {
        int difficultyCap = (int)(CurrentWave / (config.TotalWaves * 0.48f) * (4));
        int difficulty;
        if (factoryNumber != 0)
        {
            difficultyCap = Mathf.Clamp(difficultyCap - factoryNumber, 1, 4);
            difficulty = randomDifficultyGenerator.Next(0, difficultyCap);
        }
        else
        {
            difficulty = randomDifficultyGenerator.Next(0, difficultyCap + 1);
        }
       

        return difficulty;
    }

    private EnemyType GenerateType(int difficulty)
    {
        EnemyType type = difficulty switch
        {
            0 => EnemyType.EASY,
            1 => EnemyType.MEDIUM_LOW,
            2 => EnemyType.MEDIUM_HARD,
            _ => EnemyType.HARD,
        };
        return type;
    }
}
