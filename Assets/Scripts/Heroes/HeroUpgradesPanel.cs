using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroUpgradesPanel : MonoBehaviour
{
    public GameObject firstBoost;
    public GameObject secondBoost;
    public GameObject thirdBoost;

    public GameObject firstSlot;
    public GameObject secondSlot;
    public GameObject thirdSlot;

    public readonly string message = "Level up your hero to unlock!";

    public void PopulateHeroUpgrades()
    {
        if (HeroManager.Instance.isFirstUpgradeActive)
        {
            ActivateSlot(firstBoost, firstSlot, 0);
        }
        else
        {
            LockSlot(firstSlot, message + " (Lvl 5)");
        }

        if (HeroManager.Instance.isSecondUpgradeActive)
        {
            ActivateSlot(secondBoost, secondSlot, 1);
        }
        else
        {
            LockSlot(secondSlot, message + " (Lvl 10)");
        }

        if (HeroManager.Instance.isThirdUpgradeActive)
        {
            ActivateSlot(thirdBoost, thirdSlot, 2);
        }
        else
        {
            LockSlot(thirdSlot, message + " (Lvl 20)");
        }

    }

    private void ActivateSlot(GameObject boost, GameObject slot, int upgradeNumber)
    {
        slot.SetActive(true);
        if (HeroManager.Instance.activeUpgrades[upgradeNumber] != null)
        {
            boost.AddComponent(HeroManager.Instance.activeUpgrades[upgradeNumber].GetType());
            slot.GetComponent<HeroUpgradeSlot>().FillInBoost(boost.GetComponent<HeroUpgrade>());
        }
        else
        {
            slot.GetComponent<HeroUpgradeSlot>().ActivateBoostButton(true);
        }
    }

    private void LockSlot(GameObject slot, string _message)
    {
        slot.GetComponent<HeroUpgradeSlot>().titleText.text = _message;
        slot.GetComponent<HeroUpgradeSlot>().titleText.gameObject.SetActive(true);
    }

}
