using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextWaveButton : MonoBehaviour
{
    private void Start()
    {
        GameEvents.WaveStarted += WaveStarted;
    }
    public void StartWave()
    {
        StartCoroutine(WaveSpawner.Instance.SpawnWave());
    }

    private void WaveStarted()
    {
        LevelManager.Instance.sceneData.nextWaveButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0, 0f);
        LevelManager.Instance.sceneData.nextWaveButton.GetComponent<Image>().color = new Color(0, 0, 0, 0f);
        LevelManager.Instance.sceneData.greenArrow.enabled = false;
        LevelManager.Instance.sceneData.redArrow.enabled = false;
    }
}
