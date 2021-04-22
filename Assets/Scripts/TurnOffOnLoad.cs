using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TurnOffOnLoad : MonoBehaviour
{
    public EventSystem events;

    private void OnEnable()
    {
        if (SceneManager.sceneCount > 1)
        {
            events.enabled = false;
        }
    }
}
