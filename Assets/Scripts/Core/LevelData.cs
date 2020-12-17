using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Level Data", menuName ="Level Data")]
public class LevelData : ScriptableObject
{
    public TextAsset waveData;

    public Material nodeMaterial;
    public Material planeMaterial;
    public AudioClip backgroundMusic;

    public GameObject[] shopButtons;

    public GameObject[] enemies;
}
