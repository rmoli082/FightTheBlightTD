using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroUpgradeSlot : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public GameObject boostButton;

    public void FillInBoost(HeroUpgrade boost)
    {
        titleText.gameObject.SetActive(true);
        titleText.text = $"{boost.UpgradeName} ACTIVATED";
    }

    public void ActivateBoostButton(bool status)
    {
        boostButton.SetActive(status);
    }
}
