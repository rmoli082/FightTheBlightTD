using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerPanelButton : MonoBehaviour
{
    public GameObject boost;
    public int gemCost;
    public GameObject messageBox;

    public void BuyBoost()
    {
        if (Player.Instance.GetGems() >= gemCost)
        {
            Player.Instance.AddBoost(boost);
            Player.Instance.AdjustGems(-gemCost);
            StartCoroutine(PopMessageBox(boost.GetComponent<HeroUpgrade>().UpgradeName));
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

    private IEnumerator PopMessageBox(string boostName)
    {
        messageBox.GetComponentInChildren<TextMeshProUGUI>().text = $"{boostName} purchased!";
        messageBox.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        messageBox.SetActive(false);
    }
}
