using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float speed;
    public int goldReward = 1;

    public GameObject dieEffect;

    public bool isAlive = true;
    public bool isMiniBoss = false;
    public bool isBoss = false;

    public AudioClip bossAlert;
    public GameObject bossSpawn;

    public void Initialize(float speed, float health)
    {
        this.health = health;
        this.speed = speed;
    }

    public void Damage(float damageAmount, string turretType)
    {
        health -= damageAmount;
        if (health <= 0 && isAlive == true)
        {
            isAlive = false;
            if (isBoss)
            {
                if (isBoss)
                {
                    StartCoroutine(TrojanHorse(turretType));
                }
            }
            else
            {
                Die(turretType);
            }
            
        }
    }

    private void Die(string turretType)
    {
        if (dieEffect != null)
        {
            GameObject die = Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(die, 2.1f);
        }
        
        GameEvents.OnEnemyKilled();
        LevelManager.Instance.AdjustGold(goldReward);
        TurretStats.Instance.AddTurretKills(turretType, 1);
        GameManager.Instance.EnemiesRemaining--;

        if (isMiniBoss)
        {
            PlayGames.IncrementAchievement(GPGSIds.achievement_miniboss_assassin, 1);
            PlayGames.IncrementAchievement(GPGSIds.achievement_miniboss_terminator, 1);
        }
        
        Destroy(gameObject);
        return;

    }

    private IEnumerator TrojanHorse(string turretType)
    {
        int spawnAmount = Random.Range(5, 15);
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        Transform target = gameObject.GetComponent<EnemyController>().Target;
        int waypointIndex = gameObject.GetComponent<EnemyController>().WaypointIndex;
        gameObject.GetComponent<EnemyController>().enabled = false;
        gameObject.tag = "Waypoint";

        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject b = Instantiate(LevelManager.Instance.levelData.bossSpawn, transform.position, Quaternion.identity);
            b.GetComponent<EnemyController>().SetTarget(target, waypointIndex);
            GameManager.Instance.EnemiesRemaining++;
            GameEvents.OnEnemySpawned(b.GetComponent<Enemy>());
            yield return new WaitForSeconds(0.55f);
        }
        GameManager.Instance.EnemiesRemaining--;

        LevelManager.Instance.bossIsDead = true;
        yield return new WaitUntil(() => GameManager.Instance.EnemiesRemaining == 0);
        GameEvents.OnEnemyKilled();
        TurretStats.Instance.AddTurretKills(turretType, 1);
        Destroy(gameObject);

    }

}
