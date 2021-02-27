using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Placeable turret;
    Rigidbody rb;
    Collider coll;

    public bool canExplode = false;
    public float explodeRange = 0f;
    public float stunTime = 0f;
    public float stunPower = 2f;

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

                if (turret.TurretType == PlaceableType.stunner && !ec.isStunned)
                {
                    Stun(ec);
                   Destroy(gameObject);
                    return;
                }
                else
                {
                    e.Damage(turret.DamageAmount, turret.TurretType.ToString());
                    if (canExplode)
                    {
                        Explode(explodeRange);
                    }
                    Destroy(gameObject);
                }
            }
        }
    }

    void Stun(EnemyController ec)
    {
        ec.speed /= stunPower;
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
                Destroy(gameObject);
                return;
            }
            
        }
    }

    /*
    private IEnumerator DeactivateBullet(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
    */
}
