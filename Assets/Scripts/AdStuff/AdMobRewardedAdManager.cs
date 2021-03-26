using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class AdMobRewardedAdManager : MonoBehaviour
{
    [Header("Not a test ad")]
    public string gemAdUnitId = "ca-app-pub-6385360749297822/1556222074";
    public string replayAdUnitID = "ca-app-pub-6385360749297822/4520107396";

    public RewardedAd freeGemsAd;
    public RewardedAd freeReplayAd;

    public bool isGemAd;
    public bool isReplayAd;

    void Start()
    {
        this.freeGemsAd = CreateAndLoadRewardedAd(gemAdUnitId, ReceiveFreeGems);
        this.freeReplayAd = CreateAndLoadRewardedAd(replayAdUnitID, ReceiveFreeplay);
    }

    public void Update()
    {
        if (isGemAd)
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

        if (isReplayAd)
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
    }

    private RewardedAd CreateAndLoadRewardedAd(string unitID, EventHandler<Reward> rewardCallback)
    {
        RewardedAd rewardAd = new RewardedAd(unitID);

        rewardAd.OnUserEarnedReward += rewardCallback;
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

    public void ShowFreeplayAd()
    {
        if (this.freeReplayAd.IsLoaded())
        {
            Debug.Log("Show freeplay Ad");
            this.freeReplayAd.Show();
        }
    }

    public void ReceiveFreeGems(object sender, Reward args)
    {
        Debug.Log($"Receive free gems");
        Player.Instance.AdjustGems(25);
    }

    public void ReceiveFreeplay(object sender, Reward args)
    {
        Debug.Log($"Freeplay");
        LevelManager.Instance.sceneData.losePanel.GetComponent<LosePanel>().ContinueAdWatch();
    }

}
