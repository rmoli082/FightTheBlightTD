using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Placeable turret;
    Rigidbody rb;

    public bool canExplode = false;
    public float explodeRange = 0f;
    public float stunTime = 0f;
    public float stunPower = 0.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();
    }

    public void SetTurret(Placeable placeable)
    {
        turret = placeable;
    }

    public void Launch(Vector3 direction, float force)
    {
        rb.AddForce(direction * force);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController ec = other.GetComponent<EnemyController>();
            Enemy e = other.GetComponent<Enemy>();

            if (turret.TurretType == PlaceableType.stunner && !ec.isStunned)
            {
                Stun(ec);
                Destroy(this.gameObject);
                return;
            }
            else
            {
                e.Damage(turret.DamageAmount, turret.TurretType.ToString());
                if (canExplode)
                {
                    Explode(explodeRange);
                }
                Destroy(this.gameObject);
                return;
            } 
           
        }
    }

    void Stun(EnemyController ec)
    {
        ec.speed *= stunPower;
        ec.isStunned = true;
        ec.stunTime = stunTime;
    }

    void Explode(float range)
    {
        Collider[] exploded = Physics.OverlapSphere(transform.position, range);
        foreach (Collider c in exploded)
        {
            if (c.gameObject.CompareTag("Enemy"))
            {
                Enemy e = c.GetComponentInParent<Enemy>();
                e.Damage(turret.DamageAmount, turret.TurretType.ToString());
                Destroy(this);
            }
            
        }
    }

}
