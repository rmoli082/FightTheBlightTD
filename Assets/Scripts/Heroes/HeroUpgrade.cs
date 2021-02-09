using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroUpgrade : MonoBehaviour
{
    [SerializeField]
    private string upgradeName;
    public string UpgradeName { get => upgradeName; set => upgradeName = value; }
}
