using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Placeable : MonoBehaviour
{
    [SerializeField]
    private Node _locationNode;
    public Node LocationNode { get => _locationNode; set => _locationNode = value; }

    [SerializeField]
    private PlaceableType _turretType = PlaceableType.turret;
    public PlaceableType TurretType { get => _turretType; private set => _turretType = value; }

    [SerializeField]
    private float _damageAmount;
    public float DamageAmount { get => _damageAmount; set => _damageAmount = value; }

    [SerializeField]
    private int _sellCost;
    public int SellCost { get => _sellCost; set => _sellCost = value; }

}

