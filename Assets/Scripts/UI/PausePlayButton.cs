using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PausePlayButton : MonoBehaviour
{
    public TextMeshProUGUI playPauseText;

    void Start()
    {
        UpdateButtonText();
    }
    
    public void ClickAction()
    {
        GameManager.Instance.PausePlay();
        UpdateButtonText();
    }

    public void UpdateButtonText()
    {
        if (GameManager.Instance.IsPaused)
        {
            playPauseText.text = "Play";
        }
        else
        {
            playPauseText.text = "Pause";
        }
    }
}
