using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class AdMobFreeplay : MonoBehaviour
{
    [Header("Not a test ad")]
    public string replayAdUnitID = "ca-app-pub-6385360749297822/4520107396";

    public RewardedAd freeReplayAd;


    void Start()
    {
        this.freeReplayAd = CreateAndLoadRewardedAd(replayAdUnitID);
    }

    public void Update()
    {
        if (!this.freeReplayAd.IsLoaded())
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

        rewardAd.OnUserEarnedReward += ReceiveFreeplay;
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

    public void ShowFreeplayAd()
    {
        if (this.freeReplayAd.IsLoaded())
        {
            Debug.Log("Show freeplay Ad");
            this.freeReplayAd.Show();
        }
    }

    public void ReceiveFreeplay(object sender, Reward args)
    {
        Debug.Log($"Freeplay");
        LevelManager.Instance.sceneData.losePanel.GetComponent<LosePanel>().ContinueAdWatch();

        RequestAd((RewardedAd)sender);
    }

}
