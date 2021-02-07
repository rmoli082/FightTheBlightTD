using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : Singleton<Player>
{
    private int gems;

    private readonly string saveTag = "Player";

    protected override void Awake()
    {
        base.Awake();

        if (SaveLoad.SaveExists(saveTag))
        {
            LoadPlayer();
        }
        else
        {
            gems = 1500;
        }
    }

    private void Start()
    {
        GameEvents.SaveInitiated += Save;
    }

    public int GetGems()
    {
        return gems;
    }

    public void AdjustGems(int amount)
    {
        gems += amount;
        GameEvents.OnSaveInitiated();
    }

    private void Save()
    {
        SaveLoad.Save<PlayerSave>(new PlayerSave(gems), saveTag);
    }

    private void LoadPlayer()
    {
        PlayerSave player = SaveLoad.Load<PlayerSave>(saveTag);

        gems = player.playerGems;
    }

    [Serializable]
    protected class PlayerSave
    {
        public int playerGems;

        public PlayerSave() { }

        public PlayerSave(int _playerGems)
        {
            playerGems = _playerGems;
        }
    }
}
