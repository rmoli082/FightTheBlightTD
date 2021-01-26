﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    public Blueprint blueprint;
    public TextMeshProUGUI cost;

    private void Start()
    {
        cost.text = blueprint.turretCost.ToString();
    }

    private void Update()
    {
        if (LevelManager.Instance.GetGold() < blueprint.turretCost)
        {
            gameObject.GetComponentInParent<Button>().interactable = false;
        }
        else
        {
            gameObject.GetComponentInParent<Button>().interactable = true;
        }
    }

    public void SetPlaceable()
    {
        BuildManager.Instance.SelectPlaceable(blueprint);
    }

}
