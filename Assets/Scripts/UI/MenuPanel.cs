using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    [Header("Scenes to Load")]
    public string mainMenu;
    public string levelSelect;

    public void MainMenu()
    {
        GameManager.Instance.LoadScene(mainMenu);
    }

    public void LevelSelect()
    {
        GameManager.Instance.LoadScene(levelSelect);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
