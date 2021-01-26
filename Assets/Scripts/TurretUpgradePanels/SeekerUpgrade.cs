using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerUpgrade : MonoBehaviour
{
    public void FirstSlot()
    {
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.projectileForce *= 1.4f;
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.slotOneLevel++;
    }

    public void SecondSlot()
    {
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.fireRate *= 1.5f;
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.slotTwoLevel++;
    }

    public void ThirdSlot()
    {
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.DamageAmount *= 1.5f;
        LevelManager.Instance.sceneData.turretUpgradePanel.GetComponent<UpgradePanel>().turretToUpgrade.slotThreeLevel++;
    }
}
