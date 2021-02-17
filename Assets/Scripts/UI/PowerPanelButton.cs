using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerPanelButton : MonoBehaviour
{
    public GameObject boost;
    public int gemCost;

    public void BuyBoost()
    {
        if (Player.Instance.GetGems() >= gemCost)
        {
            Player.Instance.AddBoost(boost);
            Player.Instance.AdjustGems(-gemCost);
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }
}
