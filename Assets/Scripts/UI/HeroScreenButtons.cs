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
        screenData.heroName.text = blueprint.turretName;
        screenData.heroXP.text = HeroManager.Instance.GetHeroXP().ToString();
        screenData.heroDescription.text = blueprint.turretDescription;
        screenData.heroImage.sprite = blueprint.turretImage;

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
    }
}
