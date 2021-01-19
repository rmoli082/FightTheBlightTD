using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerProjectile : MonoBehaviour
{
    public Turret turret;

    public Transform target;

    private void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
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
        e.Damage(turret.DamageAmount);
        Destroy(this.gameObject);
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
}
