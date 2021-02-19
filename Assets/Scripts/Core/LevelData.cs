using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Level Data", menuName ="Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Wave Data")]
    public TextAsset waveData;
    public int winGems;
    public int replayGems;

    [Header("Level Decorations")]
    public Material nodeMaterial;
    public Material planeMaterial;
    public AudioClip backgroundMusic;

    [Header("Shop Buttons")]
    public GameObject[] shopButtons;

    [Header("Level Enemies")]
    public GameObject[] enemies;

    [Header("Level Rewards")]
    public int waveGoldReward;
}
