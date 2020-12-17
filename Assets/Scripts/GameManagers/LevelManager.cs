using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public LevelData levelData;
    public SceneData sceneData;

    protected override void Awake()
    {
        base.Awake();
        Decorate();
        SetUpShop();
    }

    private void SetUpShop()
    {
        foreach (GameObject button in levelData.shopButtons)
        {
            Instantiate(button, sceneData.shopListing);
        }
    }

    private void Decorate()
    {
        sceneData.plane.GetComponent<Renderer>().material = levelData.planeMaterial;
    }

}
