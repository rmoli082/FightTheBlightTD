using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class RewardButton : MonoBehaviour
{
    private string dateCheckUrl = "https://www.morashstudios.com/getdate.php";
    private string currentDate;
    private string currentTime;
    private bool timerReady;
    private bool timerComplete;
    public Button rewardButton;
    public TextMeshProUGUI timerTag;
    public GameObject freeText;
    public Animator animator;

    TimeSpan timeRemaining;
    private float timerValu;

    private void OnEnable()
    {
        StartCoroutine(CheckTimeRemaining());
    }

    private void Update()
    {
        if (timerReady)
        {
            if (!timerComplete)
            {
                timerValu -= Time.deltaTime * 1;

                if (timerValu <= 0)
                {
                    if (ValidateTime())
                    {
                        timerComplete = true;
                        EnableButton();
                    }
                    
                }

                TimeSpan time = TimeSpan.FromSeconds(timerValu);
                timerTag.text = $"Next Reward in : { time.ToString(@"hh\:mm\:ss")}";
            }
        }
    }

    private IEnumerator GetDate()
    {
        UnityWebRequest www = UnityWebRequest.Get(dateCheckUrl);

        yield return www.SendWebRequest();

        if (www.error != null)
        {
            Debug.Log(www.error);
        }

        string[] tokens = www.downloadHandler.text.Split('/');

        currentDate = tokens[0];
        currentTime = tokens[1];
    }

    private IEnumerator CheckTimeRemaining()
    {
        //disable button

        yield return GetDate();

        UpdateTime();

    }

    private bool ValidateTime()
    {
        StartCoroutine(CheckTimeRemaining());

        string oldDate = PlayerPrefs.GetString("Reward Date");

        if (DateTime.ParseExact(currentDate, "MM-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture)
                > DateTime.ParseExact(oldDate, "MM-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void UpdateTime()
    {
        string oldDate = PlayerPrefs.GetString("Reward Date");

        if(oldDate.Equals(String.Empty))
        {
            EnableButton();
            return;
        }

        if (DateTime.ParseExact(currentDate, "MM-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture) 
                > DateTime.ParseExact(oldDate, "MM-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture))
        {
            EnableButton();
        }
        else
        {
            DisableButton();
            ConfigTimer();
        }
    }

    private void DisableButton() 
    {
        rewardButton.interactable = false;
        freeText.SetActive(false);
    }
    private void EnableButton() 
    {
        timerTag.text = "CLAIM REWARD!";
        rewardButton.interactable = true;
        freeText.SetActive(true);
    }

    public void ClaimReward()
    {
        Player.Instance.AdjustGems(10);
        PlayerPrefs.SetString("Reward Date", currentDate);
        DisableButton();
        animator.enabled = true;
        ConfigTimer();
    }

    private void ConfigTimer()
    {
        TimeSpan endTime = TimeSpan.Parse("23:59:59");
        TimeSpan temp = TimeSpan.Parse(currentTime);
        timeRemaining = endTime.Subtract(temp);

        timerReady = true;
        timerValu = (float)timeRemaining.TotalSeconds;
    }
}
