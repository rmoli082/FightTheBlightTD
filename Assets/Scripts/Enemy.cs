using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 1;
    public int goldReward = 1;

    public GameObject dieEffect;

    public void Damage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (dieEffect != null)
        {
            Instantiate(dieEffect, transform.position, Quaternion.identity);
        }
        GameEvents.OnEnemyKilled();
        LevelManager.Instance.AdjustGold(goldReward);
        Destroy(gameObject);
        return;
    }

}
