using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Placeable
{

    private readonly string enemyTag = "Enemy";
    public Transform target;
    public float range = 6f;
    public Transform partToRotate;
    public Transform firePoint;
    private float turnSpeed = 10f;
    public float fireRate = 1f;
    public float projectileForce = 325f;
    private float shotCounter = 0;

    public GameObject projectilePrefab;

    private void Awake()
    {
        target = null;
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetTarget), 0f, 0.33f);
    }

    private void Update()
    {
        shotCounter -= Time.deltaTime;

        if (target != null)
        {
            LockOnTarget();
            if (shotCounter <= 0)
            {
                Shoot();
                shotCounter = 1 / fireRate;
            }
                
        }
            
    }

    private void GetTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            SetTarget(nearestEnemy.transform);
        }
        else
        {
            SetTarget(null);
        }
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void SetTarget(Transform transform)
    {
        target = transform;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        
        if (TurretType == PlaceableType.seeker)
        {
            SeekerProjectile sProjectile = bullet.GetComponent<SeekerProjectile>();
            sProjectile.SetTurret(this);
            sProjectile.SetTarget(target);
        }
        else
        {
            Projectile projectile = bullet.GetComponent<Projectile>();
            projectile.SetTurret(this);
            projectile.Launch(target.position - firePoint.position, projectileForce);
        }
        
    }

  
}
