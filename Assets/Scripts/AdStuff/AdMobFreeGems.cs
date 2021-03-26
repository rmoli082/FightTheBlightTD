using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class AdMobFreeGems : MonoBehaviour
{
    [Header("Not a test ad")]
    public string gemAdUnitId = "ca-app-pub-6385360749297822/1556222074";

    public RewardedAd freeGemsAd;

    void Start()
    {
        this.freeGemsAd = CreateAndLoadRewardedAd(gemAdUnitId);
    }

    public void Update()
    {
        if (!this.freeGemsAd.IsLoaded())
            {
                gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                gameObject.GetComponent<Button>().interactable = true;
            }
    }

    private RewardedAd CreateAndLoadRewardedAd(string unitID)
    {
        RewardedAd rewardAd = new RewardedAd(unitID);

        rewardAd.OnUserEarnedReward += ReceiveFreeGems;
        rewardAd.OnAdFailedToShow += HandleFailedAdLoad;

        RequestAd(rewardAd);

        return rewardAd;
    }

    private void RequestAd(RewardedAd rewardAd)
    {
        AdRequest request = new AdRequest.Builder().Build();
        rewardAd.LoadAd(request);
    }

    public void HandleFailedAdLoad(object sender, AdErrorEventArgs args)
    {
        Debug.Log(args.Message);
    }

    public void ShowGemsAd()
    {
        if (this.freeGemsAd.IsLoaded())
        {
            Debug.Log("Show Gems Ad");
            this.freeGemsAd.Show();
        }
        else
        {
            Debug.Log("Ad not loaded");
        }
    }

    public void ReceiveFreeGems(object sender, Reward args)
    {
        Debug.Log($"Receive free gems");
        Player.Instance.AdjustGems(25);

        RequestAd((RewardedAd)sender);
    }

}
