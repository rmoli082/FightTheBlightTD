using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStats : Singleton<TurretStats>
{
    private Dictionary<string, int> turretKills;
    private readonly string saveTag = "TurretKills";

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
        SaveLoad.Save<Dictionary<string, int>>(turretKills, saveTag);
    }

    private void LoadStats()
    {
        turretKills = SaveLoad.Load<Dictionary<string, int>>(saveTag);
    }
}
