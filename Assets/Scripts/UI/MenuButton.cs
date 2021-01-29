using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void OpenMenuPanel()
    {
        GameManager.Instance.PausePlay();
        LevelManager.Instance.sceneData.menuPanel.SetActive(true);
    }
}
