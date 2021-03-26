using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public static Button level5button;
    public static Button level6button;
    public static Button level7button;
    public static Button level8button;
    public static Color activeColor;

    private void Start()
    {
        CheckForAvail();
    }

    public static void CheckForAvail()
    {
        if (PlayerPrefs.GetInt("Level 1 Complete").Equals(1))
        {
            level5button.interactable = true;
            level5button.GetComponentInChildren<TextMeshProUGUI>().color = activeColor;
        }

        if (PlayerPrefs.GetInt("Level 2 Complete").Equals(1))
        {
            level6button.interactable = true;
            level6button.GetComponentInChildren<TextMeshProUGUI>().color = activeColor;
        }

        if (PlayerPrefs.GetInt("Level 3 Complete").Equals(1))
        {
            level7button.interactable = true;
            level7button.GetComponentInChildren<TextMeshProUGUI>().color = activeColor;
        }

        if (PlayerPrefs.GetInt("Level 4 Complete").Equals(1))
        {
            level8button.interactable = true;
            level8button.GetComponentInChildren<TextMeshProUGUI>().color = activeColor;
        }
    }
}
