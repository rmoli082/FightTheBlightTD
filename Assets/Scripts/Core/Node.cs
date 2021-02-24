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
        rend.material = LevelManager.Instance.levelData.nodeMaterial;
        startColor = rend.material.color;
    }

    private void OnMouseOver()
    {
        if (currentTurret != null || BuildManager.Instance.selectedTurret == null)
            return;
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (BuildManager.Instance.selectedTurret == null || currentTurret != null)
            return;
        else 
        {
            BuildManager.Instance.BuildSelectedPlaceable(this);
        }
    }
}
