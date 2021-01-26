using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberUpgrade : TurretUpgradePanels
{
    public void FirstSlot()
    {
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.DamageAmount *= 1.4f;
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.slotLevel[0]++;
        LevelManager.Instance.sceneData.turretUpgradePanel.SetActive(false);
    }

    public void SecondSlot()
    {
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.projectilePrefab
            .GetComponent<Projectile>().explodeRange *= 1.5f;
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.slotLevel[1]++;
        LevelManager.Instance.sceneData.turretUpgradePanel.SetActive(false);
    }

    public void ThirdSlot()
    {
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.range *= 1.5f;
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.slotLevel[2]++;
        LevelManager.Instance.sceneData.turretUpgradePanel.SetActive(false);
    }
}
