using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Placeable currentTurret = null;
    public Color hoverColor;

    private Renderer rend;
    private Color startColor;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseOver()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (currentTurret != null)
            return;

        if (BuildManager.Instance.selectedTurret == null)
            return;
        else 
        {
            BuildManager.Instance.BuildSelectedPlaceable(this);
        }
    }
}
