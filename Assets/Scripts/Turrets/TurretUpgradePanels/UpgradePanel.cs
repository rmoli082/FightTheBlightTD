using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradePanel : MonoBehaviour
{
    public Turret turretToUpgrade;
    public Transform contentListing;
    public TextMeshProUGUI sellText;

    private void OnEnable()
    {
        sellText.text = $"Sell for {turretToUpgrade.SellCost}";
    }

    public void SetTurret(Turret _turret)
    {
        turretToUpgrade = _turret;
    }

    public void SellTurret()
    {
        LevelManager.Instance.AdjustGold(turretToUpgrade.SellCost);
        turretToUpgrade.LocationNode.GetComponent<Collider>().enabled = true;
        turretToUpgrade.LocationNode.GetComponent<Node>().currentTurret = null;
        Destroy(turretToUpgrade.gameObject);
        LevelManager.Instance.sceneData.turretUpgradePanel.SetActive(false);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        turretToUpgrade.Glow(false);
    }
}
