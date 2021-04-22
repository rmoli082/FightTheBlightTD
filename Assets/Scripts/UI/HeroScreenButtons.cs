using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroScreenButtons : MonoBehaviour
{
    public Blueprint blueprint;
    public GameObject[] upgradeButtons;
    public HeroScreen screenData;

    private void Start()
    {
        screenData = GameObject.FindObjectOfType<HeroScreen>();
    }

    public void SetDetails()
    {
        HeroManager.Instance.CheckForLevelUp();
        HeroManager.Instance.ActivateUpgradeSlots();
        screenData.heroImage.gameObject.SetActive(true);
        screenData.heroName.text = blueprint.turretName;
        screenData.heroXP.text = $"{HeroManager.Instance.GetHeroXP()} XP";
        screenData.heroDescription.text = blueprint.turretDescription;
        screenData.heroImage.sprite = blueprint.turretImage;
        screenData.heroLevel.text = $"Lvl {HeroManager.Instance.GetHeroLevel()}";

        foreach (Transform child in screenData.upgradesPanel)
        {
            if (child.GetComponent<Button>() != null)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (GameObject button in upgradeButtons)
        {
            Instantiate(button, screenData.upgradesPanel);
        }

        screenData.upgradesPanel.GetComponent<HeroUpgradesPanel>().PopulateHeroUpgrades();
    }
}
