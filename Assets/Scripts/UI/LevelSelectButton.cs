using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectButton : MonoBehaviour
{
    public string levelToLoad;

    public void LoadLevel()
    {
        GameManager.Instance.LoadScene(levelToLoad);
    }

    public void LoadLevelNoSkip()
    {
        GameManager.Instance.LoadScene(levelToLoad);
    }

}
