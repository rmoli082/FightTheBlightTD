using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{

    public GameObject[] tutorialPopups;
    public GameObject exitCardPopup;
    public int tutorialIndex=0;
    
    public void StartTutorial()
    {
        if (!GameManager.Instance.IsPaused)
            GameManager.Instance.PausePlay();
        tutorialPopups[0].SetActive(true);
    }

    public void NextCard()
    {
        tutorialPopups[tutorialIndex].SetActive(false);
        tutorialIndex++;
        if (tutorialIndex >= tutorialPopups.Length)
        {
            exitCardPopup.SetActive(true);
        }
        else
        {
            tutorialPopups[tutorialIndex].SetActive(true);
        }
    }

    public void PreviousCard()
    {
        tutorialPopups[tutorialIndex].SetActive(false);
        tutorialIndex--;
        tutorialPopups[tutorialIndex].SetActive(true);
    }

    public void QuitTutorial()
    {
        tutorialPopups[tutorialIndex].SetActive(false);
        exitCardPopup.SetActive(true);
    }

    public void StartGame()
    {
        exitCardPopup.SetActive(false);
        PlayerPrefs.SetInt("FirstRunComplete", 1);
        GameManager.Instance.PausePlay();
    }
}
