using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : Singleton<BuildManager>
{
    public GameObject selectedTurret;

    protected override void Awake()
    {
        base.Awake();

        selectedTurret = null;
    }

    public void BuildSelectedTurret(Node node)
    {
        GameObject turret = Instantiate(selectedTurret, node.transform.position, Quaternion.identity);
        turret.GetComponent<Placeable>().LocationNode = node;
        node.currentTurret = turret.GetComponent<Placeable>();
    }
}
