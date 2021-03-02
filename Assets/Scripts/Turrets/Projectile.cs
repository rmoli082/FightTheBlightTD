using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Placeable turret;
    Rigidbody rb;
    Collider coll;

    public bool canExplode = false;
    public bool isStunner = true;

    private static object mLock = new object();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();
       coll = GetComponent<Collider>();
       coll.enabled = true;
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
            lock (mLock)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                EnemyController ec = other.GetComponent<EnemyController>();
                Enemy e = other.GetComponent<Enemy>();

                if (isStunner && !ec.isStunned)
                {
                    Stun(ec);
                    Destroy(gameObject);
                    return;
                }
                else
                {
                    Turret exploder = (Turret)turret;
                    e.Damage(turret.DamageAmount, turret.TurretType.ToString());
                    if (canExplode)
                    {
                        Explode(exploder.explodeRange);
                    }
                    Destroy(gameObject);
                }
            }
        }
    }

    void Stun(EnemyController ec)
    {
        Turret stunner = (Turret)turret;
        ec.speed /= stunner.stunPower;
        ec.isStunned = true;
        ec.stunTime = stunner.stunTime;
    }

    void Explode(float range)
    {
        Collider[] exploded = Physics.OverlapSphere(this.transform.position, range);

        foreach (Collider c in exploded)
        {
            if (c.gameObject.CompareTag("Enemy"))
            {
                Enemy e = c.GetComponent<Enemy>();
                e.Damage(turret.DamageAmount, turret.TurretType.ToString());
            }  
        }
    }

}
