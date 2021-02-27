using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
    public static ProjectilePooler SharedInstance;
    
    public List<GameObject> pooledStandardProjectiles;
    public GameObject standardProjectilePrefab;
    public List<GameObject> pooledRapidProjectiles;
    public GameObject rapidProjectilePrefab;
    public List<GameObject> pooledBomberProjectiles;
    public GameObject bomberProjectilePrefab;
    public List<GameObject> pooledSeekerProjectiles;
    public GameObject seekerProjectilePrefab;
    public List<GameObject> pooledStunnerProjectiles;
    public GameObject stunnerProjectilePrefab;

    public int amountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledStandardProjectiles = new List<GameObject>();
        pooledRapidProjectiles = new List<GameObject>();
        pooledBomberProjectiles = new List<GameObject>();
        pooledSeekerProjectiles = new List<GameObject>();
        pooledStunnerProjectiles = new List<GameObject>();

        PoolObjects();
    }

    public GameObject GetPooledObject(PlaceableType type)
    {
        switch(type)
        {
            case PlaceableType.turret:
                for (int i = 0; i < amountToPool; i++)
                {
                    if (!pooledStandardProjectiles[i].activeInHierarchy)
                    {
                        return pooledStandardProjectiles[i];
                    }
                }
                break;
            case PlaceableType.rapid:
                for (int i = 0; i < amountToPool; i++)
                {
                    if (!pooledRapidProjectiles[i].activeInHierarchy)
                    {
                        return pooledRapidProjectiles[i];
                    }
                }
                break;
            case PlaceableType.bomber:
                for (int i = 0; i < amountToPool; i++)
                {
                    if (!pooledBomberProjectiles[i].activeInHierarchy)
                    {
                        return pooledBomberProjectiles[i];
                    }
                }
                break;
            case PlaceableType.seeker:
                for (int i = 0; i < amountToPool; i++)
                {
                    if (!pooledSeekerProjectiles[i].activeInHierarchy)
                    {
                        return pooledSeekerProjectiles[i];
                    }
                }
                break;
            case PlaceableType.stunner:
                for (int i = 0; i < amountToPool; i++)
                {
                    if (!pooledStunnerProjectiles[i].activeInHierarchy)
                    {
                        return pooledStunnerProjectiles[i];
                    }
                }
                break;
        }

        return null;
    }

    private void PoolObjects()
    {
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(standardProjectilePrefab);
            tmp.SetActive(false);
            pooledStandardProjectiles.Add(tmp);
        }
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(rapidProjectilePrefab);
            tmp.SetActive(false);
            pooledRapidProjectiles.Add(tmp);
        }
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(bomberProjectilePrefab);
            tmp.SetActive(false);
            pooledBomberProjectiles.Add(tmp);
        }
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(seekerProjectilePrefab);
            tmp.SetActive(false);
            pooledSeekerProjectiles.Add(tmp);
        }
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(stunnerProjectilePrefab);
            tmp.SetActive(false);
            pooledStunnerProjectiles.Add(tmp);
        }
    }
}
