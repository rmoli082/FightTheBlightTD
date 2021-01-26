using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneData : MonoBehaviour
{
    [Header("Store stuff")]
    public Transform shopListing;

    [Header("Level Decorations")]
    public GameObject plane;
    public GameObject nodes;

    [Header("Upgrade Panels")]
    public GameObject turretUpgradePanel;
    public Transform turretButtonList;
    public GameObject heroUpgradePanel;

    [Header("Player Stats")]
    public TextMeshProUGUI playerLives;
    public TextMeshProUGUI playerGold;
}
