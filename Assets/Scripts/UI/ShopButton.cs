using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public Blueprint blueprint;

    public void SetPlaceable()
    {
        BuildManager.Instance.SelectPlaceable(blueprint);
    }
}
