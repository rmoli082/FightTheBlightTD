using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretUpgradePanels : MonoBehaviour
{
    public TextMeshProUGUI costSlot;
    public int upgradeCost;
    protected UpgradePanel upgradePanel;

    protected virtual void OnEnable()
    {
        upgradePanel = LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>();
    }

    protected void PurchaseUpgrade(int slotNumber)
    {
        upgradePanel.turretToUpgrade.slotLevel[slotNumber]++;
        upgradePanel.turretToUpgrade.SellCost += (upgradeCost / 2);
        LevelManager.Instance.AdjustGold(-upgradeCost);
        LevelManager.Instance.sceneData.turretUpgradePanel.SetActive(false);
        upgradePanel.turretToUpgrade.Glow(false);
    }
}
