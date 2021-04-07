using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePlayButton : MonoBehaviour
{
    public Sprite pauseImage;
    public Sprite playImage;
    private Image buttonImage;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    public void ClickAction()
    {
        GameManager.Instance.PausePlay();
    }

    public void SetPlayImage()
    {
        buttonImage.sprite = playImage;
    }

    public void SetPauseImage()
    {
        buttonImage.sprite = pauseImage;
    }

}
