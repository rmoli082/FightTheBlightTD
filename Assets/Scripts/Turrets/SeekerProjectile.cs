using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerProjectile : MonoBehaviour
{
    public Turret turret;

    public Transform target;

    public bool canExplode = false;
    private float range = 3f;

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
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

    }

    void HitTarget()
    {
        Enemy e = target.GetComponent<Enemy>();
        e.Damage(turret.DamageAmount, turret.TurretType.ToString());
        if (canExplode)
        {
            Explode(range);
        }
        Destroy(gameObject);
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
                Destroy(gameObject);
                return;
            }

        }
    }
}
