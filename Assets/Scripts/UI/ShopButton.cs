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

    public bool isHeroButton = false;

    private Button button;

    private void Start()
    {
        cost.text = blueprint.turretCost.ToString();
        buttonImage.sprite = blueprint.turretImage;
        button = gameObject.GetComponentInParent<Button>();
    }

    private void Update()
    {
        
        if (LevelManager.Instance.GetGold() < blueprint.turretCost)
        {
            button.interactable = false;
            costTag.SetActive(false);
        }
        else
        {
            button.interactable = true;
            costTag.SetActive(true);
        }

        if (isHeroButton && HeroManager.Instance.isSpawned)
        {
            button.interactable = false;
            costTag.SetActive(false);
        }
    }

    public void SetPlaceable()
    {
        BuildManager.Instance.SelectPlaceable(blueprint);
    }

}
