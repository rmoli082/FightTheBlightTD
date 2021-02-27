using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TurretStats : Singleton<TurretStats>
{
    private Dictionary<string, int> turretKills;
    private readonly string saveTag = "TurretKills";

    public bool[] turretPermanentBought;
    public bool[] rapidPermanentBought;
    public bool[] bomberPermanentBought;
    public bool[] seekerPermanentBought;
    public bool[] stunnerPermanentBought;

    protected override void Awake()
    {
        base.Awake();

        if (SaveLoad.SaveExists(saveTag))
        {
            LoadStats();
        }
        else
        {
            turretKills = new Dictionary<string, int>();
            turretPermanentBought = new bool[] { false, false, false } ;
            rapidPermanentBought = new bool[] { false, false, false } ;
            bomberPermanentBought = new bool[] { false, false, false };
            seekerPermanentBought = new bool[] { false, false, false };
            stunnerPermanentBought = new bool[] { false, false, false };
        }

        DontDestroyOnLoad(gameObject);        
    }

    private void Start()
    {
        GameEvents.SaveInitiated += Save;
    }

    public int GetTurretStats(string turretType)
    {
        if (turretKills.ContainsKey(turretType))
            return turretKills[turretType];
        else return 0;
    }

    public void AddTurretKills(string turretType, int killAmount)
    {
        if (turretKills.ContainsKey(turretType))
            turretKills[turretType] += killAmount;
        else
            turretKills.Add(turretType, killAmount);
    }

    private void Save()
    {
        SaveLoad.Save<TurretSave>(new TurretSave(turretKills, turretPermanentBought, rapidPermanentBought, bomberPermanentBought,
            seekerPermanentBought, stunnerPermanentBought), saveTag);
    }

    private void LoadStats()
    {
        TurretSave saveData = SaveLoad.Load<TurretSave>(saveTag);

        turretKills = saveData.turretKills;
        turretPermanentBought = saveData.turretPermanentBought;
        rapidPermanentBought = saveData.rapidPermanentBought;
        bomberPermanentBought = saveData.rapidPermanentBought;
        seekerPermanentBought = saveData.seekerPermanentBought;
        stunnerPermanentBought = saveData.stunnerPermanentBought;
    }

    [Serializable]
    public class TurretSave
    {
        public Dictionary<string, int> turretKills;
        public bool[] turretPermanentBought;
        public bool[] rapidPermanentBought;
        public bool[] bomberPermanentBought;
        public bool[] seekerPermanentBought;
        public bool[] stunnerPermanentBought;

        public TurretSave() { }

        public TurretSave(Dictionary<string, int> kills, bool[] turretPermanent, bool[] rapidPermanent, bool[] bomberPermanent, bool[] seekerPermanent,
            bool[] stunnerPermanent)
        {
            turretKills = kills;
            turretPermanentBought = turretPermanent;
            bomberPermanentBought = bomberPermanent;
            rapidPermanentBought = rapidPermanent;
            seekerPermanentBought = seekerPermanent;
            stunnerPermanentBought = stunnerPermanent;
        }
    }
}
