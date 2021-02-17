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
        gameObject.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0f);
        gameObject.transform.parent.GetComponent<Image>().color = new Color(0, 0, 0, 0f);
    }
}
