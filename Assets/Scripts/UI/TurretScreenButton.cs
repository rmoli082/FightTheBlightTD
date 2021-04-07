using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretScreenButton : MonoBehaviour
{
    public Blueprint blueprint;
    public GameObject[] upgradeButtons;
    public TurretScreen screenData;

    public Image turretImage;

    private void Start()
    {
        screenData = GameObject.FindObjectOfType<TurretScreen>();
        turretImage.sprite = blueprint.turretImage;
    }

    public void SetDetails()
    {
        screenData.turretImage.gameObject.SetActive(true);
        screenData.turretKills.text = $"{TurretStats.Instance.GetTurretStats(blueprint.prefab.GetComponent<Turret>().TurretType.ToString())} points";
        screenData.turretDescription.text = blueprint.turretDescription;
        screenData.turretImage.sprite = blueprint.turretImage;

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
