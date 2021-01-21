using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Blueprint blueprint;

    public void SetPlaceable()
    {
        BuildManager.Instance.SelectPlaceable(blueprint);
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
}
