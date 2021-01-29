using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    public GameObject objectToClose;
    public void CloseItem()
    {
        objectToClose.SetActive(false);
        GameManager.Instance.PausePlay();
    }
}
