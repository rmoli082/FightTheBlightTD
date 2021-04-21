using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;
using TMPro;

public class LevelSelectInterstitial : MonoBehaviour
{
    public string placementID = "ca-app-pub-3940256099942544/1033173712";
    public string levelToLoad;
    public bool isLocked;
    public string levelToUnlock;
    public Button button;

    private InterstitialAd interstitialAd;

    private void Start()
    {
        this.interstitialAd = new InterstitialAd(placementID);
        this.interstitialAd.OnAdFailedToLoad += HandleFailedAd;
        this.interstitialAd.OnAdClosed += HandleClosedAd;
        RequestAd(this.interstitialAd);
    }

    private void Update()
    {
        if (this.interstitialAd.IsLoaded() && !isLocked)
        {
            this.button.interactable = true;
        }
        else if (this.interstitialAd.IsLoaded() && isLocked)
        {
            if (PlayerPrefs.GetInt($"{levelToUnlock} Complete") == 1)
            {
                this.button.interactable = true;
                this.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                this.isLocked = false;
            }
            else
            {
                this.button.interactable = false;
            }
        }
        else 
        {
            this.button.interactable = false;
        }
    }

    public void LoadScene()
    {
        if (this.interstitialAd.IsLoaded())
        {
            this.interstitialAd.Show();
            GameManager.Instance.LoadScene(levelToLoad);
        }
    }

    private void RequestAd(InterstitialAd ad)
    {
        AdRequest request = new AdRequest.Builder().Build();
        ad.LoadAd(request);
    }

    private void HandleFailedAd(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log(args.Message);
    }

    private void HandleClosedAd(object sender, EventArgs args)
    {
        RequestAd((InterstitialAd)sender);
    }
}
