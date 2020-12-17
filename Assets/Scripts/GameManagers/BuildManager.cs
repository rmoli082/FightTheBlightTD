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
        GameObject turret = Instantiate(selectedTurret.prefab, node.transform.position, Quaternion.identity);
        turret.GetComponent<Placeable>().LocationNode = node;
        node.currentTurret = turret.GetComponent<Placeable>();
    }
}
