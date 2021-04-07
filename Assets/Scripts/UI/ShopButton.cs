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
    private Image costImage;
    public Color fadedCost;
    private Color fullCost;

    public Sprite redTag;
    private Sprite originalTag;

    public bool isHeroButton = false;

    private Button button;

    private void Start()
    {
        cost.text = blueprint.turretCost.ToString();
        buttonImage.sprite = blueprint.turretImage;
        button = gameObject.GetComponentInParent<Button>();
        costImage = costTag.GetComponent<Image>();
        originalTag = costImage.sprite;
        fullCost = costImage.color;
    }

    private void Update()
    {
        
        if (LevelManager.Instance.GetGold() < blueprint.turretCost)
        {
            button.interactable = false;
            costImage.color = fadedCost;
            costImage.sprite = redTag;
        }
        else
        {
            button.interactable = true;
            costImage.color = fullCost;
            costImage.sprite = originalTag;
        }

        if (isHeroButton && HeroManager.Instance.isSpawned)
        {
            button.interactable = false;
            costImage.color = fadedCost;
            costImage.sprite = redTag;
        }
    }

    public void SetPlaceable()
    {
        BuildManager.Instance.SelectPlaceable(blueprint);
    }

}
