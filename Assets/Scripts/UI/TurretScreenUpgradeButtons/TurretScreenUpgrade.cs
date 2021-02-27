using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class TurretScreenUpgrade : MonoBehaviour
{
    public TextMeshProUGUI upgradeDescription;
    public TextMeshProUGUI upgradeCost;
    public PermaGradeBlueprint blueprint;
    protected TurretScreen screenData;
    public int upgradeNumber;

    protected virtual void Awake()
    {
        upgradeDescription.text = blueprint.description;
        upgradeCost.text = blueprint.cost.ToString();
        
    }

    protected virtual void Start()
    {
        screenData = GameObject.FindObjectOfType<TurretScreen>();
    }

    public virtual void BuyUpgrade() { }
}
