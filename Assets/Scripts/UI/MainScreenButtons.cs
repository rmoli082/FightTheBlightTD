using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenButtons : MonoBehaviour
{
    public string sceneToLoad;
    public void LoadScene()
    {
        GameManager.Instance.LoadScene(sceneToLoad);
    }

}
