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
            button.interactable = true;
        }
        else if (this.interstitialAd.IsLoaded() && isLocked)
        {
            if (PlayerPrefs.GetInt($"{levelToUnlock} Complete") == 1)
            {
                button.interactable = true;
                button.GetComponentInChildren<TextMeshProUGUI>().color = new Color(50, 50, 50, 1f);
            }
            else
            {
                button.interactable = false;
            }
        }
        else 
        {
            button.interactable = false;
        }
    }

    public void LoadScene()
    {
        if (this.interstitialAd.IsLoaded())
        {
            Debug.Log("Ad shown");
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