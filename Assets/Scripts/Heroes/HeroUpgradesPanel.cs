using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroUpgradesPanel : MonoBehaviour
{
    public HeroUpgrade firstBoost;
    public HeroUpgrade secondBoost;
    public HeroUpgrade thirdBoost;

    public GameObject firstSlot;
    public GameObject secondSlot;
    public GameObject thirdSlot;

    private void Awake()
    {
        if (HeroManager.Instance.isFirstUpgradeActive)
        {
            firstSlot.SetActive(true);
            if (HeroManager.Instance.GetHero().heroUpgrades[0] != null)
            {
                firstBoost = HeroManager.Instance.GetHero().heroUpgrades[0];
                firstSlot.GetComponent<HeroUpgradeSlot>().FillInBoost(firstBoost);
            }
            else
            {
                firstSlot.GetComponent<HeroUpgradeSlot>().ActivateBoostButton();
            }
        }

        if (HeroManager.Instance.isSecondUpgradeActive)
        {
            secondSlot.SetActive(true);
            if (HeroManager.Instance.GetHero().heroUpgrades[1] != null)
            {
                secondBoost = HeroManager.Instance.GetHero().heroUpgrades[1];
                secondSlot.GetComponent<HeroUpgradeSlot>().FillInBoost(secondBoost);
            }
            else
            {
                secondSlot.GetComponent<HeroUpgradeSlot>().ActivateBoostButton();
            }
        }

        if (HeroManager.Instance.isThirdUpgradeActive)
        {
            thirdSlot.SetActive(true);
            if (HeroManager.Instance.GetHero().heroUpgrades[2] != null)
            {
                thirdBoost = HeroManager.Instance.GetHero().heroUpgrades[2];
                thirdSlot.GetComponent<HeroUpgradeSlot>().FillInBoost(thirdBoost);
            }
            else
            {
                thirdSlot.GetComponent<HeroUpgradeSlot>().ActivateBoostButton();
            }
        }

    }
}
