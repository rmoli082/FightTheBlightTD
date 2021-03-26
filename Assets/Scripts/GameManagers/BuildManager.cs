using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class BuildManager : Singleton<BuildManager>
{
    public Blueprint selectedTurret;

    protected override void Awake()
    {
        base.Awake();

        selectedTurret = null;
    }

    public void SelectPlaceable(Blueprint blueprint)
    {
        selectedTurret = blueprint;
    }

    public void BuildSelectedPlaceable(Node node)
    {
        if (LevelManager.Instance.GetGold() >= selectedTurret.turretCost)
        {
            GameObject turret = Instantiate(selectedTurret.prefab, node.transform.position, Quaternion.identity);
            turret.GetComponent<Placeable>().LocationNode = node;
            turret.GetComponent<Placeable>().SellCost = selectedTurret.turretCost / 2;
            node.currentTurret = turret.GetComponent<Placeable>();
            node.gameObject.GetComponent<Collider>().enabled = false;
            LevelManager.Instance.AdjustGold(-selectedTurret.turretCost);
            Analytics.CustomEvent("TurretPurchase", new Dictionary<string, object>
            {
                {"Level", SceneManager.GetActiveScene().name },
                {"Wave", WaveSpawner.Instance.waveNumber },
                {"Turret Bought", $"{turret.GetComponent<Placeable>().TurretType}"}
            });
            if (turret.GetComponent<Hero>())
            {
                HeroManager.Instance.isSpawned = true;
                PlayGames.IncrementAchievement(GPGSIds.achievement_heroic_actions, 1);
            }
            selectedTurret = null;
            
        }
        else
        {
            return;
        }
       
    }
}
