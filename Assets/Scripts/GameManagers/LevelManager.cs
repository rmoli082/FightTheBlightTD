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
        LoadMonsters();
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
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");

        foreach (GameObject node in nodes)
        {
            node.GetComponent<Renderer>().material = levelData.nodeMaterial;
        }

        sceneData.plane.GetComponent<Renderer>().material = levelData.planeMaterial;
    }

    private void LoadMonsters()
    {
        WaveSpawner.Instance.levelData = levelData.waveData;
    }
}
