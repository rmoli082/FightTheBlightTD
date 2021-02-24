using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    public Blueprint blueprint;
    public TextMeshProUGUI cost;
    public GameObject costTag;

    public Image buttonImage;

    private void Start()
    {
        cost.text = blueprint.turretCost.ToString();
        buttonImage.sprite = blueprint.turretImage;
    }

    private void Update()
    {
        if (LevelManager.Instance.GetGold() < blueprint.turretCost)
        {
            gameObject.GetComponentInParent<Button>().interactable = false;
            costTag.SetActive(false);
        }
        else
        {
            gameObject.GetComponentInParent<Button>().interactable = true;
            costTag.SetActive(true);
        }
    }

    public void SetPlaceable()
    {
        BuildManager.Instance.SelectPlaceable(blueprint);
    }

}
