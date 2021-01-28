using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 1;
    public int goldReward = 1;

    public GameObject dieEffect;

    public void Damage(float damageAmount, string turretType)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die(turretType);
        }
    }

    private void Die(string turretType)
    {
        if (dieEffect != null)
        {
            Instantiate(dieEffect, transform.position, Quaternion.identity);
        }
        GameEvents.OnEnemyKilled();
        LevelManager.Instance.AdjustGold(goldReward);
        TurretStats.Instance.AddTurretKills(turretType, 1);
        Destroy(gameObject);
        return;
    }

}
