using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class MenuPanel : MonoBehaviour
{
    [Header("Scenes to Load")]
    public string mainMenu;
    public string levelSelect;

    public void MainMenu()
    {
        GameManager.Instance.LoadScene(mainMenu);
        Analytics.CustomEvent("Level exited early");
        GameManager.Instance.PausePlay();
    }

    public void LevelSelect()
    {
        GameManager.Instance.LoadScene(levelSelect);
        Analytics.CustomEvent("Level exited early");
        GameManager.Instance.PausePlay();
    }

    public void Settings()
    {
        LevelManager.Instance.sceneData.settingsPanel.SetActive(true);
        LevelManager.Instance.sceneData.menuPanel.SetActive(false);
    }
}
