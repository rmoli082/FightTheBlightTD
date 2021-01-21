using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            node.currentTurret = turret.GetComponent<Placeable>();
            LevelManager.Instance.AdjustGold(-selectedTurret.turretCost);
        }
        else
        {
            return;
        }
       
    }
}
