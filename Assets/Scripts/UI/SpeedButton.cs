using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedButton : MonoBehaviour
{
    public Sprite normalImage;
    public Sprite fastImage;
    private Image buttonImage;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    public void ClickAction()
    {
        GameManager.Instance.SpeedUp();
    }

    public void SetFastImage()
    {
        buttonImage.sprite = fastImage;
    }

    public void SetNormalImage()
    {
        buttonImage.sprite = normalImage;
    }
}
