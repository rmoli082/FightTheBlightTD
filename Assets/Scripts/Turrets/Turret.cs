using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;

public class Turret : Placeable
{
    [Header("Turret Info")]
    private readonly string enemyTag = "Enemy";
    public Transform target;
    public float range = 6f;
    public Transform partToRotate;
    public Transform firePoint;
    private readonly float turnSpeed = 10f;
    public float fireRate = 1f;
    public float projectileForce = 325f;
    public float stunPower = 0f;
    public float stunTime = 0f;
    public float explodeRange = 0f;
    private float shotCounter = 0;

    public List<GameObject> pooledProjectiles;
    public int amountToPool = 7;
    public AssetReferenceGameObject projectile;
    public AssetReferenceGameObject upgradedProjectile;

    [Header("Turret Effects")]
    public AudioClip shotFX;
    public MeshRenderer[] glowObj;
    public Material[] glowMat;
    private Material[] originalMat;

    [Header("Upgrade Slots")]
    public Upgrade[] turretUpgrades = new Upgrade[3];
    public int[] slotLevel = { 0, 0, 0 };

    [Header("Upgrade Panels")]
    public GameObject upgradePanel;
    public GameObject[] upgradeSlotButtons = new GameObject[3];

    private UpgradePanel _upgradePanel;

    private void Awake()
    {
        originalMat = new Material[glowObj.Length];
        target = null;
        upgradePanel = LevelManager.Instance.sceneData.turretUpgradePanel;
        for (int i = 0; i < glowObj.Length; i++)
        {
            originalMat[i] = glowObj[i].material;
        }
        _upgradePanel = upgradePanel.GetComponent<UpgradePanel>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetTarget), 0f, 0.33f);

        switch(TurretType)
        {
            case PlaceableType.turret:
                GetTurretPerms();
                break;
            case PlaceableType.rapid:
                GetRapidPerms();
                break;
            case PlaceableType.bomber:
                GetBomberPerms();
                break;
            case PlaceableType.seeker:
                GetSeekerPerms();
                break;
            case PlaceableType.stunner:
                GetStunnerPerms();
                break;
        }

        pooledProjectiles = new List<GameObject>();

        ProjectilePooler.PoolProjectiles(this);
    }

    private void Update()
    {
        shotCounter -= Time.deltaTime;

        if (target != null)
        {
            LockOnTarget();
            if (shotCounter <= 0)
            {
                if (TurretType == PlaceableType.turret && TurretStats.Instance.turretPermanentBought[2])
                {
                    StartCoroutine(DoubleShot());
                }
                else
                {
                    if (target != null)
                        Shoot();
                }               
                shotCounter = 1 / fireRate;
            }
                
        }
            
    }

    private void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            _upgradePanel.PopupUpgradePanel(this);
        }
    }

    public void Glow(bool shouldGlow)
    {
        if (shouldGlow)
        {
            for (int i = 0; i < glowObj.Length; i++)
            {
                glowObj[i].material = glowMat[i];
            }
        }
        else
        {
            for (int i = 0; i < glowObj.Length; i++)
            {
                glowObj[i].material = originalMat[i];
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
            float distance2Enemy = Vector3.Distance(firePoint.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance || distance2Enemy < shortestDistance)
            {
                if (distanceToEnemy < distance2Enemy)
                    shortestDistance = distanceToEnemy;
                else
                    shortestDistance = distance2Enemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            if (nearestEnemy.transform.parent != null)
                SetTarget(nearestEnemy.transform.parent.transform);
            else
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
        Vector3 rotation = Quaternion.Lerp(partToRotate.localRotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.localRotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void SetTarget(Transform transform)
    {
        target = transform;
    }

    private void Shoot()
    {
        if (shotFX != null )
        {
            LevelManager.Instance.sceneData.soundEffectsPlayer.PlayAudio(shotFX);
        }

        GameObject bullet = ProjectilePooler.GetProjectile(this);
        bullet.SetActive(true);

        if (TurretType == PlaceableType.seeker)
        {
            SeekerProjectile sProjectile = bullet.GetComponent<SeekerProjectile>();
            if (sProjectile == null)
                bullet.AddComponent<SeekerProjectile>();
            sProjectile.SetTurret(this);
            sProjectile.SetTarget(target);
        }
        else
        {
            Projectile projectile = bullet.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.SetTurret(this);
                projectile.Launch(target.position - firePoint.position, projectileForce);
            }
            
        }

    }

    private IEnumerator DoubleShot()
    {
        Shoot();
        yield return new WaitForSeconds(0.125f);
        Shoot();
    }

    private void GetTurretPerms()
    {
        if (TurretStats.Instance.turretPermanentBought[0])
        {
            projectileForce = 400f;
            DamageAmount = 3;
        }
        if (TurretStats.Instance.turretPermanentBought[1])
        {
            fireRate = 1.5f;
        }
    }

    private void GetRapidPerms()
    {
        if (TurretStats.Instance.rapidPermanentBought[0])
        {
            fireRate = 2.25f;
        }
        if (TurretStats.Instance.rapidPermanentBought[1])
        {
            DamageAmount = 2;
        }
        if (TurretStats.Instance.rapidPermanentBought[2])
        {
            projectileForce = 15f;
            projectile = upgradedProjectile;
        }
    }

    private void GetBomberPerms()
    {
        if (TurretStats.Instance.bomberPermanentBought[0])
        {
            DamageAmount = 4.5f;
        }
        if (TurretStats.Instance.bomberPermanentBought[1])
        {
            explodeRange = 4f;
        }
        if (TurretStats.Instance.bomberPermanentBought[2])
        {
            fireRate = 1.25f;
            range = 16f;
        }
    }

    private void GetSeekerPerms()
    {
       
            if (TurretStats.Instance.seekerPermanentBought[0])
            {
                fireRate = 1.35f;
            }
            if (TurretStats.Instance.seekerPermanentBought[1])
            {
                projectileForce = 13f;
                DamageAmount = 3;
            }
            if (TurretStats.Instance.seekerPermanentBought[2])
            {
            projectile = upgradedProjectile;
            }
    }

    private void GetStunnerPerms()
    {
        if (TurretStats.Instance.stunnerPermanentBought[0])
        {
            stunTime = 7;
            stunPower = 2.5f;
        }
        if (TurretStats.Instance.stunnerPermanentBought[1])
        {
            DamageAmount = 1;
        }
        if (TurretStats.Instance.stunnerPermanentBought[2])
        {
            stunTime = float.PositiveInfinity;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range +2);
    }

}
