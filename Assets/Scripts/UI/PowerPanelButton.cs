using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPanelButton : MonoBehaviour
{
    public GameObject boost;
    public int gemCost;

    public void BuyBoost()
    {
        Player.Instance.AddBoost(boost);
        Player.Instance.AdjustGems(-gemCost);
    }
}
