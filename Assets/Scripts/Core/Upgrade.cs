using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Upgrade", menuName ="Upgrade")]
public class Upgrade : ScriptableObject
{
    public string upgradeName = "First Upgrade";
    public string upgradeDesc = "Upgrades something fancy";
    public float upgradeCost = 100f;
}
