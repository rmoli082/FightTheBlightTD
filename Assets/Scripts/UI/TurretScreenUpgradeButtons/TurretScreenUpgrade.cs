using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class TurretScreenUpgrade : MonoBehaviour
{
    public PermaGradeBlueprint blueprint;
    protected TurretScreen screenData;
    public int upgradeNumber;
    protected Button button;

    protected virtual void Awake()
    {
        button = GetComponentInParent<Button>();
    }

    protected virtual void Update() { }

    protected virtual void Start()
    {
        screenData = GameObject.FindObjectOfType<TurretScreen>();
    }

    public virtual void BuyUpgrade() { }
}
