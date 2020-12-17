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

}

public enum PlaceableType { turret, rapid, bomber, seeker, stunner, hero}
