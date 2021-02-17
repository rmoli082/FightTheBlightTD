using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Turret : Placeable
{
    [Header("Turret Info")]
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

    [Header("Upgrade Slots")]
    public Upgrade[] turretUpgrades = new Upgrade[3];
    public int[] slotLevel = { 0, 0, 0 };

    [Header("Upgrade Panels")]
    public GameObject upgradePanel;
    public GameObject[] upgradeSlotButtons = new GameObject[3];

    private void Awake()
    {
        target = null;
        upgradePanel = LevelManager.Instance.sceneData.turretUpgradePanel;
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

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            PopupUpgradePanel();
            LocationNode.GetComponent<Renderer>().material.color = LocationNode.hoverColor;
        }
    }

    public void PopupUpgradePanel()
    {
        LocationNode.GetComponent<Renderer>().material.color = LocationNode.hoverColor;
        PopulateUpgradePanel();
        upgradePanel.SetActive(!upgradePanel.activeSelf);
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

        if (TurretType == PlaceableType.stunner && (nearestEnemy == null 
            || nearestEnemy.GetComponent<EnemyController>().isStunned))
        {
            SetTarget(null);
        }
        else if (nearestEnemy != null && shortestDistance <= range)
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
        Destroy(bullet, 2f);
        
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
            projectile.SetTurret(this);
            projectile.Launch(target.position - firePoint.position, projectileForce);
        }
        
    }

   private void PopulateUpgradePanel()
    {
        upgradePanel.GetComponent<UpgradePanel>().SetTurret(this);
        int index = 0;

        foreach (Transform child in LevelManager.Instance.sceneData.turretButtonList)
        {
            if (child.GetComponent<Button>() != null)
                Destroy(child.gameObject);
        }

        foreach (GameObject button in upgradeSlotButtons)
        {
            if (index < upgradeSlotButtons.Length)
            {
                CreateButtons(button, index);
            }
            index++;
        }

       // upgradePanel.GetComponent<UpgradePanel>().PopulateData();
    }

    private GameObject CreateButtons(GameObject button, int index)
    {
        GameObject b = Instantiate(button);
        b.transform.SetParent(LevelManager.Instance.sceneData.turretButtonList);
        b.GetComponentInChildren<TurretUpgradePanels>().upgradeCost = (int)(turretUpgrades[index].upgradeCost + (slotLevel[index] * turretUpgrades[index].upgradeCost));
        b.GetComponentInChildren<TurretUpgradePanels>().costSlot.text = b.GetComponentInChildren<TurretUpgradePanels>().upgradeCost.ToString();
        return b;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range +2);
    }


}
