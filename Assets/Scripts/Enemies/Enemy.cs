using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 1;
    public int goldReward = 1;

    public GameObject dieEffect;

    private bool isAlive = true;
    public bool isMiniBoss = false;
    public bool isBoss = false;

    public AudioClip bossAlert;


    public void Damage(float damageAmount, string turretType)
    {
        health -= damageAmount;
        if (health <= 0 && isAlive == true)
        {
            isAlive = false;
            Die(turretType);
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
            Destroy(gameObject);
            return;
        
    }

}
