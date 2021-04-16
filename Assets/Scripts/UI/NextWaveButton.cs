using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextWaveButton : MonoBehaviour
{
    Image buttonImage;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    private void Start()
    {
        GameEvents.WaveStarted += WaveStarted;
    }
    public void StartWave()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            LevelManager.Instance.sceneData.speedButton.SetFastImage();
        }
        StartCoroutine(NewWaveSpawner.Instance.SpawnWave());
    }

    public void SetColor(Color color)
    {
        buttonImage.color = color;
    }

    private void WaveStarted()
    {
        LevelManager.Instance.sceneData.nextWaveButton.SetColor(new Color(0, 0, 0, 0f));
        LevelManager.Instance.sceneData.greenArrow.enabled = false;
        LevelManager.Instance.sceneData.redArrow.enabled = false;
    }
}
