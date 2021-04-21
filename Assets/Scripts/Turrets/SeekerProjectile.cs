using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerProjectile : MonoBehaviour
{
    public Turret turret;

    public Transform target;

    public bool canExplode = false;
    private float range = 3f;

    float bulletTimer = 2f;

    Collider coll;

    private void OnEnable()
    {
        coll = GetComponent<Collider>();
        if (coll == null)
        {
            coll = gameObject.AddComponent<SphereCollider>();
            coll.isTrigger = true;
        }
        coll.enabled = true;
    }

    private void Update()
    {
        if (target == null)
        {
            gameObject.SetActive(false);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = turret.projectileForce * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

        bulletTimer -= Time.deltaTime;
        if (bulletTimer == 0)
        {
            gameObject.SetActive(false);
            bulletTimer = 2;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collider of {other.gameObject.name} entered");
        Enemy e = other.GetComponentInParent<Enemy>();
        e.Damage(turret.DamageAmount/2, turret.TurretType.ToString());
        if (canExplode)
        {
            Explode(range);
        }
        gameObject.SetActive(false);
        return;
    }

    public void SetTurret(Turret _turret)
    {
        turret = _turret;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
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
            }

        }
    }

    void HitTarget()
    {
        target.GetComponent<Enemy>().Damage(turret.DamageAmount, turret.TurretType.ToString());
        Debug.Log($"{target.name} hit using HitTarget");
    }
}
