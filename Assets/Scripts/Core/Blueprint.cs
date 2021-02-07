using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Blueprint 
{
    [Header("Build Details")]
    public GameObject prefab;
    public int turretCost;

    [Header("Button Details")]
    public string turretName;
    public string turretDescription;
    public Sprite turretImage;
}
