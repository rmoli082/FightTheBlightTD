using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : Singleton<Player>
{
    private int gems;
    private List<string> boosts;

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

        boosts = new List<string>();
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
        GameManager.Instance.UpdateGemsDisplay();
        GameEvents.OnSaveInitiated();
    }

    public void AddBoost(GameObject boost)
    {
        boosts.Add(boost.name);
    }

    public void ApplyBoost(string boost)
    {
        boosts.Remove(boost);
    }

    private void Save()
    {
        SaveLoad.Save<PlayerSave>(new PlayerSave(gems, boosts), saveTag);
    }

    private void LoadPlayer()
    {
        PlayerSave player = SaveLoad.Load<PlayerSave>(saveTag);

        gems = player.playerGems;
        boosts = player.playerBoosts;
    }

    [Serializable]
    protected class PlayerSave
    {
        public int playerGems;
        public List<string> playerBoosts;

        public PlayerSave() { }

        public PlayerSave(int _playerGems, List<string> _playerBoosts)
        {
            playerGems = _playerGems;
            playerBoosts = _playerBoosts;
        }
    }
}
