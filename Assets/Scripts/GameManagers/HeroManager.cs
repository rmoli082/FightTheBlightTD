using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : Singleton<HeroManager>
{
    public int heroLevel = 1;
    public int heroXP = 0;

    public bool isFirstUpgradeActive = false;
    public bool isSecondUpgradeActive = false;
    public bool isThirdUpgradeActive = false;

    private readonly int levels = 50;
    private readonly int xp_for_first_level = 100;
    private readonly int xp_for_last_level = 100000;

    private int CalculateXP(int level)
    {
        float B = Mathf.Log(1.0f * xp_for_last_level / xp_for_first_level) / (levels - 1);
        float A = 1.0f * xp_for_first_level / (Mathf.Exp(B) - 1.0f);

        int x = (int)(A * Mathf.Exp(B * level));
        int y = (int)Mathf.Pow(10f, Mathf.Log(x) / Mathf.Log(10) - 2.2f);

        return (int)(x / y) * y;
    }

    public void CalculateLevel()
    {
        if (heroXP >= CalculateXP(heroLevel + 1))
        {
            heroLevel = Mathf.Clamp(heroLevel + 1, 1, 50);
        }
    }

    public void ActivateUpgradeSlots()
    {
        if (heroLevel >= 10)
        {
            isFirstUpgradeActive = true;
        }

        if (heroLevel >= 20)
        {
            isSecondUpgradeActive = true;
        }

        if (heroLevel >= 30)
        {
            isThirdUpgradeActive = true;
        }
    }
}
