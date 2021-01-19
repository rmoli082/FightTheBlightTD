using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerProjectile : MonoBehaviour
{
    Turret turret;
    Rigidbody rb;

	Transform target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(this);
            return;
        }

        Vector3 dir = target.position - transform.position;
		float distanceThisFrame = turret.projectileForce * Time.deltaTime;

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy e = other.GetComponent<Enemy>();
            e.Damage(turret.DamageAmount);
        }

        Destroy(this);
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
