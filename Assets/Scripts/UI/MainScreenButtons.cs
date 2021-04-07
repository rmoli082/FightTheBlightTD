using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainScreenButtons : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadScene()
    {
        GameManager.Instance.LoadScene(sceneToLoad);
    }

    public void LoadAchievements()
    {
        PlayGames.ShowAchievementsUI();
    }

}
