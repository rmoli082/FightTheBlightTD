using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public Button level5button;
    public Button level6button;
    public Button level7button;
    public Button level8button;
    public Color activeColor;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Level 1 Complete", 0).Equals(1))
        {
            level5button.interactable = true;
            level5button.GetComponentInChildren<TextMeshProUGUI>().color = activeColor;
        }

        if (PlayerPrefs.GetInt("Level 2 Complete", 0).Equals(1))
        {
            level6button.interactable = true;
            level6button.GetComponentInChildren<TextMeshProUGUI>().color = activeColor;
        }

        if (PlayerPrefs.GetInt("Level 3 Complete", 0).Equals(1))
        {
            level7button.interactable = true;
            level7button.GetComponentInChildren<TextMeshProUGUI>().color = activeColor;
        }

        if (PlayerPrefs.GetInt("Level 4 Complete", 0).Equals(1))
        {
            level8button.interactable = true;
            level8button.GetComponentInChildren<TextMeshProUGUI>().color = activeColor;
        }
    }
}
