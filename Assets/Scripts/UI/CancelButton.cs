using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CancelButton : MonoBehaviour
{
    public GameObject objectToClose;
    public string sceneToLoad;

    public bool useUnpause = true;

    public void CloseItem()
    {
        objectToClose.SetActive(false);
        if (GameManager.Instance.IsPaused && useUnpause)
            GameManager.Instance.PausePlay();
    }

    public void ExitScreen()
    {
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Shop"));
        }
        else
        {
            GameManager.Instance.LoadScene(sceneToLoad);
        }
    }
}
