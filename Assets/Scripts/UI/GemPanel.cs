using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GemPanel : MonoBehaviour
{
    public TextMeshProUGUI gemText;
    private void Start()
    {
        gemText.text = Player.Instance.GetGems().ToString();
        GameEvents.GemsChanged += GemsChanged;
    }

    private void GemsChanged()
    {
        gemText.text = Player.Instance.GetGems().ToString();
    }


}
