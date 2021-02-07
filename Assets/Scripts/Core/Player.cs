using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    private int gems;

    public int GetGems()
    {
        return gems;
    }

    public void AdjustGems(int amount)
    {
        gems += amount;
    }
}
