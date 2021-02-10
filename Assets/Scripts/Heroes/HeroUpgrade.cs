using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class HeroUpgrade : MonoBehaviour
{
    [SerializeField]
    private string upgradeName;
    public string UpgradeName { get => upgradeName; set => upgradeName = value; }

    public bool isActivated = false;

}
