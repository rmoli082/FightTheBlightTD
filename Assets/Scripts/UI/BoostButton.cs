using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoostButton : MonoBehaviour
{
    public Button button;
    public GameObject boostButtonPrefab;
    public GameObject boostSelectorPanel;
    public GameObject boost;
    public GameObject slot;
    public int slotNumber;

    public void OnEnable()
    {
        button.onClick.AddListener(() => PopBoostSelector(boost, slot));
    }

    public void PopBoostSelector(GameObject boost, GameObject slot)
    {
        boostSelectorPanel.transform.parent.gameObject.SetActive(true);

        foreach(string s in Player.Instance.GetBoosts())
        {
            Debug.Log(s);
        }

        foreach (string boostString in Player.Instance.GetBoosts())
        {
            GameObject b = Instantiate(boostButtonPrefab, boostSelectorPanel.transform);
            Button button = b.GetComponent<Button>();
            b.GetComponentInChildren<TextMeshProUGUI>().text = boostString;

            button.onClick.AddListener(() => SelectBoost(boost, slot, boostString, slotNumber));
        }
    }

    private void SelectBoost(GameObject boost, GameObject slot, string _boost, int _index)
    {
        foreach (HeroUpgrade b in HeroManager.Instance.availableBoosts)
        {
            if (b.UpgradeName.Equals(_boost))
            {
                SetBoost(boost, b, _boost);
                HeroManager.Instance.activeUpgrades[_index] = b.GetComponent<HeroUpgrade>();
            }
        }

        slot.GetComponent<HeroUpgradeSlot>().ActivateBoostButton(false);
        slot.GetComponent<HeroUpgradeSlot>().FillInBoost(boost.GetComponent<HeroUpgrade>());
        boostSelectorPanel.transform.parent.gameObject.SetActive(false);
        Player.Instance.ApplyBoost(_boost);

        GameEvents.OnSaveInitiated();
    }

    private void SetBoost(GameObject boostSlot, HeroUpgrade boostType, string name)
    {
        boostSlot.AddComponent(boostType.GetType());
        boostSlot.GetComponent<HeroUpgrade>().UpgradeName = name;
    }
}
