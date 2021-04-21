using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Placeable turret;
    Turret _turret;
    Rigidbody rb;
    Collider coll;

    public bool canExplode = false;
    public bool isStunner = true;

    private static object mLock = new object();

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();
       coll = GetComponent<Collider>();
       coll.enabled = true;
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        
    }

    public void SetTurret(Placeable placeable)
    {
        turret = placeable;
    }

    public void Launch(Vector3 direction, float force)
    {
        rb.AddForce(direction * force);
        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
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
                    e.Damage(turret.DamageAmount, turret.TurretType.ToString());
                    Stun(ec, e);
                }
                else
                {
                    Turret exploder = (Turret)turret;
                    e.Damage(turret.DamageAmount, turret.TurretType.ToString());
                    if (canExplode)
                    {
                        Explode(exploder.explodeRange);
                    }
                }
                gameObject.SetActive(false);
            }
        }
    }

    void Stun(EnemyController ec, Enemy e)
    {
        if (ec.isStunned)
        {
            gameObject.SetActive(false);
            return;
        }
            
        Turret stunner = (Turret)turret;
        TurretStats.Instance.AddTurretKills(turret.TurretType.ToString(), 1);
        ec.SetSpeed(ec.Speed / stunner.stunPower);
        e.speed = (ec.Speed / stunner.stunPower);
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
                e.Damage(turret.DamageAmount/2, turret.TurretType.ToString());
            }  
        }
    }

}
