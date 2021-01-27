using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnerUpgrade : TurretUpgradePanels
{
    public void FirstSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade
            .projectilePrefab.GetComponent<Projectile>().stunPower *= 1.4f;
            LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.slotLevel[0]++;
            LevelManager.Instance.AdjustGold(-upgradeCost);
            LevelManager.Instance.sceneData.turretUpgradePanel.SetActive(false);
        }
    }

    public void SecondSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade
            .projectilePrefab.GetComponent<Projectile>().stunTime *= 1.5f;
            LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.slotLevel[1]++;
            LevelManager.Instance.AdjustGold(-upgradeCost);
            LevelManager.Instance.sceneData.turretUpgradePanel.SetActive(false);
        }
    }

    public void ThirdSlot()
    {
        if (LevelManager.Instance.GetGold() >= upgradeCost)
        {
            LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.range *= 1.5f;
            LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.slotLevel[2]++;
            LevelManager.Instance.AdjustGold(-upgradeCost);
            LevelManager.Instance.sceneData.turretUpgradePanel.SetActive(false);
        }
    }
}
