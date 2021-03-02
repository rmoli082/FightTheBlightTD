using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Placeable currentTurret = null;
    public Color hoverColor;

    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.material = LevelManager.Instance.levelData.nodeMaterial;
    }

    private void Start()
    {
        gameObject.GetComponent<Collider>().enabled = true;
    }

    private void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            try
            {
                if (BuildManager.Instance.selectedTurret == null || BuildManager.Instance.selectedTurret.Equals(null)
                    || currentTurret != null)
                    return;
                else
                {
                    BuildManager.Instance.BuildSelectedPlaceable(this);
                }
            }
            catch (ArgumentException e)
            {
                Debug.Log(e.Message);
            }
        }
        
    }
}
