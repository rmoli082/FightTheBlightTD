using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUpgrade : MonoBehaviour
{
   public void FirstSlot()
    {
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.DamageAmount *= 1.4f;
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.slotOneLevel++;
    }

    public void SecondSlot()
    {
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.fireRate *= 1.5f;
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.slotTwoLevel++;
    }

    public void ThirdSlot()
    {
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.range *= 1.5f;
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.slotThreeLevel++;
    }

}
