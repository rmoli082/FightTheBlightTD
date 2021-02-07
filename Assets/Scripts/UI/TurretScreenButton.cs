using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretScreenButton : MonoBehaviour
{
    public Blueprint blueprint;
    public GameObject[] upgradeButtons;
    public TurretScreen screenData;

    private void Start()
    {
        screenData = GameObject.FindObjectOfType<TurretScreen>();
    }

    public void SetDetails()
    {
        screenData.turretName.text = blueprint.turretName;
        screenData.turretKills.text = TurretStats.Instance.GetTurretStats(blueprint.prefab.GetComponent<Turret>().TurretType.ToString()).ToString();
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
