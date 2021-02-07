using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    public GameObject objectToClose;
    public string sceneToLoad;

    public void CloseItem()
    {
        objectToClose.SetActive(false);
        GameManager.Instance.PausePlay();
    }

    public void ExitScreen()
    {
        GameManager.Instance.LoadScene(sceneToLoad);
    }
}
