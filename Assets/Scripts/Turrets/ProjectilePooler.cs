using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProjectilePooler
{
    public static void PoolProjectiles(Turret turret)
    {
        for (int i = 0; i < turret.amountToPool; i++)
        {
            ReferenceManager.Instance.Instantiate(turret.projectile, turret.transform.position, turret.transform.rotation, turret.transform)
                .Completed += reference =>
                {
                    reference.Result.gameObject.SetActive(false);
                    turret.pooledProjectiles.Add(reference.Result);
                };
        }
    }

    public static GameObject GetProjectile(Turret turret)
    {

        for (int i = 0; i < turret.pooledProjectiles.Count; i++)
        {
            if (!turret.pooledProjectiles[i].activeInHierarchy)
            {
                turret.pooledProjectiles[i].GetComponent<Collider>().enabled = true;
                turret.pooledProjectiles[i].transform.position = turret.firePoint.position;
                turret.pooledProjectiles[i].transform.rotation = Quaternion.LookRotation(turret.firePoint.position - turret.target.position);
                return turret.pooledProjectiles[i];
            }
        }

        GameObject tmp = new GameObject();
        ReferenceManager.Instance.Instantiate(turret.projectile, turret.transform.position, turret.transform.rotation, turret.transform)
                .Completed += reference =>
                {
                    turret.pooledProjectiles.Add(reference.Result);
                    tmp = reference.Result;
                };

        return tmp;
    }
}
