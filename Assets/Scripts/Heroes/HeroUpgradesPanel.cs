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

    public GameObject boostSelectorPanel;
    public GameObject boostButtonPrefab;

    public readonly string message = "Level up your hero to unlock!";

    public void PopulateHeroUpgrades()
    {
        if (HeroManager.Instance.isFirstUpgradeActive)
        {
            ActivateSlot(ref firstBoost, ref firstSlot, 0);
        }
        else
        {
            LockSlot(ref firstSlot);
        }

        if (HeroManager.Instance.isSecondUpgradeActive)
        {
            ActivateSlot(ref secondBoost, ref secondSlot, 1);
        }
        else
        {
            LockSlot(ref secondSlot);
        }

        if (HeroManager.Instance.isThirdUpgradeActive)
        {
            ActivateSlot(ref thirdBoost, ref thirdSlot, 2);
        }
        else
        {
            LockSlot(ref thirdSlot);
        }

    }

    private void ActivateSlot(ref GameObject boost, ref GameObject slot, int upgradeNumber)
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

    private void LockSlot(ref GameObject slot)
    {
        slot.GetComponent<HeroUpgradeSlot>().titleText.text = message;
        slot.GetComponent<HeroUpgradeSlot>().titleText.gameObject.SetActive(true);
    }

    private void SelectFirstBoost(string _boost)
    {
        foreach (HeroUpgrade b in HeroManager.Instance.availableBoosts)
        {
            if (b.UpgradeName.Equals(_boost))
            {
                SetFirstBoost(b, _boost);
                HeroManager.Instance.activeUpgrades[0] = b.GetComponent<HeroUpgrade>();
            }
        }

        firstSlot.GetComponent<HeroUpgradeSlot>().ActivateBoostButton(false);
        firstSlot.GetComponent<HeroUpgradeSlot>().FillInBoost(firstBoost.GetComponent<HeroUpgrade>());
        boostSelectorPanel.SetActive(false);
        Player.Instance.ApplyBoost(_boost);

        GameEvents.OnSaveInitiated();
    }

    public void PopFirstBoostSelector()
    {
        boostSelectorPanel.SetActive(true);
        foreach (string boost in Player.Instance.GetBoosts())
        {
            GameObject b = Instantiate(boostButtonPrefab, boostSelectorPanel.transform);
            Button button = b.GetComponent<Button>();
            b.GetComponentInChildren<TextMeshProUGUI>().text = boost;

            button.onClick.AddListener(() => SelectFirstBoost(boost));
        }
    }

    private void SetFirstBoost(HeroUpgrade boost, string name)
    {
        firstBoost.AddComponent(boost.GetType());
        firstBoost.GetComponent<HeroUpgrade>().UpgradeName = name;
    }

    private void SelectSecondBoost(string _boost)
    {
        foreach (HeroUpgrade b in HeroManager.Instance.availableBoosts)
        {
            if (b.UpgradeName.Equals(_boost))
            {
                SetSecondBoost(b, _boost);
                HeroManager.Instance.activeUpgrades[1] = b.GetComponent<HeroUpgrade>();
            }
        }

        secondSlot.GetComponent<HeroUpgradeSlot>().ActivateBoostButton(false);
        secondSlot.GetComponent<HeroUpgradeSlot>().FillInBoost(secondBoost.GetComponent<HeroUpgrade>());
        boostSelectorPanel.SetActive(false);
        Player.Instance.ApplyBoost(_boost);

        GameEvents.OnSaveInitiated();
    }

    public void PopSecondBoostSelector()
    {
        boostSelectorPanel.SetActive(true);
        foreach (string boost in Player.Instance.GetBoosts())
        {
            GameObject b = Instantiate(boostButtonPrefab, boostSelectorPanel.transform);
            Button button = b.GetComponent<Button>();
            b.GetComponentInChildren<TextMeshProUGUI>().text = boost;

            button.onClick.AddListener(() => SelectSecondBoost(boost));
        }
    }

    private void SetSecondBoost(HeroUpgrade boost, string name)
    {
        secondBoost.AddComponent(boost.GetType());
        secondBoost.GetComponent<HeroUpgrade>().UpgradeName = name;
    }

    private void SelectThirdBoost(string _boost)
    {
        foreach (HeroUpgrade b in HeroManager.Instance.availableBoosts)
        {
            if (b.UpgradeName.Equals(_boost))
            {
                SetThirdBoost(b, _boost);
                HeroManager.Instance.activeUpgrades[2] = b.GetComponent<HeroUpgrade>();
            }
        }

        thirdSlot.GetComponent<HeroUpgradeSlot>().ActivateBoostButton(false);
        thirdSlot.GetComponent<HeroUpgradeSlot>().FillInBoost(thirdBoost.GetComponent<HeroUpgrade>());
        boostSelectorPanel.SetActive(false);
        Player.Instance.ApplyBoost(_boost);

        GameEvents.OnSaveInitiated();
    }

    public void PopThirdBoostSelector()
    {
        boostSelectorPanel.SetActive(true);
        foreach (string boost in Player.Instance.GetBoosts())
        {
            GameObject b = Instantiate(boostButtonPrefab, boostSelectorPanel.transform);
            Button button = b.GetComponent<Button>();
            b.GetComponentInChildren<TextMeshProUGUI>().text = boost;

            button.onClick.AddListener(() => SelectThirdBoost(boost));
        }
    }

    private void SetThirdBoost(HeroUpgrade boost, string name)
    {
        thirdBoost.AddComponent(boost.GetType());
        thirdBoost.GetComponent<HeroUpgrade>().UpgradeName = name;
    }


}
