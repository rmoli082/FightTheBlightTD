using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuyBoost : MonoBehaviour
{
    public GameObject boostPanel;
    public void OpenStore()
    {
        boostPanel.SetActive(false);
        SceneManager.LoadScene("Shop", LoadSceneMode.Additive);
    }
}
