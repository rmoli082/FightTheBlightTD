using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public void PopupUpgradePanel(Turret turret)
    {
        if (gameObject.activeSelf)
            return;

        PopulateUpgradePanel(turret);
        gameObject.SetActive(true);
        turret.Glow(true);
    }

    private void PopulateUpgradePanel(Turret turret)
    {
        SetTurret(turret);
        int index = 0;

        foreach (Transform child in LevelManager.Instance.sceneData.turretButtonList)
        {
            if (child.GetComponent<Button>() != null)
                Destroy(child.gameObject);
        }

        foreach (GameObject button in turret.upgradeSlotButtons)
        {
            if (index < turret.upgradeSlotButtons.Length)
            {
                CreateButtons(turret, button, index);
            }
            index++;
        }
    }

    private GameObject CreateButtons(Turret turret, GameObject button, int index)
    {
        GameObject b = Instantiate(button);
        TurretUpgradePanels uPanel = b.GetComponentInChildren<TurretUpgradePanels>();
        b.transform.SetParent(LevelManager.Instance.sceneData.turretButtonList);
        uPanel.upgradeCost = (int)((((turret.slotLevel[index] * (turret.slotLevel[index] + 1)) / 2) * turret.turretUpgrades[index].upgradeCost)
            + turret.turretUpgrades[index].upgradeCost);
        if (turret.slotLevel[index] >= 5 || turret.slotLevel[0] + turret.slotLevel[1] + turret.slotLevel[2] >= 12)
        {
            uPanel.costSlot.text = "MAX";
            b.GetComponent<Button>().interactable = false;
        }
        else
        {
            uPanel.costSlot.text = uPanel.upgradeCost.ToString();
        }

        return b;
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        turretToUpgrade.Glow(false);
    }
}
