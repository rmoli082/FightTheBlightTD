using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroScreen : MonoBehaviour
{
    public Image heroImage;
    public TextMeshProUGUI heroName;
    public TextMeshProUGUI heroXP;
    public TextMeshProUGUI heroDescription;
    public Transform upgradesPanel;
    public Transform selectionPanel;

    public GameObject[] heroButtons;

    private void Awake()
    {
        PopulateSelectionPanel();
    }

    public void PopulateSelectionPanel()
    {
        foreach (Transform child in selectionPanel)
        {
            if (child.GetComponent<Button>() != null)
                Destroy(child.gameObject);
        }

        foreach (GameObject button in heroButtons)
        {
            Instantiate(button, selectionPanel);
        }
    }
}
