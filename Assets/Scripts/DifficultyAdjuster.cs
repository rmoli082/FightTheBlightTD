using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DifficultyAdjuster : MonoBehaviour
{
    [System.Serializable]
    public class DifficultyConfig
    {
        public int TotalWaves;
        public float healthModifier;
        public float speedModifier;
        public float factoryModifier;
        public float spawnModifier;
    }

    [SerializeField]
    static DifficultyConfig easy, medium, hard;

    public static DifficultyConfig GetDifficulty(GameDifficulty difficulty = GameDifficulty.MEDIUM)
    {
        switch(difficulty)
        {
            case GameDifficulty.EASY:
                return easy;
            case GameDifficulty.MEDIUM:
                return medium;
            case GameDifficulty.HARD:
                return hard;
        }

        return null;
    }
}
