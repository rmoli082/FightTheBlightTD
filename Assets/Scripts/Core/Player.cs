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
            gems = 500;
            boosts = new List<string>();
        }

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        GameEvents.SaveInitiated += Save;
    }

    public List<string> GetBoosts()
    {
        return boosts;
    }

    public int GetGems()
    {
        return gems;
    }

    public void AdjustGems(int amount)
    {
        gems += amount;
        GameEvents.OnGemsChanged();
        GameEvents.OnSaveInitiated();
    }

    public void AddBoost(GameObject boost)
    {
        boosts.Add(boost.GetComponent<HeroUpgrade>().UpgradeName);
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
