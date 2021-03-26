using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class AdMobInterstitialManager : MonoBehaviour
{
    public string placementID = "ca-app-pub-3940256099942544/1033173712";

    private InterstitialAd interstitialAd;

    // Start is called before the first frame update
    private void Start()
    {
        this.interstitialAd = new InterstitialAd(placementID);
        this.interstitialAd.OnAdFailedToLoad += HandleFailedAd;
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitialAd.LoadAd(request);
    }

    private void Update()
    {
        if (this.interstitialAd.IsLoaded())
        {
            this.gameObject.GetComponent<Button>().interactable = true;
            if (SceneManager.GetActiveScene().name.Equals("LevelSelect"))
            {
                LevelSelector.CheckForAvail();
            }

        }
        else
        {
            this.gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void LoadLevel(string levelToLoad)
    {
        PlayGames.IncrementAchievement(GPGSIds.achievement_play_10_games, 1);
        PlayGames.IncrementAchievement(GPGSIds.achievement_play_100_games, 1);
        LoadScene(levelToLoad);
    }

    public void LoadMenuScene(string levelToLoad)
    {
        LoadScene(levelToLoad);
    }

    private void LoadScene(string loadLevel)
    {
        if (this.interstitialAd.IsLoaded())
        {
            Debug.Log("Ad shown");
            this.interstitialAd.Show();
            GameManager.Instance.LoadScene(loadLevel);
        }
        else
        {
            Debug.Log("Ad not shown");
            GameManager.Instance.LoadScene(loadLevel);
        }
        
    }

    private void HandleFailedAd(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log(args.Message);
    }

    private void OnDestroy()
    {
        this.interstitialAd.Destroy();
    }

}
